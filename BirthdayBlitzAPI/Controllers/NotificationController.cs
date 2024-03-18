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
            foreach (var item in response)
            {
                await _notificationService.Confirm(item.Id);
            }
            return Ok(new AppResponse<List<Notification>> { Data = response });
        }
        [Authorize]
        [HttpPut("confirm-receive/{notiId}")]
        public async Task<IActionResult> Update(Guid notiId)
        {
            await _notificationService.Confirm(notiId);
            return Ok(new AppResponse<object>
            {
                Message = "Xác nhận thành công"
            });
        }
    }
}
