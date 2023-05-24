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
    public partial class AudsStateTypeRepositoryTest
    {
        private readonly IAudsStateTypeRepository _audsStateTypeRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsStateTypeRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsStateTypeRepository = new AudsStateTypeRepository(_aUDSContextTest);
            AudsStateTypeRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Create_AudsStateType_Test_OK()
        {
            //Arrange 
            var audsStateTypeModel = _aUDSContextTest.AudsStateType.FirstOrDefault();
            audsStateTypeModel.StateTypeId = 0;
            //Action
            var iResult = _audsStateTypeRepository.Create(audsStateTypeModel).Result;
            //Assert
            Assert.Equal(audsStateTypeModel.StateTypeId, iResult.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_All_AudsStateType_Test_OK()
        {
            //Action            
            var result = _audsStateTypeRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsStateType.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_AudsStateType_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsStateTypeRepository.GetList(new AudsStateTypeFilter() { StateTypeId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Update_AudsStateType_Test_OK()
        {
            //Arrange           
            var audsStateTypeToUpdate = _aUDSContextTest.AudsStateType.FirstOrDefault();
            var originalValue = audsStateTypeToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsStateTypeToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsStateTypeRepository.Update(audsStateTypeToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Delete_AudsStateType_Test_OK()
        {
            //Arrange
            var audsStateTypeModel = _aUDSContextTest.AudsStateType.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsStateTypeModel.CodeStatus;
            //Action
            var audsStateTypeUpdated = _audsStateTypeRepository.Delete(audsStateTypeModel.StateTypeId).Result;
            //Assert
            Assert.Equal(audsStateTypeUpdated.StateTypeId, audsStateTypeModel.StateTypeId);
            Assert.NotEqual(audsStateTypeUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Delete_AudsStateType_Test_KO()
        {
            //Action
            var iAudsStateTypeUpdated = _audsStateTypeRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsStateTypeUpdated.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_AudsStateType_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsStateTypeRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.StateTypeId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsStateTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsStateTypeUnitTest")]
        public void Get_AudsStateType_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsStateTypeRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.StateTypeId);
        }
    }
}