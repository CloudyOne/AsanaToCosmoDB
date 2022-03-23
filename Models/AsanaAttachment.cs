using AsanaToCosmoDB.Models.Abstract;
using System;
using System.Collections.Generic;

namespace AsanaToCosmoDB.Models
{
    public class AsanaAttachment : AsanaObject
    {
        public string task_gid;

        public string resource_subtype { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public string download_url { get; set; }
        public string host { get; set; }
        public AsanaUser parent { get; set; }
        public string permanent_url { get; set; }
        public string view_url { get; set; }
        public string blob_id { get; set; }
        public string blob_url { get; set; }
    }

    public class AsanaAttachments : IPaginationResult
    {
        public IEnumerable<AsanaAttachment> data { get; set; }
        public PaginationPage next_page { get; set; }
    }
}

