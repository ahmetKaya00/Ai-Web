using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId {get;set;}
        public string? OgrenciAd {get;set;}
        public string? OgrenciSoyad {get;set;}
        public string? Image {get;set;}
        public string? Telefon {get;set;}
        public string? Eposta {get;set;}

    }
}