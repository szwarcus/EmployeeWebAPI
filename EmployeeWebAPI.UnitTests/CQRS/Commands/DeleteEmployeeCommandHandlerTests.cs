using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee;
using EmployeeWebAPI.Application.CQRS.Mapper;
using EmployeeWebAPI.Domain.Status;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS.Commands
{
    [TestFixture]
    public class DeleteEmployeeCommandHandlerTest
    {

        private IMapper _mapper;
        private DeleteEmployeeCommandHandler _deleteEmployeeCommandHandler;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        [SetUp]
        public void SetUp()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new MappingDtos());
                mc.AddProfile(new MappingIds());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;


            _deleteEmployeeCommandHandler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task DeleteEmployeeExecutionTest()
        {
            var guid = Guid.NewGuid();
            //arrange
            var command = new DeleteEmployeeCommand()
            {
                EmployeeId = new Application.CQRS.Mapper.Dto.IdDto()
                {
                    Value = guid
                }
            };
            _employeeRepositoryMock.Setup(x => x.RemoveByIdAsync(It.Is<Guid>(y => y == guid))).ReturnsAsync(new ExecutionStatus()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None
            });

            var response = await _deleteEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            ;
            _employeeRepositoryMock.Verify(x => x.RemoveByIdAsync(It.IsAny<Guid>()), Times.Once);

        }

        [Test]
        public async Task DeleteEmployeeWithSuccessTest()
        {
            var guid = Guid.NewGuid();
            //arrange
            var command = new DeleteEmployeeCommand()
            {
                EmployeeId = new Application.CQRS.Mapper.Dto.IdDto()
                {
                    Value = guid
                }
            };
            _employeeRepositoryMock.Setup(x => x.RemoveByIdAsync(It.Is<Guid>(y => y == guid))).ReturnsAsync(new ExecutionStatus()
            {
                Source = Source.Database,
                Success = true,
                Reason = Reason.None
            });

            var response = await _deleteEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);

        }

        [Test]
        public async Task DeleteEmployeeNotFoundErrorTest()
        {
            var guid = Guid.NewGuid();
            //arrange
            var command = new DeleteEmployeeCommand()
            {
                EmployeeId = new Application.CQRS.Mapper.Dto.IdDto()
                {
                    Value = guid
                }
            };
            _employeeRepositoryMock.Setup(x => x.RemoveByIdAsync(It.Is<Guid>(y => y == guid))).ReturnsAsync(new ExecutionStatus()
            {
                Source = Source.Database,
                Success = false,
                Reason = Reason.NotFoundInDb
            });

            var response = await _deleteEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.NotFound);

        }


    }
}