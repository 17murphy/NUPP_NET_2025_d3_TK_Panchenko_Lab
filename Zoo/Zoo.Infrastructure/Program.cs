using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using University.Infrastructure.Services;
using Zoo.Infrastructure;
using Zoo.Infrastructure.Models;
using Zoo.Infrastructure.Repositories;
using Zoo.Infrastructure.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<ZooContext>(options =>
            options.UseSqlite("Data Source=zoo.db"));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));


        var provider = services.BuildServiceProvider();

        var zooService = provider.GetRequiredService<ICrudServiceAsync<ZooModel>>();

        var newZoo = new ZooModel { Name = "Екопарк", Location = "Полтава" };
        await zooService.CreateAsync(newZoo);

        var zooKeeperService = provider.GetRequiredService<ICrudServiceAsync<ZookeeperModel>>();

        var newZookeeper = new ZookeeperModel { FullName = "Браунi Бон", ExperienceYears = 10, Zoo = newZoo };
        await zooKeeperService.CreateAsync(newZookeeper);

        var birdService = provider.GetRequiredService<ICrudServiceAsync<BirdModel>>();

        var newBird = new BirdModel { Name = "Ластiвка", Age = 5, CanFly = true, Wingspan = 10, Zoo = newZoo, Zookeeper = newZookeeper };
        await birdService.CreateAsync(newBird);

        var all = await birdService.ReadAllAsync();
        foreach (var bird in all)
        {
            Console.WriteLine($"{bird.Id}: {bird.Name} — {bird.Age} years, can fly - {bird.CanFly}, wing span - {bird.Wingspan}, zoo keeper - {bird.Zookeeper.FullName}");
        }
    }
}
