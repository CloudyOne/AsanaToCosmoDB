using AsanaToCosmoDB.Models.Abstract;
using System.Collections.Generic;

namespace AsanaToCosmoDB.Models
{
    public class AsanaWorkspace : AsanaObject
    {
        public List<string> email_domains { get; set; }
        public bool is_organization { get; set; }
    }
}