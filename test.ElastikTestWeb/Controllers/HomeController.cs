using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test.DataAccess;

namespace test.ElastikTestWeb.Controllers
{
    public class HomeController : Controller
    {
        private OracleConnection connection;
        private OracleCommand oracleCommand;
        private OracleDataReader oracleDataReader;
        public ActionResult Index()
        {

            //MyElastikSearch myElastikSearch = new MyElastikSearch();
            //myElastikSearch.CreateNewIndex("blog_history", "bora_blog");

            //Connection baglanti = new Connection();
            //this.connection = baglanti.conn();

            //String sql = "SELECT * FROM MUS_ECZANELER";
            //OracleCommand command = new OracleCommand(sql, this.connection);

            //OracleDataReader dataReader = command.ExecuteReader();

            //List<Dto_MusEczacilar> eczacilar = new List<Dto_MusEczacilar>();


            //while (dataReader.Read())
            //{
            //    eczacilar.Add(new Dto_MusEczacilar
            //    {
            //        ID = Convert.ToInt32(dataReader["ID"]),
            //        ADI = dataReader["ADI"].ToString(),
            //        UNVANI = dataReader["UNVANI"].ToString(),
            //        MUS_ECZACIID = Convert.ToInt32(dataReader["MUS_ECZACIID"].ToString()),
            //        OZELKOD1 = dataReader["OZELKOD1"].ToString(),
            //        OZELKOD2 = dataReader["OZELKOD2"].ToString(),
            //        OZELKOD3 = dataReader["OZELKOD3"].ToString(),
            //        OZELKOD4 = dataReader["OZELKOD4"].ToString(),
            //        VERGIDAIRESI = dataReader["VERGIDAIRESI"].ToString(),
            //        VERGINO = dataReader["VERGINO"].ToString(),
            //        ADRES = dataReader["ADRES"].ToString(),
            //        ILID = Convert.ToInt32(dataReader["ILID"].ToString().Length == 0 ? null : dataReader["ILID"].ToString()),
            //        ILCEID = Convert.ToInt32(dataReader["ILCEID"].ToString().Length == 0 ? null : dataReader["ILCEID"].ToString()),
            //        POSTAKODU = dataReader["POSTAKODU"].ToString(),
            //        ULKEID = Convert.ToInt32(dataReader["ULKEID"].ToString().Length == 0 ? null : dataReader["ULKEID"].ToString()),
            //        SEMT = dataReader["SEMT"].ToString(),
            //        MUS_OZELBOLGEID = Convert.ToInt32(dataReader["MUS_OZELBOLGEID"].ToString().Length == 0 ? null : dataReader["MUS_OZELBOLGEID"].ToString()),
            //        ACILISTARIHI = DateTime.Parse(dataReader["ACILISTARIHI"].ToString()),
            //        M2 = Convert.ToInt32(dataReader["M2"].ToString().Length == 0 ? null : dataReader["M2"].ToString()),
            //        KONUM = dataReader["KONUM"].ToString(),
            //        SATISAKAPALI = Convert.ToInt32(dataReader["SATISAKAPALI"].ToString().Length == 0 ? null : dataReader["SATISAKAPALI"].ToString()),
            //        EKLEYENID = Convert.ToInt32(dataReader["EKLEYENID"].ToString().Length == 0 ? null : dataReader["EKLEYENID"].ToString()),
            //        EKLEMEZAMANI = DateTime.Parse(dataReader["EKLEMEZAMANI"].ToString()),
            //        GUNCELLEYENID = Convert.ToInt32(dataReader["GUNCELLEYENID"].ToString().Length == 0 ? null : dataReader["GUNCELLEYENID"].ToString()),
            //        GUNCELLEMEZAMANI = DateTime.Parse(dataReader["GUNCELLEMEZAMANI"].ToString())

            //    });
            //}

           


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