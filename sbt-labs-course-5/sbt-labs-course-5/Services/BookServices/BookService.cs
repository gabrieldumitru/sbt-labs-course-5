using Microsoft.EntityFrameworkCore;
using sbt_labs_course_5.Models.DTOs;
using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IRepositoryWrapper _repository;

        public BookService(IRepositoryWrapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CreateBookDTO> CreateBook(CreateBookDTO newBook)
        {
            var book = new Book(newBook);

            _repository.Book.Create(book);
            await _repository.SaveAsync();

            return new CreateBookDTO(book);
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _repository.Book.GetByIdAsync(id);

            if (book == null)
                throw new Exception("Book not found!");

            _repository.Book.Delete(book);
            await _repository.SaveAsync();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _repository.Book.GetAll().ToListAsync();
        }

        public async Task<Book> GetBook(Guid id)
        {
            var book = await _repository.Book.GetByIdAsync(id);
            if (book == null)
                throw new Exception("Book not found!");

            return book;
        }

        public async Task<CreateBookDTO> UpdateBook(Guid id, CreateBookDTO updateBook)
        {
            var book = await _repository.Book.GetByIdAsync(id);
            if (book == null)
                throw new Exception("Book not found!");

            book.Name = updateBook.Name;
            book.Author = updateBook.Author;
            book.Edition = updateBook.Edition;
            book.NumberOfPages = updateBook.NumberOfPages;

            await _repository.SaveAsync();

            return new CreateBookDTO(book);
        }
    }
}
