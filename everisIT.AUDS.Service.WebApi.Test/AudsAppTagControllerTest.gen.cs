using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.WebApi.Controllers;
using everisIT.AUDS.Service.WebApi.Test.DataUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public partial class AudsAppTagControllerTest
    {
        private readonly Mock<IAudsAppTagService> _audsAppTagService;
        private readonly Mock<ILogger<AudsAppTagController>> _loggerMock;
        private readonly AudsAppTagController _audsAppTagController;
        private readonly IList<AudsAppTagDto> _listAudsAppTagDto;
        public AudsAppTagControllerTest()
        {
            _audsAppTagService = new Mock<IAudsAppTagService>();
            _loggerMock = new Mock<ILogger<AudsAppTagController>>();
            _audsAppTagController = new AudsAppTagController(_audsAppTagService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsAppTagController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsAppTagDto = DataTest.AudsAppTagControllerDataTest.GetAudsAppTagDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsAppTag_Return_OK()
        {
            //Arrange
            var audsAppTagDto = _listAudsAppTagDto.FirstOrDefault();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).ReturnsAsync(audsAppTagDto);
            //Action
            var actionResult = _audsAppTagController.Create(audsAppTagDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsAppTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAppTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsAppTag_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsAppTagController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAppTag_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAppTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsAppTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAppTagService.Setup(x => x.Create(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsAppTag_Return_OK()
        {
            //Arrange
            var filter = new AudsAppTagFilter() { ApplicationId = 1 };
            _audsAppTagService.Setup(x => x.GetList(It.IsAny<AudsAppTagFilter>())).ReturnsAsync(new List<AudsAppTagDto>() { new AudsAppTagDto() { ApplicationId = 1 } });
            //Action
            var actionResult = _audsAppTagController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAppTagDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsAppTag_Return_KO()
        {
            //Arrange
            var filter = new AudsAppTagFilter() { ApplicationId = 1 };
            _audsAppTagService.Setup(x => x.GetList(It.IsAny<AudsAppTagFilter>())).ReturnsAsync(new List<AudsAppTagDto>());
            //Action
            var actionResult = _audsAppTagController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsAppTagDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsAppTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsAppTagService.Setup(x => x.GetList(It.IsAny<AudsAppTagFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsAppTagController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsAppTag_Return_OK()
        {
            //Arrange
            var audsAppTagDto = new AudsAppTagDto() { ApplicationId = 3 };
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).ReturnsAsync(_listAudsAppTagDto.FirstOrDefault());
            //Action
            var actionResult = _audsAppTagController.Update(audsAppTagDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsAppTag_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsAppTagController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsAppTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsAppTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAppTag_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto() { ApplicationId = 1 };;
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsAppTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsAppTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsAppTagDto entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAppTagService.Setup(x => x.Update(It.IsAny<AudsAppTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsAppTag_Return_OK()
        {
            //Arrange
            var audsAppTagDto = new AudsAppTagDto() { ApplicationId = 1 };
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsAppTagDto);
            //Action
            var actionResult = _audsAppTagController.Delete(audsAppTagDto.ApplicationId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAppTag_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsAppTagDto entityDto = null;
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsAppTagController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAppTag_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsAppTagController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsAppTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAppTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAppTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsAppTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsAppTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsAppTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsAppTag_Return_OK()
        {
            //Arrange
            var filter = new AudsAppTagFilter() { ApplicationId = 1 };
            var audsAppTagDto = new AudsAppTagDto() { ApplicationId = 1 };
            _audsAppTagService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAppTagDto);
            //Action
            var actionResult = _audsAppTagController.Get((int)filter.ApplicationId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsAppTagDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAppTag_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsAppTagDto audsAppTagDto = new AudsAppTagDto();
            _audsAppTagService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsAppTagDto);
            //Action
            var actionResult = _audsAppTagController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsAppTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsAppTagService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsAppTagController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsAppTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsAppTag_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsAppTagController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
