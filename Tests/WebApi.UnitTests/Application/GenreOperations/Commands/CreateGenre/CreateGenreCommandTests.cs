using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExisGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //arrange (hazırlık)
        var genre = new Genre
        {
            Name="WhenAlreadyExistGenreIsGiven_InvalidOperationException_ShouldBeReturn",
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = new CreateGenreModel{Name = genre.Name};

        //act (çalıştırma) & assert (doğrulama)
        FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Var");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        //arrange (hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(_context);
        var model = new CreateGenreModel
        {
            Name = "Yeni Kitap Türü"
        };
        command.Model = model; 

        //act (çalıştırma) 
        FluentActions
            .Invoking(()=> command.Handle()).Invoke();
            //.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");
        
        //assert (doğrulama)
        var genre = _context.Genres.SingleOrDefault(x=>x.Name==model.Name);
        genre.Should().NotBeNull();
    }
}