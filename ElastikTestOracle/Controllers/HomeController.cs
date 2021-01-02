using System;
using test.DataAccess;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElastikTestOracle;


namespace test.ElastikTestOracle.Controllers
{
    public class HomeController : Controller
    {
        private OracleConnection connection;
        private OracleCommand oracleCommand;
        private OracleDataReader oracleDataReader;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}