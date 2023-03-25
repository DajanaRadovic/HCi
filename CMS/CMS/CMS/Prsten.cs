using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CMS
{
    [Serializable]
    public class Prsten
    {
        public int Id { get; set; }
        public string Metal { get; set; }
        public string Boja { get; set; }
        public DateTime Datum { get; set; }
        public string Oblik { get; set; }
        public string Cena { get; set; }
        public string Slika { get; set; }
        public string Brend { get; set; }
        public bool isChecked { get; set; }
        
       

        public Prsten(int id, string metal, string boja, DateTime datum, string oblik, string cena, string slika, string brend, bool isChecked)
        {
            this.Id = id;
            this.Metal = metal;
            this.Boja = boja;
            this.Datum = datum;
            this.Oblik = oblik;
            this.Cena = cena;
            this.Slika = slika;
            this.Brend = brend;
            this.isChecked = isChecked;
            
        }

        public Prsten()
        {
        }
    }
}
