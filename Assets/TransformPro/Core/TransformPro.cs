namespace UntitledGames.Transforms
{
    using System;
    using UnityEngine;
    using Object = UnityEngine.Object;
    using Random = System.Random;

    /// <summary>
    ///     Provides the runtime features for the system. All main functionality can be found here.
    ///     More information can be found at http://transformpro.untitledgam.es
    /// </summary>
    public partial class TransformPro
    {
        /// <summary>
        ///     The current version number, used to display in the preferences.
        /// </summary>
        private static readonly string version = "v1.3.1";

        private static bool calculateBounds = true;

        /// <summary>
        ///     The random number provider for the rotation randomisation.
        /// </summary>
        private static Random random;

        private Vector3 displayPosition;
        private Vector3 displayRotation;
        private Vector3 displayScale;
        private Transform transform;

        public TransformPro(Transform transform)
        {
            this.transform = transform;
        }

        /// <summary>
        ///     Private constructor for TransformPro, prevents an instance without a Transform being created.
        /// </summary>
        private TransformPro()
        {
        }

        public static bool CalculateBounds { get { return TransformPro.calculateBounds; } set { TransformPro.calculateBounds = value; } }

        /// <summary>
        ///     A <see cref="System.Random" /> instance used to set random data in the editor.
        ///     <see cref="UnityEngine.Random" /> is not used to avoid polluting and procedural generation that may be being used
        ///     elsewhere.
        /// </summary>
        public static Random Random { get { return TransformPro.random ?? (TransformPro.random = new Random(DateTime.Now.GetHashCode())); } }

        /// <summary>
        ///     Gets a string containing the current version of <see cref="TransformPro" />.
        /// </summary>
        public static string Version { get { return TransformPro.version; } }

        /// <summary>
        ///     The current <see cref="Transform" /> is currently able to have its position changed.
        /// </summary>
        public bool CanChangePosition { get { return true; } }

        /// <summary>
        ///     The current <see cref="Transform" /> is currently able to have its rotation changed.
        /// </summary>
        public bool CanChangeRotation { get { return true; } }

        /// <summary>
        ///     The current <see cref="Transform" /> is currently able to have its scale changed.
        /// </summary>
        public bool CanChangeScale { get { return TransformPro.Space != TransformProSpace.World; } }

        /// <summary>
        ///     Returns true if the currently selected <see cref="Transform" /> has any child transforms. If no
        ///     <see cref="Transform" /> is selected false will be returned.
        /// </summary>
        public bool HasChildren { get { return this.Transform.childCount > 0; } }

        public string Name { get { return this.transform.name; } }

        /// <summary>
        ///     Gets or sets the <see cref="Vector3" /> Position of the current object, in the current
        ///     <see cref="TransformProSpace" />.
        ///     If you attempt to update a position when <see cref="CanChangePosition" /> is false, the request will be ignored.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                this.TryGetPosition();
                return this.displayPosition;
            }
            set { this.TrySetPosition(value); }
        }

        public Vector3 PositionLocal
        {
            get { return this.Transform.localPosition; }
            set
            {
                this.Transform.localPosition = value;
                this.TryGetPosition();
            }
        }

        public Vector3 PositionWorld
        {
            get { return this.Transform.position; }
            set
            {
                this.Transform.position = value;
                this.TryGetPosition();
            }
        }

        public float PositionX
        {
            get { return this.Position.x; }
            set
            {
                Vector3 position = this.Position;
                position.x = value;
                this.Position = position;
            }
        }

        public float PositionY
        {
            get { return this.Position.y; }
            set
            {
                Vector3 position = this.Position;
                position.y = value;
                this.Position = position;
            }
        }

        public float PositionZ
        {
            get { return this.Position.z; }
            set
            {
                Vector3 position = this.Position;
                position.z = value;
                this.Position = position;
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Quaternion" /> Rotation of the current object, in the current
        ///     <see cref="TransformProSpace" />.
        ///     If you attempt to update a rotation when <see cref="CanChangeRotation" /> is false, the request will be ignored.
        ///     Setting this property will update the bounds as required.
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                switch (TransformPro.space)
                {
                    case TransformProSpace.Local:
                        return this.Transform.localRotation;
                    case TransformProSpace.World:
                        return this.Transform.rotation;
                }
                return Quaternion.identity;
            }
            set { this.TrySetRotation(value); }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Vector3" /> Euler Rotation of the current object, in the current
        ///     <see cref="TransformProSpace" />.
        ///     If you attempt to update a rotation when <see cref="CanChangeRotation" /> is false, the request will be ignored.
        ///     Setting this property will update the bounds as required.
        /// </summary>
        public Vector3 RotationEuler
        {
            get
            {
                this.TryGetRotation();
                return this.displayRotation;
            }
            set { this.TrySetRotation(value); }
        }

        public Vector3 RotationEulerLocal { get { return this.Transform.localEulerAngles; } set { this.Transform.localEulerAngles = value; } }

        public Vector3 RotationEulerWorld { get { return this.Transform.eulerAngles; } set { this.Transform.eulerAngles = value; } }

        public float RotationEulerX
        {
            get { return this.RotationEuler.x; }
            set
            {
                Vector3 rotation = this.RotationEuler;
                rotation.x = value;
                this.RotationEuler = rotation;
            }
        }

        public float RotationEulerY
        {
            get { return this.RotationEuler.y; }
            set
            {
                Vector3 rotation = this.RotationEuler;
                rotation.y = value;
                this.RotationEuler = rotation;
            }
        }

        public float RotationEulerZ
        {
            get { return this.RotationEuler.z; }
            set
            {
                Vector3 rotation = this.RotationEuler;
                rotation.z = value;
                this.RotationEuler = rotation;
            }
        }

        public Quaternion RotationLocal { get { return this.Transform.localRotation; } set { this.Transform.localRotation = value; } }

        public Quaternion RotationWorld { get { return this.Transform.rotation; } set { this.Transform.rotation = value; } }

        /// <summary>
        ///     Gets or sets the <see cref="Vector3" /> Scale of the current object, in the current
        ///     <see cref="TransformProSpace" />.
        ///     If you attempt to update a scale when <see cref="CanChangeScale" /> is false, the request will be ignored.
        ///     Setting this property will update the bounds as required.
        /// </summary>
        public Vector3 Scale
        {
            get
            {
                this.TryGetScale();
                return this.displayScale;
            }
            set
            {
                if (this.TrySetScale(value))
                {
                    // LOOK: Becase of the new way of calculating the bounds (storing local bounds and passing in a l2w matrix) this is no longer required.
                    //this.SetComponentsDirty();
                }
            }
        }

        public float ScaleX
        {
            get { return this.Scale.x; }
            set
            {
                if (Mathf.Approximately(value, this.ScaleX))
                {
                    return;
                }

                Vector3 scale = this.Scale;
                scale.x = value;
                this.Scale = scale;
            }
        }

        public float ScaleY
        {
            get { return this.Scale.y; }
            set
            {
                if (Mathf.Approximately(value, this.ScaleY))
                {
                    return;
                }

                Vector3 scale = this.Scale;
                scale.y = value;
                this.Scale = scale;
            }
        }

        public float ScaleZ
        {
            get { return this.Scale.z; }
            set
            {
                if (Mathf.Approximately(value, this.ScaleZ))
                {
                    return;
                }

                Vector3 scale = this.Scale;
                scale.z = value;
                this.Scale = scale;
            }
        }

        public Transform Transform { get { return this.transform; } }

        /// <summary>
        ///     Clones the GameObject for the currently selected Transform, retaining the same name and parent data.
        ///     The new transform will be automatically selected allowing for fast simple scene creation.
        /// </summary>
        /// <returns>The newly created transform.</returns>
        public Transform Clone()
        {
            GameObject gameObjectOld = this.Transform.gameObject;
            Transform transformOld = gameObjectOld.transform;

            GameObject gameObjectNew = Object.Instantiate(gameObjectOld);
            gameObjectNew.name = gameObjectOld.name; // Get rid of the (Clone)(Clone)(Clone)(Clone) madness

            Transform transformNew = gameObjectNew.transform;
            transformNew.SetParent(transformOld.parent);
            transformNew.localPosition = transformOld.localPosition;
            transformNew.localRotation = transformOld.localRotation;
            transformNew.localScale = transformOld.localScale;

            return transformNew;
        }

        public void LookAt(Vector3 position)
        {
            this.Transform.LookAt(position);
        }

        public void LookAt(TransformProClipboard clipboard)
        {
            this.Transform.LookAt(clipboard.Position);
        }

        /// <summary>
        ///     Ensures the display values are correct for the current transform, in the current geometry space.
        ///     This is invoked automatically to make sure that change tracking is accurate.
        /// </summary>
        private void UpdateDisplayTransform()
        {
            this.TryGetPosition();
            this.TryGetRotation();
            this.TryGetScale();
        }
    }
}
