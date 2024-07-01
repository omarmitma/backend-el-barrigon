using Microsoft.AspNetCore.SignalR;

namespace Proyecto__Final.hubs
{
    public class llamarMesero : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
