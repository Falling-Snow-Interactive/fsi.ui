using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Categories
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class FsiInspector : Editor
    {
        #region Constants

        private const string DefaultCategory = "__UNCATEGORIZED__";
        private const string RootCategory = "__ROOT__";
        private const char PathBreak = '/';
        
        #endregion

        public override VisualElement CreateInspectorGUI()
        {
            // Only use this custom inspector if the class has [FsiInspector]
            Type targetType = target.GetType();
            bool hasFsiInspector = targetType.GetCustomAttribute<FsiAttribute>() != null;

            if (!hasFsiInspector)
            {
                // Use Unity's default inspector for everything else
                return base.CreateInspectorGUI();
            }

            // From here on: your existing custom inspector logic
            VisualElement root = new();

            // Optional: show Script field at top and lock it
            SerializedProperty scriptProperty = serializedObject.FindProperty("m_Script");
            if (scriptProperty != null)
            {
                PropertyField scriptField = new(scriptProperty)
                                            {
                                                enabledSelf = false,
                                            };
                root.Add(scriptField);
            }

            // Reflection: gather fields
            FieldInfo[] fields = serializedObject.targetObject
                                                 .GetType()
                                                 .GetFields(BindingFlags.Instance |
                                                            BindingFlags.Public |
                                                            BindingFlags.NonPublic);

            CategoryNode rootNode = new() { Name = RootCategory};

            // Collect fields into category tree
            foreach (FieldInfo field in fields)
            {
                if (!field.IsPublic && field.GetCustomAttribute<SerializeField>() == null)
                {
                    continue;
                }

                SerializedProperty prop = serializedObject.FindProperty(field.Name);
                if (prop == null)
                {
                    continue;
                }

                CategoryAttribute catAttr = field.GetCustomAttribute<CategoryAttribute>();
                string categoryPath = catAttr != null ? catAttr.Category : DefaultCategory;

                AddToCategoryTree(rootNode, categoryPath, prop, field);
            }

            root.Add(BuildCategoryUI(rootNode));
            return root;
        }

        private static void AddToCategoryTree(CategoryNode rootNode, string path, SerializedProperty prop, FieldInfo fieldInfo)
        {
            // Uncategorised → directly on root
            if (path == DefaultCategory)
            {
                rootNode.Properties.Add(new CategoryProperty { Property = prop, FieldInfo = fieldInfo });
                return;
            }

            string[] parts = path.Split(PathBreak);
            CategoryNode current = rootNode;

            foreach (string part in parts)
            {
                if (!current.Children.TryGetValue(part, out CategoryNode child))
                {
                    child = new CategoryNode { Name = part };
                    current.Children.Add(part, child);
                }

                current = child;
            }

            current.Properties.Add(new CategoryProperty { Property = prop, FieldInfo = fieldInfo });
        }

        private VisualElement BuildCategoryUI(CategoryNode node)
        {
            VisualElement container = new();

            // Root: render uncategorized fields
            if (node.Name == RootCategory)
            {
                foreach (CategoryProperty cp in node.Properties)
                {
                    PropertyField field = new(cp.Property);
                    field.Bind(serializedObject);
                    
                    Label fieldLabel = field.Q<Label>();
                    if (fieldLabel != null)
                    {
                        fieldLabel.style.unityFontStyleAndWeight = FontStyle.Normal;
                    }

                    container.Add(field);
                }
            }

            // Top-level category foldouts
            foreach (KeyValuePair<string, CategoryNode> child in node.Children)
            {
                Foldout foldout = new()
                                  {
                                      text  = child.Value.Name,
                                      value = EditorPrefs.GetBool(child.Value.Name, false),
                                      style =
                                      {
                                          marginTop = 6,
                                      },
                                  };
                foldout.RegisterValueChangedCallback(evt =>
                                                     {
                                                         EditorPrefs.SetBool(child.Value.Name, evt.newValue);
                                                     });
                Label foldoutLabel = foldout.Q<Label>();
                if (foldoutLabel != null)
                {
                    foldoutLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
                }

                foreach (CategoryProperty cp in child.Value.Properties)
                {
                    PropertyField field = new(cp.Property);
                    field.Bind(serializedObject);
                    
                    Label fieldLabel = field.Q<Label>();
                    if (fieldLabel != null)
                    {
                        fieldLabel.style.unityFontStyleAndWeight = FontStyle.Normal;
                    }

                    foldout.Add(field);
                }

                AddNestedCategories(child.Value, foldout);
                container.Add(foldout);
            }

            return container;
        }

        private void AddNestedCategories(CategoryNode node, VisualElement parent)
        {
            foreach (KeyValuePair<string, CategoryNode> child in node.Children)
            {
                Foldout foldout = new()
                                  {
                                      text  = child.Value.Name,
                                      value = EditorPrefs.GetBool($"{child.Value.Name}_{parent.name}", false),
                                      style =
                                      {
                                          marginTop = 6,
                                      },
                                  };
                foldout.RegisterValueChangedCallback(evt =>
                                                     {
                                                         EditorPrefs.SetBool($"{child.Value.Name}_{parent.name}", evt.newValue);
                                                     });
                Label foldoutLabel = foldout.Q<Label>();
                if (foldoutLabel != null)
                {
                    foldoutLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
                }

                foreach (CategoryProperty cp in child.Value.Properties)
                {
                    PropertyField field = new(cp.Property);
                    field.Bind(serializedObject);
                    Label fieldLabel = field.Q<Label>();
                    if (fieldLabel != null)
                    {
                        fieldLabel.style.unityFontStyleAndWeight = FontStyle.Normal;
                    }

                    foldout.Add(field);
                }

                AddNestedCategories(child.Value, foldout);
                parent.Add(foldout);
            }
        }
    }
    
    public class CategoryProperty
    {
        public SerializedProperty Property;
        public FieldInfo FieldInfo;
    }

    public class CategoryNode
    {
        public string Name;
        public readonly Dictionary<string, CategoryNode> Children = new();
        public readonly List<CategoryProperty> Properties = new();
    }
}