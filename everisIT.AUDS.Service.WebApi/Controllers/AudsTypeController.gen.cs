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
    public partial class AudsTypeController : ControllerBase
    {
        private readonly IAudsTypeService _audsTypeService;
        private readonly ILogger<AudsTypeController> _logger;

        /// <summary>
        /// AudsTypeController constructor
        /// </summary>
        /// <param name="audsTypeService">AudsType Interface Service</param>
        /// <param name="logger">logger interface</param>
        public AudsTypeController(IAudsTypeService audsTypeService,
            ILogger<AudsTypeController> logger)
        {
            _audsTypeService = audsTypeService ?? throw new ArgumentNullException(nameof(audsTypeService));
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
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/AudsType
        /// </code>
        /// </remarks>
        /// <returns>audsTypesDto: AudsTypesDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AudsTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> List([FromQuery] AudsTypeFilter filter)
         {
            try
            {
                var listEntityDto = await _audsTypeService.GetList(filter);
				return Ok(listEntityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get an AudsType by IdType
        /// </summary>
        /// <param name="id">AudsType IdType</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/AudsType/1
        /// </code>
        /// </remarks>
        /// <returns>AudsTypeDto</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AudsTypeDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(AudsTypeDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {                
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDto = await _audsTypeService.Get(id);
                if (entityDto != null && entityDto.IdType > 0)
                {
                    return Ok(entityDto);
                }
                return new  NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create AudsType object
        /// </summary>
        /// <param name="entityDto">AudsTypeDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/AudsType
        /// </code>        
        /// </remarks>
        /// <returns>AudsType inserted</returns>
        /// <response code="201">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(typeof(AudsTypeDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AudsTypeDto entityDto)
        {
            try
            {                
                if (entityDto is null)
                {
                    return new BadRequestResult();
                }
                var entityDtoInserted = await _audsTypeService.Create(entityDto);
				return CreatedAtAction(nameof(AudsTypeController.Create)
                        , nameof(AudsTypeController).Replace("Controller", ""), entityDtoInserted);
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
        /// Update AudsType object
        /// </summary>
        /// <param name="entityDto">AudsTypeDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/AudsType
        /// </code>        
        /// </remarks>
        /// <returns>AudsType updated</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        [ProducesResponseType(typeof(AudsTypeDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AudsTypeDto entityDto)
        {
            try
            {                
                if (entityDto is null || entityDto.IdType == 0)
                {
                    return new BadRequestResult();
                }
                var entityDtoUpdated = await _audsTypeService.Update(entityDto);

                if (entityDtoUpdated != null && entityDtoUpdated.IdType > 0)
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
        /// Change AudsType object status by IdType
        /// </summary>
        /// <param name="id">AudsType IdType</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_type
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTypeService - http://scld01dfnfe01:18288/api/v1/AudsType/1
        /// </code>
        /// </remarks>
        /// <returns>AudsType with inactive status</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AudsTypeDto), (int)HttpStatusCode.OK)]
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
                var entityDtoChangedStatus = await _audsTypeService.Delete(id);
				if (entityDtoChangedStatus != null && entityDtoChangedStatus.IdType > 0)
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