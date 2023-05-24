using everisIT.AUDS.Service.Application.Dtos;
using everisIT.AUDS.Service.Infrastructure.Models;
using everisIT.AUDS.Service.WebApi.IntegrationTest.DataTest;
using everisIT.AUDS.Service.WebApi.IntegrationTest.DataUtil;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Xunit.Priority;

namespace everisIT.AUDS.Service.WebApi.IntegrationTest
{
    [Collection("Sequential")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public partial class AudsAuditHcoIntegrationTest
    {
        private readonly HttpClient testClient;
        private readonly string requestURI;
        public AudsAuditHcoIntegrationTest()
        {
            requestURI = "/api/v1/AudsAuditHco";
            testClient = ServerTestSingleton.instanceUnique.clientTest;
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsAuditHco_Return_OK_Filtered_By_Active_CodeStatus()
        {
            //Arrange
            var filter = new Infrastructure.Filters.AudsAuditHcoFilter()
            {
                CodeStatus = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json");
            //Action
            var result = testClient.GetAsync($"{requestURI}?CodeStatus={filter.CodeStatus}").Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsAuditHcoDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
            Assert.True(parsedResult.FirstOrDefault().CodeStatus);
            Assert.True(parsedResult.LastOrDefault().CodeStatus);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_All_AudsAuditHco_Return_OK()
        {
            //Action
            var result = testClient.GetAsync(requestURI).Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsAuditHcoDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void Get_AudsAuditHco_By_Id_Return_OK()
        {
            //Arrange
            var resultGetAll = testClient.GetAsync(requestURI).Result;
            var idAudsAuditHco = JsonConvert.DeserializeObject<List<AudsAuditHcoDto>>(resultGetAll.Content.ReadAsStringAsync().Result).FirstOrDefault().AuditHcoId;
            //Action
            var result = testClient.GetAsync($"{requestURI}/{idAudsAuditHco}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.Equal(idAudsAuditHco, parsedResult.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void Get_AudsAuditHco_By_Id_Return_BadRequest_When_Id_Is_Zero()
        {
            //Arrange
            int id = 0;
            //Action
            var result = testClient.GetAsync($"{requestURI}/{id}").Result;
            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.BadRequest);
        }
        /*
         * Uncomment only if there is a physical Delete.

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Create")]
        [Trait("Category", "KO")]
        public void Create_AudsAuditHco_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PostAsync(requestURI, null).Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAuditHco_Return_NotFound_When_CodeStatus_Is_Inactive()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsAuditHcoDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsAuditHco => audsAuditHco.CodeStatus == false)
                .OrderByDescending(x => x.AuditHcoId).First();

            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.AuditHcoId}").Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsAuditHco_Return_BadRequest_When_Id_Is_Negative()
        {
            var idNotExist = -1;
            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{idNotExist}").Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsAuditHco_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PutAsync(requestURI, null).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.AuditHcoId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsAuditHco_Return_BadRequest_With_Empty_Object_Parameter()
        {
            //Arrange
            var requestContent = new StringContent(JsonConvert.SerializeObject(new AudsAuditHcoDto()), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.AuditHcoId);
        }

        [Fact, Priority(1)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Create")]
        [Trait("Category", "OK")]
        public void Create_AudsAuditHco_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsAuditHco>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.AuditHcoId).First();
            var IdLast = entity.AuditHcoId;
            entity.AuditHcoId = 0;

            AudsAuditHco newEntity = AudsAuditHcoIntegrationDataTest.ResetEntityDto(entity);
            var requestContent = new StringContent(JsonConvert.SerializeObject(newEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PostAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.Created, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.IsType<AudsAuditHcoDto>(parsedResult);
            //Assert.Equal(IdLast+1, parsedResult.AuditHcoId);
        }

        [Fact, Priority(2)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsAuditHco_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsAuditHco>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.AuditHcoId).First();
            var originalValue = entity.CodeStatus;

            AudsAuditHco changedEntity = AudsAuditHcoIntegrationDataTest.ResetEntityDto(entity);
            var updatedValue = originalValue.Equals(true) ? false : true;
            changedEntity.CodeStatus = updatedValue;
            var requestContent = new StringContent(JsonConvert.SerializeObject(changedEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.OK, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.NotEqual(originalValue, parsedResult.CodeStatus);
            Assert.Equal(updatedValue, parsedResult.CodeStatus);
        }

        [Fact, Priority(3)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsAuditHco")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsAuditHco_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsAuditHcoDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsAuditHco => audsAuditHco.CodeStatus == true)
                .OrderByDescending(x => x.AuditHcoId).First();
            var originalValue = entityDto.CodeStatus;
            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.AuditHcoId}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsAuditHcoDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(entityDto.AuditHcoId, parsedResult.AuditHcoId);
            //Assert.NotEqual(entityDto.CodeStatus, parsedResult.CodeStatus); //Uncomment only in case there is a logical deletion.
            //Assert.False(parsedResult.CodeStatus);//Uncomment only in case there is a logical deletion.
        }
        */
    }
}
