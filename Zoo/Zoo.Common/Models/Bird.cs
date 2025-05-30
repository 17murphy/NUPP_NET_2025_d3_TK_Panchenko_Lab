namespace Zoo.Common.Models
{
    public class Bird : Animal
    {
        public Bird(string name) : base(name) { }

        public override void Speak()
        {
            RaiseSpeakEvent($"{Name} пташка каже: чiрiк!");
        }
    }
}
