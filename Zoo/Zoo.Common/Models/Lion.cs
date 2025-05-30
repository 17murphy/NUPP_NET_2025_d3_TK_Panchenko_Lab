namespace Zoo.Common.Models
{
    public class Lion : Mammal
    {
        public Lion(string name) : base(name) { }

        public override void Speak()
        {
            RaiseSpeakEvent($"{Name} כוג דאנקטעü!");
        }
    }
}
