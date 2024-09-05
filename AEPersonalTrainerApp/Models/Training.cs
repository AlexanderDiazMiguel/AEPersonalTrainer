
using SQLite;

namespace AEPersonalTrainerApp.Models
{
    public class Training
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), NotNull]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? AudioText { get; set; }

        public int DefaultRest { get; set; }
    }
}
