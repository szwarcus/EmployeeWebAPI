using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee;
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
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS.Commands
{
    [TestFixture]
    public class UpdateEmployeeCommandHandlerTest
    {

        private UpdateEmployeeCommandHandler _updateEmployeeCommandHandler;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private IMapper _mapper;
        private IEmployeeFactory _employeeFactory;
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

            _updateEmployeeCommandHandler = new UpdateEmployeeCommandHandler(_employeeRepositoryMock.Object, _mapper);
            _employeeFactory = new EmployeeFactory();
        }

        [Test]
        public async Task UpdateEmployeeExecutionTest()
        {
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var employeeDto= _mapper.Map<EmployeeDto> (employee);

            //arrange
            var command = new UpdateEmployeeCommand()
            {
                EmployeeId = employeeDto.Id,
                BirthDate= employeeDto.BirthDate,
                Gender=employeeDto.Gender,
                Name=employeeDto.Name,
                Pesel=employeeDto.Pesel,
                RegistrationNumber=employeeDto.RegistrationNumber
            };

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(y => y == employee.Id.Value))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = employee
            });

            _employeeRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Employee>(y => y.Id== employee.Id ))).ReturnsAsync(new ExecutionStatus()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,

            });

            //act
            var response = await _updateEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            _employeeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _employeeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Employee>()), Times.Once);

        }

        [Test]
        public async Task UpdateEmployeeWithSuccessTest()
        {
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            //arrange
            var command = new UpdateEmployeeCommand()
            {
                EmployeeId = employeeDto.Id,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                Name = employeeDto.Name,
                Pesel = employeeDto.Pesel,
                RegistrationNumber = employeeDto.RegistrationNumber
            };

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(y => y == employee.Id.Value))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,
                ReturnValue = employee
            });

            _employeeRepositoryMock.Setup(x => x.UpdateAsync(It.Is<Employee>(y => y.Id == employee.Id))).ReturnsAsync(new ExecutionStatus()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None,

            });

            //act
            var response = await _updateEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);

        }

        [Test]
        public async Task UpdateEmployeeNotFoundErrorTest()
        {
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78121293595", new DateTime(1978, 12, 21), Gender.Men);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            //arrange
            var command = new UpdateEmployeeCommand()
            {
                EmployeeId = employeeDto.Id,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                Name = employeeDto.Name,
                Pesel = employeeDto.Pesel,
                RegistrationNumber = employeeDto.RegistrationNumber
            };

            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<Guid>(y => y == employee.Id.Value))).ReturnsAsync(new ExecutionStatus<Domain.Entities.Employee>()
            {
                Source = Source.Database,
                Success = false,
                Reason = Reason.NotFoundInDb,
            });

            //act
            var response = await _updateEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.NotFound);
        }

        [Test]
        public async Task UpdateEmployeeIncorrectDataErrorTest()
        {
            var employee = _employeeFactory.CreateEmployee("Jan", "Nowak", "78123595", new DateTime(1978, 12, 21), Gender.Men);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            //arrange
            var command = new UpdateEmployeeCommand()
            {
                EmployeeId = employeeDto.Id,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                Name = employeeDto.Name,
                Pesel = employeeDto.Pesel,
                RegistrationNumber = employeeDto.RegistrationNumber
            };

            //act
            var response = await _updateEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.BusinessLogicError);
        }


    }
}
