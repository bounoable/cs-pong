using Pong.Menu.Commands;

namespace Pong.Menu
{
    class InitGameMenu: CommandSelection<InitGame>
    {
        readonly Game _game;

        public InitGameMenu(Game game)
        {
            _game = game;

            Descriptions.Add(InitGame.StartServer, "Start server");
            Descriptions.Add(InitGame.ConnectToServer, "Connect to server");
            Descriptions.Add(InitGame.QuitGame, "Quit game");

            On(InitGame.StartServer, game.CreateServer);
            On(InitGame.ConnectToServer, game.CreateClient);
            On(InitGame.QuitGame, () => {});
        }
    }
}