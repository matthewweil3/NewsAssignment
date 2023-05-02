using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NewsAssignment.Hubs;
using NewsAssignment.Interfaces;
using NewsAssignment.Models;
using System.Linq;
using System.Reflection;

namespace NewsAssignment.Pages
{
    [Authorize(Roles ="Admin,Writer,Publisher,Editor")]
    public class SendMessageModel : PageModel
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly NewsAssignment.Data.ApplicationDbContext _context;
        public SendMessageModel(IHubContext<NotificationHub> notificationHubContext, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, NewsAssignment.Data.ApplicationDbContext context)
        {
           _notificationHubContext = notificationHubContext;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
            message = new Message();
        }
        [BindProperty]
        public Message message { get; set; }

        public void OnGet() {
            ViewData["RoleId"] = _context.Roles.Where(x => x.Name != "Sub")
                .Where(x => x.Name != "Admin")
                .Select(x => 
                    new SelectListItem{ 
                        Value = x.Id,
                        Text = x.Name
                    }) ;
        }
        public async void OnPost()
        {
            message.role = Request.Form["roles"];
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

