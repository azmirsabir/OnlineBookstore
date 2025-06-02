
using Bookstore.Entities;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController: ControllerBase
{
    private readonly IBookService _bookService;
    
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet("search-books")]
    public async Task<ActionResult<PaginatedResult<Book>>> GetBooks([FromQuery] QueryParameters request)
    {
        return Ok(await _bookService.GetAllAsync(request));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Response<Book>>> GetBookById([FromRoute] int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        return Ok(new Response<Book>("The book fetched",book));
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response<Book>>> StoreBook(BookStoreRequest book)
    {
        var newBook = await _bookService.SaveAsync(book);
        var response = new Response<Book>("Book created successfully", newBook);
        return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, response);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Book>> UpdateBook([FromRoute] int id,BookUpdateRequest book)
    {
        var b=await _bookService.UpdateAsync(id, book);
        return Ok(new Response<Book>("Book updated successfully", b));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id)
    {
        await _bookService.DeleteAsync(id);
        return Ok(new Response<string>("Book deleted successfully",""));
    }
}