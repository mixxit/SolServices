using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SolService
{
    public class World : Celestial
    {

        public CelestialType GetRandomWorldType()
        {
            List<int> celestialTypeIds = GetCelestialTypeIds(true);
            int rndtypeindex = new Random().Next(celestialTypeIds.Count);
            int rndcelestialtypeid = celestialTypeIds[rndtypeindex];
            CelestialType celestialtype = new CelestialType();
            celestialtype.Load(rndcelestialtypeid);

            return celestialtype;
        }

        // Map Generation
        public List<Location> GetRandomLocations()
        {
            List<Location> locations = new List<Location>();
            List<Tile> celestial_tiles = this.GetCelestialType().GetTilesFromBiomes();

            Random rnd = new Random();

            if (celestial_tiles.Count > 0)
            {
                for (int x = 0; x < GetWidth(); x++)
                {
                    for (int y = 0; y < GetHeight(); y++)
                    {

                        int tileindex = rnd.Next(0, celestial_tiles.Count);
                        Tile tile = celestial_tiles[tileindex];

                        Location location = new Location(this.GetID(), x, y, tile.GetID());
                        locations.Add(location);
                    }
                }
            }

            return locations;
        }

        public void GenerateRandom()
        {
            this.SetCelestialType(null);
            this.GetLocations().Clear();

            // Set a random size
            Random rnd = new Random();
            SetWidth(rnd.Next(GameManager.minWorldWidth, GameManager.maxWorldHeight));
            SetHeight(rnd.Next(GameManager.minWorldHeight, GameManager.maxWorldHeight));

            // Get a random world type
            SetCelestialType(GetRandomWorldType());

            // Commit to DB so we aquire an ID to pass to the Location creator
            SaveChanges();

            // Populate the locations
            SetLocations(GetRandomLocations());

            // Commit to DB
            SaveChanges();
        }

        public Location GetRandomSpawnPoint()
        {

            Location location = GetLocations().FirstOrDefault(loc => loc.GetTile().GetTileType().GetName().Equals("Ground"));

            if (location == null)
            {
                throw new Exception("No location found (" + GetLocations().Count + ") ");
            }

            return location;
        }

    }
}