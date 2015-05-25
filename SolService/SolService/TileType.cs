using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolService
{
    public class TileType
    {
        

        int id;
        String name;

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

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbTileType dbtiletype = se.dbTileTypes.SingleOrDefault(t => t.id.Equals(id));
                if (dbtiletype == null)
                {
                    return;
                }

                SetID(dbtiletype.id);
                SetName(dbtiletype.name);
            }
        }
            
    }
}
