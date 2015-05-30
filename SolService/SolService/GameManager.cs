using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class GameManager
    {
        public static int minWorldWidth = 50;
        public static int maxWorldWidth = 50;

        public static int minWorldHeight = 50;
        public static int maxWorldHeight = 50;

        private AccountManager accountManager;

        public GameManager()
        {
            this.accountManager = new AccountManager();
        }

        public Scene GetPlayerFullWorld(Guid playertoken)
        {
            Player player = this.accountManager.GetPlayerByToken(playertoken);
            if (player == null)
            {
                return null;
            }

            Scene scene = new Scene();
            scene.SetPlayer(player);
            scene.BuildFullWorld();

            return scene;
        }

        public Scene GetPlayerScene(Guid playertoken)
        {
            Player player = this.accountManager.GetPlayerByToken(playertoken);

            if (player == null)
            {
                return null;
            }

            Scene scene = new Scene();
            scene.SetPlayer(player);
            scene.Build();

            return scene;
        }

        public Guid GetPlayerToken(String email, String password)
        {
            return this.accountManager.GetToken(email, password);
        }

        public Boolean CreateAccount(String name, String email, String password)
        {
            return this.accountManager.CreateAccount(name, email, password);
        }
    }
}