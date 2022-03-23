using AsanaToCosmoDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class TaskService : AsanaServiceBase
    {
        private AsanaConfig Config { get; set; }
        public TaskService(AsanaConfig config) : base("tasks", config.APIKey)
        {
            Config = config;
        }
        public async Task<IEnumerable<AsanaTask>> GetTasks(string projectId)
        {
            var allTasks = new List<AsanaTask>();
            var limit = 100;
            var offset = string.Empty;
            do
            {
                var tasks = await GetTasksByPage(projectId, limit, offset);
                offset = tasks?.next_page?.offset;
                allTasks.AddRange(tasks?.data);
            } while (!string.IsNullOrEmpty(offset));
            return allTasks;
        }

        public async Task<AsanaTasks> GetTasksByPage(string projectId, int limit, string offset)
        {
            if (string.IsNullOrEmpty(offset))
                return await GetRequest<AsanaTasks>("?project={0}&limit={1}&opt_fields={2}", projectId, limit, Config.TaskFields);
            else
                return await GetRequest<AsanaTasks>("?project={0}&limit={1}&offset={2}&opt_fields={3}", projectId, limit, offset, Config.TaskFields);
        }

        public async Task<IEnumerable<AsanaAttachment>> GetAttachments(string taskId)
        {
            var attachments = (await GetRequest<AsanaAttachments>("{0}/attachments?opt_fields={1}", taskId, Config.AttachmentFields)).data;
            foreach (var attachment in attachments) attachment.task_gid = taskId;
            return attachments;
        }
    }
}
