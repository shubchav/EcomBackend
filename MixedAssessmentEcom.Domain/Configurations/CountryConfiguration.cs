using Microsoft.EntityFrameworkCore;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Configurations
{
    public class CountryConfiguration
    {

        private readonly ModelBuilder modelBuilder;
        public CountryConfiguration(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public  void Seed()
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    CountryId = 1,
                    CountryName = "India"
                   
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Australia"
                }

            );
            modelBuilder.Entity<State>().HasData(
                new State { StateId = 1, StateName = "Andaman & Nicobar Is", CountryId = 1 },
                new State { StateId = 2, StateName = "Andhra Pradesh", CountryId = 1 },
                new State { StateId = 3, StateName = "Arunachal Pradesh", CountryId = 1 },
                new State { StateId = 4, StateName = "Assam", CountryId = 1 },
                new State { StateId = 5, StateName = "Bihar", CountryId = 1 },
                new State { StateId = 6, StateName = "Chandigarh", CountryId = 1 },
                new State { StateId = 7, StateName = "Dadra & Nagar Haveli ", CountryId = 1 },
                new State { StateId = 8, StateName = "Delhi", CountryId = 1 },
                new State { StateId = 9, StateName = "Goa", CountryId = 1 },
                new State { StateId = 10, StateName = "Gujarat", CountryId = 1 },
                new State { StateId = 11, StateName = "Haryana", CountryId = 1 },
                new State { StateId = 12, StateName = "Himachal Pradesh", CountryId = 1 },
                new State { StateId = 13, StateName = "Jammu & Kashmir", CountryId = 1 },
                new State { StateId = 14, StateName = "Kerala", CountryId = 1 },
                new State { StateId = 15, StateName = "Lakshadweep", CountryId = 1 },
                new State { StateId = 16, StateName = "Madhya Pradesh", CountryId = 1 },
                new State { StateId = 17, StateName = "Maharashtra", CountryId = 1 },
                new State { StateId = 18, StateName = "Manipur", CountryId = 1 },
                new State { StateId = 19, StateName = "Meghalaya", CountryId = 1 },
                new State { StateId = 20, StateName = "Karnataka (Mysore)", CountryId = 1 },
                new State { StateId = 21, StateName = "Orissa", CountryId = 1 },
                new State { StateId = 22, StateName = "Punjab", CountryId = 1 },
                new State { StateId = 23, StateName = "Rajasthan", CountryId = 1 },
                new State { StateId = 24, StateName = "Tamil Nadu", CountryId = 1 },
                new State { StateId = 25, StateName = "Tripura", CountryId = 1 },
                new State { StateId = 26, StateName = "West Bengal", CountryId = 1 },
                new State { StateId = 27, StateName = "Sikkim", CountryId = 1 },
                new State { StateId = 28, StateName = "Mizoram", CountryId = 1 },

                // data for austrilia 

                new State { StateId = 28, StateName = "Mizoram", CountryId = 1 }






            );
        }

    }
}
