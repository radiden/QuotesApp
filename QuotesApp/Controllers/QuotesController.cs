using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuotesApp.Data;
using QuotesApp.Models;
using Microsoft.Extensions.Configuration;


namespace QuotesApp.Controllers
{
    public class QuotesController : Controller
    {
        private readonly QuoteContext _context;
        private readonly IConfiguration _config;

        public QuotesController(QuoteContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        // GET: Quotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quote.ToListAsync());
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Author,Content,Date,DeletionPasscode")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }

        // GET: Quotes/Edit/5
/*        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Author,Content,Date")] Quote quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
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
            return View(quote);
        }
*/
        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quote
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            var DeleteVM = new DeletionViewModel
            {
                Error = "",
                Id = id.Value,
                Passcode = "",
                Quote = quote
            };

            return View(DeleteVM);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string passcode)
        {
            var quote = await _context.Quote.FindAsync(id);

            var DeleteVM = new DeletionViewModel
            {
                Error = "",
                Id = id,
                Passcode = "",
                Quote = quote
            };

            if (!String.IsNullOrWhiteSpace(passcode))
            {
                if (passcode == quote.DeletionPasscode || passcode == _config.GetValue<string>("MasterPassword"))
                {
                    _context.Quote.Remove(quote);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                DeleteVM.Error = "Invalid passcode!";
                return View(DeleteVM);
            }
            DeleteVM.Error = "The passcode can't be empty!";
            return View(DeleteVM);
        }

        private bool QuoteExists(int id)
        {
            return _context.Quote.Any(e => e.Id == id);
        }
    }
}
