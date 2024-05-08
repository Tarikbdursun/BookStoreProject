
using FluentAssertions;
using WebApi.BookOperations.DeleteBooks;

namespace Applications.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        int bookId = 0;
        var command = new DeleteBookCommand(null);
        command.BookId = bookId;

        var validator = new DeleteBookCommandValidator();

        var result = validator.Validate(command);

        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        int bookId = 1;
        var command = new DeleteBookCommand(null);
        command.BookId = bookId;

        var validator = new DeleteBookCommandValidator();

        var result = validator.Validate(command);

        result.Errors.Count().Should().Be(0);
    }
}