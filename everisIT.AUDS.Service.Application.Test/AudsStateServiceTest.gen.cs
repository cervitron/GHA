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

namespace everisIT.AUDS.Service.AudsStateTest
{
    [Collection("Sequential")]
    public partial class AudsStateServiceTest
    {
        private readonly Mock<IAudsStateRepository> _audsStateRepository;
        private readonly IAudsStateService _audsStateService;
        private readonly IAudsStateService _audsStateServiceMockAdapter;
		private readonly IBaseAdapter<AudsStateDto, AudsState> adapter;
        Mock<IBaseAdapter<AudsStateDto, AudsState>> mockAdapter;

        public AudsStateServiceTest()
        {
            //Arrange
			adapter = new AudsStateAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsStateDto, AudsState>>();
            _audsStateRepository = new Mock<IAudsStateRepository>();
            _audsStateService = new AudsStateService(_audsStateRepository.Object, adapter);
            _audsStateServiceMockAdapter = new AudsStateService(_audsStateRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsStateService audsStateService;

            Assert.Throws<ArgumentNullException>(() => audsStateService = new AudsStateService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsStateService = new AudsStateService(_audsStateRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Insert_AudsState_Test_OK()
        {
            //Arrange
            var entityModel = new AudsState() { StateId = 1 };
            var entityDto = new AudsStateDto() { StateId = 1 };
            _audsStateRepository.Setup(x => x.Create(It.IsAny<AudsState>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Insert_AudsState_Test_KO()
        {
            //Arrange
            AudsState resultRepository = new AudsState();
            _audsStateRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsStateService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void GetList_AudsStates_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsState() { StateId = 1 };
            var outEntityModelDto = new AudsStateDto() { StateId = 1 };
            List<AudsState> listAudsState = new List<AudsState>{ inEntityModel };
            List<AudsStateDto> listAUDSAudsStateDto = new List<AudsStateDto>{ outEntityModelDto };
            IAudsStateFilter filters = new AudsStateFilter()
            {
                CodeStatus = false
            };
            _audsStateRepository.Setup(x => x.GetList(It.IsAny<AudsStateFilter>())).ReturnsAsync(listAudsState);
            //Action
            var result = _audsStateService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void GetList_AudsStates_Test_KO()
        {
            //Arrange
            _audsStateRepository.Setup(x => x.GetList(It.IsAny<IAudsStateFilter>())).ReturnsAsync(new List<AudsState>());
            IAudsStateFilter filters = new AudsStateFilter();
            //Action
            var result = _audsStateService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsState(){ StateId = 1 };
            var entityDto = new AudsStateDto(){ StateId = 1 };
            _audsStateRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateService.Delete(entityDto.StateId).Result;
            //Assert
            Assert.Equal(entityDto.StateId, result.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsStateDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsState>())).Returns(result); 
			AudsState result1 = null;
            _audsStateRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsStateServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Update_AudsState_Test_OK()
        {
            //Arrange
            var entityModel = new AudsState() { StateId = 1 };
            var entityDto = new AudsStateDto() { StateId = 1 };
            var dataList = new List<AudsState>{ entityModel };
            var dataListDto = new List<AudsStateDto>{ entityDto };
			_audsStateRepository.Setup(x => x.Update(It.IsAny<AudsState>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.StateId, result.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Update_AudsState_Test_KO()
        {
            //Arrange
			AudsState result = null;
            _audsStateRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsStateService.Update(null).Result);
            Assert.Equal(0, _audsStateService.Update(null).Result.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_AudsState_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsState result = new AudsState();
            _audsStateRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsStateService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_AudsState_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsState entityModel = new AudsState() { StateId = 1 };
            _audsStateRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.StateId, result.StateId);
        }
    }
}
