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
    public partial class AudsRiskRepositoryTest
    {
        private readonly IAudsRiskRepository _audsRiskRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsRiskRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsRiskRepository = new AudsRiskRepository(_aUDSContextTest);
            AudsRiskRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Create_AudsRisk_Test_OK()
        {
            //Arrange 
            var audsRiskModel = _aUDSContextTest.AudsRisk.FirstOrDefault();
            audsRiskModel.RiskId = 0;
            //Action
            var iResult = _audsRiskRepository.Create(audsRiskModel).Result;
            //Assert
            Assert.Equal(audsRiskModel.RiskId, iResult.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_All_AudsRisk_Test_OK()
        {
            //Action            
            var result = _audsRiskRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsRisk.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_AudsRisk_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsRiskRepository.GetList(new AudsRiskFilter() { RiskId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Update_AudsRisk_Test_OK()
        {
            //Arrange           
            var audsRiskToUpdate = _aUDSContextTest.AudsRisk.FirstOrDefault();
            var originalValue = audsRiskToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsRiskToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsRiskRepository.Update(audsRiskToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Delete_AudsRisk_Test_OK()
        {
            //Arrange
            var audsRiskModel = _aUDSContextTest.AudsRisk.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsRiskModel.CodeStatus;
            //Action
            var audsRiskUpdated = _audsRiskRepository.Delete(audsRiskModel.RiskId).Result;
            //Assert
            Assert.Equal(audsRiskUpdated.RiskId, audsRiskModel.RiskId);
            Assert.NotEqual(audsRiskUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Delete_AudsRisk_Test_KO()
        {
            //Action
            var iAudsRiskUpdated = _audsRiskRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsRiskUpdated.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_AudsRisk_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsRiskRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.RiskId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsRiskRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsRiskUnitTest")]
        public void Get_AudsRisk_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsRiskRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.RiskId);
        }
    }
}