namespace Zoo.Infrastructure.Models
{
    public class AnimalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        // Зв'язок один-до-одного з доглядачем
        public Guid ZookeeperId { get; set; }
        public ZookeeperModel Zookeeper { get; set; }

        // Один-до-багатьох із зоопарком
        public Guid ZooId { get; set; }
        public ZooModel Zoo { get; set; }
    }
}
