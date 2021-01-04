using Nest;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using test.DataAccess;
namespace test.test
{
    class Program
    {
        private static OracleConnection connection;
        private static OracleCommand command;
        private static OracleDataReader dataReader;

        //http://www.borakasmer.com/elasticsearch-nedir/
        [Obsolete]
        static async Task Main(string[] args)
        {
            //MyElastikSearch<ILCILACLAR> myElastikSearch = new MyElastikSearch<ILCILACLAR>();

            var connectionSettings = new ConnectionSettings(new Uri("http://127.0.0.1:9200/"))
            .DefaultMappingFor<ILCILACLAR>(i => i
                .IndexName("ind_ilcilaclar")
                .IdProperty(p => p.ID)
            )
            .EnableDebugMode()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(connectionSettings);
            //var client = new ElasticClient(settings);
            var response = client.Search<ILCILACLAR>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.ADI)
                        .Query("ALLI ECZANE STANDI")
                    )
                )
            );

            var projetcs = response.Documents;
            String adi = projetcs.First<ILCILACLAR>().ADI.ToString();



            var response2 = client.Search<ILCILACLAR>(s => s
            .Query(q=>q
                .QueryString(qs=>qs
                    .Query("Melik~")
                    .DefaultField("aDI")
                 )
             ));
            var projects2 = response2.Documents;

            var response3 = client.Search<ILCILACLAR>(s=>s
                .Query(q=>q
                    .QueryString(qs => qs
                        .Query("Melik~")
                        .Fields(f => f
                            .Field(p=>p.ADI)
                            .Field(p=>p.BARKODU)
                         )
                    )
                )
            );


            var protects3 = response3.Documents;

            
            

            String properties = new MyElastikSearch<ILCILACLAR>("ilcilaclar", "ilcilaclar_alias", "http://127.0.0.1:9200/").GetProperty<ILCILACLAR>(u=>u.ADI);
            //     var createIndexDescriptor = new CreateIndexDescriptor("ilcilaclar")
            //.Mappings(ms => ms
            //                .Map<ILCILACLAR>(m => m.AutoMap())
            //         )
            // .Aliases(a => a.Alias("ilcilaclar_alias"));
            //     var node = new Uri("http://127.0.0.1:9200/");
            //     var settings = new ConnectionSettings(node);

            //     //var client = new ElasticClient(settings);
            //     var response = client.Search<ILCILACLAR>(p => p
            //       .From(0)
            //       .Size(10)
            //       .Query(q =>
            //       q.Term(f => f.ID, 2)
            //       || q.MatchPhrasePrefix(mq => mq.Field(f => f.ADI).Query("Melik~"))
            //     )
            //     );

            //     var projects = response.Documents;

            #region Delete index From MyElastikSeach.cs
            MyElastikSearch<ILCILACLAR> ilcalarIndDel = new MyElastikSearch<ILCILACLAR>("ilcilaclar", "ilcilaclar_alias", "http://127.0.0.1:9200/");
            ilcalarIndDel.DeleteIndexAsync();

            #endregion


            #region CreateApiKeyDescriptor INDEX FROM MyElastikSearch.cs
            MyElastikSearch<ILCILACLAR> ilcalarInd = new MyElastikSearch<ILCILACLAR>("ind_ilcilaclar", "nervus", "http://127.0.0.1:9200/");
            ilcalarInd.CreateNewIndex();
            Console.WriteLine("ind_ilcilaclar index created");
            Console.ReadLine();
            #endregion

            #region Get Data then oracle
            Connection baglanti = new Connection();
            Program.connection = baglanti.conn();

            String sql = "SELECT * FROM ILCILACLAR";
            command = new OracleCommand(sql, Program.connection);

            dataReader = command.ExecuteReader();

            List<ILCILACLAR> ilaclar = new List<ILCILACLAR>();


            while (dataReader.Read())
            {
                ilaclar.Add(new ILCILACLAR
                {
                    ID = Convert.ToInt32(dataReader["ID"]),
                    ADI = dataReader["ADI"].ToString(),
                    BARKODU = dataReader["BARKODU"].ToString(),
                    KODU = dataReader["KODU"].ToString()
                });
            }
            #endregion

            #region index with bulk data

            MyElastikSearch<ILCILACLAR> myElastikSearch = new MyElastikSearch<ILCILACLAR>("ind_ilcilaclar", "nervus", "http://127.0.0.1:9200/");
            myElastikSearch.PostData(ilaclar);

            #endregion

            //string url = "http://127.0.0.1:9200/";
            //var r = new Requests(url);

            //Console.WriteLine(r.get());

            //Console.ReadLine();

            //string payload = "{\r\n    \"aDI\":\"TEST ECZANESI\",\r\n    \"uNVANI\":\"TEST ECZANESI\"\r\n}";

            //r.url = "http://127.0.0.1:9200/eczaci/_doc/_search?pretty";
            //string resp1 = r.post(payload);


            //String aa = myElastikSearch.Search("blog_history", "bora_blog", "Eczane");
            //String bb = aa;


            //http://127.0.0.1:9200/_cat/indices
            //http://localhost:9200/_cat/indices/eczaci/_serach
            //http://localhost:5601/        kibana
            //https://www.youtube.com/watch?v=G-XAwj8BbG0
        }

        [Obsolete]
        public static async Task CreateNewIndexAsync(string indexName, string aliasName)
        {
            //var node = new Uri("http://localhost:9200/");
            //string AuthUserName = "elastic";
            //string AuthPassword = "kibana";
            //var settingsConf = new ConnectionSettings(node);
            //var client = new ElasticClient(settingsConf);

            //var settings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 2 };
            //var indexConfig = new IndexState
            //{
            //    Settings = settings
            //};
            var createIndexDescriptor = new CreateIndexDescriptor(indexName.ToLower()).Mappings(ms => ms.Map<ILCILACLAR>(m => m.AutoMap())).Aliases(a => a.Alias(aliasName.ToLower()));
            var node = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);

            if (!client.Indices.Exists(indexName.ToLower()).Exists)
            {

                var b = client.Indices.CreateAsync(indexName.ToLower(),
                    index => index.Map<Dto_MusEczacilar>(x => x.AutoMap()));
                try
                {
                    Console.WriteLine(b.Result.ServerError.Error.Reason);
                    Console.ReadLine();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        [Obsolete]
        public static async Task DeleteIndexAsync(string indexName, string aliasName)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName.ToLower()).Mappings(ms => ms.Map<ILCILACLAR>(m => m.AutoMap())).Aliases(b => b.Alias(aliasName.ToLower()));
            var node = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);
            //DELETE  İNDEX
            var a = await client.Indices.DeleteAsync(indexName.ToLower());
        }

        [Obsolete]
        public static String Search(string indexName, string aliasName, string kelime)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
         .Mappings(ms => ms
                         .Map<Dto_MusEczacilar>(m => m.AutoMap())
                  )
          .Aliases(a => a.Alias(aliasName));
            var node = new Uri("http://localhost:9200/");
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);
            var response = client.Search<Dto_MusEczacilar>(p => p
              .From(0)
              .Size(10)
              .Query(q =>
              q.Term(f => f.ID, 2)
              || q.MatchPhrasePrefix(mq => mq.Field(f => f.ADI).Query("Eczane"))
            )
            );

            return response.ToString();
        }

        public static void BulkInsert()
        {

        }

    }

    public class Requests
    {

        public string url { get; set; }
        public Requests(string URL)
        {
            this.url = URL;
        }

        public string get()
        {
            string response = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(this.url);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return response;
        }

        public string post(string payload)
        {
            string response = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0");

                    response = client.UploadString(this.url, payload);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return response;
        }

    }

    public class TReturn<T> where T : class
    {
        public T TData { get; set; }
    }

}
