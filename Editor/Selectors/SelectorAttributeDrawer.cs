using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Selectors
{
    [CustomPropertyDrawer(typeof(SelectorAttribute))]
    public abstract class SelectorAttributeDrawer<T> : PropertyDrawer where T : Object, ISelectorData
    {
        protected abstract List<T> GetData();
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new();
            List<T> data = GetData(); 
            List<string> names = data.Select(d => d.Name).ToList();

            int selectedIndex = 0;
            if (property.objectReferenceValue != null
                && property.objectReferenceValue is ISelectorData t)
            {
                selectedIndex = names.IndexOf(t.Name);
            }

            ObjectField objectField = new("Data")
                                              {
                                                  objectType = typeof(T),
                                                  value = property.objectReferenceValue
                                              };
            objectField.SetEnabled(false); // Optional: make it read-only
			
            DropdownField dropdown = new(names, selectedIndex){label = property.displayName};
            dropdown.RegisterValueChangedCallback(evt =>
                                                  {
                                                      var newSelected = data[names.IndexOf(evt.newValue)];
                                                      property.objectReferenceValue = newSelected;
                                                      property.serializedObject.ApplyModifiedProperties();
                                                  });
            root.Add(dropdown);
            return root;
        }
    }
}
