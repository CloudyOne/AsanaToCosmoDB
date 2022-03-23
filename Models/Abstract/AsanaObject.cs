using System;

namespace AsanaToCosmoDB.Models.Abstract
{
    public class AsanaObject
    {
        public string id { 
            get {
                return Guid.NewGuid().ToString();
            } 
        }
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }
}

