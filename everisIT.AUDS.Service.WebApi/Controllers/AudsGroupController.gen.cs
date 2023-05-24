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
    /// AudsGroup Controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Filters.ValidateModel]
    public partial class AudsGroupController : ControllerBase
    {
        private readonly IAudsGroupService _audsGroupService;
        private readonly ILogger<AudsGroupController> _logger;
        /// <summary>
        /// AudsGroupController constructor
        /// </summary>
        /// <param name="audsGroupService">AudsGroup Interface Service</param>
        /// <param name="logger">logger interface</param>
        public AudsGroupController(IAudsGroupService audsGroupService,
            ILogger<AudsGroupController> logger)
        {
            _audsGroupService = audsGroupService ?? throw new ArgumentNullException(nameof(audsGroupService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a List of AudsGroup filtered
        /// </summary>
        /// <param name="filter">AudsGroup filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_group
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsGroupService - http://scld01dfnfe01:18288/api/v1/AudsGroup
        /// </code>
        /// </remarks>
        /// <returns>audsGroupsDto: AudsGroupsDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AudsGroupDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> List([FromQuery] AudsGroupFilter filter)
        {
            try
            {
                var listEntityDto = await _audsGroupService.GetList(filter);
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get an AudsGroup by GroupId
        /// </summary>
        /// <param name="id">AudsGroup GroupId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_group
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsGroupService - http://scld01dfnfe01:18288/api/v1/AudsGroup/1
        /// </code>
        /// </remarks>
        /// <returns>AudsGroupDto</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AudsGroupDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(AudsGroupDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDto = await _audsGroupService.Get(id);
                if (entityDto != null && entityDto.GroupId > 0)
                {
                    return Ok(entityDto);
                }
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create AudsGroup object
        /// </summary>
        /// <param name="entityDto">AudsGroupDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_group
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsGroupService - http://scld01dfnfe01:18288/api/v1/AudsGroup
        /// </code>        
        /// </remarks>
        /// <returns>AudsGroup inserted</returns>
        /// <response code="201">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(typeof(AudsGroupDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AudsGroupDto entityDto)
        {
            try
            {
                if (entityDto is null)
                {
                    return new BadRequestResult();
                }
                var entityDtoInserted = await _audsGroupService.Create(entityDto);
                return CreatedAtAction(nameof(AudsGroupController.Create)
                        , nameof(AudsGroupController).Replace("Controller", ""), entityDtoInserted);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());

                Microsoft.Data.SqlClient.SqlException sqlEx = (ex.InnerException as Microsoft.Data.SqlClient.SqlException);
                if (sqlEx != null)
                {
                    if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Violation of PRIMARY/UNIQUE KEY constraint");
                    }
                    else if (sqlEx.Number == 547)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Conflicted with FOREIGN KEY constraint");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, "SQL exception number:" + sqlEx.Number);
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "DbUpdateException exception with no internal SqlException");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update AudsGroup object
        /// </summary>
        /// <param name="entityDto">AudsGroupDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_group
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsGroupService - http://scld01dfnfe01:18288/api/v1/AudsGroup
        /// </code>        
        /// </remarks>
        /// <returns>AudsGroup updated</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        [ProducesResponseType(typeof(AudsGroupDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AudsGroupDto entityDto)
        {
            try
            {
                if (entityDto is null || entityDto.GroupId == 0)
                {
                    return new BadRequestResult();
                }
                var entityDtoUpdated = await _audsGroupService.Update(entityDto);

                if (entityDtoUpdated != null && entityDtoUpdated.GroupId > 0)
                {
                    return Ok(entityDtoUpdated);
                }
                return new NotFoundResult();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());

                Microsoft.Data.SqlClient.SqlException sqlEx = (ex.InnerException as Microsoft.Data.SqlClient.SqlException);
                if (sqlEx != null)
                {
                    if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Violation of PRIMARY/UNIQUE KEY constraint");
                    }
                    else if (sqlEx.Number == 547)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Conflicted with FOREIGN KEY constraint");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, "SQL exception number:" + sqlEx.Number);
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "DbUpdateException exception with no internal SqlException");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Change AudsGroup object status by GroupId
        /// </summary>
        /// <param name="id">AudsGroup GroupId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_group
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsGroupService - http://scld01dfnfe01:18288/api/v1/AudsGroup/1
        /// </code>
        /// </remarks>
        /// <returns>AudsGroup with inactive status</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AudsGroupDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDtoChangedStatus = await _audsGroupService.Delete(id);
                if (entityDtoChangedStatus != null && entityDtoChangedStatus.GroupId > 0)
                {
                    return Ok(entityDtoChangedStatus);
                }
                return new NotFoundResult();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                _logger.LogError(ex.ToString());

                Microsoft.Data.SqlClient.SqlException sqlEx = (ex.InnerException as Microsoft.Data.SqlClient.SqlException);
                if (sqlEx != null)
                {
                    if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Violation of PRIMARY/UNIQUE KEY constraint");
                    }
                    else if (sqlEx.Number == 547)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict, "Conflicted with FOREIGN KEY constraint");
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, "SQL exception number:" + sqlEx.Number);
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "DbUpdateException exception with no internal SqlException");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}