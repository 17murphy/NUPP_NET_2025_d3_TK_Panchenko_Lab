namespace Zoo.REST.Models
{
    public class ZookeeperModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int ExperienceYears { get; set; }

        // Один-до-одного: кожен доглядач має одну тварину
        public AnimalModel Animal { get; set; }

        // Зв’язок із зоопарком (один-до-багатьох)
        public Guid ZooId { get; set; }
        public ZooModel Zoo { get; set; }
    }
}
