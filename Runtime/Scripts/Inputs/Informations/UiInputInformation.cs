using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

namespace Fsi.Ui.Scripts.Inputs.Informations
{
    [Serializable]
    public class UiInputInformation : Information<string>
    {
        [InputControl]
        [SerializeField]
        private string path;
        public override string Type => path;

        [Header("Sprite")]

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}