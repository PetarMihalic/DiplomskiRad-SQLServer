using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SneakerShopSQLServer.Data;
using SneakerShopSQLServer.Models;

namespace SneakerShopSQLServer.Pages.Sneakers
{
    public class DeleteModel : PageModel
    {
        private readonly SneakerShopContext _context;
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(SneakerShopContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Sneaker Sneaker { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sneakers == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneakers.FirstOrDefaultAsync(m => m.ID == id);

            if (sneaker == null)
            {
                return NotFound();
            }
            else
            {
                Sneaker = sneaker;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sneakers == null)
            {
                return NotFound();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var sneaker = await _context.Sneakers.FindAsync(id);

            if (sneaker != null)
            {
                Sneaker = sneaker;
                _context.Sneakers.Remove(Sneaker);
                await _context.SaveChangesAsync();
            }
            stopwatch.Stop();
            _logger.LogInformation("Sneaker Delete Time: {0}", stopwatch.ElapsedMilliseconds);
            return RedirectToPage("/Index");
        }
    }
}
