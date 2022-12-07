
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Context
{
    public class DietManagerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocalDB;Database=DietManager;Trusted_Connection=True");
        }



        public DbSet<ClientAllergies> ClientAllergies { get; set; }
        public DbSet<FoodList> FoodList { get; set; }
        public DbSet<DietianClientLists> DietianClients  { get; set; }
        public DbSet<Allergen> Allergen { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<ClientDietList> ClientDietList { get; set; }
        public DbSet<MeasurementResult> MeasurementResults { get; set; }
        public DbSet<ClientDefaultData> ClientDefaultData { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
