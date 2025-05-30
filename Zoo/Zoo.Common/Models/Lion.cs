using System;

namespace Zoo.Common.Models
{
    public class Lion : Mammal
    {
        public Lion(string name) : base(name) { }

        public override void Speak()
        {
            RaiseSpeakEvent($"{Name} ��� �������!");
        }

        public static Lion CreateNew()
        {
            var random = new Random();
            var name = $"���_{random.Next(10, 9999)}";
            return new Lion(name);
        }
    }
}
