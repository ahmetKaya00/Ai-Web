using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class BootcampKayit
    {
        [Key]
        public int KayitId {get;set;}
        public int OgrenciId {get;set;}
        public Ogrenci Ogrenci {get;set;}
        public int BootcampId {get;set;}
        public Bootcamp Bootcamp {get;set;}
        public DateTime KayitTarihi {get;set;}

    }
}