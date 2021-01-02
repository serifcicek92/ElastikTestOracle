using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.DataAccess
{
    public class ILCILACLAR : IBaseDto
    {
        public int ID { get; set; }
        public string ADI { get; set; }
        public string KODU { get; set; }
        public string BARKODU { get; set; }
    }
}
