using System;
using fsi.settings.Informations;
using Fsi.Ui.Inputs.Prompts.Information;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.Localization;

namespace Fsi.Ui.Inputs.Information
{
    [Serializable]
    public class InputInformation : Information<string>
    {
        [InputControl]
        [SerializeField]
        private string path;
        public override string Type => path;
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description => locDescription.GetLocalizedString();

        [Header("Sprite")]

        [SerializeField]
        private PromptInformationGroup promptInformation;
        public PromptInformationGroup PromptInformation => promptInformation;
    }
}