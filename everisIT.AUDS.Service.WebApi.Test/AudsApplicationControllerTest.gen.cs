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
    public partial class AudsApplicationControllerTest
    {
        private readonly Mock<IAudsApplicationService> _audsApplicationService;

        private readonly Mock<ILogger<AudsApplicationController>> _loggerMock;

        private readonly AudsApplicationController _audsApplicationController;
        private readonly IList<AudsApplicationDto> _listAudsApplicationDto;
        public AudsApplicationControllerTest()
        {
            _audsApplicationService = new Mock<IAudsApplicationService>();

            _loggerMock = new Mock<ILogger<AudsApplicationController>>();
            _audsApplicationController = new AudsApplicationController(_audsApplicationService.Object, _loggerMock.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsApplicationController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsApplicationDto = DataTest.AudsApplicationControllerDataTest.GetAudsApplicationDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsApplication_Return_OK()
        {
            //Arrange
            var audsApplicationDto = _listAudsApplicationDto.FirstOrDefault();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).ReturnsAsync(audsApplicationDto);
            //Action
            var actionResult = _audsApplicationController.Create(audsApplicationDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsApplication_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsApplicationController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsApplication_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsApplicationController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsApplication_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsApplicationController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsApplication_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsApplicationService.Setup(x => x.Create(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsApplication_Return_OK()
        {
            //Arrange
            var filter = new AudsApplicationFilter() { ApplicationId = 1 };
            _audsApplicationService.Setup(x => x.GetList(It.IsAny<AudsApplicationFilter>())).ReturnsAsync(new List<AudsApplicationDto>() { new AudsApplicationDto() { ApplicationId = 1 } });
            //Action
            var actionResult = _audsApplicationController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsApplicationDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsApplication_Return_KO()
        {
            //Arrange
            var filter = new AudsApplicationFilter() { ApplicationId = 1 };
            _audsApplicationService.Setup(x => x.GetList(It.IsAny<AudsApplicationFilter>())).ReturnsAsync(new List<AudsApplicationDto>());
            //Action
            var actionResult = _audsApplicationController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsApplicationDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsApplication_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsApplicationService.Setup(x => x.GetList(It.IsAny<AudsApplicationFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsApplicationController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsApplication_Return_OK()
        {
            //Arrange
            var audsApplicationDto = new AudsApplicationDto() { ApplicationId = 3 };
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).ReturnsAsync(_listAudsApplicationDto.FirstOrDefault());
            //Action
            var actionResult = _audsApplicationController.Update(audsApplicationDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsApplication_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsApplicationController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsApplication_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsApplicationController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsApplication_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto() { ApplicationId = 1 };;
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsApplicationController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsApplication_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsApplicationDto entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsApplicationService.Setup(x => x.Update(It.IsAny<AudsApplicationDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsApplication_Return_OK()
        {
            //Arrange
            var audsApplicationDto = new AudsApplicationDto() { ApplicationId = 1 };
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsApplicationDto);
            //Action
            var actionResult = _audsApplicationController.Delete(audsApplicationDto.ApplicationId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsApplication_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsApplicationDto entityDto = null;
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsApplicationController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsApplication_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsApplicationController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsApplication_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsApplicationController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsApplication_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsApplication_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsApplicationService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsApplicationController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsApplication_Return_OK()
        {
            //Arrange
            var filter = new AudsApplicationFilter() { ApplicationId = 1 };
            var audsApplicationDto = new AudsApplicationDto() { ApplicationId = 1 };
            _audsApplicationService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsApplicationDto);
            //Action
            var actionResult = _audsApplicationController.Get((int)filter.ApplicationId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsApplicationDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsApplication_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsApplicationDto audsApplicationDto = new AudsApplicationDto();
            _audsApplicationService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsApplicationDto);
            //Action
            var actionResult = _audsApplicationController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsApplication_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsApplicationService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsApplicationController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsApplicationController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsApplication_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsApplicationController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
