using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace test.DataAccess
{

    public class MyElastikSearch<T>
        where T : class, IBaseDto, new()
    {
        private string indexName, aliasName;
        public MyElastikSearch(string indexName, string aliasName)
        {
            this.indexName = indexName;
            this.aliasName = aliasName;
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
            var node = new Uri("http://localhost:9200/");
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
        public String Search(string indexName, string aliasName, string kelime)
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
         .Mappings(ms => ms
                         .Map<Dto_MusEczacilar>(m => m.AutoMap())
                  )
          .Aliases(a => a.Alias(aliasName));
            var node = new Uri(url);
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

    }
}
