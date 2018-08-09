using GameNet;

namespace Pong.Network
{
    class ServerPlayer: Player
    {
        public GameNet.Player GameNetPlayer { get; }

        public ServerPlayer(GameNet.Player player, int id): base(id)
        {
            GameNetPlayer = player;
        }
    }
}