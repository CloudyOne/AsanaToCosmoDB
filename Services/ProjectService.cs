using AsanaToCosmoDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class ProjectService : AsanaServiceBase
    {
        private AsanaConfig Config { get; set; }
        public ProjectService(AsanaConfig config) : base("projects", config.APIKey)
        {
            Config = config;
        }

        public async Task<IEnumerable<AsanaProject>> GetProjects()
        {
            var allProjects = new List<AsanaProject>();
            var limit = 100;
            var offset = string.Empty;
            do
            {
                var projects = await GetProjectsByPage(limit, offset);
                offset = projects?.next_page?.offset;
                allProjects.AddRange(projects.data);
            } while (!string.IsNullOrEmpty(offset));
            return allProjects;
        }

        public async Task<AsanaProjects> GetProjectsByPage(int limit, string offset)
        {
            if (string.IsNullOrEmpty(offset))
                return await GetRequest<AsanaProjects>("?limit={0}&opt_fields={1}&workspace={2}", limit, Config.ProjectFields, Config.WorkspaceId);
            else
                return await GetRequest<AsanaProjects>("?limit={0}&offset={1}&opt_fields={2}&workspace={3}", limit, offset, Config.ProjectFields, Config.WorkspaceId);
        }

    }

}
