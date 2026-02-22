using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class Bootcamp
    {
        [Key]
        public int BootcampId {get;set;}
        public string? BootcampName {get;set;}
        public string? Image {get;set;}
        public ICollection<BootcampKayit> BootcampKayitlar {get;set;} = new List<BootcampKayit>();
    }
}