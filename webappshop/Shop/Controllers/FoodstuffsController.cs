using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Controllers
{
    public class FoodstuffsController : Controller
    {
        private readonly ShopContext _context;

        public FoodstuffsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Foodstuffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Foodstuffs.ToListAsync());
        }

        // GET: Foodstuffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodstuffs = await _context.Foodstuffs.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (foodstuffs == null)
            {
                return NotFound();
            }

            return View(foodstuffs);
        }

        // GET: Foodstuffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foodstuffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Weight,Cost")] Foodstuffs foodstuffs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodstuffs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(foodstuffs);
        }

        // GET: Foodstuffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodstuffs = await _context.Foodstuffs.FindAsync(id);
            if (foodstuffs == null)
            {
                return NotFound();
            }

            return View(foodstuffs);
        }

        // POST: Foodstuffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Weight,Cost")] Foodstuffs foodstuffs)
        {
            if (id != foodstuffs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodstuffs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodstuffsExists(foodstuffs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(foodstuffs);
        }

        // GET: Foodstuffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodstuffs = await _context.Foodstuffs.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (foodstuffs == null)
            {
                return NotFound();
            }

            return View(foodstuffs);
        }

        // POST: Foodstuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodstuffs = await _context.Foodstuffs.FindAsync(id);
            _context.Foodstuffs.Remove(foodstuffs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodstuffsExists(int id)
        {
            return _context.Foodstuffs.Any(e => e.Id == id);
        }
    }
}
