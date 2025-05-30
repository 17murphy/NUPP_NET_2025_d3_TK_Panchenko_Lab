namespace Zoo.Common.Models
{
    public class Mammal : Animal
    {
        public Mammal(string name) : base(name) { }

        public override void Speak()
        {
            RaiseSpeakEvent($"{Name} ссавець каже: гар!");
        }
    }
}

    
