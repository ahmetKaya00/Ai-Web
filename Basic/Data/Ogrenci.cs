using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId {get;set;}
        public string? OgrenciAd {get;set;}
        public string? OgrenciSoyad {get;set;}
        public string? EPosta {get;set;}
        public string? Telefon {get;set;}
        public ICollection<BootcampKayit> bootcampKayitlari {get;set;} = new List<BootcampKayit>();

    }
}