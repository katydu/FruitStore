using System;
// using DbContext
using Microsoft.EntityFrameworkCore;
using FruitStore.Models;

namespace FruitStore.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<Members> Members { get; set; }
	public DbSet<Orders> Orders { get; set; }
	public DbSet<Products> Products { get; set; }
	public DbSet<OrderDetails> OrderDetails { get; set; }

}

