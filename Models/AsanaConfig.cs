namespace AsanaToCosmoDB.Models
{
    public class AsanaConfig
    {
        public string APIKey { get; set; }
        public string WorkspaceId { get; set; }
        public string ProjectFields { get; set; }
        public string TaskFields { get; set; }
        public string UserFields { get; set; }
        public string AttachmentFields { get; set; }
    }
}

