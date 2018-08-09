using System;
using Pong.Network;
using GameNet.Messages;

namespace Pong.Messages.Handlers
{
    class CreateLobbyHandler: MessageHandler<CreateLobby>
    {
        readonly GameServer _server;

        public CreateLobbyHandler(GameServer server)
        {
            _server = server;
        }

        override protected void HandleMessage(CreateLobby message)
        {
            GameNet.Player player = _server.Server.GetPlayerBySecret(message.Secret);

            if (player == null)
                return;

            try {
                ServerLobby lobby = _server.CreateLobby(message.Name, message.Password);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}