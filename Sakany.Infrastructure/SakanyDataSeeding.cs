using Sakany.Application.DTOS;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sakany.Infrastructure
{
    public static class SakanyDataSeeding
    {
        public static async Task AddDateSeeding(ApplicationDbContext context)
        {
            if (!context.Governorates.Any())
            {
                var governoratesData = File.ReadAllText("../Sakany.Infrastructure/DataSeeding/Governorate.json");
                var governoratesObjects = JsonSerializer.Deserialize<List<Governorate>>(governoratesData);
                if (governoratesObjects?.Count() > 0)
                {

                    foreach (var governorate in governoratesObjects)
                    {
                        context.Set<Governorate>().Add(governorate);
                    }
                    context.SaveChanges();
                }
            }
            if (!context.Cities.Any())
            {
                var citiesData = File.ReadAllText("../Sakany.Infrastructure/DataSeeding/Cities.json");
                var citiesObjects = JsonSerializer.Deserialize<List<CityDataDTO>>(citiesData);
                if (citiesObjects?.Count() > 0)
                {
                    foreach (var cityData in citiesObjects)
                    {
                        foreach (var cityName in cityData.Cities)
                        {
                            var city = new City
                            {
                                GovernorateID = cityData.GovernorateID,
                                Name = cityName
                            };
                            context.Set<City>().Add(city);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}