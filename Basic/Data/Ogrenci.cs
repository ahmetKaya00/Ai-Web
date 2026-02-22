using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId {get;set;}
        public string? OgrenciAd {get;set;}
        public string? OgrenciSoyad {get;set;}
        public string AdSoyad
        {
            get{return this.OgrenciAd + this.OgrenciSoyad;}
        }
        public string? Image {get;set;}
        public string? Telefon {get;set;}
        public string? Eposta {get;set;}
        public ICollection<BootcampKayit> BootcampKayitlar {get;set;} = new List<BootcampKayit>();

    }
}