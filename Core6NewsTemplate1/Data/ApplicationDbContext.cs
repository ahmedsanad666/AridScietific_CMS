using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebOS.Models;

namespace WebOS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<WebOS.Models.Country> Country { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WebOS.Models.City> City { get; set; }
        public DbSet<WebOS.Models.BlogPost> BlogPost { get; set; }
        public DbSet<WebOS.Models.BlogTaxonomy> BlogTaxonomy { get; set; }
        public DbSet<WebOS.Models.BlogPostComment> BlogPostComment { get; set; }
        public DbSet<WebOS.Models.Statement> Statement { get; set; }
        public DbSet<WebOS.Models.Page> Page { get; set; }
        public DbSet<WebOS.Models.Ticket> Ticket { get; set; }
        public DbSet<WebOS.Models.TicketReply> TicketReply { get; set; }
        public DbSet<WebOS.Models.TicketCategory> TicketCategory { get; set; }
        public DbSet<WebOS.Models.NavMenu> NavMenus { get; set; }
        public DbSet<WebOS.Models.NavMenuItem> NavMenuItems { get; set; }
        public DbSet<WebOS.Models.NavMenuItemSub> NavMenuItemSub { get; set; }
        public DbSet<WebOS.Models.Slider> Slider { get; set; }
        public DbSet<WebOS.Models.Block> Block { get; set; }
        public DbSet<WebOS.Models.BlockItem> BlockItem { get; set; }
        public DbSet<WebOS.Models.News> News { get; set; }
        public DbSet<WebOS.Models.HomeCard> HomeCard { get; set; }
        public DbSet<WebOS.Models.Notification> Notification { get; set; }
        public DbSet<WebOS.Messages.Models.Message> Messages { get; set; }
        public DbSet<WebOS.Models.MessageReply> MessageReplies { get; set; }
        public DbSet<WebOS.Models.GalleryCategory> GalleryCategory { get; set; }
        public DbSet<WebOS.Models.GalleryImage> GalleryImage { get; set; }
        public DbSet<ARID.Models.CalendarEvent> CalendarEvent { get; set; }
        public DbSet<ARID.Models.CalenderEventCategory> CalenderEventCategory { get; set; }
        public DbSet<WebOS.Models.SystemSettings> SystemSettings { get; set; }
        public DbSet<WebOS.Models.Service> Service { get; set; }
        public DbSet<WebOS.Models.TeamMember> TeamMember { get; set; }
        public DbSet<WebOS.Models.Cause> Cause { get; set; }
        public DbSet<WebOS.Models.Testimonial> Testimonial { get; set; }
        public DbSet<WebOS.Models.BlogPostImage> BlogPostImage { get; set; }
        public DbSet<WebOS.Models.CauseCategory> CauseCategory { get; set; }
        public DbSet<WebOS.Models.CauseImage> CauseImage { get; set; }
    }
}