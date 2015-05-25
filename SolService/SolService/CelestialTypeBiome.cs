using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class CelestialTypeBiome
    {
        int id;
        int celestialtypeid;
        Biome biome;

        public int GetID()
        {
            return this.id;
        }

        public void SetID(int id)
        {
            this.id = id;
        }

        public int GetCelestialTypeID()
        {
            return this.celestialtypeid;
        }

        public void SetCelestialTypeID(int celestialtypeid)
        {
            this.celestialtypeid = celestialtypeid;
        }

        public void SetBiome(Biome biome)
        {
            this.biome = biome;
        }

        public Biome GetBiome()
        {
            return this.biome;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbCelestialTypeBiome dbcelestialtypebiome = se.dbCelestialTypeBiomes.SingleOrDefault(b => b.id.Equals(id));
                if (dbcelestialtypebiome == null)
                {
                    return;
                }

                SetID(dbcelestialtypebiome.id);
                SetCelestialTypeID(dbcelestialtypebiome.celestialtype_id);

                Biome biome = new Biome();
                biome.Load(dbcelestialtypebiome.biome_id);
                if (biome.GetID() > 0)
                {
                    SetBiome(biome);
                }
            }
        }
    }
}