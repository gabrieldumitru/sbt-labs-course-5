using Newtonsoft.Json;
using sbt_labs_course_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.DTOs
{
    public class CreateBookDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("numberOfPages")]
        public int NumberOfPages { get; set; }
        [JsonProperty("edition")]
        public string Edition { get; set; }

        public CreateBookDTO() { }
        public CreateBookDTO(Book book)
        {
            this.Name = book.Name;
            this.Author = book.Author;
            this.NumberOfPages = book.NumberOfPages;
            this.Edition = book.Edition;
        }
    }
}
