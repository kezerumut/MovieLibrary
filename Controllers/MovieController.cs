
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDBApp.Data;

namespace MovieDBApp.Controllers{
 

 public class MovieController : Controller{
    private readonly DataContext  _contex;

    public MovieController(DataContext context){
        _contex = context;
    }

    public async Task<IActionResult> Index(){
        return View(await _contex.Movies0.ToListAsync());
    }
        [HttpGet]
         public IActionResult Create(){
            return View();
        }
       [HttpPost]
[HttpPost]
public async Task<IActionResult> Create(Movie model, IFormFile imageFile)
{
    if (ModelState.IsValid)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            // Resim türü kontrolü
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("imageFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
                return View(model);
            }

            // Resim boyutu kontrolü (örneğin, 5 MB)
            if (imageFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("imageFile", "The image file size must be less than 5 MB.");
                return View(model);
            }

            // Resim dosyasını kaydetme veya mevcut dosyayı kullanma
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!System.IO.File.Exists(filePath)) // Dosya mevcut değilse kaydet
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            // Resim yolunu modelde sakla
            model.ImagePath = "/images/" + uniqueFileName;
        }

        _contex.Movies0.Add(model);
        await _contex.SaveChangesAsync();
        return RedirectToAction("Index", "Movie");
    }

    return View(model);
}

        public async Task<IActionResult> Update(int? id){
            if(id == null){

                return NotFound();
            }
            
            var mv = await _contex.Movies0.FindAsync(id);

            if(mv == null){
                return NotFound();
            }
            return View(mv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        // güvenlik için alınan bir tedbir aynı anda farklı işlemleri önler
       public async Task<IActionResult> Update(int id, Movie model, IFormFile imageFile)
{
    if (id != model.MovieId)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        // ModelState hatalarını loglama
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
            Console.WriteLine(error.ErrorMessage); // Konsola yazdırabilirsiniz
        }
        return View(model);
    }

    if (imageFile != null && imageFile.Length > 0)
    {
        // Resim türü kontrolü
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            ModelState.AddModelError("imageFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
            return View(model);
        }

        // Resim boyutu kontrolü (örneğin, 5 MB)
        if (imageFile.Length > 5 * 1024 * 1024)
        {
            ModelState.AddModelError("imageFile", "The image file size must be less than 5 MB.");
            return View(model);
        }

        // Resim dosyasını kaydetme veya mevcut dosyayı kullanma
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Path.GetFileName(imageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        if (!System.IO.File.Exists(filePath)) // Dosya mevcut değilse kaydet
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Eski resmi silme
            if (!string.IsNullOrEmpty(model.ImagePath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            // Yeni resim yolunu modelde sakla
            model.ImagePath = "/images/" + uniqueFileName;
        }
        else
        {
            // Resim mevcutsa mevcut dosya yolunu kullan
            model.ImagePath = "/images/" + uniqueFileName;
        }
    }

    try
    {
        _contex.Update(model);
        await _contex.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!_contex.Movies0.Any(m => m.MovieId == model.MovieId))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    return RedirectToAction("Index");
}

        [HttpGet]
        public async Task<IActionResult>Delete(int? id){
            if(id == null){
                return NotFound();
            }

            var movie = await _contex.Movies0.FindAsync(id);

            if(movie == null){
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromForm]int id){
            var ogrenci = await _contex.Movies0.FindAsync(id);
            if(ogrenci == null){
                return NotFound();
            }
            _contex.Movies0.Remove(ogrenci);
            await _contex.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}