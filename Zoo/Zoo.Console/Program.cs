using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Common.Models;
using Zoo.Common.Services;

namespace Zoo.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var filePath = "animals.json";
            var animalService = new CrudServiceAsync<Animal>(filePath);
            var animals = new ConcurrentBag<Animal>();

            // Паралельне створення 1000 об'єктів
            await Task.Run(() =>
            {
                Parallel.For(0, 1000, i =>
                {
                    var lion = Lion.CreateNew();
                    animals.Add(lion);
                });
            });

            foreach (var animal in animals)
            {
                await animalService.CreateAsync(animal);
            }

            // Обчислення мінімального, максимального та середнього значень довжини імен
            var nameLengths = animals.Select(a => a.Name.Length);
            var min = nameLengths.Min();
            var max = nameLengths.Max();
            var avg = nameLengths.Average();

            System.Console.WriteLine($"Мiнiмальна довжина iменi: {min}");
            System.Console.WriteLine($"Максимальна довжина iменi: {max}");
            System.Console.WriteLine($"Середня довжина iменi: {avg:F2}");

            // Збереження у файл
            await animalService.SaveAsync();

            System.Console.WriteLine("Данi збережено!");
        }
    }
}
