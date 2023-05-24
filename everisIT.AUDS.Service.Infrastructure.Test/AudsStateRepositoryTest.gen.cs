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
    public partial class AudsStateRepositoryTest
    {
        private readonly IAudsStateRepository _audsStateRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsStateRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsStateRepository = new AudsStateRepository(_aUDSContextTest);
            AudsStateRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Create_AudsState_Test_OK()
        {
            //Arrange 
            var audsStateModel = _aUDSContextTest.AudsState.FirstOrDefault();
            audsStateModel.StateId = 0;
            //Action
            var iResult = _audsStateRepository.Create(audsStateModel).Result;
            //Assert
            Assert.Equal(audsStateModel.StateId, iResult.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_All_AudsState_Test_OK()
        {
            //Action            
            var result = _audsStateRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsState.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_AudsState_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsStateRepository.GetList(new AudsStateFilter() { StateId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Update_AudsState_Test_OK()
        {
            //Arrange           
            var audsStateToUpdate = _aUDSContextTest.AudsState.FirstOrDefault();
            var originalValue = audsStateToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsStateToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsStateRepository.Update(audsStateToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Delete_AudsState_Test_OK()
        {
            //Arrange
            var audsStateModel = _aUDSContextTest.AudsState.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsStateModel.CodeStatus;
            //Action
            var audsStateUpdated = _audsStateRepository.Delete(audsStateModel.StateId).Result;
            //Assert
            Assert.Equal(audsStateUpdated.StateId, audsStateModel.StateId);
            Assert.NotEqual(audsStateUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Delete_AudsState_Test_KO()
        {
            //Action
            var iAudsStateUpdated = _audsStateRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsStateUpdated.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_AudsState_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsStateRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.StateId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateUnitTest")]
        public void Get_AudsState_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsStateRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.StateId);
        }
    }
}