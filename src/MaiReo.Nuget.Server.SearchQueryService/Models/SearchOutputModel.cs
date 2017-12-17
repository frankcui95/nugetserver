using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Models
{
    public class SearchOutputModel
    {
        public SearchOutputModel()
        {
            Data = new List<SearchResultModel>();
        }
        public int TotalHits { get; set; }

        public List<SearchResultModel> Data { get; set; }
    }
}
