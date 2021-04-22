using System;
using Microsoft.EntityFrameworkCore;
using QuotesApp.Models;

namespace QuotesApp.Data
{
    public class QuoteContext : DbContext
    {
        public QuoteContext(DbContextOptions<QuoteContext> options): base(options) { }
        public DbSet<Quote> Quote { get; set; }
    }
}
