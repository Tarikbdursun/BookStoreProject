using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Applications.BookOperations.Commands.CreateBook;
public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    
    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExisBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //arrange (hazırlık)
        var book = new Book
        {
            Title="WhenAlreadyExisBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            PageCount=100,
            PublishDate = new DateTime(1990, 01, 10),
            GenreId = 1,
            AuthorId = 1
        };
        _context.Books.Add(book);
        _context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_context,_mapper);
        command.Model = new CreateBookCommand.CreateBookModel{Title = book.Title};

        //act (çalıştırma) & assert (doğrulama)
        FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //arrange (hazırlık)
        CreateBookCommand command = new CreateBookCommand(_context,_mapper);
        var model = new CreateBookCommand.CreateBookModel
        {
            Title="Hobbit",
            PageCount=1000,
            PublishDate = new DateTime(1990, 01, 10),
            GenreId = 1,
            AuthorId = 1
        };
        command.Model = model; 

        //act (çalıştırma) 
        FluentActions
            .Invoking(()=> command.Handle()).Invoke();
            //.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Zaten Mevcut");
        
        //assert (doğrulama)
        var book = _context.Books.SingleOrDefault(book=>book.Title==model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        //book.Title.Should().Be(model.Title); Unrequired
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);
    }
}