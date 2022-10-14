using Azure.Data.Tables;
using TestCode.Entities;
using TestCode.Services.Interfaces;

namespace TestCode.Services
{
    public class UserService : IUserService
    {
        private const string TableName = "User";
        private readonly IConfiguration _configuration;

        private async Task<TableClient> GetTableClient()
        {
            var tableServiceClient = new TableServiceClient(_configuration["StorageConnectionString"]);

            var tableClient = tableServiceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Azure.AsyncPageable<UserEntity>> GetAll()
        {
            var tableClient = await GetTableClient();
            return tableClient.QueryAsync<UserEntity>();
        }

        public async Task<UserEntity> Create(UserEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }
    }
}
