using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sbt_labs_course_5.Models.DTOs;
using sbt_labs_course_5.Models.Entities;
using sbt_labs_course_5.Repositories;
using sbt_labs_course_5.Services.BookServices;
using sbt_labs_course_5.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Controllers.BookControllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly IBookService _bookService;
        public BookController(IRepositoryWrapper repository,
                                       UserManager<User> userManager,
                                       IBookService bookService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO newBook)
        {
            try
            {
                var book = await _bookService.CreateBook(newBook);

                return Ok(new { book });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooks();
                return Ok(new { books });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBook(Guid id)
        {
            try
            {
                var book = await _bookService.GetBook(id);
                return Ok(new { book });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] CreateBookDTO updatedBook)
        {
            try
            {
                var book = await _bookService.UpdateBook(id, updatedBook);
                return Ok(new { book });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook([FromQuery] Guid id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
