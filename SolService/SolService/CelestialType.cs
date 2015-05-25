using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SolService
{
    public class CelestialType
    {
        int id;
        String name;
        bool isworld;
        List<CelestialTypeBiome> celestialtypebiomes = new List<CelestialTypeBiome>();

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

        public void SetIsWorld(bool isworld)
        {
            this.isworld = isworld;
        }

        public bool GetIsWorld()
        {
            return this.isworld;
        }

        public List<Tile> GetTilesFromBiomes()
        {
            List<Tile> tiles = new List<Tile>();

            foreach(CelestialTypeBiome celestialtypebiome in GetCelestialTypeBiomes())
            {
                foreach(BiomeTile biometile in celestialtypebiome.GetBiome().GetBiomeTiles())
                {
                    
                    tiles.Add(biometile.GetTile());
                }
            }

            return tiles;
        }

        public void AddCelestialTypeBiome(CelestialTypeBiome celestialtypebiome)
        {
            this.celestialtypebiomes.Add(celestialtypebiome);
        }

        public List<CelestialTypeBiome> GetCelestialTypeBiomes()
        {
            return this.celestialtypebiomes;
        }

        public void SetCelestialTypeBiomes(List<CelestialTypeBiome> celestialtypebiomes)
        {
            this.celestialtypebiomes = celestialtypebiomes;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbCelestialType dbcelestialtype = se.dbCelestialTypes.SingleOrDefault(t => t.id.Equals(id));
                if (dbcelestialtype == null)
                {
                    return;
                }

                SetID(dbcelestialtype.id);
                SetIsWorld(dbcelestialtype.isworld);
                SetName(dbcelestialtype.name);

                List<CelestialTypeBiome> celestialtypebomes = GetCelestialTypeBiomesByCelestialTypeId(dbcelestialtype.id);

                if (celestialtypebiomes != null)
                {
                    SetCelestialTypeBiomes(celestialtypebomes);
                }


            }
        }

        private List<CelestialTypeBiome> GetCelestialTypeBiomesByCelestialTypeId(int id)
        {
            List<CelestialTypeBiome> celestialtypebiomes = new List<CelestialTypeBiome>();

            using (SolEntities se = new SolEntities())
            {
                List<dbCelestialTypeBiome> dbcelestialtypebiomes = se.dbCelestialTypeBiomes.Where(b => b.celestialtype_id.Equals(id)).ToList<dbCelestialTypeBiome>();
                foreach (dbCelestialTypeBiome dbcelestialtypebiome in dbcelestialtypebiomes)
                {
                    CelestialTypeBiome celestialtypebiome = new CelestialTypeBiome();
                    celestialtypebiome.Load(dbcelestialtypebiome.id);

                    if (celestialtypebiome.GetID() > 0)
                    {
                        celestialtypebiomes.Add(celestialtypebiome);
                    }
                }
            }

            return celestialtypebiomes;
        }


    }
}