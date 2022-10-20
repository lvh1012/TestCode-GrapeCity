using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;
using TestCode.Attributes;

namespace TestCode.Entities
{
    public class UserEntity : ITableEntity
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        [Required]
        [EmailAttribute]
        public string Email { get; set; }
        public string Note { get; set; }
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
