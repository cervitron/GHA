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
    public partial class AudsStateTypeControllerTest
    {
        private readonly Mock<IAudsStateTypeService> _audsStateTypeService;
        private readonly Mock<ILogger<AudsStateTypeController>> _loggerMock;
        private readonly AudsStateTypeController _audsStateTypeController;
        private readonly IList<AudsStateTypeDto> _listAudsStateTypeDto;
        public AudsStateTypeControllerTest()
        {
            _audsStateTypeService = new Mock<IAudsStateTypeService>();
            _loggerMock = new Mock<ILogger<AudsStateTypeController>>();
            _audsStateTypeController = new AudsStateTypeController(_audsStateTypeService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsStateTypeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsStateTypeDto = DataTest.AudsStateTypeControllerDataTest.GetAudsStateTypeDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsStateType_Return_OK()
        {
            //Arrange
            var audsStateTypeDto = _listAudsStateTypeDto.FirstOrDefault();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).ReturnsAsync(audsStateTypeDto);
            //Action
            var actionResult = _audsStateTypeController.Create(audsStateTypeDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsStateType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsStateType_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsStateTypeController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsStateType_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsStateTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsStateType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateTypeService.Setup(x => x.Create(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsStateType_Return_OK()
        {
            //Arrange
            var filter = new AudsStateTypeFilter() { StateTypeId = 1 };
            _audsStateTypeService.Setup(x => x.GetList(It.IsAny<AudsStateTypeFilter>())).ReturnsAsync(new List<AudsStateTypeDto>() { new AudsStateTypeDto() { StateTypeId = 1 } });
            //Action
            var actionResult = _audsStateTypeController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsStateTypeDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsStateType_Return_KO()
        {
            //Arrange
            var filter = new AudsStateTypeFilter() { StateTypeId = 1 };
            _audsStateTypeService.Setup(x => x.GetList(It.IsAny<AudsStateTypeFilter>())).ReturnsAsync(new List<AudsStateTypeDto>());
            //Action
            var actionResult = _audsStateTypeController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsStateTypeDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsStateType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsStateTypeService.Setup(x => x.GetList(It.IsAny<AudsStateTypeFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateTypeController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsStateType_Return_OK()
        {
            //Arrange
            var audsStateTypeDto = new AudsStateTypeDto() { StateTypeId = 3 };
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).ReturnsAsync(_listAudsStateTypeDto.FirstOrDefault());
            //Action
            var actionResult = _audsStateTypeController.Update(audsStateTypeDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsStateType_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsStateTypeController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsStateType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsStateType_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto() { StateTypeId = 1 };;
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsStateTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsStateType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsStateTypeDto entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateTypeService.Setup(x => x.Update(It.IsAny<AudsStateTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsStateType_Return_OK()
        {
            //Arrange
            var audsStateTypeDto = new AudsStateTypeDto() { StateTypeId = 1 };
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsStateTypeDto);
            //Action
            var actionResult = _audsStateTypeController.Delete(audsStateTypeDto.StateTypeId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsStateType_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsStateTypeDto entityDto = null;
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsStateTypeController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsStateType_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsStateTypeController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsStateType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsStateType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsStateType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsStateTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsStateTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsStateType_Return_OK()
        {
            //Arrange
            var filter = new AudsStateTypeFilter() { StateTypeId = 1 };
            var audsStateTypeDto = new AudsStateTypeDto() { StateTypeId = 1 };
            _audsStateTypeService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsStateTypeDto);
            //Action
            var actionResult = _audsStateTypeController.Get((int)filter.StateTypeId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsStateTypeDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsStateType_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsStateTypeDto audsStateTypeDto = new AudsStateTypeDto();
            _audsStateTypeService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsStateTypeDto);
            //Action
            var actionResult = _audsStateTypeController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsStateType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsStateTypeService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsStateTypeController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsStateTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsStateType_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsStateTypeController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
