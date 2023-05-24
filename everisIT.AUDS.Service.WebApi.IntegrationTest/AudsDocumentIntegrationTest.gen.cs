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
    public partial class AudsDocumentIntegrationTest
    {
        private readonly HttpClient testClient;
        private readonly string requestURI;
        public AudsDocumentIntegrationTest()
        {
            requestURI = "/api/v1/AudsDocument";
            testClient = ServerTestSingleton.instanceUnique.clientTest;
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_AudsDocument_Return_OK_Filtered_By_Active_CodeStatus()
        {
            //Arrange
            var filter = new Infrastructure.Filters.AudsDocumentFilter()
            {
                CodeStatus = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json");
            //Action
            var result = testClient.GetAsync($"{requestURI}?CodeStatus={filter.CodeStatus}").Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsDocumentDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
            Assert.True(parsedResult.FirstOrDefault().CodeStatus);
            Assert.True(parsedResult.LastOrDefault().CodeStatus);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "GetList")]
        [Trait("Category", "OK")]
        public void GetList_All_AudsDocument_Return_OK()
        {
            //Action
            var result = testClient.GetAsync(requestURI).Result;
            var parsedResult = JsonConvert.DeserializeObject<List<AudsDocumentDto>>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.True(parsedResult.Any());
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "GetById")]
        [Trait("Category", "OK")]
        public void Get_AudsDocument_By_Id_Return_OK()
        {
            //Arrange
            var resultGetAll = testClient.GetAsync(requestURI).Result;
            var idAudsDocument = JsonConvert.DeserializeObject<List<AudsDocumentDto>>(resultGetAll.Content.ReadAsStringAsync().Result).FirstOrDefault().DocumentId;
            //Action
            var result = testClient.GetAsync($"{requestURI}/{idAudsDocument}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(result.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.True(result.IsSuccessStatusCode);
            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.Equal(idAudsDocument, parsedResult.DocumentId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "GetById")]
        [Trait("Category", "KO")]
        public void Get_AudsDocument_By_Id_Return_BadRequest_When_Id_Is_Zero()
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
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Create")]
        [Trait("Category", "KO")]
        public void Create_AudsDocument_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PostAsync(requestURI, null).Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsDocument_Return_NotFound_When_CodeStatus_Is_Inactive()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsDocumentDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsDocument => audsDocument.CodeStatus == false)
                .OrderByDescending(x => x.DocumentId).First();

            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.DocumentId}").Result;
            //Assert
            Assert.False(returned.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, returned.StatusCode);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Delete")]
        [Trait("Category", "KO")]
        public void Delete_AudsDocument_Return_BadRequest_When_Id_Is_Negative()
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
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsDocument_Return_UnsupportedMediaType_With_Null_Object_Parameter()
        {
            //Action
            var returned = testClient.PutAsync(requestURI, null).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.DocumentId);
        }

        [Fact]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Update")]
        [Trait("Category", "KO")]
        public void Update_AudsDocument_Return_BadRequest_With_Empty_Object_Parameter()
        {
            //Arrange
            var requestContent = new StringContent(JsonConvert.SerializeObject(new AudsDocumentDto()), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, returned.StatusCode);
            Assert.False(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.Equal(0, parsedResult.DocumentId);
        }

        [Fact, Priority(1)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Create")]
        [Trait("Category", "OK")]
        public void Create_AudsDocument_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsDocument>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.DocumentId).First();
            var IdLast = entity.DocumentId;
            entity.DocumentId = 0;

            AudsDocument newEntity = AudsDocumentIntegrationDataTest.ResetEntityDto(entity);
            var requestContent = new StringContent(JsonConvert.SerializeObject(newEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PostAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.Created, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.IsType<AudsDocumentDto>(parsedResult);
            //Assert.Equal(IdLast+1, parsedResult.DocumentId);
        }

        [Fact, Priority(2)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Update")]
        [Trait("Category", "OK")]
        public void Update_AudsDocument_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entity = JsonConvert.DeserializeObject<List<AudsDocument>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .OrderByDescending(x => x.DocumentId).First();
            var originalValue = entity.CodeStatus;

            AudsDocument changedEntity = AudsDocumentIntegrationDataTest.ResetEntityDto(entity);
            var updatedValue = originalValue.Equals(true) ? false : true;
            changedEntity.CodeStatus = updatedValue;
            var requestContent = new StringContent(JsonConvert.SerializeObject(changedEntity), Encoding.UTF8, "application/json");
            //Action
            var returned = testClient.PutAsync(requestURI, requestContent).Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(HttpStatusCode.OK, returned.StatusCode);
            Assert.True(returned.IsSuccessStatusCode);
            Assert.NotNull(parsedResult);
            Assert.NotEqual(originalValue, parsedResult.CodeStatus);
            Assert.Equal(updatedValue, parsedResult.CodeStatus);
        }

        [Fact, Priority(3)]
        [Trait("Category", "IntegrationTest")]
        [Trait("Category", "AudsDocument")]
        [Trait("Category", "Delete")]
        [Trait("Category", "OK")]
        public void Delete_AudsDocument_Return_OK()
        {
            //Arrange
            var listAllEntityDto = testClient.GetAsync(requestURI).Result;
            var entityDto = JsonConvert.DeserializeObject<List<AudsDocumentDto>>(listAllEntityDto.Content.ReadAsStringAsync().Result)
                .Where(audsDocument => audsDocument.CodeStatus == true)
                .OrderByDescending(x => x.DocumentId).First();
            var originalValue = entityDto.CodeStatus;
            //Action
            var returned = testClient.DeleteAsync($"{requestURI}/{entityDto.DocumentId}").Result;
            var parsedResult = JsonConvert.DeserializeObject<AudsDocumentDto>(returned.Content.ReadAsStringAsync().Result);
            //Assert
            Assert.Equal(entityDto.DocumentId, parsedResult.DocumentId);
            //Assert.NotEqual(entityDto.CodeStatus, parsedResult.CodeStatus); //Uncomment only in case there is a logical deletion.
            //Assert.False(parsedResult.CodeStatus);//Uncomment only in case there is a logical deletion.
        }
        */
    }
}
