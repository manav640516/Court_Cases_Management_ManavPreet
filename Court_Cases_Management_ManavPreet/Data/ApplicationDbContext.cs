using Court_Cases_Management_ManavPreet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Court_Cases_Management_ManavPreet.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Judge> Judges { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<CourtRoom> CourtRooms { get; set; }
        public DbSet<Hearing> Hearings { get; set; }
        public DbSet<Case> Cases { get; set; }

    }
}
