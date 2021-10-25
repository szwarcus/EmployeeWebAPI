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
    public class EmployeeController : Controller
    {

        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "addemployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeId>> Create([FromBody] CreateEmployeeRequest employee)
        {
            CreateEmployeeCommand c = new CreateEmployeeCommand()
            {
                BirthDate=employee.BirthDate,
                Name=employee.Name,
                Gender=employee.Gender,
                Pesel=employee.Pesel
            };

            var result = await _mediator.Send(c);

    
            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.BusinessLogicError|| result.Status == ResponseStatus.InvalidQuery)
                return BadRequest(result.Message);


            return Ok(result.EmployeeId);
        }

        [HttpPut("{id}",Name = "Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateEmployeeRequest employee)
        {
            UpdateEmployeeCommand updateEmployee = new UpdateEmployeeCommand()
            {
                EmployeeId=employee.EmployeeId,
                BirthDate=employee.BirthDate,
                Gender=employee.Gender,
                Name=employee.Name,
                Pesel=employee.Pesel,
                RegistrationNumber=employee.RegistrationNumber
            };

            var result = await _mediator.Send(updateEmployee);


            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);
            if (result.Status == ResponseStatus.BusinessLogicError)
                return BadRequest(result.Message);


            return NoContent();
        }

        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteCommand = new DeleteEmployeeCommand()
            {
                EmployeeId = new EmployeeId(id)
            };

            var result = await _mediator.Send(deleteCommand);

            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);

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

            return Ok(result.EmployeeList);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(Guid uid)
        {
            var result = await _mediator.Send
                (new GetEmployeeQuery() { EmployeeId = new EmployeeId(uid) });

            if (result.Status == ResponseStatus.NotFound)
                return NotFound(result.Message);

            return Ok(result.Employee);
        }
    }
}
