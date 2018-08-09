using System.Collections.Generic;

namespace Pong.Network
{
    abstract class Lobby
    {
        public string Name { get; }

        readonly HashSet<Player> _players = new HashSet<Player>();

        public Lobby(string name)
        {
            Name = name;
        }
    }
}