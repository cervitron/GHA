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
    public partial class AudsTagRepositoryTest
    {
        private readonly IAudsTagRepository _audsTagRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsTagRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsTagRepository = new AudsTagRepository(_aUDSContextTest);
            AudsTagRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Create_AudsTag_Test_OK()
        {
            //Arrange 
            var audsTagModel = _aUDSContextTest.AudsTag.FirstOrDefault();
            audsTagModel.TagId = 0;
            //Action
            var iResult = _audsTagRepository.Create(audsTagModel).Result;
            //Assert
            Assert.Equal(audsTagModel.TagId, iResult.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_All_AudsTag_Test_OK()
        {
            //Action            
            var result = _audsTagRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsTag.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_AudsTag_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsTagRepository.GetList(new AudsTagFilter() { TagId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Update_AudsTag_Test_OK()
        {
            //Arrange           
            var audsTagToUpdate = _aUDSContextTest.AudsTag.FirstOrDefault();
            var originalValue = audsTagToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsTagToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsTagRepository.Update(audsTagToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Delete_AudsTag_Test_OK()
        {
            //Arrange
            var audsTagModel = _aUDSContextTest.AudsTag.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsTagModel.CodeStatus;
            //Action
            var audsTagUpdated = _audsTagRepository.Delete(audsTagModel.TagId).Result;
            //Assert
            Assert.Equal(audsTagUpdated.TagId, audsTagModel.TagId);
            Assert.NotEqual(audsTagUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Delete_AudsTag_Test_KO()
        {
            //Action
            var iAudsTagUpdated = _audsTagRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsTagUpdated.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_AudsTag_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsTagRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.TagId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTagUnitTest")]
        public void Get_AudsTag_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsTagRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.TagId);
        }
    }
}