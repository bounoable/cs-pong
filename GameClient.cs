using System;
using GameNet;
using Pong.IO;
using Pong.Menu;
using System.Net;
using Pong.Messages;
using GameNet.Messages;
using Pong.Messages.Handlers;
using Pong.Messages.Serializers;
using Pong.Menu.Commands.Client;

namespace Pong
{
    class GameClient
    {
        public Client Client { get; }
        
        readonly IPAddress _serverAddress;
        readonly ushort _serverPort;

        public GameClient(Client client, IPAddress serverAddress, ushort serverPort)
        {
            Client = client;
            _serverAddress = serverAddress;
            _serverPort = serverPort;
        }

        public static GameClient Create()
        {
            Output.Clear();

            IPAddress parsedIp = null;
            Input.GetString("Server-IP-Address", input => IPAddress.TryParse(input, out parsedIp));
            ushort tcpPort = (ushort)Input.GetInt("Server-TCP-Port");
            int udpPort = Input.GetInt("Local UDP-Port (leave blank for default)", null, -1);

            try {
                var client = new GameClient(
                    GameNetFactory.Instance.CreateClient(udpPort > -1 ? (ushort)udpPort : NetworkConfiguration.DEFAULT_UDP_PORT),
                    parsedIp,
                    tcpPort
                );

                Output.LineBreak();
                Output.WriteLine("Client started.", ConsoleColor.Green);
                Output.LineBreak();

                return client;
            } catch (Exception e) {
                Output.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Initialize the game client.
        /// </summary>
        public void Init()
        {
            RegisterMessageTypes();

            Client.Connect(_serverAddress, _serverPort);

            new ClientControlMenu(this).Draw();
        }

        void RegisterMessageTypes()
        {
            MessageTypeConfig types = Client.Messenger.TypeConfig;

            types.RegisterMessageType<CreateLobby>(new CreateLobbySerializer());
        }

        public void InitCreateLobby()
        {
            Console.Clear();

            string name = Input.GetString("Enter a lobby name", input => input.Length > 0);
            string password = Input.GetString("Enter a password (optional)", input => input.Length > 0, "__NO_PASSWORD__");
            password = password == "__NO_PASSWORD__" ? null : password;

            Client.Send(new CreateLobby(name, Client.Secret, password));
        }
    }
}