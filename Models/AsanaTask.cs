using AsanaToCosmoDB.Models.Abstract;
using System;
using System.Collections.Generic;

namespace AsanaToCosmoDB.Models
{
    public class AsanaTask : AsanaObject
    {
        public AsanaUser assignee { get; set; }
        public string notes { get; set; }
        public bool completed { get; set; }
        public DateTimeOffset? completed_at { get; set; }
        public AsanaUser completed_by { get; set; }
        public AsanaTask parent { get; set; }
        public string html_notes { get; set; }
        public List<AsanaTask> dependencies { get; set; }
        public string description { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public string customer_fields { get; set; }
        public List<AsanaProject> projects { get; set; }
    }

        public class AsanaTasks : IPaginationResult
    {
        public IEnumerable<AsanaTask> data { get; set; }
        public PaginationPage next_page { get; set; }
    }
}

