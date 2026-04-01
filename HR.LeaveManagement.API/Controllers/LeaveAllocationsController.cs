using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<LeaveAllocations>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveAllocations = await mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocations>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var leaveAllocation = await mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocations>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto leaveAllocation)
        {
            var command = new CreateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        // PUT api/<LeaveAllocations>/5
        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] UpdateLeaveAllocationDto leaveAllocation)
        {
            var command = new UpdateLeaveAllocationCommand { LeaveAllocationDto = leaveAllocation };
            var response = await mediator.Send(command);
            return response.IsSuccess ? NoContent() : BadRequest(response);
        }

        // DELETE api/<LeaveAllocations>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
