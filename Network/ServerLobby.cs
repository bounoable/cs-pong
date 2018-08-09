namespace Pong.Network
{
    class ServerLobby: Lobby
    {
        public string Password { get; }

        public ServerLobby(string name, string password = null): base(name)
        {
            Password = password;
        }
    }
}