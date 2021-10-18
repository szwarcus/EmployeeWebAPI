using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using EmployeeWebAPI.Application.CQRS.Mapper;
using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS
{
    [TestFixture]
    public class Tests
    {

        private IMapper _mapper;
        private CreateEmployeeCommandHandler _createEmployeeCommandHandler;
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
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


            _createEmployeeCommandHandler = new CreateEmployeeCommandHandler(_employeeRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task CreateEmployeWithPeselWhichNotExistSuccessTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name=new Name("Jan","Kowalski"),
                Gender=Gender.Men,
                Pesel = new Pesel("75080413758"),
                BirthDate=new System.DateTime(1975,08,04),
            };

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.Database,
                    Success = false,
                    Reason = Reason.DuplicatedUniqueId
                });

            _employeeRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Employee>())).ReturnsAsync(new ExecutionStatus<EmployeeId>()
            {
                ReturnValue = new EmployeeId(),
                Source = Source.Database,
                Success = true,
                Reason = Reason.None
            }
            );
            //act
            var response = await _createEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);
        }

        [Test]
        public async Task CreateEmployeeWithExistingPeselShouldFailTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new Name("Jan", "Kowalski"),
                Gender = Gender.Men,
                Pesel = new Pesel("75080413758"),
                BirthDate = new System.DateTime(1975, 08, 04),
            };

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>() {
                    ReturnValue = true,
                    Source=Source.Database,
                    Success=false,
                    Reason=Reason.DuplicatedUniqueId
                });
            //act
            var response = await _createEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.InvalidQuery);
        }

        [Test]
        public async Task CreateEmployeeWithIncorrectPeselResultFailTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new Name("Jan", "Kowalski"),
                Gender = Gender.Men,
                Pesel = new Pesel("750858"),
                BirthDate = new System.DateTime(1975, 08, 04),
            };

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.ApplicationLogic,
                    Success = false,
                    Reason = Reason.UnhandledException
                });
            //act
            var response = await _createEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeFalse();
            response.Status.Should().Be(Application.Common.ResponseStatus.BusinessLogicError);
        }


        [Test]
        public async Task CreateMultipleEmployeesShouldResultDifferentUniqueIdsTest()
        {
            //arrange
            #region Create command 1
            var command = new CreateEmployeeCommand
            {
                Name = new Name("Jan", "Kowalski"),
                Gender = Gender.Men,
                Pesel = new Pesel("75080413758"),
                BirthDate = new System.DateTime(1975, 08, 04),
            };

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == command.Pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.Database,
                    Success = false,
                    Reason = Reason.DuplicatedUniqueId
                });

            _employeeRepositoryMock.Setup(x => x.AddAsync(It.Is<Employee>(x => x.Pesel.Value == "75080413758"))).ReturnsAsync(new ExecutionStatus<EmployeeId>()
            {
                ReturnValue = new EmployeeId(),
                Source = Source.Database,
                Success = true,
                Reason = Reason.None
            }
            );
            #endregion
            #region Create command 2
            var command2 = new CreateEmployeeCommand
            {
                Name = new Name("Janina", "Kowalska"),
                Gender = Gender.Men,
                Pesel = new Pesel("75050757882"),
                BirthDate = new System.DateTime(1975, 05, 07),
            };

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == command2.Pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.Database,
                    Success = false,
                    Reason = Reason.DuplicatedUniqueId
                });

            _employeeRepositoryMock.Setup(x => x.AddAsync(It.Is<Employee>(x => x.Pesel.Value == "75050757882"))).ReturnsAsync(new ExecutionStatus<EmployeeId>()
            {
                ReturnValue = new EmployeeId(),
                Source = Source.Database,
                Success = true,
                Reason = Reason.None
            }
            );
            #endregion
            //act
            var response = await _createEmployeeCommandHandler.Handle(command, new System.Threading.CancellationToken());
            var response2 = await _createEmployeeCommandHandler.Handle(command2, new System.Threading.CancellationToken());

            //assert
            response.Success.Should().BeTrue();
            response.Status.Should().Be(Application.Common.ResponseStatus.Success);
            response.EmployeeId.Value.Should().NotBe(response2.EmployeeId.Value);
        }
    }
}