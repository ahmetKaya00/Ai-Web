using Basic.Data;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(Ogrenci model, IFormFile imageFile)
        {
            var allowedExtensions = new[]{".jpg",".png",".jpeg"};
            var extensions = Path.GetExtension(imageFile.FileName);
            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);

            if(imageFile != null)
            {
                if (!allowedExtensions.Contains(extensions))
                {
                    ModelState.AddModelError("","Lütfen geçerli bir format giriniz!.");
                }
            }
            if (ModelState.IsValid)
            {
                if(imageFile != null)
                { 
                    using(var stream =new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                }
                model.Image = randomFileName;
                _context.Ogrenciler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}