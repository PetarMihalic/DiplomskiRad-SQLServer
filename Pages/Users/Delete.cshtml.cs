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

namespace SneakerShopSQLServer.Pages.Users
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
        public User User { get; set; } = default!;
        public List<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var user = await _context.Users.FindAsync(id);
            Orders = await _context.Orders.Where(o => o.UserID == id).ToListAsync();

            foreach (var o in Orders)
            {
                _context.Orders.Remove(o);
            }

            await _context.SaveChangesAsync();

            if (user != null)
            {
                User = user;
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            }
            stopwatch.Stop();
            _logger.LogInformation("User Delete Time: {0}", stopwatch.ElapsedMilliseconds);
            if (HttpContext.Session.GetString("Email") == "admin@sneakershop.com") return RedirectToPage("./Index");
            else {
                try
                {
                    HttpContext.Session.Clear();
                    return RedirectToPage("../Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Page();
                }
            }
        }
    }
}
