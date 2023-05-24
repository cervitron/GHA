using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.WebApi.Controllers;
using everisIT.AUDS.Service.WebApi.Test.DataUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public partial class AudsAuditControllerTest
    {
        private readonly Mock<IAudsAuditService> _audsAuditService;
        private readonly Mock<ILogger<AudsAuditController>> _loggerMock;
        private readonly AudsAuditController _audsAuditController;
        private readonly IList<AudsAuditDto> _listAudsAuditDto;
        public AudsAuditControllerTest()
        {
            _audsAuditService = new Mock<IAudsAuditService>();
            _loggerMock = new Mock<ILogger<AudsAuditController>>();
            _audsAuditController = new AudsAuditController(_audsAuditService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsAuditController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsAuditDto = DataTest.AudsAuditControllerDataTest.GetAudsAuditDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsAudit_Return_OK()
        {
            //Arrange
            var audsAuditDto = _listAudsAuditDto.FirstOrDefault();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).ReturnsAsync(audsAuditDto);
            //Action
            var actionResult = _audsAuditController.Create(audsAuditDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsAudit_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsAudit_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsAuditController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAudit_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAuditController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAudit_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditService.Setup(x => x.Create(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsAudit_Return_OK()
        {
            //Arrange
            var filter = new AudsAuditFilter() { AuditId = 1 };
            _audsAuditService.Setup(x => x.GetList(It.IsAny<AudsAuditFilter>())).ReturnsAsync(new List<AudsAuditDto>() { new AudsAuditDto() { AuditId = 1 } });
            //Action
            var actionResult = _audsAuditController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAuditDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsAudit_Return_KO()
        {
            //Arrange
            var filter = new AudsAuditFilter() { AuditId = 1 };
            _audsAuditService.Setup(x => x.GetList(It.IsAny<AudsAuditFilter>())).ReturnsAsync(new List<AudsAuditDto>());
            //Action
            var actionResult = _audsAuditController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAuditDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsAudit_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsAuditService.Setup(x => x.GetList(It.IsAny<AudsAuditFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsAudit_Return_OK()
        {
            //Arrange
            var audsAuditDto = new AudsAuditDto() { AuditId = 3 };
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).ReturnsAsync(_listAudsAuditDto.FirstOrDefault());
            //Action
            var actionResult = _audsAuditController.Update(audsAuditDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsAudit_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsAuditController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsAudit_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto() { AuditId = 1 };
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAudit_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto() { AuditId = 1 };;
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAuditController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto() { AuditId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto() { AuditId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAudit_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAuditDto entityDto = new AudsAuditDto() { AuditId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditService.Setup(x => x.Update(It.IsAny<AudsAuditDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsAudit_Return_OK()
        {
            //Arrange
            var audsAuditDto = new AudsAuditDto() { AuditId = 1 };
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsAuditDto);
            //Action
            var actionResult = _audsAuditController.Delete(audsAuditDto.AuditId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAudit_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsAuditDto entityDto = null;
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsAuditController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAudit_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsAuditController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsAudit_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAudit_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAudit_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAuditService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAuditController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsAudit_Return_OK()
        {
            //Arrange
            var filter = new AudsAuditFilter() { AuditId = 1 };
            var audsAuditDto = new AudsAuditDto() { AuditId = 1 };
            _audsAuditService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAuditDto);
            //Action
            var actionResult = _audsAuditController.Get((int)filter.AuditId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsAuditDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAudit_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsAuditDto audsAuditDto = new AudsAuditDto();
            _audsAuditService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAuditDto);
            //Action
            var actionResult = _audsAuditController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsAudit_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAuditService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAuditController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAuditController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAudit_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsAuditController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
