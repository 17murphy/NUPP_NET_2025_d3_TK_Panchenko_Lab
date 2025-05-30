using System;
using Zoo.Common.Models;
using Zoo.Common.Services;

namespace Zoo.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var animalService = new CrudService<Animal>();

            var lion = new Lion("Симба");
            lion.OnSpeak += System.Console.WriteLine;

            var parrot = new Bird("Полi");
            parrot.OnSpeak += System.Console.WriteLine;

            animalService.Create(lion);
            animalService.Create(parrot);

            foreach (var animal in animalService.ReadAll())
            {
                System.Console.WriteLine(animal.Describe());
                animal.Speak();
            }

            System.Console.WriteLine($"\nВсього тваринок: {Animal.TotalAnimals}");
        }
    }
}
