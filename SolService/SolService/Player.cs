using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolService
{
    public class Player
    {
        private int id;
        private String name;
        private Location location;
        private Account owner;

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

        public Location GetLocation()
        {
            return this.location;
        }

        public void SetLocation(Location location)
        {
            this.location = location;
        }

        public int GetWorldID()
        {
            return GetLocation().GetWorldID();
        }

        public int GetX()
        {
            return GetLocation().GetX();
        }

        public int GetY()
        {
            return GetLocation().GetY();
        }

        public Account GetOwner()
        {
            return this.owner;
        }

        public void SetOwner(Account owner)
        {
            this.owner = owner;
        }

        private void SaveNew()
        {
            using (SolEntities se = new SolEntities())
            {
                dbPlayer dbplayer = new dbPlayer();
                dbplayer.name = GetName();



                dbplayer.location_id = GetLocation().GetID();
                dbplayer.owner_id = GetOwner().GetID();

                se.dbPlayers.AddObject(dbplayer);
                se.SaveChanges();

                SetID(dbplayer.id);
            }
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbPlayer dbplayer = se.dbPlayers.SingleOrDefault(p => p.id.Equals(id));

                if (dbplayer == null)
                {
                    return;
                }

                this.SetID(dbplayer.id);
                this.SetName(dbplayer.name);

                Location location = new Location();
                location.Load(dbplayer.location_id);

                if (location.GetID() > 0)
                {
                    this.SetLocation(location);
                }

                Account account = new Account();

                if (account.GetID() > 0)
                {
                    account.Load(dbplayer.owner_id);
                    this.SetOwner(account);
                }
            }
        }

        public void LoadByOwnerId(int ownerid)
        {
            using (SolEntities se = new SolEntities())
            {
                dbPlayer dbplayer = se.dbPlayers.SingleOrDefault(p => p.owner_id.Equals(ownerid));
                if (dbplayer == null)
                {
                    return;
                } else {
                    Load(dbplayer.id);
                }
            }
        }

        private void Save()
        {
            using (SolEntities se = new SolEntities())
            {
                int keyid = this.GetID();
                dbPlayer dbplayer = se.dbPlayers.SingleOrDefault(p => p.id.Equals(keyid));

                dbplayer.name = GetName();
                dbplayer.location_id = GetLocation().GetID();
                dbplayer.owner_id = GetOwner().GetID();

                se.dbPlayers.AddObject(dbplayer);
                se.SaveChanges();

                SetID(dbplayer.id);
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
    }
}
