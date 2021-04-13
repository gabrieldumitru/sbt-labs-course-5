using sbt_labs_course_5.Models.DTOs;
using sbt_labs_course_5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Services.BookServices
{
    public interface IBookService
    {
        Task<CreateBookDTO> CreateBook(CreateBookDTO newBook);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBook(Guid id);
        Task<CreateBookDTO> UpdateBook(Guid id, CreateBookDTO updateBook);
        Task DeleteBook(Guid id);
    }
}
