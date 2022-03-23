using AsanaToCosmoDB.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class CosmoDBService
    {
        private CosmosClient Client;
        private CosmoDBConfig Config;
        private Database Database { get; set; }
        public CosmoDBService(CosmoDBConfig config)
        {
            Config = config;
            Client = new CosmosClient(config.EndpointURI, config.PrimaryKey);            
        }

        public async Task EnsureDatabaseExistsAsync()
        {
            var response = await Client.CreateDatabaseIfNotExistsAsync(Config.DatabaseName);
            if (response != null)
                Database = response.Database;
        }

        public async Task<Container> CreateContainerAsync(string id, string partitionKey)
        {
            Container container = null;
            try
            {
                container = (await Database.CreateContainerIfNotExistsAsync(id, partitionKey)).Container;
            }
            catch (Exception err)
            {
                throw;
            }
            return container;
        }        
    }
}
