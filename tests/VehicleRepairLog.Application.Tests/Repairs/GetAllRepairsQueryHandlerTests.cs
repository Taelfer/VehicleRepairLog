using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Repairs;
using VehicleRepairLog.Application.MappingProfiles;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using Xunit;
using FluentAssertions;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Exceptions;

namespace VehicleRepairLog.Application.Tests.Repairs
{
    public class GetAllRepairsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        //Mock<IRepairRepository> repositoryMock = new Mock<IRepairRepository>();

        public GetAllRepairsQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<RepairMappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        private async void Handle_ForNullRepairs_ThrowsNotFoundException()
        {
            // arrange
            var repositoryMock = new Mock<IRepairRepository>();
            repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => null);

            var queryHandler = new GetAllRepairsQueryHandler(_mapper, repositoryMock.Object);

            // act
            Func<Task> action = async () => await queryHandler.Handle(new GetAllRepairsQuery(), CancellationToken.None);


            // assert
            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        private async Task Handle_GivenCorrectQuery_ReturnsListOfRepairDto()
        {
            // arrange
            List<Repair> repairs = new() 
            {
                new Repair{Id = 1, Date = new DateTime(2022, 4, 9), CarWorkshopName = "testWorkshop"},
                new Repair{Id = 2, Date = new DateTime(2022, 4, 9), CarWorkshopName = "testWorkshop2"}
            };

            var repositoryMock = new Mock<IRepairRepository>();
            repositoryMock
                .Setup(repairRepo => repairRepo.GetAllAsync())
                .ReturnsAsync(repairs);

            var queryHandler = new GetAllRepairsQueryHandler(_mapper, repositoryMock.Object);

            // act
            List<RepairDto> result = await queryHandler.Handle(new GetAllRepairsQuery(), CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            result.Should().BeOfType<List<RepairDto>>();
        }
    }
}
