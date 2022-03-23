using AsanaToCosmoDB.Models.Abstract;
using System.Collections.Generic;

namespace AsanaToCosmoDB.Models
{
    public class AsanaUser : AsanaObject
    {
        public string email { get; set; }
        public Dictionary<string,string> photo { get; set; }
        public AsanaWorkspace workspace { get; set; }
    }

    public class AsanaUsers : IPaginationResult
    {
        public IEnumerable<AsanaUser> data { get; set; }
        public PaginationPage next_page { get; set; }
    }
}

