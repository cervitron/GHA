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

namespace everisIT.AUDS.Service.AudsAuditHcoTest
{
    [Collection("Sequential")]
    public partial class AudsAuditHcoServiceTest
    {
        private readonly Mock<IAudsAuditHcoRepository> _audsAuditHcoRepository;
        private readonly IAudsAuditHcoService _audsAuditHcoService;
        private readonly IAudsAuditHcoService _audsAuditHcoServiceMockAdapter;
		private readonly IBaseAdapter<AudsAuditHcoDto, AudsAuditHco> adapter;
        Mock<IBaseAdapter<AudsAuditHcoDto, AudsAuditHco>> mockAdapter;

        public AudsAuditHcoServiceTest()
        {
            //Arrange
			adapter = new AudsAuditHcoAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsAuditHcoDto, AudsAuditHco>>();
            _audsAuditHcoRepository = new Mock<IAudsAuditHcoRepository>();
            _audsAuditHcoService = new AudsAuditHcoService(_audsAuditHcoRepository.Object, adapter);
            _audsAuditHcoServiceMockAdapter = new AudsAuditHcoService(_audsAuditHcoRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsAuditHcoService audsAuditHcoService;

            Assert.Throws<ArgumentNullException>(() => audsAuditHcoService = new AudsAuditHcoService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsAuditHcoService = new AudsAuditHcoService(_audsAuditHcoRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Insert_AudsAuditHco_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAuditHco() { AuditHcoId = 1 };
            var entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            _audsAuditHcoRepository.Setup(x => x.Create(It.IsAny<AudsAuditHco>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditHcoService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Insert_AudsAuditHco_Test_KO()
        {
            //Arrange
            AudsAuditHco resultRepository = new AudsAuditHco();
            _audsAuditHcoRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsAuditHcoService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void GetList_AudsAuditHcos_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsAuditHco() { AuditHcoId = 1 };
            var outEntityModelDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            List<AudsAuditHco> listAudsAuditHco = new List<AudsAuditHco>{ inEntityModel };
            List<AudsAuditHcoDto> listAUDSAudsAuditHcoDto = new List<AudsAuditHcoDto>{ outEntityModelDto };
            IAudsAuditHcoFilter filters = new AudsAuditHcoFilter()
            {
                CodeStatus = false
            };
            _audsAuditHcoRepository.Setup(x => x.GetList(It.IsAny<AudsAuditHcoFilter>())).ReturnsAsync(listAudsAuditHco);
            //Action
            var result = _audsAuditHcoService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void GetList_AudsAuditHcos_Test_KO()
        {
            //Arrange
            _audsAuditHcoRepository.Setup(x => x.GetList(It.IsAny<IAudsAuditHcoFilter>())).ReturnsAsync(new List<AudsAuditHco>());
            IAudsAuditHcoFilter filters = new AudsAuditHcoFilter();
            //Action
            var result = _audsAuditHcoService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAuditHco(){ AuditHcoId = 1 };
            var entityDto = new AudsAuditHcoDto(){ AuditHcoId = 1 };
            _audsAuditHcoRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditHcoService.Delete(entityDto.AuditHcoId).Result;
            //Assert
            Assert.Equal(entityDto.AuditHcoId, result.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsAuditHcoDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsAuditHco>())).Returns(result); 
			AudsAuditHco result1 = null;
            _audsAuditHcoRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsAuditHcoServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Update_AudsAuditHco_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAuditHco() { AuditHcoId = 1 };
            var entityDto = new AudsAuditHcoDto() { AuditHcoId = 1 };
            var dataList = new List<AudsAuditHco>{ entityModel };
            var dataListDto = new List<AudsAuditHcoDto>{ entityDto };
			_audsAuditHcoRepository.Setup(x => x.Update(It.IsAny<AudsAuditHco>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditHcoService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.AuditHcoId, result.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Update_AudsAuditHco_Test_KO()
        {
            //Arrange
			AudsAuditHco result = null;
            _audsAuditHcoRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsAuditHcoService.Update(null).Result);
            Assert.Equal(0, _audsAuditHcoService.Update(null).Result.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_AudsAuditHco_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsAuditHco result = new AudsAuditHco();
            _audsAuditHcoRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsAuditHcoService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_AudsAuditHco_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsAuditHco entityModel = new AudsAuditHco() { AuditHcoId = 1 };
            _audsAuditHcoRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAuditHcoService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.AuditHcoId, result.AuditHcoId);
        }
    }
}
