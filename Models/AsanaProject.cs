using AsanaToCosmoDB.Models.Abstract;
using System.Collections.Generic;

namespace AsanaToCosmoDB.Models
{
    public class AsanaProject : AsanaObject
    {
        public string title { get; set; }
        public string text { get; set; }
        public string color { get; set; }
        public bool archived { get; set; }
        public string html_notes { get; set; }
        public IEnumerable<AsanaUser> members { get; set; }
        public string notes { get; set; }
        public AsanaWorkspace workspace { get; set; }
    }

    public class AsanaProjects : IPaginationResult
    {
        public IEnumerable<AsanaProject> data { get; set; }
        public PaginationPage next_page { get; set; }
    }
}

