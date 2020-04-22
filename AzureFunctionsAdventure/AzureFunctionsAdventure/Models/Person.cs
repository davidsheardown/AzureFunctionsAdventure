using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;


namespace AzureFunctionsAdventure.Models
{
    public class Person
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n"); // Generates new GUID without dashes
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; } = true;
    }


    public class PersonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
    }


    // This is the class inherited from the Azure Storage Table Entity
    public class PersonTableEntity : TableEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public bool IsActive { get; set; }
    }


    // Mappings from/to DTO and Person Entity
    public static class MappingsPerson
    {
        public static PersonTableEntity toTableEntity(this Person person)
        {
            return new PersonTableEntity()
            {
                PartitionKey = "Person",
                RowKey = person.Id,
                CreatedAt = person.CreatedAt,
                Name = person.Name,
                Age = person.Age,
                Occupation = person.Occupation,
                IsActive = person.IsActive
            };
        }


        public static Person ToPerson(this PersonTableEntity person)
        {
            return new Person()
            {
                Id = person.RowKey,
                CreatedAt = person.CreatedAt,
                Name = person.Name,
                Age = person.Age,
                Occupation = person.Occupation,
                IsActive = person.IsActive
            };
        }
    }
}
