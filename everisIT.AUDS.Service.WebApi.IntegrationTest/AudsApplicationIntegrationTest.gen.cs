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
    public partial class AudsApplicationIntegrationTest
    {
        private readonly HttpClient testClient;
        private readonly string requestURI;
        public AudsApplicationIntegrationTest()
        {
            requestURI = "/api/v1/AudsApplication";
            testClient = ServerTestSingleton.instanceUnique.clientTest;
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsApplication_Return_OK_Filtered_By_Active_CodeStatus()
        {
            //Arrange
            var filter = new Infrastructure.Filters.AudsApplicationFilter()
            {
                CodeStatus = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json");
            //Action
            var result = testClient.GetAsync($"{requestURI}?CodeStatus={filter.CodeStatus}").Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsApplicationDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
            Assert.True(parsedResult.FirstOrDefault().CodeStatus);
            Assert.True(parsedResult.LastOrDefault().CodeStatus);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_All_AudsApplication_Return_OK()
        {
            //Action
            var result = testClient.GetAsync(requestURI).Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsApplicationDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void Get_AudsApplication_By_Id_Return_OK()
        {
            //Arrange
            var resultGetAll = testClient.GetAsync(requestURI).Result;
            var idAudsApplication = JsonConvert.DeserializeObject<List<AudsApplicationDto>>(resultGetAll.Content.ReadAsStringAsync().Result).FirstOrDefault().ApplicationId;
            //Action
            var result = testClient.GetAsync($"{requestURI}/{idAudsApplication}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.Equal(idAudsApplication, parsedResult.ApplicationId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void Get_AudsApplication_By_Id_Return_BadRequest_When_Id_Is_Zero()
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
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Create")]
        [Trait("Category", "KO")]
        public void Create_AudsApplication_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PostAsync(requestURI, null).Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsApplication_Return_NotFound_When_CodeStatus_Is_Inactive()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsApplicationDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsApplication => audsApplication.CodeStatus == false)
                .OrderByDescending(x => x.ApplicationId).First();

            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.ApplicationId}").Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsApplication_Return_BadRequest_When_Id_Is_Negative()
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
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsApplication_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PutAsync(requestURI, null).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.ApplicationId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsApplication_Return_BadRequest_With_Empty_Object_Parameter()
        {
            //Arrange
            var requestContent = new StringContent(JsonConvert.SerializeObject(new AudsApplicationDto()), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.ApplicationId);
        }

        [Fact, Priority(1)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Create")]
        [Trait("Category", "OK")]
        public void Create_AudsApplication_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsApplication>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.ApplicationId).First();
            var IdLast = entity.ApplicationId;
            entity.ApplicationId = 0;

            AudsApplication newEntity = AudsApplicationIntegrationDataTest.ResetEntityDto(entity);
            var requestContent = new StringContent(JsonConvert.SerializeObject(newEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PostAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.Created, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.IsType<AudsApplicationDto>(parsedResult);
            //Assert.Equal(IdLast+1, parsedResult.ApplicationId);
        }

        [Fact, Priority(2)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsApplication_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsApplication>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.ApplicationId).First();
            var originalValue = entity.CodeStatus;

            AudsApplication changedEntity = AudsApplicationIntegrationDataTest.ResetEntityDto(entity);
            var updatedValue = originalValue.Equals(true) ? false : true;
            changedEntity.CodeStatus = updatedValue;
            var requestContent = new StringContent(JsonConvert.SerializeObject(changedEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.OK, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.NotEqual(originalValue, parsedResult.CodeStatus);
            Assert.Equal(updatedValue, parsedResult.CodeStatus);
        }

        [Fact, Priority(3)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsApplication")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsApplication_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsApplicationDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsApplication => audsApplication.CodeStatus == true)
                .OrderByDescending(x => x.ApplicationId).First();
            var originalValue = entityDto.CodeStatus;
            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.ApplicationId}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsApplicationDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(entityDto.ApplicationId, parsedResult.ApplicationId);
            //Assert.NotEqual(entityDto.CodeStatus, parsedResult.CodeStatus); //Uncomment only in case there is a logical deletion.
            //Assert.False(parsedResult.CodeStatus);//Uncomment only in case there is a logical deletion.
        }
        */
    }
}
