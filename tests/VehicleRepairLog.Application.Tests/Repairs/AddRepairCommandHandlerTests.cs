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
        private readonly IMapper _mapper;
        private readonly Mock<IRepairRepository> _repoMock;

        public AddRepairCommandHandlerTests()
        {
            _repoMock = RepairRepositoryMock.RepositoryMock(); 

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<RepairMappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            
        }

        //[Fact]
        //private async Task Handle_AddsRepairToDb_ReturnsCorrectAmountOfAddedItems()
        //{
        //    var repairs = new List<Repair>();
            
        //    var strings = new List<string>
        //    {
        //        "test",
        //        "test1"
        //    };

        //    //arrange
        //    //var RepositoryMock = new Mock<IRepairRepository>();
        //    //repositoryMock
        //    //    .Setup(r => r.AddAsync(It.IsAny<Repair>(), It.IsAny<List<string>>()))
        //    //    .ReturnsAsync((Repair repair) =>
        //    //     {
        //    //         repairs.Add(repair);
        //    //         return repair;
        //    //     });
        //    var repairRepositoryMock = new Mock<IRepairRepository>();
        //    repairRepositoryMock
        //        .Setup(r => r.AddAsync(It.IsAny<Repair>()))
        //        .ReturnsAsync((Repair repair) =>
        //        {
        //            repairs.Add(repair);
        //            return repair;
        //        });
        //    var partRepositoryMock = new Mock<IPartRepository>();
        //    partRepositoryMock
        //        .Setup(r => r.GetByNameAsync(It.IsAny<List<string>>()))
        //        .ReturnsAsync(() => strings);

        //    var commandHandler = new AddRepairCommandHandler(this.mapper, repairRepositoryMock.Object, partRepositoryMock.Object);

        //    //act
        //    var result = await commandHandler.Handle(new AddRepairCommand()
        //    {
        //        VehicleId = 1,
        //        Date = new DateTime(2022, 4, 10),
        //        Description = "Test",
        //        CarWorkshopName = "Test",
        //        PartNames = new List<string>() { "test" }
        //    },
        //    CancellationToken.None);

        //    var addedRepair = await repairRepositoryMock.Object.GetAllAsync();

        //    //assert
        //    result.Should().BeOfType<RepairDto>();
        //    addedRepair.Count.Should().Be(1);
        //}
    }
}
