
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;

namespace Applications.BookOperations.Commands.UpdateBook;
public class UpdateBookCommandValidatorTests
{
    [Theory]
    [InlineData(0,0,0,"")] //i,i,i,i
    [InlineData(0,1,0,"")] //i,v,i,i
    [InlineData(1,1,1,"")] //v,v,v,i
    [InlineData(0,6,6,"Book Title")] //i,i,i,v
    [InlineData(0,6,1,"")] //i,i,v,i
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, int genreId, int authorId, string bookTitle)
    {
        //arrange
        UpdateBookCommand command = new(null);
        command.BookId = bookId;
        command.Model = new UpdateBookModel
        {
            Title = bookTitle,
            GenreId = genreId,
            AuthorId = authorId
        };

        //act
        UpdateBookCommandValidator validator = new();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        //arrange
        UpdateBookCommand command = new(null);
        command.BookId=1;
        command.Model = new UpdateBookModel
        {
            Title = "Book Title",
            GenreId = 1,
            AuthorId = 1
        };

        //act
        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count().Should().Be(0);
    }
}