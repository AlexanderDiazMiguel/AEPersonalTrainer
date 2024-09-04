using SQLite;

namespace AEPersonalTrainerApp.Models
{
    public class TrainingItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AudioText { get; set; }
    }
}
