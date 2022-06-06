using Moq;
using System;
using System.Collections.Generic;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Tests.Mocks
{
    public static class RepairRepositoryMock
    {
        public static Mock<IRepairRepository> RepositoryMock()
        {
            List<Repair> repairs = new()
            {
                new Repair()
                {
                    VehicleId = 1,
                    Date = new DateTime(2022, 4, 10),
                    Description = "Test",
                    CarWorkshopName = "Test",
                    Parts = new List<Part>()
                    {
                        new Part()
                        {
                            Name = "Test"
                        }
                    }
                },
                new Repair()
                {
                    VehicleId = 2,
                    Date = new DateTime(2022, 4, 10),
                    Description = "Test2",
                    CarWorkshopName = "Test2",
                    Parts = new List<Part>()
                    {
                        new Part()
                        {
                            Name = "Test2"
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepairRepository>();

            repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(repairs);

            repositoryMock
                .Setup(r => r.GetByIdAsync(repairs[0].Id))
                .ReturnsAsync(repairs[0]);

            //repositoryMock
            //    .Setup(r => r.AddAsync(It.IsAny<Repair>(), It.IsAny<List<string>>()))
            //    .ReturnsAsync((Repair repair) =>
            //    {
            //        repairs.Add(repair);
            //        return repair;
            //    });

            return repositoryMock;
        }
    }
}
