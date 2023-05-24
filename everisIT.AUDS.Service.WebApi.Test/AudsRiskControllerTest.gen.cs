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
    public partial class AudsRiskControllerTest
    {
        private readonly Mock<IAudsRiskService> _audsRiskService;
        private readonly Mock<ILogger<AudsRiskController>> _loggerMock;
        private readonly AudsRiskController _audsRiskController;
        private readonly IList<AudsRiskDto> _listAudsRiskDto;
        public AudsRiskControllerTest()
        {
            _audsRiskService = new Mock<IAudsRiskService>();
            _loggerMock = new Mock<ILogger<AudsRiskController>>();
            _audsRiskController = new AudsRiskController(_audsRiskService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsRiskController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsRiskDto = DataTest.AudsRiskControllerDataTest.GetAudsRiskDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsRisk_Return_OK()
        {
            //Arrange
            var audsRiskDto = _listAudsRiskDto.FirstOrDefault();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).ReturnsAsync(audsRiskDto);
            //Action
            var actionResult = _audsRiskController.Create(audsRiskDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsRisk_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsRiskController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsRisk_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsRiskController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsRisk_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsRiskController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsRisk_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsRiskService.Setup(x => x.Create(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsRisk_Return_OK()
        {
            //Arrange
            var filter = new AudsRiskFilter() { RiskId = 1 };
            _audsRiskService.Setup(x => x.GetList(It.IsAny<AudsRiskFilter>())).ReturnsAsync(new List<AudsRiskDto>() { new AudsRiskDto() { RiskId = 1 } });
            //Action
            var actionResult = _audsRiskController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsRiskDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsRisk_Return_KO()
        {
            //Arrange
            var filter = new AudsRiskFilter() { RiskId = 1 };
            _audsRiskService.Setup(x => x.GetList(It.IsAny<AudsRiskFilter>())).ReturnsAsync(new List<AudsRiskDto>());
            //Action
            var actionResult = _audsRiskController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsRiskDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsRisk_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsRiskService.Setup(x => x.GetList(It.IsAny<AudsRiskFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsRiskController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsRisk_Return_OK()
        {
            //Arrange
            var audsRiskDto = new AudsRiskDto() { RiskId = 3 };
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).ReturnsAsync(_listAudsRiskDto.FirstOrDefault());
            //Action
            var actionResult = _audsRiskController.Update(audsRiskDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsRisk_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsRiskController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsRisk_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto() { RiskId = 1 };
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsRiskController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsRisk_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto() { RiskId = 1 };;
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsRiskController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto() { RiskId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto() { RiskId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsRisk_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsRiskDto entityDto = new AudsRiskDto() { RiskId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsRiskService.Setup(x => x.Update(It.IsAny<AudsRiskDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsRisk_Return_OK()
        {
            //Arrange
            var audsRiskDto = new AudsRiskDto() { RiskId = 1 };
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsRiskDto);
            //Action
            var actionResult = _audsRiskController.Delete(audsRiskDto.RiskId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsRisk_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsRiskDto entityDto = null;
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsRiskController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsRisk_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsRiskController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsRisk_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsRiskController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsRisk_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsRisk_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsRiskService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsRiskController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsRisk_Return_OK()
        {
            //Arrange
            var filter = new AudsRiskFilter() { RiskId = 1 };
            var audsRiskDto = new AudsRiskDto() { RiskId = 1 };
            _audsRiskService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsRiskDto);
            //Action
            var actionResult = _audsRiskController.Get((int)filter.RiskId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsRiskDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsRisk_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsRiskDto audsRiskDto = new AudsRiskDto();
            _audsRiskService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsRiskDto);
            //Action
            var actionResult = _audsRiskController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsRisk_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsRiskService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsRiskController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsRiskController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsRisk_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsRiskController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
