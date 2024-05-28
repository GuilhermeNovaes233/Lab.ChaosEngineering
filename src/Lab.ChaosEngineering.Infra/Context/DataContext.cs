using Lab.ChaosEngineering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.ChaosEngineering.Infra.Context
{
	public class DataContext : DbContext
	{
		public DataContext() { }

		public DataContext(DbContextOptions<DataContext> options)
		   : base(options)
		{ }

		public DbSet<Payment> Payments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}
	}
}