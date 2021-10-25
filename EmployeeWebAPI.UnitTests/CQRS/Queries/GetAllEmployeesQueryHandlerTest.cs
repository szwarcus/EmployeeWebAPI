using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee;
using EmployeeWebAPI.Application.CQRS.Mapper;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.Factories;
using EmployeeWebAPI.Domain.Status;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS.Queries
{
    [TestFixture]
    public class GetAllEmployeesQueryHandlerTest
    {
        private IMapper _mapper;
        private GetAllEmployeesInListQueryHandler _getAllEmployeesInListQueryHandler;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private IEmployeeFactory _employeeFactory;
        private readonly GetAllEmployeesInListQuery _query;
        private readonly IEnumerable<Employee> _response;

        public GetAllEmployeesQueryHandlerTest()
        {
            _employeeFactory = new EmployeeFactory();
            _query = new GetAllEmployeesInListQuery();
            var employee1 = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var employee2 = _employeeFactory.CreateEmployee("Janina", "Kowalska", "81041638988", new DateTime(1981, 4, 16), Gender.Woman);
            var employee3 = _employeeFactory.CreateEmployee("Włodzisław", "Zielinski", "75040146595", new DateTime(1975, 4, 1), Gender.Men);
            _response = new List<Employee>()
            {
                employee1,
                employee2,
                employee3
            };
        }

        [SetUp]
        public void SetUp()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new MappingDtos());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
         

            _getAllEmployeesInListQueryHandler = new GetAllEmployeesInListQueryHandler(_employeeRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetAllEmployeeExecutionTest()
        {

            //arrange
            _employeeRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new ExecutionStatus<IEnumerable<Employee>>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = _response
            });

            //act
            var response = await _getAllEmployeesInListQueryHandler.Handle(_query, new System.Threading.CancellationToken());

            //assert
            _employeeRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllEmployeesWithSuccessTest()
        {
            //arrange
            _employeeRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new ExecutionStatus<IEnumerable<Employee>>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = _response
            });

            //act
            var response = await _getAllEmployeesInListQueryHandler.Handle(_query, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);
            response.EmployeeList.Count.Should().Be(3);
        }

        [Test]
        public async Task GetAllEmployeesWithDBFailureTest()
        {
            //arrange
            _employeeRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new ExecutionStatus<IEnumerable<Employee>>()
            {
                Source = Source.Database,
                Success = false,
                Reason = Reason.NotFoundInDb,
            });

            //act
            var response = await _getAllEmployeesInListQueryHandler.Handle(_query, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.NotFound);
            response.EmployeeList.Should().BeNullOrEmpty();
        }
    }
}
