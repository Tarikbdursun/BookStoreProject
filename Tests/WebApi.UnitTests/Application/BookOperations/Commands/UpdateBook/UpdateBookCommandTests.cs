using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.BookOperations.Commands.UpdateBook;

 public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
 {
    private readonly BookStoreDbContext _context;
    
    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int inputId =_context.Books.Count();
        inputId++;
        var command = new UpdateBookCommand(_context);
        FluentActions.Invoking(command.Handle).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek Kitap Bulunamadı.");
    }

    [Fact]
    public void WhenValidInputIsGiven_UpdateBook_ShouldBeUpdate()
    {
        if(!_context.Books.Any())
        {
            var newBook = new Book
            {
                Id = 1,
                Title = "NewBook",
                PageCount = 200,
                GenreId = 1,
                AuthorId = 1,
                PublishDate = DateTime.Now.Date.AddYears(-2)
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();
        }
        var command = new UpdateBookCommand(_context);
        var book = _context.Books.SingleOrDefault(x => x.Id == 1);
        command.BookId = book.Id;
            
        FluentActions.
            Invoking(command.Handle).Should().NotThrow<InvalidOperationException>();
    }
 }