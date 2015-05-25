using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class BiomeTile
    {
        int id;
        int biomeid;
        Tile tile;

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public int GetBiomeID()
        {
            return this.biomeid;
        }

        public void SetBiome(int biomeid)
        {
            this.biomeid = biomeid;
        }

        public Tile GetTile()
        {
            return this.tile;
        }

        public void SetTile(Tile tile)
        {
            this.tile = tile;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbBiomeTile dbbiometile = se.dbBiomeTiles.SingleOrDefault(b => b.id.Equals(id));
                if (dbbiometile == null)
                {
                    return;
                }

                SetID(dbbiometile.id);
                SetBiome(dbbiometile.biome_id);

                Tile tile = new Tile();
                tile.Load(dbbiometile.tile_id);
                if (tile.GetID() > 0)
                {
                    SetTile(tile);
                }
            }
        }


    }
}