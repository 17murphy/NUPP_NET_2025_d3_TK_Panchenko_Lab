namespace Zoo.Infrastructure.Models
{
    public class ZooModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        // Один-до-багатьох: Зоопарк має багато тварин
        public ICollection<AnimalModel> Animals { get; set; }
        public ICollection<ZookeeperModel> Zookeepers { get; set; }
    }
}
