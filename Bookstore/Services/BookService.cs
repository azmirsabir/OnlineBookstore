using Bookstore.Data;
using Bookstore.Entities;
using Bookstore.Exceptions;
using Bookstore.Extensions;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services;

public class BookService: IBookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<Book>> GetAllAsync(QueryParameters request)
    {
        return await _context.Books.ApplySearchAndSort(request).PaginateAsync(request.PerPage,request.Page);
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book=await _context.Books.FindAsync(id);
        if(book is null) throw new NotFoundException("Book Not found");
        return book;
    }

    public async Task<Book> SaveAsync(BookStoreRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            IsAvailable = true
        };
        
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateAsync(int id, BookUpdateRequest request)
    {
        var existingBook = await _context.Books.FindAsync(id);

        if (existingBook is null)
            throw new NotFoundException("Book not found");

        existingBook.Title = request.Title;
        existingBook.Author = request.Author;
        existingBook.Genre = request.Genre;
        existingBook.Price = request.Price;
        existingBook.IsAvailable = request.IsAvailable;

        _context.Books.Update(existingBook);
        await _context.SaveChangesAsync();

        return existingBook;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) throw new NotFoundException("Book not found");

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}