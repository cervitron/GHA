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

namespace everisIT.AUDS.Service.AudsAppTagTest
{
    [Collection("Sequential")]
    public partial class AudsAppTagServiceTest
    {
        private readonly Mock<IAudsAppTagRepository> _audsAppTagRepository;
        private readonly IAudsAppTagService _audsAppTagService;
        private readonly IAudsAppTagService _audsAppTagServiceMockAdapter;
		private readonly IBaseAdapter<AudsAppTagDto, AudsAppTag> adapter;
        Mock<IBaseAdapter<AudsAppTagDto, AudsAppTag>> mockAdapter;

        public AudsAppTagServiceTest()
        {
            //Arrange
			adapter = new AudsAppTagAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsAppTagDto, AudsAppTag>>();
            _audsAppTagRepository = new Mock<IAudsAppTagRepository>();
            _audsAppTagService = new AudsAppTagService(_audsAppTagRepository.Object, adapter);
            _audsAppTagServiceMockAdapter = new AudsAppTagService(_audsAppTagRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsAppTagService audsAppTagService;

            Assert.Throws<ArgumentNullException>(() => audsAppTagService = new AudsAppTagService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsAppTagService = new AudsAppTagService(_audsAppTagRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Insert_AudsAppTag_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAppTag() { ApplicationId = 1 };
            var entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            _audsAppTagRepository.Setup(x => x.Create(It.IsAny<AudsAppTag>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAppTagService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Insert_AudsAppTag_Test_KO()
        {
            //Arrange
            AudsAppTag resultRepository = new AudsAppTag();
            _audsAppTagRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsAppTagService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void GetList_AudsAppTags_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsAppTag() { ApplicationId = 1 };
            var outEntityModelDto = new AudsAppTagDto() { ApplicationId = 1 };
            List<AudsAppTag> listAudsAppTag = new List<AudsAppTag>{ inEntityModel };
            List<AudsAppTagDto> listAUDSAudsAppTagDto = new List<AudsAppTagDto>{ outEntityModelDto };
            IAudsAppTagFilter filters = new AudsAppTagFilter()
            {
                CodeStatus = false
            };
            _audsAppTagRepository.Setup(x => x.GetList(It.IsAny<AudsAppTagFilter>())).ReturnsAsync(listAudsAppTag);
            //Action
            var result = _audsAppTagService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void GetList_AudsAppTags_Test_KO()
        {
            //Arrange
            _audsAppTagRepository.Setup(x => x.GetList(It.IsAny<IAudsAppTagFilter>())).ReturnsAsync(new List<AudsAppTag>());
            IAudsAppTagFilter filters = new AudsAppTagFilter();
            //Action
            var result = _audsAppTagService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAppTag(){ ApplicationId = 1 };
            var entityDto = new AudsAppTagDto(){ ApplicationId = 1 };
            _audsAppTagRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAppTagService.Delete(entityDto.ApplicationId).Result;
            //Assert
            Assert.Equal(entityDto.ApplicationId, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsAppTagDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsAppTag>())).Returns(result); 
			AudsAppTag result1 = null;
            _audsAppTagRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsAppTagServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Update_AudsAppTag_Test_OK()
        {
            //Arrange
            var entityModel = new AudsAppTag() { ApplicationId = 1 };
            var entityDto = new AudsAppTagDto() { ApplicationId = 1 };
            var dataList = new List<AudsAppTag>{ entityModel };
            var dataListDto = new List<AudsAppTagDto>{ entityDto };
			_audsAppTagRepository.Setup(x => x.Update(It.IsAny<AudsAppTag>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAppTagService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.ApplicationId, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Update_AudsAppTag_Test_KO()
        {
            //Arrange
			AudsAppTag result = null;
            _audsAppTagRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsAppTagService.Update(null).Result);
            Assert.Equal(0, _audsAppTagService.Update(null).Result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_AudsAppTag_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsAppTag result = new AudsAppTag();
            _audsAppTagRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsAppTagService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_AudsAppTag_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsAppTag entityModel = new AudsAppTag() { ApplicationId = 1 };
            _audsAppTagRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsAppTagService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.ApplicationId, result.ApplicationId);
        }
    }
}
