using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationPageOutputModel
    {
        public RegistrationPageOutputModel()
        {
        }
        /// <summary>
        /// The URL to the registration page
        /// </summary>
        [JsonProperty("@id")]
        public string Id { get; set; }
        /// <summary>
        /// The number of registration leaves in the page
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// The array of registration leaves and their associate metadata
        /// </summary>
        public List<RegistrationLeafOutputModel> Items { get; set; }
        /// <summary>
        /// The lowest SemVer 2.0.0 version in the page (inclusive)
        /// </summary>
        public string Lower { get; set; }
        /// <summary>
        /// The URL to the registration index
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// The highest SemVer 2.0.0 version in the page (inclusive)
        /// </summary>
        public string Upper { get; set; }
    }
}
