using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;

namespace Applications.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = 0;

        FluentActions.Invoking(query.Handle)
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
    }

    [Fact]
    public void WhenValidInputIsGiven_Genre_ShouldNotBeReturnError()
    {
        var query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = 1;

        var genre = _context.Genres.SingleOrDefault(x => x.Id == query.GenreId);
        genre.Should().NotBeNull();
    }
}