using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class Tile
    {
        private int id;
        private String name;
        private String description;
        private TileType type;
        private String prefab;

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public String GetDescription()
        {
            return this.description;
        }

        public void SetDescription(String description)
        {
            this.description = description;
        }

        public TileType GetTileType()
        {
            return this.type;
        }

        public void SetTileType(TileType type)
        {
            this.type = type;
        }

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public String GetPrefab()
        {
            return this.prefab;
        }

        public void SetPrefab(String prefab)
        {
            this.prefab = prefab;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbTile dbtile = se.dbTiles.SingleOrDefault(t => t.id.Equals(id));
                if (dbtile == null)
                {
                    return;
                }

                SetID(dbtile.id);
                SetName(dbtile.name);
                SetDescription(dbtile.description);
                SetPrefab(dbtile.prefab);
                TileType tiletype = new TileType();

                tiletype.Load(dbtile.type_id);

                if (tiletype.GetID() > 0)
                {
                    SetTileType(tiletype);
                }
            }
        }
    }
}