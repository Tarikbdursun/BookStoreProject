using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Applications.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        int genreId = 0;
        var command = new DeleteGenreCommand(null);
        command.GenreId = genreId;

        var validator = new DeleteGenreCommandValidator();

        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        int genreId = 1;
        var command = new DeleteGenreCommand(null);
        command.GenreId = genreId;

        var validator = new DeleteGenreCommandValidator();

        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}