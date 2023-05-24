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

namespace everisIT.AUDS.Service.AudsDocumentTest
{
    [Collection("Sequential")]
    public partial class AudsDocumentServiceTest
    {
        private readonly Mock<IAudsDocumentRepository> _audsDocumentRepository;
        private readonly IAudsDocumentService _audsDocumentService;
        private readonly IAudsDocumentService _audsDocumentServiceMockAdapter;
		private readonly IBaseAdapter<AudsDocumentDto, AudsDocument> adapter;
        Mock<IBaseAdapter<AudsDocumentDto, AudsDocument>> mockAdapter;

        public AudsDocumentServiceTest()
        {
            //Arrange
			adapter = new AudsDocumentAdapter();
            mockAdapter = new Mock<IBaseAdapter<AudsDocumentDto, AudsDocument>>();
            _audsDocumentRepository = new Mock<IAudsDocumentRepository>();
            _audsDocumentService = new AudsDocumentService(_audsDocumentRepository.Object, adapter);
            _audsDocumentServiceMockAdapter = new AudsDocumentService(_audsDocumentRepository.Object, mockAdapter.Object);
        }

		[Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "Constructor")]
        public void Constructor_Test_Exception()
        {
            AudsDocumentService audsDocumentService;

            Assert.Throws<ArgumentNullException>(() => audsDocumentService = new AudsDocumentService(null, adapter));
            Assert.Throws<ArgumentNullException>(() => audsDocumentService = new AudsDocumentService(_audsDocumentRepository.Object, null));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Insert_AudsDocument_Test_OK()
        {
            //Arrange
            var entityModel = new AudsDocument() { DocumentId = 1 };
            var entityDto = new AudsDocumentDto() { DocumentId = 1 };
            _audsDocumentRepository.Setup(x => x.Create(It.IsAny<AudsDocument>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsDocumentService.Create(entityDto).Result;
            //Assert
            Assert.NotNull(result);        
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Insert_AudsDocument_Test_KO()
        {
            //Arrange
            AudsDocument resultRepository = new AudsDocument();
            _audsDocumentRepository.Setup(x => x.Create(null)).ReturnsAsync(resultRepository);
            //Action
            var result = _audsDocumentService.Create(null).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void GetList_AudsDocuments_Test_OK()
        {
            //Arrange
            var inEntityModel = new AudsDocument() { DocumentId = 1 };
            var outEntityModelDto = new AudsDocumentDto() { DocumentId = 1 };
            List<AudsDocument> listAudsDocument = new List<AudsDocument>{ inEntityModel };
            List<AudsDocumentDto> listAUDSAudsDocumentDto = new List<AudsDocumentDto>{ outEntityModelDto };
            IAudsDocumentFilter filters = new AudsDocumentFilter()
            {
                CodeStatus = false
            };
            _audsDocumentRepository.Setup(x => x.GetList(It.IsAny<AudsDocumentFilter>())).ReturnsAsync(listAudsDocument);
            //Action
            var result = _audsDocumentService.GetList(filters).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void GetList_AudsDocuments_Test_KO()
        {
            //Arrange
            _audsDocumentRepository.Setup(x => x.GetList(It.IsAny<IAudsDocumentFilter>())).ReturnsAsync(new List<AudsDocument>());
            IAudsDocumentFilter filters = new AudsDocumentFilter();
            //Action
            var result = _audsDocumentService.GetList(filters).Result;
            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Delete_by_id_Test_OK()
        {
            //Arrange
            var entityModel = new AudsDocument(){ DocumentId = 1 };
            var entityDto = new AudsDocumentDto(){ DocumentId = 1 };
            _audsDocumentRepository.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsDocumentService.Delete(entityDto.DocumentId).Result;
            //Assert
            Assert.Equal(entityDto.DocumentId, result.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Delete_by_id_Test_KO()
        {
            //Arrange
            AudsDocumentDto result = null;
            mockAdapter.Setup(maper => maper.Map(It.IsAny<AudsDocument>())).Returns(result); 
			AudsDocument result1 = null;
            _audsDocumentRepository.Setup(x => x.Delete(0)).ReturnsAsync(result1);
            //Action && Assert
            Assert.Null(_audsDocumentServiceMockAdapter.Delete(0).Result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Update_AudsDocument_Test_OK()
        {
            //Arrange
            var entityModel = new AudsDocument() { DocumentId = 1 };
            var entityDto = new AudsDocumentDto() { DocumentId = 1 };
            var dataList = new List<AudsDocument>{ entityModel };
            var dataListDto = new List<AudsDocumentDto>{ entityDto };
			_audsDocumentRepository.Setup(x => x.Update(It.IsAny<AudsDocument>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsDocumentService.Update(entityDto).Result;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(entityDto.DocumentId, result.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Update_AudsDocument_Test_KO()
        {
            //Arrange
			AudsDocument result = null;
            _audsDocumentRepository.Setup(x => x.Update(null)).ReturnsAsync(result);
            //Action && Assert
            Assert.NotNull(_audsDocumentService.Update(null).Result);
            Assert.Equal(0, _audsDocumentService.Update(null).Result.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_AudsDocument_by_id_Test_KO()
        {
            //Arrange
            int id = 1;
            AudsDocument result = new AudsDocument();
            _audsDocumentRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(result);
            //Action
            var resultGet = _audsDocumentService.Get(id).Result;
            //Assert
            Assert.Equal(0, resultGet.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentService")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_AudsDocument_by_id_Test_OK()
        {
            //Arrange
            int id = 1;
			AudsDocument entityModel = new AudsDocument() { DocumentId = 1 };
            _audsDocumentRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(entityModel);
            //Action
            var result = _audsDocumentService.Get(id).Result;
            //Assert
            Assert.Equal(entityModel.DocumentId, result.DocumentId);
        }
    }
}
