using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Applications.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        var query = new GetGenreDetailQuery(null,null);
        query.GenreId = 0;
        
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        var query = new GetGenreDetailQuery(null,null);
        query.GenreId = 1;
        
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count().Should().Be(0);
    }
}