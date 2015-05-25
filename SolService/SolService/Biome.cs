using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class Biome
    {
        int id;
        String name;
        List<BiomeTile> biometiles = new List<BiomeTile>();

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

        public void AddBiomeTile(BiomeTile biometile)
        {
            this.biometiles.Add(biometile);
        }

        public List<BiomeTile> GetBiomeTiles()
        {
            return this.biometiles;
        }

        public void SetBiomeTiles(List<BiomeTile> biometiles)
        {
            this.biometiles = biometiles;
        }

        public List<BiomeTile> GetBiomeTilesFromBiomeId(int id)
        {
            List<BiomeTile> biometiles = new List<BiomeTile>();

            using (SolEntities se = new SolEntities())
            {
                List<dbBiomeTile> dbbiometiles = se.dbBiomeTiles.Where(b => b.biome_id.Equals(id)).ToList<dbBiomeTile>();
                foreach (dbBiomeTile dbbiometile in dbbiometiles)
                {
                    BiomeTile biometile = new BiomeTile();
                    biometile.Load(dbbiometile.id);

                    if (biometile.GetID() > 0)
                    {
                        biometiles.Add(biometile);
                    }
                }
            }

            return biometiles;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbBiome dbbiome = se.dbBiomes.FirstOrDefault(b => b.id.Equals(id));
                if (dbbiome == null)
                {
                    return;
                }

                SetID(dbbiome.id);
                SetName(dbbiome.name);

                List<BiomeTile> biometiles = GetBiomeTilesFromBiomeId(id);

                if (biometiles != null)
                {
                    SetBiomeTiles(biometiles);
                }
            }
        }
    }
}