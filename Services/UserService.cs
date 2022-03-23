using AsanaToCosmoDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsanaToCosmoDB.Services
{
    public class UserService : AsanaServiceBase
    {
        private AsanaConfig Config { get; set; }
        public UserService(AsanaConfig config) : base("users", config.APIKey)
        {
            Config = config;
        }
        public async Task<IEnumerable<AsanaUser>> GetUsers()
        {
            return (await GetRequest<AsanaUsers>("?workspace={0}&opt_fields={1}", Config.WorkspaceId, Config.UserFields)).data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdentifier">A string identifying a user. This can either be the string "me", an email, or the gid of a user.</param>
        /// <returns></returns>
        public async Task<AsanaUser> GetUser(string userIdentifier)
        {            
                return await GetRequest<AsanaUser>("{0}?opt_fields={1}", userIdentifier, Config.UserFields);
        }
    }
}
