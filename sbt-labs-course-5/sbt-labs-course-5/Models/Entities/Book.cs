using sbt_labs_course_5.Models.Base;
using sbt_labs_course_5.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Models.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public string Edition { get; set; }

        protected Book() { }

        public Book(CreateBookDTO book)
        {
            this.Name = book.Name;
            this.Author = book.Author;
            this.NumberOfPages = book.NumberOfPages;
            this.Edition = book.Edition;
        }
    }
}
