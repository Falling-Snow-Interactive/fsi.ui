using System;
using fsi.settings.Informations;
using UnityEngine;

namespace Fsi.Ui.Inputs.Prompts.Information
{
    [Serializable]
    public class PromptInformation : Information<PromptType>
    {
        [SerializeField]
        private PromptType prompt;
        public override PromptType Type => prompt;

        [SerializeField]
        private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}