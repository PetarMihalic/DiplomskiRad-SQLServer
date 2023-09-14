using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SneakerShopSQLServer.Data;
using SneakerShopSQLServer.Models;

namespace SneakerShopSQLServer.Pages.Inventories
{
    public class CreateModel : PageModel
    {
        private readonly SneakerShopContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(SneakerShopContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            ViewData["Name"] = new SelectList(_context.Sneakers, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Inventory Inventory { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (Inventory == null)
            {
                return Page();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _context.Inventory.Add(Inventory);
            await _context.SaveChangesAsync();
            stopwatch.Stop();
            _logger.LogInformation("Inventory Create Time: {0}", stopwatch.ElapsedMilliseconds);
            return RedirectToPage("./Index");
        }
    }
}
