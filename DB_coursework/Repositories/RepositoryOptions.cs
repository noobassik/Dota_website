using System.ComponentModel.DataAnnotations;

namespace DB_coursework.Repositories
{
    public class RepositoryOptions
    {
        public const string Key = "DotaRepository";

        [Required]

        public string ConnectionString { get; set; }
    }
}
