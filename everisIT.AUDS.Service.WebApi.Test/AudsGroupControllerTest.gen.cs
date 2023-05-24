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
    public partial class AudsGroupControllerTest
    {
        private readonly Mock<IAudsGroupService> _audsGroupService;
        private readonly Mock<ILogger<AudsGroupController>> _loggerMock;
        private readonly AudsGroupController _audsGroupController;
        private readonly IList<AudsGroupDto> _listAudsGroupDto;
        public AudsGroupControllerTest()
        {
            _audsGroupService = new Mock<IAudsGroupService>();
            _loggerMock = new Mock<ILogger<AudsGroupController>>();
            _audsGroupController = new AudsGroupController(_audsGroupService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsGroupController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsGroupDto = DataTest.AudsGroupControllerDataTest.GetAudsGroupDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsGroup_Return_OK()
        {
            //Arrange
            var audsGroupDto = _listAudsGroupDto.FirstOrDefault();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).ReturnsAsync(audsGroupDto);
            //Action
            var actionResult = _audsGroupController.Create(audsGroupDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsGroup_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsGroupController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsGroup_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsGroupController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsGroup_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsGroupController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsGroup_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsGroupService.Setup(x => x.Create(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsGroup_Return_OK()
        {
            //Arrange
            var filter = new AudsGroupFilter() { GroupId = 1 };
            _audsGroupService.Setup(x => x.GetList(It.IsAny<AudsGroupFilter>())).ReturnsAsync(new List<AudsGroupDto>() { new AudsGroupDto() { GroupId = 1 } });
            //Action
            var actionResult = _audsGroupController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsGroupDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsGroup_Return_KO()
        {
            //Arrange
            var filter = new AudsGroupFilter() { GroupId = 1 };
            _audsGroupService.Setup(x => x.GetList(It.IsAny<AudsGroupFilter>())).ReturnsAsync(new List<AudsGroupDto>());
            //Action
            var actionResult = _audsGroupController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsGroupDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsGroup_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsGroupService.Setup(x => x.GetList(It.IsAny<AudsGroupFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsGroupController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsGroup_Return_OK()
        {
            //Arrange
            var audsGroupDto = new AudsGroupDto() { GroupId = 3 };
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).ReturnsAsync(_listAudsGroupDto.FirstOrDefault());
            //Action
            var actionResult = _audsGroupController.Update(audsGroupDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsGroup_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsGroupController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsGroup_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto() { GroupId = 1 };
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsGroupController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsGroup_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto() { GroupId = 1 };;
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsGroupController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto() { GroupId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto() { GroupId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsGroup_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsGroupDto entityDto = new AudsGroupDto() { GroupId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsGroupService.Setup(x => x.Update(It.IsAny<AudsGroupDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsGroup_Return_OK()
        {
            //Arrange
            var audsGroupDto = new AudsGroupDto() { GroupId = 1 };
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsGroupDto);
            //Action
            var actionResult = _audsGroupController.Delete(audsGroupDto.GroupId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsGroup_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsGroupDto entityDto = null;
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsGroupController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsGroup_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsGroupController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsGroup_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsGroupController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsGroup_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsGroup_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsGroupService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsGroupController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsGroup_Return_OK()
        {
            //Arrange
            var filter = new AudsGroupFilter() { GroupId = 1 };
            var audsGroupDto = new AudsGroupDto() { GroupId = 1 };
            _audsGroupService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsGroupDto);
            //Action
            var actionResult = _audsGroupController.Get((int)filter.GroupId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsGroupDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsGroup_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsGroupDto audsGroupDto = new AudsGroupDto();
            _audsGroupService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsGroupDto);
            //Action
            var actionResult = _audsGroupController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsGroup_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsGroupService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsGroupController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsGroupController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsGroup_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsGroupController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
