using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeyValue.Models
{
    public class KeyContext:DbContext
    {
        public KeyContext(DbContextOptions<KeyContext> options)
            : base(options)
        {
        }

        public DbSet<Key> Keys { get; set; }
    }
}
