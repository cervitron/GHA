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
    public partial class AudsDocumentControllerTest
    {
        private readonly Mock<IAudsDocumentService> _audsDocumentService;
        private readonly Mock<ILogger<AudsDocumentController>> _loggerMock;
        private readonly AudsDocumentController _audsDocumentController;
        private readonly IList<AudsDocumentDto> _listAudsDocumentDto;
        public AudsDocumentControllerTest()
        {
            _audsDocumentService = new Mock<IAudsDocumentService>();
            _loggerMock = new Mock<ILogger<AudsDocumentController>>();
            _audsDocumentController = new AudsDocumentController(_audsDocumentService.Object, _loggerMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Administrador"),
                new Claim("Employee-Number", "145704"),
            }, "mockClaims"));
            _audsDocumentController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            _listAudsDocumentDto = DataTest.AudsDocumentControllerDataTest.GetAudsDocumentDtoList();
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "OK")]
        public void Create_AudsDocument_Return_OK()
        {
            //Arrange
            var audsDocumentDto = _listAudsDocumentDto.FirstOrDefault();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).ReturnsAsync(audsDocumentDto);
            //Action
            var actionResult = _audsDocumentController.Create(audsDocumentDto).Result;
            var contentResult = actionResult as CreatedAtActionResult;
            //Assert
            Assert.True(actionResult is ActionResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.Created);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "Exception")]
        public void Create_AudsDocument_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsDocumentController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "KO")]
        public void Create_AudsDocument_Return_BadRequest_With_Null_Parameter()
        {
            //Arrange
            var actionResult = _audsDocumentController.Create(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsDocument_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsDocumentController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Insert")]
        [Trait("Category", "DbUpdateException")]
        public void Create_AudsDocument_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto();
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsDocumentService.Setup(x => x.Create(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Create(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsDocument_Return_OK()
        {
            //Arrange
            var filter = new AudsDocumentFilter() { DocumentId = 1 };
            _audsDocumentService.Setup(x => x.GetList(It.IsAny<AudsDocumentFilter>())).ReturnsAsync(new List<AudsDocumentDto>() { new AudsDocumentDto() { DocumentId = 1 } });
            //Action
            var actionResult = _audsDocumentController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsDocumentDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "KO")]
        public void GetList_AudsDocument_Return_KO()
        {
            //Arrange
            var filter = new AudsDocumentFilter() { DocumentId = 1 };
            _audsDocumentService.Setup(x => x.GetList(It.IsAny<AudsDocumentFilter>())).ReturnsAsync(new List<AudsDocumentDto>());
            //Action
            var actionResult = _audsDocumentController.List(filter).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is List<AudsDocumentDto>);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetList")]
        [Trait("Category", "Exception")]
        public void GetList_AudsDocument_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange            
            _audsDocumentService.Setup(x => x.GetList(It.IsAny<AudsDocumentFilter>())).Throws(new Exception());
            //Action
            var actionResult = _audsDocumentController.List(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsDocument_Return_OK()
        {
            //Arrange
            var audsDocumentDto = new AudsDocumentDto() { DocumentId = 3 };
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).ReturnsAsync(_listAudsDocumentDto.FirstOrDefault());
            //Action
            var actionResult = _audsDocumentController.Update(audsDocumentDto).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsDocument_Return_BadRequest_With_Null_Parameter()
        {
            //Action
            var actionResult = _audsDocumentController.Update(null).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "Exception")]
        public void Update_AudsDocument_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto() { DocumentId = 1 };
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).Throws(new Exception());
            //Action
            var actionResult = _audsDocumentController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsDocument_Return_InternalServerError_When_Service_Return_Null_sqlException()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto() { DocumentId = 1 };;
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException());
            //Action
            var actionResult = _audsDocumentController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto() { DocumentId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto() { DocumentId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Update")]
        [Trait("Category", "DbUpdateException")]
        public void Update_AudsDocument_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            AudsDocumentDto entityDto = new AudsDocumentDto() { DocumentId = 1 };
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsDocumentService.Setup(x => x.Update(It.IsAny<AudsDocumentDto>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Update(entityDto).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsDocument_Return_OK()
        {
            //Arrange
            var audsDocumentDto = new AudsDocumentDto() { DocumentId = 1 };
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(audsDocumentDto);
            //Action
            var actionResult = _audsDocumentController.Delete(audsDocumentDto.DocumentId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsDocument_Return_NotFound_When_Service_Return_Null_Object()
        {
            //Arrange
            int id = 111;
            AudsDocumentDto entityDto = null;
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityDto);
            //Action
            var actionResult = _audsDocumentController.Delete(id).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsDocument_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Arrange
            var actionResult = _audsDocumentController.Delete(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "Exception")]
        public void Delete_AudsDocument_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsDocumentController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_2601()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(2601)
                .WithErrorMessage("Violation of PRIMARY / UNIQUE KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsDocument_Return_Conflict_When_Service_Return_sqlException_number_547()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(547)
                .WithErrorMessage("Conflicted with FOREIGN KEY constraint")
                .Build();
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "Delete")]
        [Trait("Category", "DbUpdateException")]
        public void Delete_AudsDocument_Return_InternalServerError_When_Service_Return_sqlException_uncontrolled_number()
        {
            //Arrange
            var sqlException = new SqlExceptionBuilder().WithErrorNumber(111)
                .WithErrorMessage("SQL exception number: 111")
                .Build();
            _audsDocumentService.Setup(x => x.Delete(It.IsAny<int>())).Throws(new DbUpdateException("", sqlException));
            //Action
            var actionResult = _audsDocumentController.Delete(1).Result;
            //Assert
            Assert.True(actionResult is ObjectResult);
            Assert.Equal(((ObjectResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void GetById_AudsDocument_Return_OK()
        {
            //Arrange
            var filter = new AudsDocumentFilter() { DocumentId = 1 };
            var audsDocumentDto = new AudsDocumentDto() { DocumentId = 1 };
            _audsDocumentService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsDocumentDto);
            //Action
            var actionResult = _audsDocumentController.Get((int)filter.DocumentId).Result;
            var contentResult = actionResult as OkObjectResult;
            //Assert
            Assert.True(actionResult is OkObjectResult);
            Assert.True(contentResult.Value is AudsDocumentDto);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsDocument_Return_NotFound_When_Service_Return_Empty_List()
        {
            //Arrange
            AudsDocumentDto audsDocumentDto = new AudsDocumentDto();
            _audsDocumentService.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(audsDocumentDto);
            //Action
            var actionResult = _audsDocumentController.Get(1).Result;
            var contentResult = actionResult as NotFoundResult;
            //Assert
            Assert.True(actionResult is NotFoundResult);
            Assert.Equal(contentResult.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "Exception")]
        public void GetById_AudsDocument_Return_InternalServerError_When_Service_Return_Exception()
        {
            //Arrange
            _audsDocumentService.Setup(x => x.Get(It.IsAny<int>())).Throws(new Exception());
            //Action
            var actionResult = _audsDocumentController.Get(1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        [Trait("Category", "AudsDocumentController")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void GetById_AudsDocument_Return_BadRequest_With_Negative_Id_Parameter()
        {
            //Action
            var actionResult = _audsDocumentController.Get(-1).Result;
            //Assert
            Assert.True(actionResult is StatusCodeResult);
            Assert.Equal(((StatusCodeResult)actionResult).StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
