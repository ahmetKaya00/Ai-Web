using System.Threading.Tasks;
using Basic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Basic.Controllers
{
    [Authorize]
    public class BootcampKayitController : Controller
    {
        private readonly DataContext _context;
        public BootcampKayitController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var BootcampKayitlari = await _context.BootcampKayit.Include(x=>x.Bootcamp).Include(x=>x.Ogrenci).ToListAsync();
            return View(BootcampKayitlari);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(),"OgrenciId","AdSoyad");
            ViewBag.Bootcampler = new SelectList(await _context.Bootcamps.ToListAsync(),"BootcampId","BootcampName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(BootcampKayit model)
        {
            model.KayitTarihi = DateTime.Now;
            _context.BootcampKayit.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}