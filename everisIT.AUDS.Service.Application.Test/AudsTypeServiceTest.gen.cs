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

namespace everisIT.AUDS.Service.AudsTypeTest
{
    [Collection("Sequential")]
    public partial class AudsTypeServiceTest
    {
        private readonly Mock<IAudsTypeRepository> _audsTypeRepository;
        private readonly IAudsTypeService _audsTypeService;
        private readonly IAudsTypeService _audsTypeServiceMockAdapter;
		private readonly IBaseAdapter<AudsTypeDto, AudsType> adapter;
        Mock<IBaseAdapter<AudsTypeDto, AudsType>> mockAdapter;

        public AudsTypeServiceTest()
        {
            //Arrange
			adapter = new AudsTypeAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsTypeDto, AudsType>>();
            _audsTypeRepository = new Mock<IAudsTypeRepository>();
            _audsTypeService = new AudsTypeService(_audsTypeRepository.Object, adapter);
            _audsTypeServiceMockAdapter = new AudsTypeService(_audsTypeRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsTypeService audsTypeService;

            Assert.Throws<ArgumentNullException>(() => audsTypeService = new AudsTypeService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsTypeService = new AudsTypeService(_audsTypeRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Insert_AudsType_Test_OK()
        {
            //Arrange
            var entityModel = new AudsType() { IdType = 1 };
            var entityDto = new AudsTypeDto() { IdType = 1 };
            _audsTypeRepository.Setup(x => x.Create(It.IsAny<AudsType>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTypeService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Insert_AudsType_Test_KO()
        {
            //Arrange
            AudsType resultRepository = new AudsType();
            _audsTypeRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsTypeService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void GetList_AudsTypes_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsType() { IdType = 1 };
            var outEntityModelDto = new AudsTypeDto() { IdType = 1 };
            List<AudsType> listAudsType = new List<AudsType>{ inEntityModel };
            List<AudsTypeDto> listAUDSAudsTypeDto = new List<AudsTypeDto>{ outEntityModelDto };
            IAudsTypeFilter filters = new AudsTypeFilter()
            {
                CodeStatus = false
            };
            _audsTypeRepository.Setup(x => x.GetList(It.IsAny<AudsTypeFilter>())).ReturnsAsync(listAudsType);
            //Action
            var result = _audsTypeService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void GetList_AudsTypes_Test_KO()
        {
            //Arrange
            _audsTypeRepository.Setup(x => x.GetList(It.IsAny<IAudsTypeFilter>())).ReturnsAsync(new List<AudsType>());
            IAudsTypeFilter filters = new AudsTypeFilter();
            //Action
            var result = _audsTypeService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsType(){ IdType = 1 };
            var entityDto = new AudsTypeDto(){ IdType = 1 };
            _audsTypeRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTypeService.Delete(entityDto.IdType).Result;
            //Assert
            Assert.Equal(entityDto.IdType, result.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsTypeDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsType>())).Returns(result); 
			AudsType result1 = null;
            _audsTypeRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsTypeServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Update_AudsType_Test_OK()
        {
            //Arrange
            var entityModel = new AudsType() { IdType = 1 };
            var entityDto = new AudsTypeDto() { IdType = 1 };
            var dataList = new List<AudsType>{ entityModel };
            var dataListDto = new List<AudsTypeDto>{ entityDto };
			_audsTypeRepository.Setup(x => x.Update(It.IsAny<AudsType>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTypeService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.IdType, result.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Update_AudsType_Test_KO()
        {
            //Arrange
			AudsType result = null;
            _audsTypeRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsTypeService.Update(null).Result);
            Assert.Equal(0, _audsTypeService.Update(null).Result.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_AudsType_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsType result = new AudsType();
            _audsTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsTypeService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_AudsType_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsType entityModel = new AudsType() { IdType = 1 };
            _audsTypeRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTypeService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.IdType, result.IdType);
        }
    }
}
