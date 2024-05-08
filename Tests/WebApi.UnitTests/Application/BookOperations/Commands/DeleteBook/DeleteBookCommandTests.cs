using System.Runtime.InteropServices;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBooks;
using WebApi.DBOperations;

namespace Applications.BookOperations.Commands.DeleteBook;
public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    
    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int bookId = 0;
        var command = new DeleteBookCommand(_context);
        command.BookId=bookId;

        FluentActions.Invoking(command.Handle)
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Olan Kitap Bulunamadı");
    }

    [Fact]
    public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
    {
        int bookId = 1;
        var command = new DeleteBookCommand(_context);
        command.BookId = bookId;

        FluentActions.Invoking(command.Handle).Invoke();

        var book = _context.Books.SingleOrDefault(book=>book.Id==bookId);
        book.Should().BeNull();
    }
}