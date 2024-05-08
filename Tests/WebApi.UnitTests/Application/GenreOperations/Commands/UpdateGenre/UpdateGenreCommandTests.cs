using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        int genreId =_context.Genres.Count();
        genreId++;
        var command = new UpdateGenreCommand(_context);
        command.GenreId = genreId;
        FluentActions.Invoking(command.Handle).Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kitap Türü Bulunamadı");
    }

    [Fact]
    public void WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        if(!_context.Genres.Any())
        {
            var genre1 = new Genre
            {
                Id = 1,
                Name = "Personal Growth"
            };

            var genre2 = new Genre
            {
                Id = 2,
                Name = "Science Fiction"
            };

            _context.Genres.Add(genre1);
            _context.Genres.Add(genre2);
            _context.SaveChanges();
        }
        // if(_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id==GenreId))
        //     throw new InvalidOperationException("Aynı isimli Kitap Türü Zaten Mevcut");
        string genreName = "Personal Growth";
        var command = new UpdateGenreCommand(_context);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel
        {
            Name = genreName,
            IsActive = true
        };

        FluentActions.Invoking(command.Handle).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Aynı isimli Kitap Türü Zaten Mevcut");
    }

    [Fact]
    public void WhenValidInputIsGiven_UpdateBook_ShouldBeUpdate()
    {
        if(!_context.Genres.Any())
        {
            var newGenre = new Genre
            {
                Id = 1,
                Name = "New Genre"
            };

            _context.Genres.Add(newGenre);
            _context.SaveChanges();
        }

        var command = new UpdateGenreCommand(_context);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel{ Name = "Valid New Genre", IsActive=true };
            
        FluentActions.
            Invoking(command.Handle).Should().NotThrow<InvalidOperationException>();
    }
}