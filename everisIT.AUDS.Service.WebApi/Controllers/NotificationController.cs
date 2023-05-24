using Microsoft.Extensions.Logging;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace everisIT.AUDS.Service.WebApi.Controllers
{
    /// <summary>
    /// AudsType Controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Filters.ValidateModel]
    public partial class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        /// <summary>
        /// AudsTypeController constructor
        /// </summary>
        /// <param name="audsTypeService">AudsType Interface Service</param>
        /// <param name="logger">logger interface</param>
        public NotificationController(INotificationService notificationService,
            ILogger<NotificationController> logger)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a List of AudsType filtered
        /// </summary>
        /// <param name="filter">AudsType filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/Notification
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("~/api/v1/[Controller]/NotificationsMitigation")]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NotificationsMitigation()
         {
            try
            {
                _logger.LogError("NotificationsMitigation start");
                IList<MessageDto> listEntityDto = await _notificationService.NotificationsMitigation();
                _logger.LogError("NotificationsMitigation end " + listEntityDto.FirstOrDefault()?.Body.ToString());
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get a List of AudsType filtered
        /// </summary>
        /// <param name="filter">AudsType filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/Notification
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("~/api/v1/[Controller]/NotificationsResponsible")]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NotificationsResponsible()
        {
            try
            {
                _logger.LogError("NotificationsResponsible start");
                var listEntityDto = await _notificationService.NotificationsResponsible();
                _logger.LogError("NotificationsResponsible end"  + listEntityDto.ToString());
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get a List of AudsType filtered
        /// </summary>
        /// <param name="filter">AudsType filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/Notification
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("~/api/v1/[Controller]/NotificationsUpdate")]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NotificationsUpdate()
        {
            try
            {
                _logger.LogError("NotificationsUpdate start");
                var listEntityDto = await _notificationService.NotificationsUpdate();
                _logger.LogError("NotificationsUpdate end");
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get a List of AudsType filtered
        /// </summary>
        /// <param name="filter">AudsType filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/Notification
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("~/api/v1/[Controller]/NotificationEscalation")]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NotificationEscalation()
        {
            try
            {
                _logger.LogError("NotificationEscalation start");
                var listEntityDto = await _notificationService.NotificationEscalation();
                _logger.LogError("NotificationEscalation end");
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get a List of AudsType filtered
        /// </summary>
        /// <param name="filter">AudsType filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/Notification
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet("~/api/v1/[Controller]/NotificationEndFollowUp")]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NotificationEndFollowUp()
        {
            try
            {
                _logger.LogError("Init Bacth");
                var listEntityDto = await _notificationService.NotificationEndFollowUp();
                _logger.LogError("End Bacth");
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}