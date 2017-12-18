using Newtonsoft.Json;
using System.Collections.Generic;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationDependencyGroupOutputModel
    {
        public RegistrationDependencyGroupOutputModel()
        {
            Dependencies = new List<RegistrationDependencyOutputModel>(0);
        }
        [JsonProperty("@id")]
        public string Id_alias { get; set; }

        public string TargetFramework { get; set; }

        public List<RegistrationDependencyOutputModel> Dependencies { get; set; }
    }
}