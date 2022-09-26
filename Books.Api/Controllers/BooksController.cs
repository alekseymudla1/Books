using System.Net;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using Books.Domain.Models;
using Books.Domain.Models.Actions;
using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Books.Api.Controllers;
[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IDynamoDBContext _context;
    private readonly IQueueClient _queueClient;
    
    public BooksController(IDynamoDBContext context, IQueueClient queueClient)
    {
        _context = context;
        _queueClient = queueClient;
    }
    
    [HttpGet("{bookId}")]
    public async Task<IActionResult> Get(string bookId)
    {
        var book = await _context.LoadAsync<Book>(bookId);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), 201)]
    public async Task<IActionResult> Create(BookCreateDTO bookCreateDTO)
    {
        var book = bookCreateDTO.ToBook();
        try
        {
            await _context.SaveAsync(book);
            await _queueClient.SendAction(JsonConvert.SerializeObject(new CreateAction(book)));
        }
        catch (Exception e)
        {
            Console.Write($"Error: {e}");
            throw;
        }
        return CreatedAtAction(nameof(Get), new { bookId = book.BookId }, book);
    }

    [HttpDelete("{bookId}")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(typeof(bool), 204)]
    public async Task<IActionResult> Delete(string bookId)
    {
        var table = _context.GetTargetTable<Book>();
        
        if (await _context.LoadAsync<Book>(bookId) == null)
        {
            return NoContent();
        }

        await _context.DeleteAsync<Book>(bookId);
        await _queueClient.SendAction(JsonConvert.SerializeObject(new DeleteAction(new Book() { BookId = bookId })));
        return Ok();
    }

    [HttpPut()]
    [ProducesResponseType(typeof(Book), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Update(BookDTO bookDto)
    {
        var book = await _context.LoadAsync<Book>(bookDto.BookId);
        if (book == null)
        {
            return NoContent();
        }

        var request = bookDto.ToBook();
        try
        {
            await _context.SaveAsync(request);
            await _queueClient.SendAction(JsonConvert.SerializeObject(new UpdateAction(request, book)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok(request);

    }
}