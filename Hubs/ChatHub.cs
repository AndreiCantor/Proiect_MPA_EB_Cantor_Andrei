using Microsoft.AspNetCore.SignalR;

namespace Proiect_MPA_EB_Cantor_Andrei.Hubs
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
