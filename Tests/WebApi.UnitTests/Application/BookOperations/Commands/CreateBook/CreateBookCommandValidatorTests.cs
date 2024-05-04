using System.Runtime.InteropServices;
using FluentAssertions;
using WebApi.BookOperations.CreateBook;

namespace Applications.BookOperations.Commands.CreateBook;
public class CreateBookCommandValidatorTests
{
    [Theory]
    [InlineData("Lord Of The Rings",0,0,0)]
    [InlineData("Lord Of The Rings",0,1,0)]
    [InlineData("",0,1,0)]
    [InlineData("",100,1,0)]
    [InlineData("Lord",0,1,0)]
    [InlineData(" ",100,1,0)]
    //[InlineData("Lord Of The Rings",100,1,2)] //Valid Inputs
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount, int genreId,int authorId)
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null,null);
        command.Model = new CreateBookCommand.CreateBookModel
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId,
            AuthorId = authorId
        };

        //act 
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null,null);
        command.Model = new CreateBookCommand.CreateBookModel
        {
            Title = "Lord Of The Ring", //Valid
            PageCount = 200, //Valid
            PublishDate = DateTime.Now.Date,
            GenreId = 1, //Valid
            AuthorId = 1 //Valid
        };

        //act 
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count().Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null,null);
        command.Model = new CreateBookCommand.CreateBookModel
        {
            Title = "Lord Of The Ring", //Valid
            PageCount = 200, //Valid
            PublishDate = DateTime.Now.Date.AddYears(-2),
            GenreId = 1, //Valid
            AuthorId = 1 //Valid
        };

        //act 
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count().Should().Be(0);
    }
}