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
        public List<Location> locations = new List<Location>();

        // Default scene height/width
        private int scenesize = 9;

        public void BuildFullWorld()
        {
            Debug.WriteLine("Building full world.....");
            World world = new World();
            world.Load(player.GetWorldID());
            locations = world.GetLocations();

        }

        public void Build()
        {
            Debug.WriteLine("Building scene........");
            // build locations from player position
            int curx = player.GetX();
            int cury = player.GetY();

            World world = new World();
            world.Load(player.GetWorldID());
            locations = world.GetLocationsAround(player.GetX(), player.GetY(), (int)Math.Floor(Convert.ToDouble(scenesize) / 2));
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public List<Location> GetLocations()
        {
            return this.locations;
        }

        public void AddLocation(Location location)
        {
            this.locations.Add(location);
        }

        public void SetLocations(List<Location> locations)
        {
            this.locations = locations;
        }
    }
}