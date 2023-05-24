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

namespace everisIT.AUDS.Service.AudsTagTest
{
    [Collection("Sequential")]
    public partial class AudsTagServiceTest
    {
        private readonly Mock<IAudsTagRepository> _audsTagRepository;
        private readonly IAudsTagService _audsTagService;
        private readonly IAudsTagService _audsTagServiceMockAdapter;
		private readonly IBaseAdapter<AudsTagDto, AudsTag> adapter;
        Mock<IBaseAdapter<AudsTagDto, AudsTag>> mockAdapter;

        public AudsTagServiceTest()
        {
            //Arrange
			adapter = new AudsTagAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsTagDto, AudsTag>>();
            _audsTagRepository = new Mock<IAudsTagRepository>();
            _audsTagService = new AudsTagService(_audsTagRepository.Object, adapter);
            _audsTagServiceMockAdapter = new AudsTagService(_audsTagRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsTagService audsTagService;

            Assert.Throws<ArgumentNullException>(() => audsTagService = new AudsTagService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsTagService = new AudsTagService(_audsTagRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Insert_AudsTag_Test_OK()
        {
            //Arrange
            var entityModel = new AudsTag() { TagId = 1 };
            var entityDto = new AudsTagDto() { TagId = 1 };
            _audsTagRepository.Setup(x => x.Create(It.IsAny<AudsTag>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTagService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Insert_AudsTag_Test_KO()
        {
            //Arrange
            AudsTag resultRepository = new AudsTag();
            _audsTagRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsTagService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void GetList_AudsTags_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsTag() { TagId = 1 };
            var outEntityModelDto = new AudsTagDto() { TagId = 1 };
            List<AudsTag> listAudsTag = new List<AudsTag>{ inEntityModel };
            List<AudsTagDto> listAUDSAudsTagDto = new List<AudsTagDto>{ outEntityModelDto };
            IAudsTagFilter filters = new AudsTagFilter()
            {
                CodeStatus = false
            };
            _audsTagRepository.Setup(x => x.GetList(It.IsAny<AudsTagFilter>())).ReturnsAsync(listAudsTag);
            //Action
            var result = _audsTagService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void GetList_AudsTags_Test_KO()
        {
            //Arrange
            _audsTagRepository.Setup(x => x.GetList(It.IsAny<IAudsTagFilter>())).ReturnsAsync(new List<AudsTag>());
            IAudsTagFilter filters = new AudsTagFilter();
            //Action
            var result = _audsTagService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsTag(){ TagId = 1 };
            var entityDto = new AudsTagDto(){ TagId = 1 };
            _audsTagRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTagService.Delete(entityDto.TagId).Result;
            //Assert
            Assert.Equal(entityDto.TagId, result.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsTagDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsTag>())).Returns(result); 
			AudsTag result1 = null;
            _audsTagRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsTagServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Update_AudsTag_Test_OK()
        {
            //Arrange
            var entityModel = new AudsTag() { TagId = 1 };
            var entityDto = new AudsTagDto() { TagId = 1 };
            var dataList = new List<AudsTag>{ entityModel };
            var dataListDto = new List<AudsTagDto>{ entityDto };
			_audsTagRepository.Setup(x => x.Update(It.IsAny<AudsTag>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTagService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.TagId, result.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Update_AudsTag_Test_KO()
        {
            //Arrange
			AudsTag result = null;
            _audsTagRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsTagService.Update(null).Result);
            Assert.Equal(0, _audsTagService.Update(null).Result.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_AudsTag_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsTag result = new AudsTag();
            _audsTagRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsTagService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_AudsTag_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsTag entityModel = new AudsTag() { TagId = 1 };
            _audsTagRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsTagService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.TagId, result.TagId);
        }
    }
}
