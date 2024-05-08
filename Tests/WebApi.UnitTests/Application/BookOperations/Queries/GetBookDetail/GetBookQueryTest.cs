using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;

namespace Applications.BookOperations.Queries.GetBookDetail;

public class GetBookQueryTest : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public GetBookQueryTest(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var query = new GetBookQuery(_context, _mapper);
        query.BookId = 0;
        FluentActions.Invoking(query.Handle)
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");

    }

    [Fact]
    public void WhenValidInputIsGiven_Book_ShouldNotBeReturnError()
    {
        var query = new GetBookQuery(_context, _mapper);
        query.BookId = 1;
        
        var book = _context.Books.SingleOrDefault(x => x.Id == query.BookId);
        book.Should().NotBeNull();
    }
}