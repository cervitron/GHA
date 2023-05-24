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
    public partial class AudsAuditRepositoryTest
    {
        private readonly IAudsAuditRepository _audsAuditRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsAuditRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsAuditRepository = new AudsAuditRepository(_aUDSContextTest);
            AudsAuditRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Create_AudsAudit_Test_OK()
        {
            //Arrange 
            var audsAuditModel = _aUDSContextTest.AudsAudit.FirstOrDefault();
            audsAuditModel.AuditId = 0;
            //Action
            var iResult = _audsAuditRepository.Create(audsAuditModel).Result;
            //Assert
            Assert.Equal(audsAuditModel.AuditId, iResult.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_All_AudsAudit_Test_OK()
        {
            //Action            
            var result = _audsAuditRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsAudit.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_AudsAudit_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsAuditRepository.GetList(new AudsAuditFilter() { AuditId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Update_AudsAudit_Test_OK()
        {
            //Arrange           
            var audsAuditToUpdate = _aUDSContextTest.AudsAudit.FirstOrDefault();
            var originalValue = audsAuditToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsAuditToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsAuditRepository.Update(audsAuditToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Delete_AudsAudit_Test_OK()
        {
            //Arrange
            var audsAuditModel = _aUDSContextTest.AudsAudit.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsAuditModel.CodeStatus;
            //Action
            var audsAuditUpdated = _audsAuditRepository.Delete(audsAuditModel.AuditId).Result;
            //Assert
            Assert.Equal(audsAuditUpdated.AuditId, audsAuditModel.AuditId);
            Assert.NotEqual(audsAuditUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Delete_AudsAudit_Test_KO()
        {
            //Action
            var iAudsAuditUpdated = _audsAuditRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsAuditUpdated.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_AudsAudit_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsAuditRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.AuditId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsAuditRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsAuditUnitTest")]
        public void Get_AudsAudit_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsAuditRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.AuditId);
        }
    }
}