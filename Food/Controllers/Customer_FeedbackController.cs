using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Food.Data;
using Food.Models;

namespace Food.Controllers
{
    public class Customer_FeedbackController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public Customer_FeedbackController(ApplicationDbcontext context)
        {
            _context = context;
        }

        // GET: Customer_Feedback
        public async Task<IActionResult> Index()
        {
            var applicationDbcontext = _context.CustomerFeedbacks.Include(c => c.Customer);
            return View(await applicationDbcontext.ToListAsync());
        }

        // GET: Customer_Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerFeedbacks == null)
            {
                return NotFound();
            }

            var customer_Feedback = await _context.CustomerFeedbacks
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (customer_Feedback == null)
            {
                return NotFound();
            }

            return View(customer_Feedback);
        }

        // GET: Customer_Feedback/Create
        public IActionResult Create()
        {
            var Name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Name))
            {
                return RedirectToAction("Login", "Acount");
            }


            return View();
        }

        // POST: Customer_Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedId,FirstName,LastName,Cleens,Service,FoodQuality,Suggetions,CustomerID")] Customer_Feedback customer_Feedback)
        {
            if (ModelState.IsValid)
            {
                customer_Feedback.CustomerID = HttpContext.Session.GetInt32("ID");
                _context.Add(customer_Feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
           
            return View(customer_Feedback);
        }

        // GET: Customer_Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerFeedbacks == null)
            {
                return NotFound();
            }

            var customer_Feedback = await _context.CustomerFeedbacks.FindAsync(id);
            if (customer_Feedback == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "ConfirmPassword", customer_Feedback.CustomerID);
            return View(customer_Feedback);
        }

        // POST: Customer_Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedId,FirstName,LastName,Cleens,Service,FoodQuality,Suggetions,CustomerID")] Customer_Feedback customer_Feedback)
        {
            if (id != customer_Feedback.FeedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer_Feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Customer_FeedbackExists(customer_Feedback.FeedId))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerId", "ConfirmPassword", customer_Feedback.CustomerID);
            return View(customer_Feedback);
        }

        // GET: Customer_Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerFeedbacks == null)
            {
                return NotFound();
            }

            var customer_Feedback = await _context.CustomerFeedbacks
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (customer_Feedback == null)
            {
                return NotFound();
            }

            return View(customer_Feedback);
        }

        // POST: Customer_Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerFeedbacks == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.CustomerFeedbacks'  is null.");
            }
            var customer_Feedback = await _context.CustomerFeedbacks.FindAsync(id);
            if (customer_Feedback != null)
            {
                _context.CustomerFeedbacks.Remove(customer_Feedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Customer_FeedbackExists(int id)
        {
          return (_context.CustomerFeedbacks?.Any(e => e.FeedId == id)).GetValueOrDefault();
        }
    }
}
