using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolService
{
    public class Account
    {
        private int id;

        private String name;
        private String password;
        private String email;
        private Guid token = Guid.NewGuid();

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

        public void SetPassword(String password)
        {
            this.password = password;
        }

        public String GetPassword()
        {
            return this.password;
        }

        public Guid GetToken()
        {
            return this.token;
        }

        public void SetToken(Guid token)
        {
            this.token = token;
        }     

        public String GetEmail()
        {
            return this.email;
        }

        public void SetEmail(String email)
        {
            this.email = email;
        }

        public void Load(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbAccount dbaccount = se.dbAccounts.SingleOrDefault(a => a.id.Equals(id));
                if (dbaccount == null)
                {
                    return;
                }

                SetID(dbaccount.id);
                SetName(dbaccount.name);
                SetToken(dbaccount.guid);
                SetPassword(dbaccount.password);
                SetEmail(dbaccount.email);
            }
        }

        private void SaveNew()
        {
            using (SolEntities se = new SolEntities())
            {
                dbAccount dbaccount = new dbAccount();
                dbaccount.name = GetName();
                dbaccount.guid = GetToken();
                dbaccount.password = GetPassword();
                dbaccount.email = GetEmail();

                se.dbAccounts.AddObject(dbaccount);
                se.SaveChanges();

                SetID(dbaccount.id);
            }
        }

        private void Save()
        {
            using (SolEntities se = new SolEntities())
            {
                int keyid = this.GetID();
                dbAccount dbaccount = se.dbAccounts.SingleOrDefault(a => a.id.Equals(keyid));

                dbaccount.name = GetName();
                dbaccount.guid = GetToken();
                dbaccount.password = GetPassword();
                dbaccount.email = GetEmail();

                se.dbAccounts.AddObject(dbaccount);
                se.SaveChanges();

                SetID(dbaccount.id);
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