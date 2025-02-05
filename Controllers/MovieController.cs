
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
        public async Task<IActionResult> Create(Movie model){
            _contex.Movies0.Add(model);
            await _contex.SaveChangesAsync();
            return RedirectToAction("Index","Home");    
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
        public async Task<IActionResult>Update(int id, Movie model){
            if(id !=model.MovieId){
                return NotFound();
            }

            if(ModelState.IsValid){
                try
                {
                    _contex.Update(model);
                    await _contex.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_contex.Movies0.Any(m=>m.MovieId == model.MovieId)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction("Index");

            }
            return View(model);
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