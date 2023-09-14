using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SneakerShopSQLServer.Data;
using SneakerShopSQLServer.Models;

namespace SneakerShopSQLServer.Pages.Users
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
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (User == null)
            {
                return Page();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            User.Password = PasswordHasher.Hash(User.Password);
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            stopwatch.Stop();
            _logger.LogInformation("User Create Time: {0}", stopwatch.ElapsedMilliseconds);
            if (HttpContext.Session.GetString("Email") == "admin@sneakershop.com")
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("./Login");
            }
        }
    }
}
