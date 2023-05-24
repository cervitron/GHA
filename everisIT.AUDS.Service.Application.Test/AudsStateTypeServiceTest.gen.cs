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

namespace everisIT.AUDS.Service.AudsStateTypeTest
{
    [Collection("Sequential")]
    public partial class AudsStateTypeServiceTest
    {
        private readonly Mock<IAudsStateTypeRepository> _audsStateTypeRepository;
        private readonly IAudsStateTypeService _audsStateTypeService;
        private readonly IAudsStateTypeService _audsStateTypeServiceMockAdapter;
		private readonly IBaseAdapter<AudsStateTypeDto, AudsStateType> adapter;
        Mock<IBaseAdapter<AudsStateTypeDto, AudsStateType>> mockAdapter;

        public AudsStateTypeServiceTest()
        {
            //Arrange
			adapter = new AudsStateTypeAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsStateTypeDto, AudsStateType>>();
            _audsStateTypeRepository = new Mock<IAudsStateTypeRepository>();
            _audsStateTypeService = new AudsStateTypeService(_audsStateTypeRepository.Object, adapter);
            _audsStateTypeServiceMockAdapter = new AudsStateTypeService(_audsStateTypeRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsStateTypeService audsStateTypeService;

            Assert.Throws<ArgumentNullException>(() => audsStateTypeService = new AudsStateTypeService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsStateTypeService = new AudsStateTypeService(_audsStateTypeRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Insert_AudsStateType_Test_OK()
        {
            //Arrange
            var entityModel = new AudsStateType() { StateTypeId = 1 };
            var entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            _audsStateTypeRepository.Setup(x => x.Create(It.IsAny<AudsStateType>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateTypeService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Insert_AudsStateType_Test_KO()
        {
            //Arrange
            AudsStateType resultRepository = new AudsStateType();
            _audsStateTypeRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsStateTypeService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void GetList_AudsStateTypes_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsStateType() { StateTypeId = 1 };
            var outEntityModelDto = new AudsStateTypeDto() { StateTypeId = 1 };
            List<AudsStateType> listAudsStateType = new List<AudsStateType>{ inEntityModel };
            List<AudsStateTypeDto> listAUDSAudsStateTypeDto = new List<AudsStateTypeDto>{ outEntityModelDto };
            IAudsStateTypeFilter filters = new AudsStateTypeFilter()
            {
                CodeStatus = false
            };
            _audsStateTypeRepository.Setup(x => x.GetList(It.IsAny<AudsStateTypeFilter>())).ReturnsAsync(listAudsStateType);
            //Action
            var result = _audsStateTypeService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void GetList_AudsStateTypes_Test_KO()
        {
            //Arrange
            _audsStateTypeRepository.Setup(x => x.GetList(It.IsAny<IAudsStateTypeFilter>())).ReturnsAsync(new List<AudsStateType>());
            IAudsStateTypeFilter filters = new AudsStateTypeFilter();
            //Action
            var result = _audsStateTypeService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsStateType(){ StateTypeId = 1 };
            var entityDto = new AudsStateTypeDto(){ StateTypeId = 1 };
            _audsStateTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateTypeService.Delete(entityDto.StateTypeId).Result;
            //Assert
            Assert.Equal(entityDto.StateTypeId, result.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsStateTypeDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsStateType>())).Returns(result); 
			AudsStateType result1 = null;
            _audsStateTypeRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsStateTypeServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Update_AudsStateType_Test_OK()
        {
            //Arrange
            var entityModel = new AudsStateType() { StateTypeId = 1 };
            var entityDto = new AudsStateTypeDto() { StateTypeId = 1 };
            var dataList = new List<AudsStateType>{ entityModel };
            var dataListDto = new List<AudsStateTypeDto>{ entityDto };
			_audsStateTypeRepository.Setup(x => x.Update(It.IsAny<AudsStateType>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateTypeService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.StateTypeId, result.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Update_AudsStateType_Test_KO()
        {
            //Arrange
			AudsStateType result = null;
            _audsStateTypeRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsStateTypeService.Update(null).Result);
            Assert.Equal(0, _audsStateTypeService.Update(null).Result.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_AudsStateType_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsStateType result = new AudsStateType();
            _audsStateTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsStateTypeService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_AudsStateType_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsStateType entityModel = new AudsStateType() { StateTypeId = 1 };
            _audsStateTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsStateTypeService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.StateTypeId, result.StateTypeId);
        }
    }
}
