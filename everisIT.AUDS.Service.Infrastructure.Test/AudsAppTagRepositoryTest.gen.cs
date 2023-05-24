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
    public partial class AudsAppTagRepositoryTest
    {
        private readonly IAudsAppTagRepository _audsAppTagRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsAppTagRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsAppTagRepository = new AudsAppTagRepository(_aUDSContextTest);
            AudsAppTagRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Create_AudsAppTag_Test_OK()
        {
            //Arrange 
            var audsAppTagModel = _aUDSContextTest.AudsAppTag.FirstOrDefault();
            audsAppTagModel.ApplicationId = 0;
            //Action
            var iResult = _audsAppTagRepository.Create(audsAppTagModel).Result;
            //Assert
            Assert.Equal(audsAppTagModel.ApplicationId, iResult.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_All_AudsAppTag_Test_OK()
        {
            //Action            
            var result = _audsAppTagRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsAppTag.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_AudsAppTag_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsAppTagRepository.GetList(new AudsAppTagFilter() { ApplicationId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Update_AudsAppTag_Test_OK()
        {
            //Arrange           
            var audsAppTagToUpdate = _aUDSContextTest.AudsAppTag.FirstOrDefault();
            var originalValue = audsAppTagToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsAppTagToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsAppTagRepository.Update(audsAppTagToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Delete_AudsAppTag_Test_OK()
        {
            //Arrange
            var audsAppTagModel = _aUDSContextTest.AudsAppTag.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsAppTagModel.CodeStatus;
            //Action
            var audsAppTagUpdated = _audsAppTagRepository.Delete(audsAppTagModel.ApplicationId).Result;
            //Assert
            Assert.Equal(audsAppTagUpdated.ApplicationId, audsAppTagModel.ApplicationId);
            Assert.NotEqual(audsAppTagUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Delete_AudsAppTag_Test_KO()
        {
            //Action
            var iAudsAppTagUpdated = _audsAppTagRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsAppTagUpdated.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_AudsAppTag_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsAppTagRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAppTagRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAppTagUnitTest")]
        public void Get_AudsAppTag_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsAppTagRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.ApplicationId);
        }
    }
}