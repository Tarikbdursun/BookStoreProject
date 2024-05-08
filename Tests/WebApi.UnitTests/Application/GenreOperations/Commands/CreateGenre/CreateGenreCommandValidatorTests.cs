using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Applications.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests
{
    [Theory]
    [InlineData("A")]
    [InlineData("")]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(string genreName)
    {
        var command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel{Name = genreName};
        var validator = new CreateGenreCommandValidator();

        var result = validator.Validate(command);
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        var command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel{Name = "New Genre Name"};
        var validator = new CreateGenreCommandValidator();

        var result = validator.Validate(command);
        result.Errors.Count().Should().Be(0);
    }
}