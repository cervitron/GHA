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
    /// AudsRisk Controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Filters.ValidateModel]
    public partial class AudsRiskController : ControllerBase
    {
        private readonly IAudsRiskService _audsRiskService;
        private readonly ILogger<AudsRiskController> _logger;

        /// <summary>
        /// AudsRiskController constructor
        /// </summary>
        /// <param name="audsRiskService">AudsRisk Interface Service</param>
        /// <param name="logRecordUtilityWrapper">Wrapper for LogRecordUtility</param>
        public AudsRiskController(IAudsRiskService audsRiskService,
            ILogger<AudsRiskController> logger)
        {
            _audsRiskService = audsRiskService ?? throw new ArgumentNullException(nameof(audsRiskService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a List of AudsRisk filtered
        /// </summary>
        /// <param name="filter">AudsRisk filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_risk
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsRiskService - http://scld01dfnfe01:18288/api/v1/AudsRisk
        /// </code>
        /// </remarks>
        /// <returns>audsRisksDto: AudsRisksDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AudsRiskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> List([FromQuery] AudsRiskFilter filter)
        {
            try
            {
                var listEntityDto = await _audsRiskService.GetList(filter);
                return Ok(listEntityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get an AudsRisk by RiskId
        /// </summary>
        /// <param name="id">AudsRisk RiskId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_risk
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsRiskService - http://scld01dfnfe01:18288/api/v1/AudsRisk/1
        /// </code>
        /// </remarks>
        /// <returns>AudsRiskDto</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AudsRiskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(AudsRiskDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDto = await _audsRiskService.Get(id);
                if (entityDto != null && entityDto.RiskId > 0)
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
        /// Create AudsRisk object
        /// </summary>
        /// <param name="entityDto">AudsRiskDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_risk
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsRiskService - http://scld01dfnfe01:18288/api/v1/AudsRisk
        /// </code>        
        /// </remarks>
        /// <returns>AudsRisk inserted</returns>
        /// <response code="201">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(typeof(AudsRiskDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AudsRiskDto entityDto)
        {
            try
            {
                if (entityDto is null)
                {
                    return new BadRequestResult();
                }
                var entityDtoInserted = await _audsRiskService.Create(entityDto);
                return CreatedAtAction(nameof(AudsRiskController.Create)
                        , nameof(AudsRiskController).Replace("Controller", ""), entityDtoInserted);
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
        /// Update AudsRisk object
        /// </summary>
        /// <param name="entityDto">AudsRiskDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_risk
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsRiskService - http://scld01dfnfe01:18288/api/v1/AudsRisk
        /// </code>        
        /// </remarks>
        /// <returns>AudsRisk updated</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        [ProducesResponseType(typeof(AudsRiskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AudsRiskDto entityDto)
        {
            try
            {
                if (entityDto is null || entityDto.RiskId == 0)
                {
                    return new BadRequestResult();
                }
                var entityDtoUpdated = await _audsRiskService.Update(entityDto);

                if (entityDtoUpdated != null && entityDtoUpdated.RiskId > 0)
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
        /// Change AudsRisk object status by RiskId
        /// </summary>
        /// <param name="id">AudsRisk RiskId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_risk
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsRiskService - http://scld01dfnfe01:18288/api/v1/AudsRisk/1
        /// </code>
        /// </remarks>
        /// <returns>AudsRisk with inactive status</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AudsRiskDto), (int)HttpStatusCode.OK)]
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
                var entityDtoChangedStatus = await _audsRiskService.Delete(id);
                if (entityDtoChangedStatus != null && entityDtoChangedStatus.RiskId > 0)
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