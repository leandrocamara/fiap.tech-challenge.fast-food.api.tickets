using Adapters.Controllers;
using Application.UseCases.Tickets;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Routers;

[ApiController]
[Route("api/tickets")]
public class TicketRouter(ITicketController controller) : BaseRouter
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "", typeof(CreateTicketResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTicket(CreateTicketRequest request)
    {
        var result = await controller.CreateTicket(request);
        return HttpResponse(result);
    }

    [HttpPut("{orderId:guid}/status")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(UpdateStatusResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid orderId)
    {
        var result = await controller.UpdateStatus(new UpdateStatusRequest(orderId));
        return HttpResponse(result);
    }
}