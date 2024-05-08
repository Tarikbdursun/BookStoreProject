using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Applications.GenreOperations.Commands.UpdateGenre;
public class UpdateGenreCommandValidatorTests
{
    //RuleFor(command=>command.Model.Name).MinimumLength(2).When(x=>x.Model.Name.Trim() != string.Empty);
    [Theory]
    [InlineData("")]
    [InlineData("A")]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(string name)
    {
        var command = new UpdateGenreCommand(null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel{Name=name, IsActive = true};
        
        var validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        var command = new UpdateGenreCommand(null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel{Name="name", IsActive = true};
        
        var validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}