using Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraDAL.Contexts
{
    public class NotificationDbContext : DbContext
    {

        public DbSet<Notification> Notifications { get; set; }

        public NotificationDbContext(DbContextOptions options)
            : base(options)
        {
        }


    }
}
