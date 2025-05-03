using System;
using fsi.settings.Informations;
using Fsi.Ui.Inputs;
using UnityEngine;

namespace Fsi.Ui.Settings.SchemeInformations
{
    [Serializable]
    public class SchemeInformation : Information<string>
    {
        [SerializeField]
        private string scheme;
        public override String Type => scheme;

        [SerializeField]
        private InputType input;
        public InputType Input => input;
    }
}