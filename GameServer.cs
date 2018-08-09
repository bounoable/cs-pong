using System;
using GameNet;
using Pong.IO;
using Pong.Menu;
using System.Net;
using Pong.Network;
using Pong.Messages;
using GameNet.Messages;
using Pong.Messages.Handlers;
using Pong.Messages.Serializers;
using Pong.Menu.Commands.Server;
using System.Collections.Generic;

namespace Pong
{
    class GameServer
    {
        public Server Server { get; }

        int NextPlayerId
        {
            get
            {
                int id = _nextPlayerId;
                _nextPlayerId++;

                return id;
            }
        }

        int _nextPlayerId = 1;

        readonly HashSet<Lobby> _lobbies = new HashSet<Lobby>();
        readonly HashSet<ServerPlayer> _players = new HashSet<ServerPlayer>();

        public GameServer(Server server)
        {
            Server = server;
        }

        public static GameServer Create()
        {
            Output.Clear();

            IPAddress parsedIp = null;
            Input.GetString("IP-Address", input => IPAddress.TryParse(input, out parsedIp));
            ushort tcpPort = (ushort)Input.GetInt("TCP-Port");
            ushort udpPort = (ushort)Input.GetInt("UDP-Port");

            try {
                var server = new GameServer(GameNetFactory.Instance.CreateServer(parsedIp, tcpPort, udpPort));

                Output.LineBreak();
                Output.WriteLine("\nServer started.\n", ConsoleColor.Green);
                Output.LineBreak();

                return server;
            } catch (Exception e) {
                Output.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Shutdown the game server.
        /// </summary>
        public void Shutdown() => Server.Stop();

        /// <summary>
        /// Initialize the game server.
        /// </summary>
        public void Init()
        {
            RegisterMessageTypes();
            InitServerEvents();

            Server.Start();

            var serverControl = new CommandSelection<ServerControl>()
            {
                Descriptions = {
                    { ServerControl.Shutdown, "Stop server" }
                }
            };

            serverControl.On(ServerControl.Shutdown, Shutdown);
            serverControl.Draw();
        }

        void RegisterMessageTypes()
        {
            MessageTypeConfig types = Server.Messenger.TypeConfig;

            types.RegisterMessageType<CreateLobby>(new CreateLobbySerializer(), new CreateLobbyHandler(this));
        }

        void InitServerEvents()
        {
            Server.PlayerConnected += (sender, args) => {
                var player = new ServerPlayer(args.Player, NextPlayerId);
                _players.Add(player);
            };
        }

        /// <summary>
        /// Create a new lobby.
        /// </summary>
        /// <param name="name">The lobby name</param>
        /// <param name="password">The lobby password.</param>
        /// <returns>The created lobby.</returns>
        public ServerLobby CreateLobby(string name, string password = null)
        {
            foreach (Lobby existing in _lobbies) {
                if (existing.Name == name) {
                    throw new ArgumentException("Lobby name has already been taken.", "name");
                }
            }

            var lobby = new ServerLobby(name, password);
            _lobbies.Add(lobby);

            Output.WriteLine($"Name: {lobby.Name}");
            Output.WriteLine($"Password: {lobby.Password}");

            return lobby;
        }
    }
}