using System;

namespace Zoo.Common.Models
{
    public abstract class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public static int TotalAnimals;

        static Animal()
        {
            TotalAnimals = 0;
        }
        public Animal(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            TotalAnimals++;
        }

        public virtual void Speak()
        {
            System.Console.WriteLine("якийсь звук тварини...");
        }

        public event Action<string> OnSpeak;

        protected void RaiseSpeakEvent(string message)
        {
            OnSpeak?.Invoke(message);
        }
    }

    public static class AnimalExtensions
    {
        public static string Describe(this Animal animal)
        {
            return $"“варина: {animal.Name} (ID: {animal.Id})";
        }
    }
}
