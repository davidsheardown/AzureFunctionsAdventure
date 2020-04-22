using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctionsAdventure.Models;
using AzureFunctionsAdventure.Helpers;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace AzureFunctionsAdventure
{
    public static class PersonApi
    {
        [FunctionName("PersonApiCreate")]
        public static async Task<IActionResult> CreatePerson(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "person")] HttpRequest req,
            [Table("person", Connection = "AzureWebJobsStorage")] IAsyncCollector<PersonTableEntity> personTable,
            ILogger log)
        {
            log.LogInformation("PersonApi: Create");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var jsonObject = JsonConvert.DeserializeObject<PersonDTO>(requestBody);


            try
            {
                if (jsonObject != null)
                {
                    var person = new Person()
                    {
                        Name = jsonObject.Name,
                        Age = jsonObject.Age,
                        Occupation = jsonObject.Occupation,
                        IsActive = true
                    };
                    await personTable.AddAsync(person.toTableEntity());
                    return new OkObjectResult(new ApiJsonResponse()
                    {
                        Success = true,
                        ErrorMessage = null,
                        Entity = person
                    });
                }
                else
                {
                    return new BadRequestObjectResult(new ApiJsonResponse()
                    {
                        Entity = null,
                        Success = false,
                        ErrorMessage = "No person object sent"
                    });
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ApiJsonResponse()
                {
                    Entity = jsonObject,
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }


        }


        [FunctionName("PersonApiGetPeople")]
        public static async Task<IActionResult> GetAllPeople(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "person")] HttpRequest req,
            [Table("person", Connection = "AzureWebJobsStorage")] CloudTable personTable,
            ILogger log)
        {
            log.LogInformation("PersonApi: Get All People");


            var query = new TableQuery<PersonTableEntity>();
            var segment = await personTable.ExecuteQuerySegmentedAsync(query, null);
            List<Person> people = new List<Person>();
            foreach (var item in segment.Results)
            {
                people.Add(MappingsPersion.ToPerson(item));
            }


            return new OkObjectResult(new ApiJsonResponse()
            {
                Success = true,
                Entity = people
            });
        }


        [FunctionName("PersonApiGetPersonById")]
        public static async Task<IActionResult> PersonApiGetPersonById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "person/{id}")] HttpRequest req,
            [Table("person", Connection = "AzureWebJobsStorage")] CloudTable personTable,
            ILogger log,
            string id)
        {
            log.LogInformation("PersonApi: Get Person by Id");


            if (id == null)
            {
                log.LogError($"Person by Id {id} not found");
                return new NotFoundResult();
            }


            var query = new TableQuery<PersonTableEntity>().Where($"Segment='Person', RowKey={id}");
            var segment = await personTable.ExecuteQuerySegmentedAsync(query, null);


            return new OkObjectResult(new ApiJsonResponse()
            {
                Success = true,
                Entity = segment.Results
            });
        }
    }
}
