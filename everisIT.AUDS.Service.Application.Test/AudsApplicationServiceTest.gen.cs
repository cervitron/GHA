using everisIT.AUDS.Service.Application.Adapters;
using everisIT.AUDS.Service.Application.Adapters.Interfaces;
using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Application.Services;
using everisIT.AUDS.Service.Application.Services.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.Infrastructure.Filters.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace everisIT.AUDS.Service.AudsApplicationTest
{
    [Collection("Sequential")]
    public partial class AudsApplicationServiceTest
    {
        private readonly Mock<IAudsApplicationRepository> _audsApplicationRepository;
        private readonly IAudsApplicationService _audsApplicationService;
        private readonly IAudsApplicationService _audsApplicationServiceMockAdapter;
		private readonly IBaseAdapter<AudsApplicationDto, AudsApplication> adapter;
        Mock<IBaseAdapter<AudsApplicationDto, AudsApplication>> mockAdapter;

        public AudsApplicationServiceTest()
        {
            //Arrange
			adapter = new AudsApplicationAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsApplicationDto, AudsApplication>>();
            _audsApplicationRepository = new Mock<IAudsApplicationRepository>();
            _audsApplicationService = new AudsApplicationService(_audsApplicationRepository.Object, adapter);
            _audsApplicationServiceMockAdapter = new AudsApplicationService(_audsApplicationRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsApplicationService audsApplicationService;

            Assert.Throws<ArgumentNullException>(() => audsApplicationService = new AudsApplicationService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsApplicationService = new AudsApplicationService(_audsApplicationRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Insert_AudsApplication_Test_OK()
        {
            //Arrange
            var entityModel = new AudsApplication() { ApplicationId = 1 };
            var entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            _audsApplicationRepository.Setup(x => x.Create(It.IsAny<AudsApplication>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsApplicationService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Insert_AudsApplication_Test_KO()
        {
            //Arrange
            AudsApplication resultRepository = new AudsApplication();
            _audsApplicationRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsApplicationService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void GetList_AudsApplications_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsApplication() { ApplicationId = 1 };
            var outEntityModelDto = new AudsApplicationDto() { ApplicationId = 1 };
            List<AudsApplication> listAudsApplication = new List<AudsApplication>{ inEntityModel };
            List<AudsApplicationDto> listAUDSAudsApplicationDto = new List<AudsApplicationDto>{ outEntityModelDto };
            IAudsApplicationFilter filters = new AudsApplicationFilter()
            {
                CodeStatus = false
            };
            _audsApplicationRepository.Setup(x => x.GetList(It.IsAny<AudsApplicationFilter>())).ReturnsAsync(listAudsApplication);
            //Action
            var result = _audsApplicationService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void GetList_AudsApplications_Test_KO()
        {
            //Arrange
            _audsApplicationRepository.Setup(x => x.GetList(It.IsAny<IAudsApplicationFilter>())).ReturnsAsync(new List<AudsApplication>());
            IAudsApplicationFilter filters = new AudsApplicationFilter();
            //Action
            var result = _audsApplicationService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsApplication(){ ApplicationId = 1 };
            var entityDto = new AudsApplicationDto(){ ApplicationId = 1 };
            _audsApplicationRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsApplicationService.Delete(entityDto.ApplicationId).Result;
            //Assert
            Assert.Equal(entityDto.ApplicationId, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsApplicationDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsApplication>())).Returns(result); 
			AudsApplication result1 = null;
            _audsApplicationRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsApplicationServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Update_AudsApplication_Test_OK()
        {
            //Arrange
            var entityModel = new AudsApplication() { ApplicationId = 1 };
            var entityDto = new AudsApplicationDto() { ApplicationId = 1 };
            var dataList = new List<AudsApplication>{ entityModel };
            var dataListDto = new List<AudsApplicationDto>{ entityDto };
			_audsApplicationRepository.Setup(x => x.Update(It.IsAny<AudsApplication>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsApplicationService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.ApplicationId, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Update_AudsApplication_Test_KO()
        {
            //Arrange
			AudsApplication result = null;
            _audsApplicationRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsApplicationService.Update(null).Result);
            Assert.Equal(0, _audsApplicationService.Update(null).Result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_AudsApplication_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsApplication result = new AudsApplication();
            _audsApplicationRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsApplicationService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_AudsApplication_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsApplication entityModel = new AudsApplication() { ApplicationId = 1 };
            _audsApplicationRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsApplicationService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.ApplicationId, result.ApplicationId);
        }
    }
}
