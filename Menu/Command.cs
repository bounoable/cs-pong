namespace Pong.Menu
{
    class Command
    {
        public string Description { get; }

        public Command(string description)
            => Description = description;
    }
}