using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Provides a faster interface to tile details, shippable over web services

namespace SolService
{
    public class SceneTile
    {
        public int tile_id;
        public int location_id;
        public String tile_name;
        public String tile_description;
        public String tile_prefab;
        public int tiletype_id;
        public String tiletype_name;

        public void Load(int tile_id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbTile dbtile = se.dbTiles.SingleOrDefault(t => t.id.Equals(tile_id));
                if (dbtile == null)
                {
                    return;
                }

                this.tile_id = dbtile.id;
                this.tile_name = dbtile.name;
                this.tile_description = dbtile.description;
                this.tile_prefab = dbtile.prefab;
                this.tiletype_id = dbtile.type_id;

                dbTileType dbtiletype = se.dbTileTypes.SingleOrDefault(tt => tt.id.Equals(this.tiletype_id));

                if (dbtiletype == null)
                {
                    return;
                }
                this.tiletype_name = dbtiletype.name;
            }
        }
    }
}