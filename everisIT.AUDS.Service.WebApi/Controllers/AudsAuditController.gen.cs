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
    /// AudsAudit Controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Filters.ValidateModel]
    public partial class AudsAuditController : ControllerBase
    {
        private readonly IAudsAuditService _audsAuditService;
        private readonly ILogger<AudsAuditController> _logger;
        /// <summary>
        /// AudsAuditController constructor
        /// </summary>
        /// <param name="audsAuditService">AudsAudit Interface Service</param>
        /// <param name="logger">logger interface</param>
        public AudsAuditController(IAudsAuditService audsAuditService,
            ILogger<AudsAuditController> logger)
        {
            _audsAuditService = audsAuditService ?? throw new ArgumentNullException(nameof(audsAuditService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a List of AudsAudit filtered
        /// </summary>
        /// <param name="filter">AudsAudit filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_audit
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsAuditService - http://scld01dfnfe01:18288/api/v1/AudsAudit
        /// </code>
        /// </remarks>
        /// <returns>audsAuditsDto: AudsAuditsDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AudsAuditDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> List([FromQuery] AudsAuditFilter filter)
        {
            try
            {
                var listEntityDto = await _audsAuditService.GetList(filter);
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get an AudsAudit by AuditId
        /// </summary>
        /// <param name="id">AudsAudit AuditId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_audit
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsAuditService - http://scld01dfnfe01:18288/api/v1/AudsAudit/1
        /// </code>
        /// </remarks>
        /// <returns>AudsAuditDto</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AudsAuditDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(AudsAuditDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDto = await _audsAuditService.Get(id);
                if (entityDto != null && entityDto.AuditId > 0)
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
        /// Create AudsAudit object
        /// </summary>
        /// <param name="entityDto">AudsAuditDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_audit
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsAuditService - http://scld01dfnfe01:18288/api/v1/AudsAudit
        /// </code>        
        /// </remarks>
        /// <returns>AudsAudit inserted</returns>
        /// <response code="201">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(typeof(AudsAuditDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AudsAuditDto entityDto)
        {
            try
            {
                if (entityDto is null)
                {
                    return new BadRequestResult();
                }
                var entityDtoInserted = await _audsAuditService.Create(entityDto);
                return CreatedAtAction(nameof(AudsAuditController.Create)
                        , nameof(AudsAuditController).Replace("Controller", ""), entityDtoInserted);
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
        /// Update AudsAudit object
        /// </summary>
        /// <param name="entityDto">AudsAuditDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_audit
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsAuditService - http://scld01dfnfe01:18288/api/v1/AudsAudit
        /// </code>        
        /// </remarks>
        /// <returns>AudsAudit updated</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        [ProducesResponseType(typeof(AudsAuditDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AudsAuditDto entityDto)
        {
            try
            {
                if (entityDto is null || entityDto.AuditId == 0)
                {
                    return new BadRequestResult();
                }
                var entityDtoUpdated = await _audsAuditService.Update(entityDto);

                if (entityDtoUpdated != null && entityDtoUpdated.AuditId > 0)
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
        /// Change AudsAudit object status by AuditId
        /// </summary>
        /// <param name="id">AudsAudit AuditId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_audit
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsAuditService - http://scld01dfnfe01:18288/api/v1/AudsAudit/1
        /// </code>
        /// </remarks>
        /// <returns>AudsAudit with inactive status</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AudsAuditDto), (int)HttpStatusCode.OK)]
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
                var entityDtoChangedStatus = await _audsAuditService.Delete(id);
                if (entityDtoChangedStatus != null && entityDtoChangedStatus.AuditId > 0)
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