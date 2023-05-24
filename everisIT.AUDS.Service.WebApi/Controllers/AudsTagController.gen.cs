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
    /// AudsTag Controller
    /// </summary>
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [ApiController]
    [Filters.ValidateModel]
    public partial class AudsTagController : ControllerBase
    {
        private readonly IAudsTagService _audsTagService;
        private readonly ILogger<AudsTagController> _logger;

        /// <summary>
        /// AudsTagController constructor
        /// </summary>
        /// <param name="audsTagService">AudsTag Interface Service</param>
        /// <param name="logger">logger interface</param>
        public AudsTagController(IAudsTagService audsTagService,
            ILogger<AudsTagController> logger)
        {
            _audsTagService = audsTagService ?? throw new ArgumentNullException(nameof(audsTagService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get a List of AudsTag filtered
        /// </summary>
        /// <param name="filter">AudsTag filter</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_tag
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTagService - http://scld01dfnfe01:18288/api/v1/AudsTag
        /// </code>
        /// </remarks>
        /// <returns>audsTagsDto: AudsTagsDto List</returns>
        /// <response code="200">Value returned</response>
        /// <response code="204">No Content</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<AudsTagDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> List([FromQuery] AudsTagFilter filter)
         {
            try
            {
                var listEntityDto = await _audsTagService.GetList(filter);
				return Ok(listEntityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get an AudsTag by TagId
        /// </summary>
        /// <param name="id">AudsTag TagId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_tag
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTagService - http://scld01dfnfe01:18288/api/v1/AudsTag/1
        /// </code>
        /// </remarks>
        /// <returns>AudsTagDto</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AudsTagDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(AudsTagDto), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {                
                if (id < 1)
                {
                    return new BadRequestResult();
                }
                var entityDto = await _audsTagService.Get(id);
                if (entityDto != null && entityDto.TagId > 0)
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
        /// Create AudsTag object
        /// </summary>
        /// <param name="entityDto">AudsTagDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_tag
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTagService - http://scld01dfnfe01:18288/api/v1/AudsTag
        /// </code>        
        /// </remarks>
        /// <returns>AudsTag inserted</returns>
        /// <response code="201">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPost]
        [ProducesResponseType(typeof(AudsTagDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AudsTagDto entityDto)
        {
            try
            {                
                if (entityDto is null)
                {
                    return new BadRequestResult();
                }
                var entityDtoInserted = await _audsTagService.Create(entityDto);
				return CreatedAtAction(nameof(AudsTagController.Create)
                        , nameof(AudsTagController).Replace("Controller", ""), entityDtoInserted);
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
        /// Update AudsTag object
        /// </summary>
        /// <param name="entityDto">AudsTagDto object</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_tag
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTagService - http://scld01dfnfe01:18288/api/v1/AudsTag
        /// </code>        
        /// </remarks>
        /// <returns>AudsTag updated</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="409">Conflict error</response>
        /// <response code="415">UnsupportedMediaType</response>
        /// <response code="500">Internal error</response>
        [HttpPut]
        [ProducesResponseType(typeof(AudsTagDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.UnsupportedMediaType)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] AudsTagDto entityDto)
        {
            try
            {                
                if (entityDto is null || entityDto.TagId == 0)
                {
                    return new BadRequestResult();
                }
                var entityDtoUpdated = await _audsTagService.Update(entityDto);

                if (entityDtoUpdated != null && entityDtoUpdated.TagId > 0)
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
        /// Change AudsTag object status by TagId
        /// </summary>
        /// <param name="id">AudsTag TagId</param>
        /// <remarks>
        /// ###BBDD
        /// <code>
        /// **AUDS** - auds_tag
        /// </code>
        /// ###SERVICE INVOKE
        /// <code>
        /// **SYSTEM** - AudsTagService - http://scld01dfnfe01:18288/api/v1/AudsTag/1
        /// </code>
        /// </remarks>
        /// <returns>AudsTag with inactive status</returns>
        /// <response code="200">Value returned</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not found error</response>
        /// <response code="500">Internal error</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(AudsTagDto), (int)HttpStatusCode.OK)]
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
                var entityDtoChangedStatus = await _audsTagService.Delete(id);
				if (entityDtoChangedStatus != null && entityDtoChangedStatus.TagId > 0)
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