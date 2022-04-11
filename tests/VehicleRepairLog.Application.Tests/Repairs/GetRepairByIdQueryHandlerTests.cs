using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Exceptions;
using VehicleRepairLog.Application.Features.Repairs;
using VehicleRepairLog.Application.MappingProfiles;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using Xunit;

namespace VehicleRepairLog.Application.Tests.Repairs
{
    public class GetRepairByIdQueryHandlerTests
    {
        public readonly IMapper mapper;

        public GetRepairByIdQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<RepairProfile>();
            });

            this.mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        private async Task Handle_GivenNotExistingRepairId_ReturnsNotFoundException()
        {
            //arrange
            var repositoryMock = new Mock<IRepairRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(() => null);

            var queryHandler = new GetRepairByIdQueryHandler(this.mapper, repositoryMock.Object);

            //act
            Func<Task> result = async () => await queryHandler.Handle(new GetRepairByIdQuery() { RepairId = 1 }, CancellationToken.None);

            //assert
            await result.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        private async Task Handle_GivenExistingRepairId_ReturnsCorrectRepairDto()
        {
            //arrange
            var repositoryMock = new Mock<IRepairRepository>();
            repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(new Repair { Id = 1, Date = new DateTime(2022, 4, 9), CarWorkshopName = "testWorkshop" });

            var queryHandler = new GetRepairByIdQueryHandler(this.mapper, repositoryMock.Object);

            //act
            var result = await queryHandler.Handle(new GetRepairByIdQuery() { RepairId = 1 }, CancellationToken.None);

            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Should().BeOfType<RepairDto>();
        }
    }
}
