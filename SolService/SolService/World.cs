using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SolService
{
    public class World
    {
        private int id;

        private String name = "Unknown World";

        private int width;
        private int height;

        private int x;
        private int y;

        private CelestialType celestialtype;
        private List<Location> locations = new List<Location>();

        public World()
        {
            this.locations.Clear();
        }

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public int GetWidth()
        {
            return this.width;
        }

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public int GetHeight()
        {
            return this.height;
        }

        public void SetHeight(int height)
        {
            this.height = height;
        }

        public int GetX()
        {
            return this.x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return this.y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public CelestialType GetCelestialType()
        {
            return this.celestialtype;
        }

        public void SetCelestialType(CelestialType celestialtype)
        {
            this.celestialtype = celestialtype;
        }

        public List<Location> GetLocations()
        {
            return this.locations;
        }

        public void SetLocations(List<Location> locations)
        {
            this.locations = locations;
        }

        public List<int> GetCelestialTypeIds(bool isworld)
        {
            List<int> celestialtypeids = new List<int>();

            using (SolEntities se = new SolEntities())
            {
                celestialtypeids = se.dbCelestialTypes.Where(w => w.isworld.Equals(isworld)).Select(w => w.id).ToList<int>();
            }

            return celestialtypeids;
        }

        
        // Map Generation
        public List<Location> GetRandomLocations()
        {
            List<Location> locations = new List<Location>();
            List<Tile> celestial_tiles = this.celestialtype.GetTilesFromBiomes();

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

        private void SaveNew()
        {
            using (SolEntities se = new SolEntities())
            {
                dbCelestial dbcelestial = new dbCelestial();
                dbcelestial.name = GetName();
                dbcelestial.width = GetWidth();
                dbcelestial.height = GetHeight();
                dbcelestial.type_id = GetCelestialType().GetID();
                dbcelestial.x = GetX();
                dbcelestial.y = GetY();

                se.dbCelestials.AddObject(dbcelestial);
                se.SaveChanges();

                SetID(dbcelestial.id);

                foreach (Location location in GetLocations())
                {
                    dbLocation dblocation = new dbLocation();
                    dblocation.celestial_id = GetID();
                    dblocation.tile_id = location.GetTileID();
                    dblocation.x = location.GetX();
                    dblocation.y = location.GetY();

                    se.dbLocations.AddObject(dblocation);

                    se.SaveChanges();

                    location.SetID(dblocation.id);
                }

            }
        }

        private void Save()
        {
            using (SolEntities se = new SolEntities())
            {
                int keyid = this.GetID();
                dbCelestial dbcelestial = se.dbCelestials.SingleOrDefault(c => c.id.Equals(keyid));

                dbcelestial.name = GetName();
                dbcelestial.width = GetWidth();
                dbcelestial.height = GetHeight();
                dbcelestial.type_id = GetCelestialType().GetID();
                dbcelestial.x = GetX();
                dbcelestial.y = GetY();

                foreach (Location location in GetLocations())
                {
                    int location_keyid = location.GetID();
                    dbLocation dblocation = se.dbLocations.SingleOrDefault(l => l.id.Equals(location_keyid));
                    if (dblocation == null)
                    {
                        dblocation = new dbLocation();
                        dblocation.celestial_id = dbcelestial.id;
                        dblocation.tile_id = location.GetTileID();
                        dblocation.x = location.GetX();
                        dblocation.y = location.GetY();
                        se.dbLocations.AddObject(dblocation);
                        

                        location.SetID(dblocation.id);
                    }
                    else
                    {
                        dblocation.celestial_id = dbcelestial.id;
                        dblocation.tile_id = location.GetTileID();
                        dblocation.x = location.GetX();
                        dblocation.y = location.GetY();
                    }
                }

                se.SaveChanges();
                Load(keyid);
                
            }

        }
        

        public void SaveChanges()
        {
            if (id > 0)
            {
                Save();
            }
            else
            {
                SaveNew();
            }
        }

        public List<Location> GetLocationsFromCelestialId(int id)
        {
            List<Location> locations = new List<Location>();

            using (SolEntities se = new SolEntities())
            {
                Debug.WriteLine("Fetching world locations");
                List<dbLocation> dblocations = se.dbLocations.Where(l => l.celestial_id.Equals(id)).ToList<dbLocation>();
                Debug.WriteLine("Adding World Locations");
                foreach (dbLocation dblocation in dblocations)
                {
                    Location location = new Location();
                    // inefficient
                    //location.Load(dblocation.id);
                    location.SetID(dblocation.id);
                    location.SetTileID(dblocation.tile_id);
                    location.SetWorldID(dblocation.celestial_id);
                    location.SetX(dblocation.x);
                    location.SetY(dblocation.y);
                    if (location.GetID() > 0)
                    {
                        locations.Add(location);
                    }
                }
            }

            return locations;
        }


        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbCelestial dbcelestial = se.dbCelestials.SingleOrDefault(c => c.id.Equals(id));
                if (dbcelestial == null)
                {
                    return;
                }

                SetID(dbcelestial.id);
                SetName(dbcelestial.name);
                SetHeight(dbcelestial.height);
                SetWidth(dbcelestial.width);
                SetX(dbcelestial.x);
                SetY(dbcelestial.y);

                CelestialType celestialtype = new CelestialType();
                celestialtype.Load(dbcelestial.type_id);
                if (celestialtype.GetID() > 0)
                {
                    SetCelestialType(celestialtype);
                }

                Debug.WriteLine("Populating World Locations");
                List<Location> locations = GetLocationsFromCelestialId(dbcelestial.id);
                if (locations != null)
                {
                    SetLocations(locations);
                }
            }
        }

        public CelestialType GetRandomWorldType()
        {
            List<int> celestialTypeIds = GetCelestialTypeIds(true);
            int rndtypeindex = new Random().Next(celestialTypeIds.Count);
            int rndcelestialtypeid = celestialTypeIds[rndtypeindex];
            CelestialType celestialtype = new CelestialType();
            celestialtype.Load(rndcelestialtypeid);

            return celestialtype;
        }

        public void GenerateRandom()
        {
            this.celestialtype = null;
            this.locations.Clear();

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

            Location location = GetLocations().FirstOrDefault(loc => loc.GetTile().GetTileType().GetName().Equals("Floor"));

            if (location == null)
            {
                throw new Exception("No location found ("+GetLocations().Count+") ");
            }

            return location;
        }

        public List<Location> GetLocationsAround(int x, int y, int distance)
        {
            int fromx = x - distance;
            int fromy = y - distance;

            int tox = x + distance;
            int toy = y + distance;

            List<Location> locationsaround = locations.Where(location => location.GetX() >= fromx && location.GetX() <= tox && location.GetY() >= fromy && location.GetY() <= toy).ToList<Location>();
            return locationsaround;
        }
    }
}