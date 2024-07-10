    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using QuizApplication.Models;

    namespace QuizApplication.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
            public DbSet<QuizQuestions> QuizQuestions { get; set; }
            public DbSet<QuizAnswer> QuizAnswer { get; set; }
        }
    }
