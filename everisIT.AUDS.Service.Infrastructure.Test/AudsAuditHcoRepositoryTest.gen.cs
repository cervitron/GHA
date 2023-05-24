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
    public partial class AudsAuditHcoRepositoryTest
    {
        private readonly IAudsAuditHcoRepository _audsAuditHcoRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsAuditHcoRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsAuditHcoRepository = new AudsAuditHcoRepository(_aUDSContextTest);
            AudsAuditHcoRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Create_AudsAuditHco_Test_OK()
        {
            //Arrange 
            var audsAuditHcoModel = _aUDSContextTest.AudsAuditHco.FirstOrDefault();
            audsAuditHcoModel.AuditHcoId = 0;
            //Action
            var iResult = _audsAuditHcoRepository.Create(audsAuditHcoModel).Result;
            //Assert
            Assert.Equal(audsAuditHcoModel.AuditHcoId, iResult.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_All_AudsAuditHco_Test_OK()
        {
            //Action            
            var result = _audsAuditHcoRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsAuditHco.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_AudsAuditHco_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsAuditHcoRepository.GetList(new AudsAuditHcoFilter() { AuditHcoId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Update_AudsAuditHco_Test_OK()
        {
            //Arrange           
            var audsAuditHcoToUpdate = _aUDSContextTest.AudsAuditHco.FirstOrDefault();
            var originalValue = audsAuditHcoToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsAuditHcoToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsAuditHcoRepository.Update(audsAuditHcoToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Delete_AudsAuditHco_Test_OK()
        {
            //Arrange
            var audsAuditHcoModel = _aUDSContextTest.AudsAuditHco.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsAuditHcoModel.CodeStatus;
            //Action
            var audsAuditHcoUpdated = _audsAuditHcoRepository.Delete(audsAuditHcoModel.AuditHcoId).Result;
            //Assert
            Assert.Equal(audsAuditHcoUpdated.AuditHcoId, audsAuditHcoModel.AuditHcoId);
            Assert.NotEqual(audsAuditHcoUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Delete_AudsAuditHco_Test_KO()
        {
            //Action
            var iAudsAuditHcoUpdated = _audsAuditHcoRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsAuditHcoUpdated.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_AudsAuditHco_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsAuditHcoRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditHcoRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditHcoUnitTest")]
        public void Get_AudsAuditHco_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsAuditHcoRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.AuditHcoId);
        }
    }
}