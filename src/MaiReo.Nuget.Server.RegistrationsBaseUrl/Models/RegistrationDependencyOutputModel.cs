using Newtonsoft.Json;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationDependencyOutputModel
    {
        public RegistrationDependencyOutputModel()
        {
            Range = "(, )";
        }
        public string Id { get; set; }
        [JsonProperty("@id")]
        public string Id_Alias { get; set; }
        public string Range { get; set; }
        public string Registration { get; set; }
    }
}