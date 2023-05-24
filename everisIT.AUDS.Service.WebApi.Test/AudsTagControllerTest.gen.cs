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
    public partial class AudsTagControllerTest
    {
        private readonly Mock<IAudsTagService> _audsTagService;
        private readonly Mock<ILogger<AudsTagController>> _loggerMock;
        private readonly AudsTagController _audsTagController;
        private readonly IList<AudsTagDto> _listAudsTagDto;
        public AudsTagControllerTest()
        {
            _audsTagService = new Mock<IAudsTagService>();
            _loggerMock = new Mock<ILogger<AudsTagController>>();
            _audsTagController = new AudsTagController(_audsTagService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsTagController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsTagDto = DataTest.AudsTagControllerDataTest.GetAudsTagDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsTag_Return_OK()
        {
            //Arrange
            var audsTagDto = _listAudsTagDto.FirstOrDefault();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).ReturnsAsync(audsTagDto);
            //Action
            var actionResult = _audsTagController.Create(audsTagDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsTag_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsTagController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsTag_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTagService.Setup(x => x.Create(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsTag_Return_OK()
        {
            //Arrange
            var filter = new AudsTagFilter() { TagId = 1 };
            _audsTagService.Setup(x => x.GetList(It.IsAny<AudsTagFilter>())).ReturnsAsync(new List<AudsTagDto>() { new AudsTagDto() { TagId = 1 } });
            //Action
            var actionResult = _audsTagController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsTagDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsTag_Return_KO()
        {
            //Arrange
            var filter = new AudsTagFilter() { TagId = 1 };
            _audsTagService.Setup(x => x.GetList(It.IsAny<AudsTagFilter>())).ReturnsAsync(new List<AudsTagDto>());
            //Action
            var actionResult = _audsTagController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsTagDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsTagService.Setup(x => x.GetList(It.IsAny<AudsTagFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsTagController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsTag_Return_OK()
        {
            //Arrange
            var audsTagDto = new AudsTagDto() { TagId = 3 };
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).ReturnsAsync(_listAudsTagDto.FirstOrDefault());
            //Action
            var actionResult = _audsTagController.Update(audsTagDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsTag_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsTagController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto() { TagId = 1 };
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsTag_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto() { TagId = 1 };;
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto() { TagId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto() { TagId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsTagDto entityDto = new AudsTagDto() { TagId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTagService.Setup(x => x.Update(It.IsAny<AudsTagDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsTag_Return_OK()
        {
            //Arrange
            var audsTagDto = new AudsTagDto() { TagId = 1 };
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsTagDto);
            //Action
            var actionResult = _audsTagController.Delete(audsTagDto.TagId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsTag_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsTagDto entityDto = null;
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsTagController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsTag_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsTagController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsTag_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsTag_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTagService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTagController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsTag_Return_OK()
        {
            //Arrange
            var filter = new AudsTagFilter() { TagId = 1 };
            var audsTagDto = new AudsTagDto() { TagId = 1 };
            _audsTagService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsTagDto);
            //Action
            var actionResult = _audsTagController.Get((int)filter.TagId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsTagDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsTag_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsTagDto audsTagDto = new AudsTagDto();
            _audsTagService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsTagDto);
            //Action
            var actionResult = _audsTagController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsTag_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsTagService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsTagController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTagController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsTag_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsTagController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
