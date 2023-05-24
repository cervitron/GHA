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
    public partial class AudsGroupRepositoryTest
    {
        private readonly IAudsGroupRepository _audsGroupRepository;
        private readonly AUDSContextTest _aUDSContextTest;
        private readonly Fen2.Utils.EF.IUserResolverService _userResolver;

        public AudsGroupRepositoryTest()
        {
            _userResolver = new Fen2.Utils.EF.MoqUserResolverService();
            _aUDSContextTest = new AUDSContextTest(_userResolver, new DbContextOptions<AUDSContext>());
            _audsGroupRepository = new AudsGroupRepository(_aUDSContextTest);
            AudsGroupRepositoryDataTest.LoadContext(_aUDSContextTest);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Create_AudsGroup_Test_OK()
        {
            //Arrange 
            var audsGroupModel = _aUDSContextTest.AudsGroup.FirstOrDefault();
            audsGroupModel.GroupId = 0;
            //Action
            var iResult = _audsGroupRepository.Create(audsGroupModel).Result;
            //Assert
            Assert.Equal(audsGroupModel.GroupId, iResult.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_All_AudsGroup_Test_OK()
        {
            //Action            
            var result = _audsGroupRepository.GetList(null).Result;
            //Assert
            Assert.Equal(_aUDSContextTest.AudsGroup.Count(), result.Count());
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_AudsGroup_Filtered_Test_KO()
        {
            //Arrange            
            var result = _audsGroupRepository.GetList(new AudsGroupFilter() { GroupId = 0}).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Update_AudsGroup_Test_OK()
        {
            //Arrange           
            var audsGroupToUpdate = _aUDSContextTest.AudsGroup.FirstOrDefault();
            var originalValue = audsGroupToUpdate.CodeStatus;

            var updatedValue = originalValue.Equals(true) ? false : true;
            audsGroupToUpdate.CodeStatus = updatedValue;
            //Action
            var result = _audsGroupRepository.Update(audsGroupToUpdate).Result;
            //Assert
            Assert.NotEqual(originalValue, result.CodeStatus);
            Assert.Equal(updatedValue, result.CodeStatus);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Delete_AudsGroup_Test_OK()
        {
            //Arrange
            var audsGroupModel = _aUDSContextTest.AudsGroup.Where(x => x.CodeStatus == true).FirstOrDefault();
            var codeStatusOriginal = audsGroupModel.CodeStatus;
            //Action
            var audsGroupUpdated = _audsGroupRepository.Delete(audsGroupModel.GroupId).Result;
            //Assert
            Assert.Equal(audsGroupUpdated.GroupId, audsGroupModel.GroupId);
            Assert.NotEqual(audsGroupUpdated.CodeStatus, codeStatusOriginal);//Uncomment only in case there is a logical deletion.
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Delete_AudsGroup_Test_KO()
        {
            //Action
            var iAudsGroupUpdated = _audsGroupRepository.Delete(0).Result;
            //Assert
            Assert.Equal(0, iAudsGroupUpdated.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "OK")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_AudsGroup_by_id_Test_OK()
        {
            //Arrange
            var id = 3;
            //Action
            var result = _audsGroupRepository.Get(id).Result;
            //Assert
            Assert.Equal(id, result.GroupId);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "AudsGroupRepository")]
        [Trait("Category", "KO")]
        [Trait("Category", "AudsGroupUnitTest")]
        public void Get_AudsGroup_by_id_Test_KO()
        {
            //Arrange
            var id = -1;
            //Action
            var result = _audsGroupRepository.Get(id).Result;
            //Assert
            Assert.Equal(0, result.GroupId);
        }
    }
}