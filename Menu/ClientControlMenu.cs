using Pong.Menu.Commands.Client;

namespace Pong.Menu
{
    class ClientControlMenu: CommandSelection<ClientControl>
    {
        public ClientControlMenu(GameClient client)
        {
            Descriptions.Add(ClientControl.ListLobbies, "List lobbies");
            Descriptions.Add(ClientControl.CreateLobby, "Create lobby");
            Descriptions.Add(ClientControl.JoinLobby, "Join lobby");
            Descriptions.Add(ClientControl.LeaveServer, "Leave server");

            On(ClientControl.CreateLobby, client.InitCreateLobby);
        }
    }
}