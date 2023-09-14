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
    public class DetailsModel : PageModel
    {
        private readonly SneakerShopContext _context;
        private readonly ILogger<DetailsModel> _logger;
        public DetailsModel(SneakerShopContext context, ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var user = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            stopwatch.Stop();
            _logger.LogInformation("User Details Time: {0}", stopwatch.ElapsedMilliseconds);
            return Page();
        }

        public void OnPost()
        {
            try
            {
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Response.Redirect("/Index");
        }
    }
}
