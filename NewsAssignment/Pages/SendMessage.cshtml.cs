using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using NewsAssignment.Hubs;
using NewsAssignment.Interfaces;
using NewsAssignment.Models;
using System.Reflection;

namespace NewsAssignment.Pages
{
    // [Authorize(Roles ="Admin,Writer,Publisher,Editor")]
    public class SendMessageModel : PageModel
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        public SendMessageModel(IHubContext<NotificationHub> notificationHubContext, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager)
        {
           _notificationHubContext = notificationHubContext;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            message = new Message();
        }
        [BindProperty]
        public Message message { get; set; }

        public void OnGet() { }
        public async void OnPost()
        {
            var connections = _userConnectionManager.GetUserConnections(message.role);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", message.header, message.content);
                }
            }
            Redirect("~/Index");
        }

    }
}

