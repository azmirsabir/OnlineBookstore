using Bookstore.Entities;
using Bookstore.Model;
using Bookstore.Model.DTOs;
using Bookstore.Model.Response;

namespace Bookstore.Services.Interfaces;

public interface IBookService
{
    Task<PaginatedResult<Book>> GetAllAsync(QueryParameters request);
    Task<Book?> GetByIdAsync(int id);
    Task<Book> SaveAsync(BookStoreRequest book);
    Task<Book> UpdateAsync(int id,BookUpdateRequest book);
    Task<bool> DeleteAsync(int id);
}