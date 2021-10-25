using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using EmployeeWebAPI.Application.CQRS.Mapper;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EmployeeWebAPI.UnitTests.CQRS.Commands
{
    [TestFixture]
    public class CreateEmployeeCommandHandlerTest
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
        public async Task CreateEmployeeExecutionTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new NameDto() 
                {
                    First = "Jan", 
                    Last = "Kowalski" 
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "75080413758"
                },
                BirthDate = new System.DateTime(1975, 08, 04),
            };
            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.Database,
                    Success = true,
                    Reason = Reason.None
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
            _employeeRepositoryMock.Verify(x => x.PeselExists(pesel), Times.Once);
            _employeeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public async Task CreateEmployeWithPeselWhichNotExistSuccessTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new NameDto()
                {
                    First = "Jan",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "75080413758"
                },
                BirthDate = new System.DateTime(1975, 08, 04),
            };
            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
                .ReturnsAsync(new ExecutionStatus<bool>()
                {
                    ReturnValue = false,
                    Source = Source.Database,
                    Success = true,
                    Reason = Reason.None
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
                Name = new NameDto()
                {
                    First = "Jan",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "75080413758"
                },
                BirthDate = new System.DateTime(1975, 08, 04),
            };

            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
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
        public async Task CreateEmployeeWithIncorrectPeselLengthResultFailTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new NameDto()
                {
                    First = "Jan",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "750858"
                },
                BirthDate = new System.DateTime(1975, 08, 04),
            };

            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
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

        //[Test]
        public async Task CreateEmployeeWithIncorrectPeselValidationResultFailTest()
        {
            //arrange
            var command = new CreateEmployeeCommand
            {
                Name = new NameDto()
                {
                    First = "Jan",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "51081199769"
                },
                BirthDate = new System.DateTime(1951, 08, 11),
            };

            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
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
            response.Errors.Should().Contain("Value is incorrect!");
        }


        [Test]
        public async Task CreateMultipleEmployeesShouldResultDifferentUniqueIdsTest()
        {
            //arrange
            #region Create command 1
            var command = new CreateEmployeeCommand
            {
                Name = new NameDto()
                {
                    First = "Jan",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "75080413758"
                },
                BirthDate = new System.DateTime(1975, 08, 04),
            };
            var pesel = _mapper.Map<PeselDto, Pesel>(command.Pesel);
            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel)))
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
                Name = new NameDto()
                {
                    First = "Janina",
                    Last = "Kowalski"
                },
                Gender = Gender.Men,
                Pesel = new PeselDto()
                {
                    Value = "75050757882"
                },
                BirthDate = new System.DateTime(1975, 05, 07),
            };
            var pesel2 = _mapper.Map<PeselDto, Pesel>(command2.Pesel);

            _employeeRepositoryMock
                .Setup(x => x.PeselExists(It.Is<Pesel>(x => x == pesel2)))
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