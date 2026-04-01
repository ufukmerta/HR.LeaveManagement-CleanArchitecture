using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<LeaveAllocations>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var leaveRequests = await mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveAllocations>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveAllocations>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto leaveRequest)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = leaveRequest };
            var response = await mediator.Send(command);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        // PUT api/<LeaveAllocations>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] UpdateLeaveRequestDto leaveRequest)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, UpdateLeaveRequestDto = leaveRequest };
            var response = await mediator.Send(command);
            return response.IsSuccess ? NoContent() : BadRequest(response);
        }

        // PUT api/<LeaveAllocations>/5
        [HttpPut("/{id}/approval")]
        public async Task<ActionResult<BaseCommandResponse>> Put(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApproval)
        {
            var command = new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = changeLeaveRequestApproval };
            var response = await mediator.Send(command);
            return response.IsSuccess ? NoContent() : BadRequest(response);
        }

        // DELETE api/<LeaveAllocations>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
