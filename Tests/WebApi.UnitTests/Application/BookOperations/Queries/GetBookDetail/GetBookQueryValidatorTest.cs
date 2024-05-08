
using FluentAssertions;
using WebApi.BookOperations.GetBookDetail;

namespace Applications.BookOperations.Queries.GetBookDetail;
public class GetBookQueryValidatorTest
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        var query = new GetBookQuery(null,null);
        var validator = new GetBookQueryValidator();
        query.BookId = 0;

        var result = validator.Validate(query);
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        var query = new GetBookQuery(null,null);
        var validator = new GetBookQueryValidator();
        query.BookId = 1;

        var result = validator.Validate(query);
        result.Errors.Count().Should().Be(0);
    }
}