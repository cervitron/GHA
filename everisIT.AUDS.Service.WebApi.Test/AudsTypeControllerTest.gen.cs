using Microsoft.Extensions.Logging;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.WebApi.Controllers;
using everisIT.AUDS.Service.WebApi.Test.DataUtil;
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
    public partial class AudsTypeControllerTest
    {
        private readonly Mock<IAudsTypeService> _audsTypeService;
        private readonly AudsTypeController _audsTypeController;
        private readonly Mock<ILogger<AudsTypeController>> _loggerMock;
        private readonly IList<AudsTypeDto> _listAudsTypeDto;
        public AudsTypeControllerTest()
        {
            _audsTypeService = new Mock<IAudsTypeService>();
            _loggerMock = new Mock<ILogger<AudsTypeController>>();
            _audsTypeController = new AudsTypeController(_audsTypeService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsTypeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsTypeDto = DataTest.AudsTypeControllerDataTest.GetAudsTypeDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsType_Return_OK()
        {
            //Arrange
            var audsTypeDto = _listAudsTypeDto.FirstOrDefault();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).ReturnsAsync(audsTypeDto);
            //Action
            var actionResult = _audsTypeController.Create(audsTypeDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsType_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsTypeController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsType_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTypeService.Setup(x => x.Create(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsType_Return_OK()
        {
            //Arrange
            var filter = new AudsTypeFilter() { IdType = 1 };
            _audsTypeService.Setup(x => x.GetList(It.IsAny<AudsTypeFilter>())).ReturnsAsync(new List<AudsTypeDto>() { new AudsTypeDto() { IdType = 1 } });
            //Action
            var actionResult = _audsTypeController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsTypeDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsType_Return_KO()
        {
            //Arrange
            var filter = new AudsTypeFilter() { IdType = 1 };
            _audsTypeService.Setup(x => x.GetList(It.IsAny<AudsTypeFilter>())).ReturnsAsync(new List<AudsTypeDto>());
            //Action
            var actionResult = _audsTypeController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsTypeDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsTypeService.Setup(x => x.GetList(It.IsAny<AudsTypeFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsTypeController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsType_Return_OK()
        {
            //Arrange
            var audsTypeDto = new AudsTypeDto() { IdType = 3 };
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).ReturnsAsync(_listAudsTypeDto.FirstOrDefault());
            //Action
            var actionResult = _audsTypeController.Update(audsTypeDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsType_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsTypeController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto() { IdType = 1 };
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsType_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto() { IdType = 1 };;
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto() { IdType = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto() { IdType = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsTypeDto entityDto = new AudsTypeDto() { IdType = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTypeService.Setup(x => x.Update(It.IsAny<AudsTypeDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsType_Return_OK()
        {
            //Arrange
            var audsTypeDto = new AudsTypeDto() { IdType = 1 };
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsTypeDto);
            //Action
            var actionResult = _audsTypeController.Delete(audsTypeDto.IdType).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsType_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsTypeDto entityDto = null;
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsTypeController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsType_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsTypeController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsType_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsType_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsType_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsTypeService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsTypeController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsType_Return_OK()
        {
            //Arrange
            var filter = new AudsTypeFilter() { IdType = 1 };
            var audsTypeDto = new AudsTypeDto() { IdType = 1 };
            _audsTypeService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsTypeDto);
            //Action
            var actionResult = _audsTypeController.Get((int)filter.IdType).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsTypeDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsType_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsTypeDto audsTypeDto = new AudsTypeDto();
            _audsTypeService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsTypeDto);
            //Action
            var actionResult = _audsTypeController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsType_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsTypeService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsTypeController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsTypeController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsType_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsTypeController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
