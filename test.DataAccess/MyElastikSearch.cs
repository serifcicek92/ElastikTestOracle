using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace test.DataAccess
{

    public class MyElastikSearch<T>
        where T : class, IBaseDto, new()
    {
        private string indexName, aliasName, url;
        public MyElastikSearch(string indexName, string aliasName, string url)
        {
            this.indexName = indexName;
            this.aliasName = aliasName;
            this.url = url;
        }

        public ElasticClient GetClient() {
            var connectionSettings = new ConnectionSettings(new Uri(url))
            .DefaultMappingFor<T>(i => i
                .IndexName(indexName)
                .IdProperty(p => p.ID)
            )
            .EnableDebugMode()
            .PrettyJson()
            .RequestTimeout(TimeSpan.FromMinutes(2));

            var client = new ElasticClient(connectionSettings);
        return client;
    }


        [Obsolete]
        public void CreateNewIndex()
        {

            #region yöntem1
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
              .Mappings(ms => ms
                              .Map<T>(m => m.AutoMap())
                       )
               .Aliases(a => a.Alias(aliasName));
            var node = new Uri(url);
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);
            var response = client.Indices.CreateAsync(createIndexDescriptor);
            try
            {
                Console.WriteLine(response.Result.ServerError.Error.Reason);
                Console.ReadLine();
            }
            catch (Exception)
            {

                //throw;
            }
            #endregion
            #region yöntem2

            //node = new Uri("http://localhost:9200/");
            //settings = new ConnectionSettings(node);

            //settings.DefaultIndex("defaultindex")
            //    .MapDefaultTypeIndices(m => m.Add(typeof(Post), "my_blog"));

            //client = new ElasticClient(settings);
            #endregion
        }

        [Obsolete]
        public async Task DeleteIndexAsync()
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName.ToLower()).Mappings(ms => ms.Map<T>(m => m.AutoMap()));
            var node = new Uri(url);
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);
            //DELETE  İNDEX
            var a = client.Indices.DeleteAsync(indexName.ToLower());

            try
            {
                Console.WriteLine(a.Result.ServerError.Error.Reason);
                Console.WriteLine("has any error");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("İndex is deleted");
                Console.ReadLine();
                //throw;
            }
        }

        #region replica ve shard ile index oluşturma

        //public static void CreateNewIndex()
        //{
        //    var indexSettings = new IndexSettings();
        //    indexSettings.NumberOfReplicas = 1;
        //    indexSettings.NumberOfShards = 3;
        //    var createIndexDescriptor = new CreateIndexDescriptor("blog_history")
        //  .Mappings(ms => ms
        //                  .Map<Post>(m => m.AutoMap())
        //           )
        //   .InitializeUsing(new IndexState() { Settings = indexSettings })
        //   .Aliases(a => a.Alias("bora_blog"));
        //    node = new Uri("http://localhost:9200/");
        //    settings = new ConnectionSettings(node);

        //    client = new ElasticClient(settings);
        //    var response = client.CreateIndex(createIndexDescriptor);

        //}
        #endregion


        [Obsolete]
        public void PostData(List<T> eczacilar)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName.ToLower())
             .Mappings(ms => ms
                          .Map<ILCILACLAR>(m => m.AutoMap())
                   )
           .Aliases(a => a.Alias(aliasName.ToLower()));
            var node = new Uri(url);
            var settings = new ConnectionSettings(node);

            var client = new ElasticClient(settings);

            //BULK İNSERT DATA
            var bulkIndexer = new BulkDescriptor();
            foreach (var document in eczacilar)
            {
                bulkIndexer.Index<T>(i => i.Document(document).Id(document.ID).Index(indexName.ToLower()));
            }

            client.Bulk(bulkIndexer);

            Console.WriteLine("inserted Bulk Data..");
            Console.ReadLine();
        }

        [Obsolete]
        public IEnumerable<T> Search(string kelime)
        {

            var client = GetClient();
            var response = client.Search<T>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(kelime)
                        .Fields(f => f
                            .Field("aDI")
                            //.Field("bARKODU")
                            //.Field(p => p.ADI)
                            //.Field(p => p.BARKODU)
                         )
                    )
                )
            );


            var protects = response.Documents;

            return protects;
        }
        public void Serach2()
        {
//            searchResponse = _client.Search<Project>(s => s
//    .Query(q => q
//        .Match(m => m
//            .Field(f => f.LeadDeveloper.FirstName)
//            .Query("Russ")
//        ) && q
//        .Match(m => m
//            .Field(f => f.LeadDeveloper.LastName)
//            .Query("Cam")
//        ) && +q
//        .DateRange(r => r
//            .Field(f => f.StartedOn)
//            .GreaterThanOrEquals(new DateTime(2017, 01, 01))
//            .LessThan(new DateTime(2018, 01, 01))
//        )
//    )
//);
        }

        public void search3()
        {
//            var searchResponse = _client.Search<Project>(s => s
//    .Query(q => q
//        .Match(m => m
//            .Field(f => f.LeadDeveloper.FirstName)
//            .Query("Russ")
//        )
//    )
//);
        }

        public string GetProperty<T>(Expression<Func<T,Object>> property)
        {

            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is System.Linq.Expressions.UnaryExpression)
            {
                System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
                memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
            }
            return ((System.Reflection.PropertyInfo)memberExpression.Member).Name;
        }

        enum SearchTypes
        {
            Similar,
            Match,
            DefaultField,
            Fields

        }
    }
}
