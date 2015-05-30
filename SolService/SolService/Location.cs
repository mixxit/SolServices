using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class Location
    {
        public int id;
        public int x;
        public int y;
        private int tile_id;
        private int world_id;
        public SceneTile scenetile;

        public Location()
        {
            
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbLocation dblocation = se.dbLocations.SingleOrDefault(l => l.id.Equals(id));
                if (dblocation == null)
                {
                    return;
                }
                SetID(dblocation.id);
                SetX(dblocation.x);
                SetY(dblocation.y);
                SetWorldID(dblocation.celestial_id);
                SetTileID(dblocation.tile_id);

                SceneTile scenetile = new SceneTile();
                scenetile.Load(dblocation.tile_id);
                this.scenetile = scenetile;
            }
        }

        public void SetSceneTile(SceneTile scenetile)
        {
            this.scenetile = scenetile;
        }

        public SceneTile GetSCeneTile()
        {
            return this.scenetile;
        }

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public Location(int world_id, int x, int y)
        {
            this.world_id = world_id;
            this.x = x;
            this.y = y;
        }

        public Location(int world_id, int x, int y, int tile_id)
        {
            this.world_id = world_id;
            this.x = x;
            this.y = y;
            this.tile_id = tile_id;

        }

        public void SetTileID(int tileid)
        {
            this.tile_id = tileid;
        }

        public int GetTileID()
        {
            return this.tile_id;
        }

        public void SetWorldID(int world_id)
        {
            this.world_id = world_id;
        }

        public int GetWorldID()
        {
            return this.world_id;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetX()
        {
            return this.x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetY()
        {
            return this.y;
        }



        public Tile GetTile()
        {
            Tile tile = new Tile();
            tile.Load(GetTileID());
            return tile;
        }
    }
}