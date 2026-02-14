using System.ComponentModel.DataAnnotations;

namespace Basic.Data
{
    public class Bootcamp
    {
        [Key]
        public int BootcampId {get;set;}
        public string? Baslik {get;set;}
        public ICollection<BootcampKayit> bootcampKayitlari {get;set;} = new List<BootcampKayit>();
    }
}