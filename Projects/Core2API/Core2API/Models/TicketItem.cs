using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core2API.Models
{
    public class TicketItem
    {
        public long Id { get; set; }

        public string Consert { get; set; }

        public string Artist { get; set; }

        public bool Available { get; set; }
    }

    public class TicketContext: DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<TicketItem> TicketItems { get; set; }
    }

}
