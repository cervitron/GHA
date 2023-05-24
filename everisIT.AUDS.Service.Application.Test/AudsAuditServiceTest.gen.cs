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

namespace everisIT.AUDS.Service.AudsAuditTest
{
    [Collection("Sequential")]
    public partial class AudsAuditServiceTest
    {
        private readonly Mock<IAudsAuditRepository> _audsAuditRepository;
        private readonly IAudsAuditService _audsAuditService;
        private readonly IAudsAuditService _audsAuditServiceMockAdapter;
		private readonly IBaseAdapter<AudsAuditDto, AudsAudit> adapter;
        Mock<IBaseAdapter<AudsAuditDto, AudsAudit>> mockAdapter;

        public AudsAuditServiceTest()
        {
            //Arrange
			adapter = new AudsAuditAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsAuditDto, AudsAudit>>();
            _audsAuditRepository = new Mock<IAudsAuditRepository>();
            _audsAuditService = new AudsAuditService(_audsAuditRepository.Object, adapter);
            _audsAuditServiceMockAdapter = new AudsAuditService(_audsAuditRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsAuditService audsAuditService;

            Assert.Throws<ArgumentNullException>(() => audsAuditService = new AudsAuditService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsAuditService = new AudsAuditService(_audsAuditRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Insert_AudsAudit_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAudit() { AuditId = 1 };
            var entityDto = new AudsAuditDto() { AuditId = 1 };
            _audsAuditRepository.Setup(x => x.Create(It.IsAny<AudsAudit>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Insert_AudsAudit_Test_KO()
        {
            //Arrange
            AudsAudit resultRepository = new AudsAudit();
            _audsAuditRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsAuditService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void GetList_AudsAudits_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsAudit() { AuditId = 1 };
            var outEntityModelDto = new AudsAuditDto() { AuditId = 1 };
            List<AudsAudit> listAudsAudit = new List<AudsAudit>{ inEntityModel };
            List<AudsAuditDto> listAUDSAudsAuditDto = new List<AudsAuditDto>{ outEntityModelDto };
            IAudsAuditFilter filters = new AudsAuditFilter()
            {
                CodeStatus = false
            };
            _audsAuditRepository.Setup(x => x.GetList(It.IsAny<AudsAuditFilter>())).ReturnsAsync(listAudsAudit);
            //Action
            var result = _audsAuditService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void GetList_AudsAudits_Test_KO()
        {
            //Arrange
            _audsAuditRepository.Setup(x => x.GetList(It.IsAny<IAudsAuditFilter>())).ReturnsAsync(new List<AudsAudit>());
            IAudsAuditFilter filters = new AudsAuditFilter();
            //Action
            var result = _audsAuditService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAudit(){ AuditId = 1 };
            var entityDto = new AudsAuditDto(){ AuditId = 1 };
            _audsAuditRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditService.Delete(entityDto.AuditId).Result;
            //Assert
            Assert.Equal(entityDto.AuditId, result.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsAuditDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsAudit>())).Returns(result); 
			AudsAudit result1 = null;
            _audsAuditRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsAuditServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Update_AudsAudit_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAudit() { AuditId = 1 };
            var entityDto = new AudsAuditDto() { AuditId = 1 };
            var dataList = new List<AudsAudit>{ entityModel };
            var dataListDto = new List<AudsAuditDto>{ entityDto };
			_audsAuditRepository.Setup(x => x.Update(It.IsAny<AudsAudit>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.AuditId, result.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Update_AudsAudit_Test_KO()
        {
            //Arrange
			AudsAudit result = null;
            _audsAuditRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsAuditService.Update(null).Result);
            Assert.Equal(0, _audsAuditService.Update(null).Result.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_AudsAudit_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsAudit result = new AudsAudit();
            _audsAuditRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsAuditService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_AudsAudit_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsAudit entityModel = new AudsAudit() { AuditId = 1 };
            _audsAuditRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.AuditId, result.AuditId);
        }
    }
}
