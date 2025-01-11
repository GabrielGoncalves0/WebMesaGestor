﻿using Microsoft.EntityFrameworkCore;
using WebMesaGestor.Domain.Entities;

namespace WebMesaGestor.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Empresa> Empresas { get; set; }
    }
}
