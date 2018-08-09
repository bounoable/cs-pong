namespace Pong.Messages
{
    class CreateLobby
    {
        public string Name { get; }
        public string Password { get; }
        public string Secret { get; }

        public CreateLobby(string name, string secret, string password = null)
        {
            Name = name;
            Secret = secret;
            Password = password;
        }
    }
}