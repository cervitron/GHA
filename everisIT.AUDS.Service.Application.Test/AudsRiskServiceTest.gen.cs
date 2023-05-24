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

namespace everisIT.AUDS.Service.AudsRiskTest
{
    [Collection("Sequential")]
    public partial class AudsRiskServiceTest
    {
        private readonly Mock<IAudsRiskRepository> _audsRiskRepository;
        private readonly IAudsRiskService _audsRiskService;
        private readonly IAudsRiskService _audsRiskServiceMockAdapter;
		private readonly IBaseAdapter<AudsRiskDto, AudsRisk> adapter;
        Mock<IBaseAdapter<AudsRiskDto, AudsRisk>> mockAdapter;

        public AudsRiskServiceTest()
        {
            //Arrange
			adapter = new AudsRiskAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsRiskDto, AudsRisk>>();
            _audsRiskRepository = new Mock<IAudsRiskRepository>();
            _audsRiskService = new AudsRiskService(_audsRiskRepository.Object, adapter);
            _audsRiskServiceMockAdapter = new AudsRiskService(_audsRiskRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsRiskService audsRiskService;

            Assert.Throws<ArgumentNullException>(() => audsRiskService = new AudsRiskService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsRiskService = new AudsRiskService(_audsRiskRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Insert_AudsRisk_Test_OK()
        {
            //Arrange
            var entityModel = new AudsRisk() { RiskId = 1 };
            var entityDto = new AudsRiskDto() { RiskId = 1 };
            _audsRiskRepository.Setup(x => x.Create(It.IsAny<AudsRisk>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsRiskService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Insert_AudsRisk_Test_KO()
        {
            //Arrange
            AudsRisk resultRepository = new AudsRisk();
            _audsRiskRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsRiskService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void GetList_AudsRisks_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsRisk() { RiskId = 1 };
            var outEntityModelDto = new AudsRiskDto() { RiskId = 1 };
            List<AudsRisk> listAudsRisk = new List<AudsRisk>{ inEntityModel };
            List<AudsRiskDto> listAUDSAudsRiskDto = new List<AudsRiskDto>{ outEntityModelDto };
            IAudsRiskFilter filters = new AudsRiskFilter()
            {
                CodeStatus = false
            };
            _audsRiskRepository.Setup(x => x.GetList(It.IsAny<AudsRiskFilter>())).ReturnsAsync(listAudsRisk);
            //Action
            var result = _audsRiskService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void GetList_AudsRisks_Test_KO()
        {
            //Arrange
            _audsRiskRepository.Setup(x => x.GetList(It.IsAny<IAudsRiskFilter>())).ReturnsAsync(new List<AudsRisk>());
            IAudsRiskFilter filters = new AudsRiskFilter();
            //Action
            var result = _audsRiskService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsRisk(){ RiskId = 1 };
            var entityDto = new AudsRiskDto(){ RiskId = 1 };
            _audsRiskRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsRiskService.Delete(entityDto.RiskId).Result;
            //Assert
            Assert.Equal(entityDto.RiskId, result.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsRiskDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsRisk>())).Returns(result); 
			AudsRisk result1 = null;
            _audsRiskRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsRiskServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Update_AudsRisk_Test_OK()
        {
            //Arrange
            var entityModel = new AudsRisk() { RiskId = 1 };
            var entityDto = new AudsRiskDto() { RiskId = 1 };
            var dataList = new List<AudsRisk>{ entityModel };
            var dataListDto = new List<AudsRiskDto>{ entityDto };
			_audsRiskRepository.Setup(x => x.Update(It.IsAny<AudsRisk>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsRiskService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.RiskId, result.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Update_AudsRisk_Test_KO()
        {
            //Arrange
			AudsRisk result = null;
            _audsRiskRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsRiskService.Update(null).Result);
            Assert.Equal(0, _audsRiskService.Update(null).Result.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_AudsRisk_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsRisk result = new AudsRisk();
            _audsRiskRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsRiskService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_AudsRisk_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsRisk entityModel = new AudsRisk() { RiskId = 1 };
            _audsRiskRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsRiskService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.RiskId, result.RiskId);
        }
    }
}
