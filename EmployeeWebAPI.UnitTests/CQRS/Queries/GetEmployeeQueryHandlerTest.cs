using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee;
using EmployeeWebAPI.Application.CQRS.Mapper;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.Factories;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS.Queries
{
    [TestFixture]
    public class GetEmployeeQueryHandlerTest
    {
        private IMapper _mapper;
        private GetEmployeeQueryHandler _getEmployeeQueryHandler;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private IEmployeeFactory _employeeFactory;
        [SetUp]
        public void SetUp()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new MappingDtos());
                mc.AddProfile(new MappingIds());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _employeeFactory = new EmployeeFactory();
            _getEmployeeQueryHandler = new GetEmployeeQueryHandler(_employeeRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetEmployeeExecutionTest()
        {
            //arrange
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var query = new GetEmployeeQuery()
            {
                Id = employee.Id,
            };

            _employeeRepositoryMock.Setup(x => x.Get(It.Is<EmployeeId>(y => y == employee.Id))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = employee
            });

            //act
            var response = await _getEmployeeQueryHandler.Handle(query, new System.Threading.CancellationToken());

            //assert
            _employeeRepositoryMock.Verify(x => x.Get(It.IsAny<EmployeeId>()), Times.Once);
        }

        [Test]
        public async Task GetEmployeeWithSuccessTest()
        {
            //arrange
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var query = new GetEmployeeQuery()
            {
                Id = employee.Id,
            };

            _employeeRepositoryMock.Setup(x => x.Get(It.Is<EmployeeId>(y => y == employee.Id))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = employee
            });


            //act
            var response = await _getEmployeeQueryHandler.Handle(query, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);
            response.Employee.Should().NotBeNull();
            response.Employee.Pesel.Value.Should().Be("78121293595");
        }

        [Test]
        public async Task GetEmployeeWhichNotExistFailTest()
        {
            //arrange
            var query = new GetEmployeeQuery()
            {
                Id = new Domain.ValueObjects.Ids.EmployeeId(Guid.NewGuid())
            };

            _employeeRepositoryMock.Setup(x => x.Get(It.Is<EmployeeId>(y => y == query.Id))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = false,
                Reason = Reason.NotFoundInDb,
            });


            //act
            var response = await _getEmployeeQueryHandler.Handle(query, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.NotFound);
        }
    }
}
