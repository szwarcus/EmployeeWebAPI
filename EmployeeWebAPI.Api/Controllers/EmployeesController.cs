using EmployeeWebAPI.Api.Models;
using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator) => _mediator = mediator; 

        [HttpPost(Name = "Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEmployeeRequest employee)
        {
            var command = new CreateEmployeeCommand()
            {
                BirthDate=employee.BirthDate,
                Name=employee.Name,
                Gender=employee.Gender,
                Pesel=employee.Pesel
            };

            var result = await _mediator.Send(command);

            return result.Status switch
            {
                ResponseStatus.NotFound => NotFound(result),
                ResponseStatus.BusinessLogicError or ResponseStatus.InvalidQuery or ResponseStatus.DatabaseError => BadRequest(result),
                _ => Ok(result),
            };
        }

        [HttpPut("{id}",Name = "Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEmployeeRequest employee, Guid id)
        {
            var employeeId = new EmployeeId(id);
            var command = new UpdateEmployeeCommand()
            {
                EmployeeId=employeeId,
                BirthDate=employee.BirthDate,
                Gender=employee.Gender,
                Name=employee.Name,
                Pesel=employee.Pesel,
            };

            var result = await _mediator.Send(command);

            return result.Status switch
            {
                ResponseStatus.NotFound => NotFound(result),
                ResponseStatus.BusinessLogicError => BadRequest(result),
                _ => NoContent(),
            };
        }

        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var employeeId = new EmployeeId(id);
            var deleteCommand = new DeleteEmployeeCommand()
            {
                Id =  employeeId
            };

            var result = await _mediator.Send(deleteCommand);

            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result);

            return NoContent();
        }


        [HttpGet(Name = "GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<EmployeesInListViewModel>>> GetAllEmployees()
        {
            var result = await _mediator.Send(new GetAllEmployeesInListQuery());

            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee([FromRoute] Guid id)
        {
            var employeeId = new EmployeeId(id);
            var query = new GetEmployeeQuery() { Id = employeeId };
            var result = await _mediator.Send(query);

            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);

            return Ok(result);
        }
    }
}
