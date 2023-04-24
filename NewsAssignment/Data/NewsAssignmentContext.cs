using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Models;

namespace NewsAssignment.Data
{
    public class NewsAssignmentContext : DbContext
    {
        public NewsAssignmentContext (DbContextOptions<NewsAssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<NewsAssignment.Models.Comment> Comment { get; set; } = default!;
    }
}
