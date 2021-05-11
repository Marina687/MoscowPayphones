using Microsoft.EntityFrameworkCore;
using MoscowTrafficRestriction.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.InfrastructureServices.Gateways.Database
{
    public class TrafficRestrictionContext : DbContext
    {
        public DbSet<TrafficRestriction> TrafficRestrictions { get; set; }

        public TrafficRestrictionContext(DbContextOptions<TrafficRestrictionContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FillTestData(modelBuilder);
        }
        private void FillTestData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrafficRestriction>().HasData(
               new
               {
                   Id = 1L,
                   Name = "Таксофон № 1857",
                   DescriptionLocation = "Вавилова улица, дом 5А",
                   PayWay = "карта",
                   IntercityConnectionPayment = "бесплатно",
                   ValidUniversalServicesCard = "не действует"


               },
                        new
                        {
                            Id = 2L,
                            Name = "Таксофон № 1857",
                            DescriptionLocation = "Вавилова улица, дом 5А",
                            PayWay = "карта",
                            IntercityConnectionPayment = "бесплатно",
                            ValidUniversalServicesCard = "не действует"


                        },
                        new
                        {
                            Id = 3L,
                            Name = "Таксофон № 1857",
                            DescriptionLocation = "Вавилова улица, дом 5А",
                            PayWay = "карта",
                            IntercityConnectionPayment = "бесплатно",
                            ValidUniversalServicesCard = "не действует"

                        },
                        new
                        {
                            Id = 4L,
                            Name = "Таксофон № 1857",
                            DescriptionLocation = "Вавилова улица, дом 5А",
                            PayWay = "карта",
                            IntercityConnectionPayment = "бесплатно",
                            ValidUniversalServicesCard = "не действует"

                        });
        }
    }
}
