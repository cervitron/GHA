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
    public partial class AudsTypeRepositoryTest
    {
        private readonly IAudsTypeRepository _audsTypeRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsTypeRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsTypeRepository = new AudsTypeRepository(_aUDSContextTest);
            AudsTypeRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Create_AudsType_Test_OK()
        {
            //Arrange 
            var audsTypeModel = _aUDSContextTest.AudsType.FirstOrDefault();
            audsTypeModel.IdType = 0;
            //Action
            var iResult = _audsTypeRepository.Create(audsTypeModel).Result;
            //Assert
            Assert.Equal(audsTypeModel.IdType, iResult.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_All_AudsType_Test_OK()
        {
            //Action            
            var result = _audsTypeRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsType.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_AudsType_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsTypeRepository.GetList(new AudsTypeFilter() { IdType = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Update_AudsType_Test_OK()
        {
            //Arrange           
            var audsTypeToUpdate = _aUDSContextTest.AudsType.FirstOrDefault();
            var originalValue = audsTypeToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsTypeToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsTypeRepository.Update(audsTypeToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Delete_AudsType_Test_OK()
        {
            //Arrange
            var audsTypeModel = _aUDSContextTest.AudsType.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsTypeModel.CodeStatus;
            //Action
            var audsTypeUpdated = _audsTypeRepository.Delete(audsTypeModel.IdType).Result;
            //Assert
            Assert.Equal(audsTypeUpdated.IdType, audsTypeModel.IdType);
            Assert.NotEqual(audsTypeUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Delete_AudsType_Test_KO()
        {
            //Action
            var iAudsTypeUpdated = _audsTypeRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsTypeUpdated.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_AudsType_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsTypeRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.IdType);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsTypeRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsTypeUnitTest")]
        public void Get_AudsType_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsTypeRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.IdType);
        }
    }
}