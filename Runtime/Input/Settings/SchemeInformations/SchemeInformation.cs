using System;
using fsi.settings.Informations;
using Fsi.Ui.Input.Prompts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fsi.Ui.Input.Settings.SchemeInformations
{
    [Serializable]
    public class SchemeInformation : Information<string>
    {
        [SerializeField]
        private string scheme;
        public override String Type => scheme;

        [FormerlySerializedAs("prompt")]
        [FormerlySerializedAs("input")]
        [SerializeField]
        private PromptType prompts;
        public PromptType Prompts => prompts;
    }
}