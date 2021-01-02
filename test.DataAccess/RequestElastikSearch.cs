using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Newtonsoft.Json;

namespace test.DataAccess
{
    public class RequestElastikSearch
    {
        //public string url { get; set; }
        //public RequestElastikSearch(string URL)
        //{
        //    this.url = URL;
        //}

        //public string Get()
        //{
        //    string response = "";
        //    try
        //    {
        //        using (WebClient client = new WebClient())
        //        {
        //            response = client.DownloadString("http://localhost:9200/");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return response;
        //}


        //public void Post(List<Dto_MusEczacilar> eczacilar)
        //{
        //    //ElasticClient elasticClient = new ElasticClient();
        //    Dto_MusEczacilar eczaci = new Dto_MusEczacilar();

        //    var defaultIndex = "eczaci";

        //    ConnectionSettings connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("eczaci").DefaultMappingFor<Dto_MusEczacilar>(m => m.IndexName("eczaci"));

        //    var client = new ElasticClient(connectionSettings);



        //    if (client.Indices.Exists(defaultIndex).Exists)
        //    {
        //        client.Indices.Delete(defaultIndex);
        //    }
        //    if (!client.Indices.Exists("location_alias").Exists)
        //    {
        //        client.Indices.Create(defaultIndex, c => c
        //        .Mappings(m => m
        //            .Map<Dto_MusEczacilar>(mm => mm.AutoMap())
        //        ).Aliases(a => a.Alias("location_alias"))
        //        );
        //    }

        //    //classic insert data
        //    for (int i = 0; i < eczacilar.Count; i++)
        //    {
        //        var item = eczacilar[i];
        //        client.Index<Dto_MusEczacilar>(item, idx => idx.Index("eczaci").Id(item.ID));
        //    }


        //    //BULK İNSERT DATA
        //    var bulkIndexer = new BulkDescriptor();
        //    foreach (var document in eczacilar)
        //    {
        //        bulkIndexer.Index<Dto_MusEczacilar>(i => i.Document(document).Id(document.ID).Index("eczaci"));
        //    }

        //    client.Bulk(bulkIndexer);

        //}
        ////http://www.borakasmer.com/elasticsearch-nedir/
        ////https://www.elastic.co/blog/indexing-documents-with-the-nest-elasticsearch-net-client
        //public void Search1()
        //{
        //    ConnectionSettings connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("eczaci").DefaultMappingFor<Dto_MusEczacilar>(m => m.IndexName("eczaci"));

        //    var client = new ElasticClient(connectionSettings);
        //    var geoResult = client.Search<Dto_MusEczacilar>(s => s.From(0).Size(10000).Query(query => query.Bool(b => b.Filter(filter => filter
        //       .GeoDistance(geo => geo
        //           .Field(f => f.ADI)
        //           .Distance("Ecza").Location(41, 28)
        //           .DistanceType(GeoDistanceType.Plane)
        //       ))
        //       )));
            
        //    foreach (var customer in geoResult.Documents)
        //    {
        //        Console.WriteLine(customer.ADI + ":" + customer.ADRES);
        //    }
        //}

        //public void Search2()
        //{
        //    ConnectionSettings connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("eczaci").DefaultMappingFor<Dto_MusEczacilar>(m => m.IndexName("eczaci"));

        //    var client = new ElasticClient(connectionSettings);


        //    var query = BuildQuery("Eczane");

        //    var result = client
        //        .Search(query)
        //        .Documents
        //        .Select(d => d)
        //        .Distinct()
        //        .ToList();
        //}

        //private static SearchDescriptor<Dto_MusEczacilar> BuildQuery(string searchPhrase)
        //{
        //    var querifiedKeywords = string.Join("AND", searchPhrase.Split(' '));

        //    var filters = new BaseFilter[1];

        //    filters[0] = Filter<Dto_MusEczacilar>.Bool(b => b.Should(m => m.Query(q =>
        //        q.FuzzyLikeThis(flt =>
        //            flt.OnFields(new[] {
        //                name
        //            }).LikeText(querifiedKeywords)
        //            .PrefixLength(2)
        //            .MaxQueryTerms(1)
        //            .Boost(2))
        //        )));

        //    var searchDescriptor = new SearchDescriptor<TestItem>()
        //        .Filter(f => f.Bool(b => b.Must(filters)))
        //        .Index(elastictesting)
        //        .Type(TestItem)
        //        .Size(500);

        //    var jsons = JsonConvert.SerializeObject(searchDescriptor, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //    return searchDescriptor;
        //}

    }
}