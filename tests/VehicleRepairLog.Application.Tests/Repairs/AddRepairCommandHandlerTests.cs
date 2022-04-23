using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Repairs;
using VehicleRepairLog.Application.MappingProfiles;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Tests.Mocks;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using Xunit;

namespace VehicleRepairLog.Application.Tests.Repairs
{
    public class AddRepairCommandHandlerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<IRepairRepository> repoMock;

        public AddRepairCommandHandlerTests()
        {
            repoMock = RepairRepositoryMock.RepositoryMock(); 

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<RepairProfile>();
            });

            mapper = mapperConfig.CreateMapper();

            
        }

        [Fact]
        private async Task Handle_()
        {
            var repairs = new List<Repair>();

            //arrange
            var repositoryMock = new Mock<IRepairRepository>();
            repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Repair>(), It.IsAny<List<string>>()))
                .ReturnsAsync((Repair repair) =>
                 {
                     repairs.Add(repair);
                     return repair;
                 });

            var commandHandler = new AddRepairCommandHandler(this.mapper, repoMock.Object);

            //act
            var result = await commandHandler.Handle(new AddRepairCommand()
            {
                VehicleId = 1,
                Date = new DateTime(2022, 4, 10),
                Description = "Test",
                CarWorkshopName = "Test",
                PartNames = new List<string>()
                {
                    "test"
                }
            },
            CancellationToken.None);

            var addedRepair = await repoMock.Object.GetAllAsync();

            //assert
            result.Should().BeOfType<RepairDto>();
            addedRepair.Count.Should().Be(3);
        }
    }
}
