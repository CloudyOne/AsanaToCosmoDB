using AsanaToCosmoDB.Models;
using System.Collections.Generic;
using AsanaToCosmoDB.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net;
using System.IO;

namespace AsanaToCosmoDB
{
    public class AsanaMigrator
    {
        private readonly IConfiguration Configuration;
        private CosmoDBService CosmoDBService;
        private ProjectService projectService;
        private TaskService taskService;
        private UserService userService;  
        private AzureStorageService azureStorageService;

        public AsanaMigrator(IConfiguration configuration)
        {
            Configuration = configuration;
            var asanaConfig = Configuration.GetSection(nameof(AsanaConfig)).Get<AsanaConfig>();
            projectService = new ProjectService(asanaConfig);
            taskService = new TaskService(asanaConfig);
            userService = new UserService(asanaConfig);
            var cosmoDBConfig = Configuration.GetSection(nameof(CosmoDBConfig)).Get<CosmoDBConfig>();
            CosmoDBService = new CosmoDBService(cosmoDBConfig);
            var azureStorageConfig = Configuration.GetSection(nameof(AzureStorageConfig)).Get<AzureStorageConfig>();
            azureStorageService = new AzureStorageService(azureStorageConfig);
        }

        public async Task Run()
        {            
            var tasks = new List<AsanaTask>();
            var users = await userService.GetUsers();
            var projects = await projectService.GetProjects();
            // Get Tasks
            foreach(var project in projects)
                tasks.AddRange(await taskService.GetTasks(project.gid));
            
            // Create CosmoDB DB
            await CosmoDBService.EnsureDatabaseExistsAsync();

            // Migrate Projects
            var projectContainer = await CosmoDBService.CreateContainerAsync("Projects", "/id");
            foreach (var project in projects)
                await projectContainer.CreateItemAsync(project);

            // Migrate Tasks
            var taskContainer = await CosmoDBService.CreateContainerAsync("Tasks", "/id");
            foreach (var task in tasks)
                await taskContainer.CreateItemAsync(task);

            // Migrate Users
            var userContainer = await CosmoDBService.CreateContainerAsync("Users", "/id");
            foreach(var user in users)
                await userContainer.CreateItemAsync(user);

            // Migrate Attachmennts
            var attachmentContainer = await CosmoDBService.CreateContainerAsync("Attachments", "/id");
            using (var wc = new WebClient())
            {
                foreach (var task in tasks)
                {   
                    // Download URL has timelimit
                    var attachments = await taskService.GetAttachments(task.gid);
                    foreach (var attachment in attachments)
                    {
                        if (!string.IsNullOrEmpty(attachment.download_url))
                        using (MemoryStream stream = new MemoryStream(wc.DownloadData(attachment.download_url)))
                        {
                            attachment.blob_id = $"attachment{attachment.gid}_{attachment.name}";                            
                            var blob = await azureStorageService.Upload(attachment.blob_id, stream);
                            attachment.blob_url = $"{azureStorageService.BaseUri}/{attachment.blob_id}";                            
                        }
                        await attachmentContainer.CreateItemAsync(attachment);
                    }
                }
            }

            Console.WriteLine($"Users: {users.Count()}");
            Console.WriteLine($"Projects: {projects.Count()}");
            Console.WriteLine($"Tasks: {tasks.Count()}");
            Console.ReadKey();
        }
        
    }
}
