using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        List<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        bool Delete(int id);
    }

    public class RestaurantService : IRestaurantService
    {//serwis do pobierania wszystkich restauracji, konkretnej restauracji oraz do tworzenia nowgo obieku restaurant

        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper mapper;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public bool Delete(int id)
        {
            var restaurant = _dbContext
                          .Restaurants
                          .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }

        public RestaurantDto GetById(int id)
        {

            var restaurant = _dbContext
                           .Restaurants
                           .Include(r => r.Address)
                           .Include(r => r.Dishes)
                           .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return null;

            var result = mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public List<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var result = mapper.Map<List<RestaurantDto>>(restaurants);
            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

    }
}
