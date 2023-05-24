using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.WebApi.Controllers;
using everisIT.AUDS.Service.WebApi.Test.DataUtil;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Xunit;

namespace everisIT.AUDS.Service.WebApi.Test
{
    [Collection("Sequential")]
    public partial class AudsAuditHcoControllerTest
    {
        private readonly Mock<IAudsAuditHcoService> _audsAuditHcoService;
        private readonly Mock<ILogger<AudsAuditHcoController>> _loggerMock;
        private readonly AudsAuditHcoController _audsAuditHcoController;
        private readonly IList<AudsAuditHcoDto> _listAudsAuditHcoDto;
        public AudsAuditHcoControllerTest()
        {
            _audsAuditHcoService = new Mock<IAudsAuditHcoService>();
            _loggerMock = new Mock<ILogger<AudsAuditHcoController>>();
            _audsAuditHcoController = new AudsAuditHcoController(_audsAuditHcoService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsAuditHcoController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsAuditHcoDto = DataTest.AudsAuditHcoControllerDataTest.GetAudsAuditHcoDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsAuditHco_Return_OK()
        {
            //Arrange
            var audsAuditHcoDto = _listAudsAuditHcoDto.FirstOrDefault();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).ReturnsAsync(audsAuditHcoDto);
            //Action
            var actionResult = _audsAuditHcoController.Create(audsAuditHcoDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsAuditHco_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditHcoController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsAuditHco_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsAuditHcoController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAuditHco_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAuditHcoController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAuditHco_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditHcoService.Setup(x => x.Create(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsAuditHco_Return_OK()
        {
            //Arrange
            var filter = new AudsAuditHcoFilter() { AuditHcoId = 1 };
            _audsAuditHcoService.Setup(x => x.GetList(It.IsAny<AudsAuditHcoFilter>())).ReturnsAsync(new List<AudsAuditHcoDto>() { new AudsAuditHcoDto() { AuditHcoId = 1 } });
            //Action
            var actionResult = _audsAuditHcoController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAuditHcoDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsAuditHco_Return_KO()
        {
            //Arrange
            var filter = new AudsAuditHcoFilter() { AuditHcoId = 1 };
            _audsAuditHcoService.Setup(x => x.GetList(It.IsAny<AudsAuditHcoFilter>())).ReturnsAsync(new List<AudsAuditHcoDto>());
            //Action
            var actionResult = _audsAuditHcoController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAuditHcoDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsAuditHco_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsAuditHcoService.Setup(x => x.GetList(It.IsAny<AudsAuditHcoFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditHcoController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsAuditHco_Return_OK()
        {
            //Arrange
            var audsAuditHcoDto = new AudsAuditHcoDto() { AuditHcoId = 3 };
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).ReturnsAsync(_listAudsAuditHcoDto.FirstOrDefault());
            //Action
            var actionResult = _audsAuditHcoController.Update(audsAuditHcoDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsAuditHco_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsAuditHcoController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsAuditHco_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditHcoController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAuditHco_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };;
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAuditHcoController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAuditHco_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAuditHcoDto entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditHcoService.Setup(x => x.Update(It.IsAny<AudsAuditHcoDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsAuditHco_Return_OK()
        {
            //Arrange
            var audsAuditHcoDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsAuditHcoDto);
            //Action
            var actionResult = _audsAuditHcoController.Delete(audsAuditHcoDto.AuditHcoId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAuditHco_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsAuditHcoDto entityDto = null;
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsAuditHcoController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAuditHco_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsAuditHcoController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsAuditHco_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditHcoController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAuditHco_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAuditHco_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditHcoService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditHcoController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsAuditHco_Return_OK()
        {
            //Arrange
            var filter = new AudsAuditHcoFilter() { AuditHcoId = 1 };
            var audsAuditHcoDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            _audsAuditHcoService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAuditHcoDto);
            //Action
            var actionResult = _audsAuditHcoController.Get((int)filter.AuditHcoId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsAuditHcoDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAuditHco_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsAuditHcoDto audsAuditHcoDto = new AudsAuditHcoDto();
            _audsAuditHcoService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAuditHcoDto);
            //Action
            var actionResult = _audsAuditHcoController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsAuditHco_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAuditHcoService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditHcoController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditHcoController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAuditHco_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsAuditHcoController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
