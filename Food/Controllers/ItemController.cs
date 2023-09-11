using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Food.Data;
using Food.Models;
using Microsoft.Extensions.Hosting;

namespace Food.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbcontext _context;
        private readonly IWebHostEnvironment _environment;

        public ItemController(ApplicationDbcontext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))   
            {
                return RedirectToAction("Login", "Acount");
            }
            else
            {
                var test = HttpContext.Session.GetString("statous");
                if (test == "Admin" ||test=="Oner")
                {
                    return _context.Items != null ?
                        View(await _context.Items.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbcontext.Items'  is null.");
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }

           
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))
            {
                return RedirectToAction("Login", "Acount");
            }
            else
            {
                var test = HttpContext.Session.GetString("statous");
                if (test == "Admin" || test == "Oner")
                {
                    if (id == null || _context.Items == null)
                    {
                        return NotFound();
                    }

                    var items = await _context.Items
                        .FirstOrDefaultAsync(m => m.ItemId == id);
                    if (items == null)
                    {
                        return NotFound();
                    }

                    return View(items);
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }
           
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))
            {
                return RedirectToAction("Login", "Acount");
            }
            else
            {
                var test = HttpContext.Session.GetString("statous");
                if (test == "Admin" || test == "Oner")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }

            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,Description,Item_Pic,ItemPrice")] Items items, IFormFile img_file)
        {
            string path = Path.Combine(_environment.WebRootPath, "imgg"); // wwwroot/Img/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (img_file != null)
            {
                path = Path.Combine(path, img_file.FileName); // for example : /Img/Photoname.png
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await img_file.CopyToAsync(stream);
                    //ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                    items.Item_Pic = img_file.FileName;
                }


            }
            else
            {
                items.Item_Pic = "default.jpeg"; // to save the default image path in database.
            }
            try
            {
                _context.Add(items);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) { ViewBag.exc = ex.Message; }


            return View();


        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))
            {
                return RedirectToAction("Login", "Acount");
            }
            else
            {
                var test = HttpContext.Session.GetString("statous");
                if (test == "Admin" || test == "Oner")
                {
                    if (id == null || _context.Items == null)
                    {
                        return NotFound();
                    }

                    var items = await _context.Items.FindAsync(id);
                    if (items == null)
                    {
                        return NotFound();
                    }
                    return View(items);
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }
          
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,Description,Item_Pic,ItemPrice")] Items items)
        {
            if (id != items.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.ItemId))
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
            return View(items);
        }

        private bool ItemsExists(int itemId)
        {
            throw new NotImplementedException();
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))
            {
                return RedirectToAction("Login", "Acount");
            }
            else
            {
                var test = HttpContext.Session.GetString("statous");
                if (test == "Admin" || test == "Oner")
                {
                    if (id == null || _context.Items == null)
                    {
                        return NotFound();
                    }

                    var items = await _context.Items
                        .FirstOrDefaultAsync(m => m.ItemId == id);
                    if (items == null)
                    {
                        return NotFound();
                    }

                    return View(items);
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }

           
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Items'  is null.");
            }
            var items = await _context.Items.FindAsync(id);
            if (items != null)
            {
                _context.Items.Remove(items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string Title)
        {

            var search = _context.Items.Where(x => x.ItemName.Contains(Title)).ToList();

            return View(search);


        }
    }
}
