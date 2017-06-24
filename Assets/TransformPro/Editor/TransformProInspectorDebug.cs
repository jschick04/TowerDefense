namespace UntitledGames.Transforms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    public static class TransformProInspectorDebug
    {
        public static bool OtherInspectorsInstalled()
        {
            return TransformProInspectorDebug.GetOtherTypes(TransformProInspectorDebug.GetInspectorTypes()).Any();
        }

        public static void OutputInspectors()
        {
            IEnumerable<Type> types = TransformProInspectorDebug.GetInspectorTypes().ToList();
            IEnumerable<Type> otherTypes = TransformProInspectorDebug.GetOtherTypes(types).ToList();
            if (!otherTypes.Any())
            {
                Debug.Log("[<color=red>TransformPro</color>] TransformPro installed correctly.");
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Format("[<color=red>TransformPro</color>] {0} conflicting Transform Inspector{1} found. {2}",
                                                   otherTypes.Count(),
                                                   otherTypes.Count() == 1 ? "" : "s",
                                                   otherTypes.Count() > 1 ? "(select this message to see all)" : ""));
            foreach (Type type in otherTypes)
            {
                stringBuilder.AppendLine(string.Format("    {0}", type.FullName));
            }

            Debug.LogWarning(stringBuilder.ToString());
        }

        private static IEnumerable<Type> GetInspectorTypes()
        {
            Type editorType = typeof(Editor);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                                               .SelectMany(s => s.GetTypes())
                                               .Where(p => editorType.IsAssignableFrom(p) && p.IsClass);

            foreach (Type type in types)
            {
                IEnumerable<Attribute> attributes = type.GetCustomAttributes(true).Cast<Attribute>();
                foreach (Attribute attribute in attributes)
                {
                    CustomEditor customEditor = attribute as CustomEditor;
                    if (customEditor == null)
                    {
                        continue;
                    }

                    FieldInfo reflectedTypeField = typeof(CustomEditor).GetField("m_InspectedType", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (reflectedTypeField == null)
                    {
                        Debug.LogError("Failed to locate CustomEditor attribute inspected type field.");
                        continue;
                    }
                    Type inspectedType = (Type) reflectedTypeField.GetValue(customEditor);
#if UNITY_5_0
                    if (typeof(Transform) == inspectedType)
                    {
                        yield return type;
                    }

#else
                    if ((typeof(Transform) == inspectedType) || (typeof(Transform).IsAssignableFrom(inspectedType) && customEditor.isFallback))
                    {
                        yield return type;
                    }
#endif
                }
            }
        }

        private static IEnumerable<Type> GetOtherTypes(IEnumerable<Type> types)
        {
            return types.Where(type => (type != typeof(TransformProEditor)) && (type.FullName != "UnityEditor.TransformInspector"));
        }
    }
}
