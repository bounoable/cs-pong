using System;
using GameNet;
using Pong.IO;
using Pong.Menu;
using System.Net;
using Pong.Messages;
using System.Threading.Tasks;

namespace Pong
{
    class Game
    {
        public void Init()
        {
            InitGameNetFactory();
            
            new InitGameMenu(this).Draw();
        }

        void InitGameNetFactory()
        {
            GameNetFactory factory = GameNetFactory.Instance;
        }

        public void CreateServer()
        {
            GameServer.Create().Init();
        }

        public void CreateClient()
        {
            GameClient.Create().Init();
        }
    }
}