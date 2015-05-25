using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SolService
{
    /// <summary>
    /// Summary description for SolService
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SolService : System.Web.Services.WebService
    {
        GameManager gameManager;
        public SolService()
        {
            this.gameManager = new GameManager();
        }

        [WebMethod]
        public Scene GetScene(String playertoken)
        {
            return this.gameManager.GetPlayerScene(Guid.Parse(playertoken));
        }

        [WebMethod]
        public Guid GetPlayerToken(String email, String password)
        {
            return this.gameManager.GetPlayerToken(email,password);
        }

        [WebMethod]
        public Boolean CreateAccount(String name, String email, String password)
        {
            return this.gameManager.CreateAccount(name, email, password);
        }
    }
}
