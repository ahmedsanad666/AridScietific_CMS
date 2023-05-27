using WebOS.Data;
using WebOS.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public interface IBulkDBUpdater
    {
        Task BulkDBUpdateAsync(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager);
    }
}
