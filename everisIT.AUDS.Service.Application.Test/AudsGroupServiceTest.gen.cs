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

namespace everisIT.AUDS.Service.AudsGroupTest
{
    [Collection("Sequential")]
    public partial class AudsGroupServiceTest
    {
        private readonly Mock<IAudsGroupRepository> _audsGroupRepository;
        private readonly IAudsGroupService _audsGroupService;
        private readonly IAudsGroupService _audsGroupServiceMockAdapter;
		private readonly IBaseAdapter<AudsGroupDto, AudsGroup> adapter;
        Mock<IBaseAdapter<AudsGroupDto, AudsGroup>> mockAdapter;

        public AudsGroupServiceTest()
        {
            //Arrange
			adapter = new AudsGroupAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsGroupDto, AudsGroup>>();
            _audsGroupRepository = new Mock<IAudsGroupRepository>();
            _audsGroupService = new AudsGroupService(_audsGroupRepository.Object, adapter);
            _audsGroupServiceMockAdapter = new AudsGroupService(_audsGroupRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsGroupService audsGroupService;

            Assert.Throws<ArgumentNullException>(() => audsGroupService = new AudsGroupService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsGroupService = new AudsGroupService(_audsGroupRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Insert_AudsGroup_Test_OK()
        {
            //Arrange
            var entityModel = new AudsGroup() { GroupId = 1 };
            var entityDto = new AudsGroupDto() { GroupId = 1 };
            _audsGroupRepository.Setup(x => x.Create(It.IsAny<AudsGroup>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsGroupService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Insert_AudsGroup_Test_KO()
        {
            //Arrange
            AudsGroup resultRepository = new AudsGroup();
            _audsGroupRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsGroupService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void GetList_AudsGroups_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsGroup() { GroupId = 1 };
            var outEntityModelDto = new AudsGroupDto() { GroupId = 1 };
            List<AudsGroup> listAudsGroup = new List<AudsGroup>{ inEntityModel };
            List<AudsGroupDto> listAUDSAudsGroupDto = new List<AudsGroupDto>{ outEntityModelDto };
            IAudsGroupFilter filters = new AudsGroupFilter()
            {
                CodeStatus = false
            };
            _audsGroupRepository.Setup(x => x.GetList(It.IsAny<AudsGroupFilter>())).ReturnsAsync(listAudsGroup);
            //Action
            var result = _audsGroupService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void GetList_AudsGroups_Test_KO()
        {
            //Arrange
            _audsGroupRepository.Setup(x => x.GetList(It.IsAny<IAudsGroupFilter>())).ReturnsAsync(new List<AudsGroup>());
            IAudsGroupFilter filters = new AudsGroupFilter();
            //Action
            var result = _audsGroupService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsGroup(){ GroupId = 1 };
            var entityDto = new AudsGroupDto(){ GroupId = 1 };
            _audsGroupRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsGroupService.Delete(entityDto.GroupId).Result;
            //Assert
            Assert.Equal(entityDto.GroupId, result.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsGroupDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsGroup>())).Returns(result); 
			AudsGroup result1 = null;
            _audsGroupRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsGroupServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Update_AudsGroup_Test_OK()
        {
            //Arrange
            var entityModel = new AudsGroup() { GroupId = 1 };
            var entityDto = new AudsGroupDto() { GroupId = 1 };
            var dataList = new List<AudsGroup>{ entityModel };
            var dataListDto = new List<AudsGroupDto>{ entityDto };
			_audsGroupRepository.Setup(x => x.Update(It.IsAny<AudsGroup>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsGroupService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.GroupId, result.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Update_AudsGroup_Test_KO()
        {
            //Arrange
			AudsGroup result = null;
            _audsGroupRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsGroupService.Update(null).Result);
            Assert.Equal(0, _audsGroupService.Update(null).Result.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_AudsGroup_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsGroup result = new AudsGroup();
            _audsGroupRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsGroupService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_AudsGroup_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsGroup entityModel = new AudsGroup() { GroupId = 1 };
            _audsGroupRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsGroupService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.GroupId, result.GroupId);
        }
    }
}
