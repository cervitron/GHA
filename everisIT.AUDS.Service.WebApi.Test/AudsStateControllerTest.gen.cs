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
    public partial class AudsStateControllerTest
    {
        private readonly Mock<IAudsStateService> _audsStateService;
        private readonly Mock<ILogger<AudsStateController>> _loggerMock;
        private readonly AudsStateController _audsStateController;
        private readonly IList<AudsStateDto> _listAudsStateDto;
        public AudsStateControllerTest()
        {
            _audsStateService = new Mock<IAudsStateService>();
            _loggerMock = new Mock<ILogger<AudsStateController>>();
            _audsStateController = new AudsStateController(_audsStateService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsStateController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsStateDto = DataTest.AudsStateControllerDataTest.GetAudsStateDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsState_Return_OK()
        {
            //Arrange
            var audsStateDto = _listAudsStateDto.FirstOrDefault();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).ReturnsAsync(audsStateDto);
            //Action
            var actionResult = _audsStateController.Create(audsStateDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsState_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsState_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsStateController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsState_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsStateController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsState_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsState_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsState_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateService.Setup(x => x.Create(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsState_Return_OK()
        {
            //Arrange
            var filter = new AudsStateFilter() { StateId = 1 };
            _audsStateService.Setup(x => x.GetList(It.IsAny<AudsStateFilter>())).ReturnsAsync(new List<AudsStateDto>() { new AudsStateDto() { StateId = 1 } });
            //Action
            var actionResult = _audsStateController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsStateDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsState_Return_KO()
        {
            //Arrange
            var filter = new AudsStateFilter() { StateId = 1 };
            _audsStateService.Setup(x => x.GetList(It.IsAny<AudsStateFilter>())).ReturnsAsync(new List<AudsStateDto>());
            //Action
            var actionResult = _audsStateController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsStateDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsState_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsStateService.Setup(x => x.GetList(It.IsAny<AudsStateFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsState_Return_OK()
        {
            //Arrange
            var audsStateDto = new AudsStateDto() { StateId = 3 };
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).ReturnsAsync(_listAudsStateDto.FirstOrDefault());
            //Action
            var actionResult = _audsStateController.Update(audsStateDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsState_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsStateController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsState_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto() { StateId = 1 };
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsState_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto() { StateId = 1 };;
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsStateController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsState_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto() { StateId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsState_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto() { StateId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsState_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsStateDto entityDto = new AudsStateDto() { StateId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateService.Setup(x => x.Update(It.IsAny<AudsStateDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsState_Return_OK()
        {
            //Arrange
            var audsStateDto = new AudsStateDto() { StateId = 1 };
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsStateDto);
            //Action
            var actionResult = _audsStateController.Delete(audsStateDto.StateId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsState_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsStateDto entityDto = null;
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsStateController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsState_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsStateController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsState_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsState_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsState_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsState_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsState_Return_OK()
        {
            //Arrange
            var filter = new AudsStateFilter() { StateId = 1 };
            var audsStateDto = new AudsStateDto() { StateId = 1 };
            _audsStateService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsStateDto);
            //Action
            var actionResult = _audsStateController.Get((int)filter.StateId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsStateDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsState_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsStateDto audsStateDto = new AudsStateDto();
            _audsStateService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsStateDto);
            //Action
            var actionResult = _audsStateController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsState_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsStateService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsState_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsStateController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
