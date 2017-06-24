namespace UntitledGames.Transforms
{
    using UnityEngine;

    public partial class TransformPro
    {
        /// <summary>
        ///     Gets the display position value out of the object, where possible.
        /// </summary>
        /// <returns>A <see cref="bool" /> value indicating if the operation was a success.</returns>
        private bool TryGetPosition()
        {
            if (this.Transform == null)
            {
                return false;
            }

            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    if (!this.displayPosition.ApproximatelyEquals(this.Transform.localPosition))
                    {
                        this.displayPosition = this.Transform.localPosition;
                        return true;
                    }
                    return false;
                case TransformProSpace.World:
                    if (!this.displayPosition.ApproximatelyEquals(this.Transform.position))
                    {
                        this.displayPosition = this.Transform.position;
                        return true;
                    }
                    return false;
            }
        }

        /// <summary>
        ///     Gets the display rotation value out of the object, where possible.
        ///     Note this function is only invoked once after the <see cref="Transform" /> is updated.
        ///     Euler angles are tracked after that to prevent gimbal lock.
        /// </summary>
        /// <returns>A <see cref="bool" /> value indicating if the operation was a success.</returns>
        private bool TryGetRotation()
        {
            if (this.Transform == null)
            {
                return false;
            }

            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    if (!this.displayRotation.ApproximatelyEquals(this.Transform.localEulerAngles))
                    {
                        this.displayRotation = this.Transform.localEulerAngles;
                        return true;
                    }
                    return false;
                case TransformProSpace.World:
                    if (!this.displayRotation.ApproximatelyEquals(this.Transform.eulerAngles))
                    {
                        this.displayRotation = this.Transform.eulerAngles;
                        return true;
                    }
                    return false;
            }
        }

        /// <summary>
        ///     Gets the display scale value out of the object, where possible.
        /// </summary>
        /// <returns>A <see cref="bool" /> value indicating if the operation was a success.</returns>
        private bool TryGetScale()
        {
            if (this.Transform == null)
            {
                return false;
            }

            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    if (!this.displayScale.ApproximatelyEquals(this.Transform.localScale))
                    {
                        this.displayScale = this.Transform.localScale;
                        return true;
                    }
                    return false;
                case TransformProSpace.World:
                    if (!this.displayScale.ApproximatelyEquals(this.Transform.lossyScale))
                    {
                        this.displayScale = this.Transform.lossyScale;
                        return true;
                    }
                    return false;
            }
        }

        private bool TrySetPosition(Vector3 position)
        {
            if (this.Transform == null)
            {
                return false;
            }

            if (!this.CanChangePosition)
            {
                return false;
            }
            // WARN: Must be careful with preventing values from being set.
            if (this.displayPosition.ApproximatelyEquals(position))
            {
                return false;
            }

            this.displayPosition = position;
            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    this.Transform.localPosition = position;
                    return true;
                case TransformProSpace.World:
                    this.Transform.position = position;
                    return true;
            }
        }

        private bool TrySetRotation(Vector3 value)
        {
            if (this.Transform == null)
            {
                return false;
            }

            if (!this.CanChangeRotation)
            {
                return false;
            }

            // LOOK: Becase of the new way of calculating the bounds (storing local bounds and passing in a l2w matrix) this is no longer required.
            //this.SetBoundsDirtyWorld();

            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    this.Transform.localEulerAngles = value;
                    return true;
                case TransformProSpace.World:
                    this.Transform.eulerAngles = value;
                    return true;
            }
        }

        private bool TrySetRotation(Quaternion rotation)
        {
            if (this.Transform == null)
            {
                return false;
            }

            if (!this.CanChangeRotation)
            {
                return false;
            }

            // LOOK: Becase of the new way of calculating the bounds (storing local bounds and passing in a l2w matrix) this is no longer required.
            //this.SetBoundsDirtyWorld();

            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    this.Transform.localRotation = rotation;
                    this.displayRotation = this.Transform.localEulerAngles;
                    return true;
                case TransformProSpace.World:
                    this.Transform.rotation = rotation;
                    this.displayRotation = this.Transform.eulerAngles;
                    return true;
            }
        }

        private bool TrySetScale(Vector3 scale)
        {
            if (this.Transform == null)
            {
                return false;
            }

            if (!this.CanChangeScale)
            {
                return false;
            }
            // WARN: Must be careful with preventing values from being set.
            if (this.displayScale.ApproximatelyEquals(scale))
            {
                return false;
            }

            this.displayScale = scale;
            switch (TransformPro.space)
            {
                default:
                    Debug.LogError(string.Format("[<color=red>TransformPro</color>] Space mode {0} not handled!", TransformPro.space));
                    return false;
                case TransformProSpace.Local:
                    this.Transform.localScale = scale;
                    return true;
                case TransformProSpace.World:
                    // TODO: Approximate the lossy value, convert it to a local scale and set it
                    return false;
            }
        }
    }
}
