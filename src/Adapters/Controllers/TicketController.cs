using Adapters.Controllers.Common;
using Application.UseCases.Tickets;

namespace Adapters.Controllers;

public interface ITicketController
{
    Task<Result> CreateTicket(CreateTicketRequest request);
    Task<Result> UpdateStatus(UpdateStatusRequest request);
}

public class TicketController(
    ICreateTicketUseCase createTicketUseCase,
    IUpdateStatusUseCase updateStatusUseCase) : BaseController, ITicketController
{
    public async Task<Result> CreateTicket(CreateTicketRequest request)
    {
        try
        {
            var response = await Execute(() => createTicketUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> UpdateStatus(UpdateStatusRequest request)
    {
        try
        {
            var response = await Execute(() => updateStatusUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}