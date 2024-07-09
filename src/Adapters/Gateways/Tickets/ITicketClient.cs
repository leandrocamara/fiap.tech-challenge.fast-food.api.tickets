namespace Adapters.Gateways.Tickets;

public interface ITicketClient
{
    Task<string> GenerateQrCode(decimal value);
}