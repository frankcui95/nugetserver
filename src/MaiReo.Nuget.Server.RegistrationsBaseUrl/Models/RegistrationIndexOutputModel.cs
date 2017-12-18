using System;
using System.Collections.Generic;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class RegistrationIndexOutputModel
    {
        public RegistrationIndexOutputModel()
        {
            Items = new List<RegistrationPageOutputModel>(0);
        }

        public int Count { get; set; }

        public List<RegistrationPageOutputModel> Items { get; set; }
    }
}
