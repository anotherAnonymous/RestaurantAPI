using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString =
            "Server=WIN-5LP0NG2E4KA\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection = True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Adresses {get;set;}
        public DbSet<Dish> Dishes { get; set; }


        //konfiguracja parametrow wymaganych przy tworzeniu elemntow bazy danych
        protected override void OnModelCreating(ModelBuilder modelBuilder)// nadpisanie metody modelbuilder
        {

            //parametryzacja encjii Restaurant
            modelBuilder.Entity<Restaurant>()// jaka encja
                .Property(r => r.Name) //wybor wlasciwosci
                .IsRequired()//jest wymagana
                .HasMaxLength(25); // max dlugosc 25 znakow

            //parametryzacja encjii Dish
            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

            //parametryzacja encji Adress
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(50);

        }


        //konfiguracja połaczenia bazy danych
        //konfiguracja jakiej typu bazy chcemy uzywac oraz jak powinno wygladac polaczenie
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
