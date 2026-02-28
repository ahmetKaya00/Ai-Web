using Basic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basic.Controllers
{
    [Authorize]
    public class BootcampController : Controller
    {
        private readonly DataContext _context;
        public BootcampController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var Bootcampler = _context.Bootcamps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                ViewBag.SearchString = searchString;

                Bootcampler = Bootcampler
                    .Where(o => o.BootcampName!.Contains(searchString));
            }

            return View(await Bootcampler.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Bootcamp model, IFormFile imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
            var extensions = Path.GetExtension(imageFile.FileName);
            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

            if (imageFile != null)
            {
                if (!allowedExtensions.Contains(extensions))
                {
                    ModelState.AddModelError("", "Lütfen geçerli bir format giriniz!.");
                }
            }
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }
                model.Image = randomFileName;
                _context.Bootcamps.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Bootcamp");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.Bootcamps.Include(o=>o.BootcampKayitlar).ThenInclude(b=>b.Ogrenci).FirstOrDefaultAsync(o=>o.BootcampId == id);
            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogr = await _context.Bootcamps.FindAsync(id);
            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Bootcamp model, IFormFile? imageFile)
        {
            if (id != model.BootcampId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                    var extensions = Path.GetExtension(imageFile.FileName);
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    if (path != null)
                    {
                        var fullPath = Path.Combine(path, randomFileName);
                        if (fullPath != null)
                        {
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }
                            model.Image = randomFileName;
                        }
                    }
                }
                else
                {
                    var existingImage = _context.Bootcamps.Where(o=>o.BootcampId == id).Select(o=>o.Image).FirstOrDefault();
                    model.Image = existingImage;
                }
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var ogr = await _context.Bootcamps.FindAsync(id);
            if(ogr == null)
            {
                return NotFound(); 
            }
            _context.Bootcamps.Remove(ogr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}