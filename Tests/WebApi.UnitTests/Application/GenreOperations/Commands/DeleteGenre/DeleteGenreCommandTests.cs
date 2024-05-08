using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;

namespace Applications.GenreOperations.Commands.DeleteGenre;
public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int genreId = 0;
        var command = new DeleteGenreCommand(_context);
        command.GenreId=genreId;

        FluentActions.Invoking(command.Handle)
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
    }

    [Fact]
    public void WhenValidInputIsGiven_Genre_ShouldBeDeleted()
    {
        int genreId = 1;
        var command = new DeleteGenreCommand(_context);
        command.GenreId = genreId;

        FluentActions.Invoking(command.Handle).Invoke();

        var genre = _context.Books.SingleOrDefault(x=>x.Id==genreId);
        genre.Should().BeNull();
    }
}