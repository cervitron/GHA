using everisIT.AUDS.Service.Infrastructure.Filters;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.Infrastructure.Repositories;
using everisIT.AUDS.Service.Infrastructure.Repositories.Interfaces;
using everisIT.AUDS.Service.Infrastructure.Test.DataTest;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace everisIT.AUDS.Service.Infrastructure.Test
{
    [Collection("Sequential")]
    public partial class AudsDocumentRepositoryTest
    {
        private readonly IAudsDocumentRepository _audsDocumentRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsDocumentRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsDocumentRepository = new AudsDocumentRepository(_aUDSContextTest);
            AudsDocumentRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Create_AudsDocument_Test_OK()
        {
            //Arrange 
            var audsDocumentModel = _aUDSContextTest.AudsDocument.FirstOrDefault();
            audsDocumentModel.DocumentId = 0;
            //Action
            var iResult = _audsDocumentRepository.Create(audsDocumentModel).Result;
            //Assert
            Assert.Equal(audsDocumentModel.DocumentId, iResult.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_All_AudsDocument_Test_OK()
        {
            //Action            
            var result = _audsDocumentRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsDocument.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_AudsDocument_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsDocumentRepository.GetList(new AudsDocumentFilter() { DocumentId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Update_AudsDocument_Test_OK()
        {
            //Arrange           
            var audsDocumentToUpdate = _aUDSContextTest.AudsDocument.FirstOrDefault();
            var originalValue = audsDocumentToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsDocumentToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsDocumentRepository.Update(audsDocumentToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Delete_AudsDocument_Test_OK()
        {
            //Arrange
            var audsDocumentModel = _aUDSContextTest.AudsDocument.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsDocumentModel.CodeStatus;
            //Action
            var audsDocumentUpdated = _audsDocumentRepository.Delete(audsDocumentModel.DocumentId).Result;
            //Assert
            Assert.Equal(audsDocumentUpdated.DocumentId, audsDocumentModel.DocumentId);
            Assert.NotEqual(audsDocumentUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Delete_AudsDocument_Test_KO()
        {
            //Action
            var iAudsDocumentUpdated = _audsDocumentRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsDocumentUpdated.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_AudsDocument_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsDocumentRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.DocumentId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsDocumentRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsDocumentUnitTest")]
        public void Get_AudsDocument_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsDocumentRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.DocumentId);
        }
    }
}