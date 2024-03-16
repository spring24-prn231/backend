using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class NotificationController : ApiControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetNotificationFilterRequest filter)
        {
            var response = await _notificationService.Get(filter).ToListAsync();
            return Ok(new AppResponse<List<Notification>> { Data = response });
        }
    }
}
