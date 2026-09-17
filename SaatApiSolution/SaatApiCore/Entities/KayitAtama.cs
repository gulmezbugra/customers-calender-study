using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaatApiCore.Entities
{
    public class KayitAtama
    {
        public int ID { get; set; }
        public string BoslukID { get; set; } = string.Empty;
        public int KullaniciID { get; set; }
        public string KullaniciIsmi { get; set; } = string.Empty;
        public string Door { get; set; } = string.Empty;
        
    }
}
