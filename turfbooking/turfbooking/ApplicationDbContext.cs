using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace restrauntBooking
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TurfUser> TurfUsers { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<SlotBooking> SlotBookings { get; set; }

    }

}
