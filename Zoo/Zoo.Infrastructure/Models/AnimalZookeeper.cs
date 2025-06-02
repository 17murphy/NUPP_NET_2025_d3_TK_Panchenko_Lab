using System;
using Zoo.Infrastructure.Models;

public class AnimalZookeeperModel
{
    public Guid AnimalId { get; set; }
    public AnimalModel Animal { get; set; }

    public Guid ZookeeperId { get; set; }
    public ZookeeperModel Zookeeper { get; set; }
}
