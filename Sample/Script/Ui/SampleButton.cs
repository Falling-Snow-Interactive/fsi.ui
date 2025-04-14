using Fsi.Ui.Buttons;
using UnityEditor;
using UnityEngine;

namespace Fsi.Ui.Sample.Ui
{
    public class SampleButton : FsiButton
    {
        #region Create Menu
        #if UNITY_EDITOR
        
        private const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Sample/Prefabs/Buttons/SampleButton_Ui.prefab";
        
        [MenuItem("GameObject/FSI/Ui/Sample/Button")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            Object button = Instantiate(asset.gameObject, parent.transform);
            button.name = "SampleButton_Ui_0";
        }
        
        #endif
        #endregion
    }
}