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
    public partial class AudsApplicationRepositoryTest
    {
        private readonly IAudsApplicationRepository _audsApplicationRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsApplicationRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsApplicationRepository = new AudsApplicationRepository(_aUDSContextTest);
            AudsApplicationRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Create_AudsApplication_Test_OK()
        {
            //Arrange 
            var audsApplicationModel = _aUDSContextTest.AudsApplication.FirstOrDefault();
            audsApplicationModel.ApplicationId = 0;
            //Action
            var iResult = _audsApplicationRepository.Create(audsApplicationModel).Result;
            //Assert
            Assert.Equal(audsApplicationModel.ApplicationId, iResult.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_All_AudsApplication_Test_OK()
        {
            //Action            
            var result = _audsApplicationRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsApplication.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_AudsApplication_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsApplicationRepository.GetList(new AudsApplicationFilter() { ApplicationId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Update_AudsApplication_Test_OK()
        {
            //Arrange           
            var audsApplicationToUpdate = _aUDSContextTest.AudsApplication.FirstOrDefault();
            var originalValue = audsApplicationToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsApplicationToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsApplicationRepository.Update(audsApplicationToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Delete_AudsApplication_Test_OK()
        {
            //Arrange
            var audsApplicationModel = _aUDSContextTest.AudsApplication.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsApplicationModel.CodeStatus;
            //Action
            var audsApplicationUpdated = _audsApplicationRepository.Delete(audsApplicationModel.ApplicationId).Result;
            //Assert
            Assert.Equal(audsApplicationUpdated.ApplicationId, audsApplicationModel.ApplicationId);
            Assert.NotEqual(audsApplicationUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Delete_AudsApplication_Test_KO()
        {
            //Action
            var iAudsApplicationUpdated = _audsApplicationRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsApplicationUpdated.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_AudsApplication_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsApplicationRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.ApplicationId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsApplicationRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsApplicationUnitTest")]
        public void Get_AudsApplication_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsApplicationRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.ApplicationId);
        }
    }
}