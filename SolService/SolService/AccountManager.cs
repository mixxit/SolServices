using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SolService
{
    class AccountManager
    {
        public AccountManager()
        {
            
        }

        public Boolean CreateAccount(String name, String email,String password)
        {

            using (SolEntities se = new SolEntities())
            {
                List<dbAccount> accounts = se.dbAccounts.Where(a => a.name.Equals(name) || a.email.Equals(email)).ToList <dbAccount>();
                if (accounts.Count > 0)
                {
                    return false;
                }

                Account account = new Account();
                account.SetName(name);
                account.SetEmail(email);
                account.SetPassword(password);

                account.SaveChanges();
                return true;
            }
        }

        public Guid GetToken(String email, String password)
        {
            using (SolEntities se = new SolEntities())
            {
                dbAccount account = se.dbAccounts.SingleOrDefault(a => a.email.Equals(email) && a.password.Equals(password));
                if (account == null)
                {
                    return Guid.Empty;
                }

                Guid generatedguid = Guid.NewGuid();

                account.guid = generatedguid;

                se.SaveChanges();
                return generatedguid;
            }
        }

        public Account GetAccountById(int id)
        {
            using (SolEntities se = new SolEntities())
            {
                dbAccount dbaccount = se.dbAccounts.SingleOrDefault(a => a.id.Equals(id));
                if (dbaccount == null)
                {
                    return null;
                }
                else
                {
                    Account account = new Account();
                    account.SetID(dbaccount.id);
                    account.SetEmail(dbaccount.email);
                    account.SetName(dbaccount.name);
                    account.SetToken(dbaccount.guid);
                    account.SetPassword(dbaccount.password);

                    return account;
                }
            }
        }

        public Account GetAccountByToken(Guid token)
        {
            using (SolEntities se = new SolEntities())
            {
                dbAccount dbaccount = se.dbAccounts.SingleOrDefault(a => a.guid.Equals(token));
                if (dbaccount == null)
                {
                    return null;
                }
                else
                {
                    return GetAccountById(dbaccount.id);
                }
            }
        }


        public Player GetPlayerByToken(Guid accounttoken)
        {
            Account account = GetAccountByToken(accounttoken);

            if (account == null)
            {
                return null;
            }

            Player player = new Player();
            player.LoadByOwnerId(account.GetID());

            if (player.GetID() > 0)
            {
                if (player.GetLocation() == null)
                {
                    World world = new World();
                    world.GenerateRandom();
                    Location spawnPoint = world.GetRandomSpawnPoint();
                    player.SetLocation(spawnPoint);
                    player.SaveChanges();
                }
            }
            else
            {
                // player does not exist
                // First Create a new world for every new player
                World world = new World();
                world.GenerateRandom();

                Location spawnPoint = world.GetRandomSpawnPoint();

                // Build the new player
                player = new Player();
                player.SetLocation(spawnPoint);
                player.SetName(account.GetName());
                player.SetOwner(account);
                player.SaveChanges();
            }

            return player;
        }
    }
}
