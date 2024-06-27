using Microsoft.AspNetCore.SignalR;
using SignalRSimpleChat.Models.Chat;

namespace SignalRSimpleChat.Hug
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { User = this.Context.User.Identity.Name, Text = message });
        }
    }
}
