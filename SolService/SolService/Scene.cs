using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SolService
{
    public class Scene
    {
        private Player player;
        public List<Location> locations;

        // Default scene height/width
        private int scenesize = 9;

        public void Build()
        {
            Debug.WriteLine("Building scene........");
            // build locations from player position
            int curx = player.GetX();
            int cury = player.GetY();

            World world = new World();
            world.Load(player.GetWorldID());
            Debug.WriteLine("Locating scene tiles.......");
            locations = world.GetLocationsAround(player.GetX(), player.GetY(), (int)Math.Floor(Convert.ToDouble(scenesize) / 2));
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }
    }
}