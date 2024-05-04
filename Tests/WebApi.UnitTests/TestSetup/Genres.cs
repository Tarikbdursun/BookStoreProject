using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;

public static class Genres
{
    public static void AddGenres(this BookStoreDbContext context)
    {
        if(context.Genres.Any())
            return;

        context.Genres.AddRange
        (
            new Genre
            {
                Name="Personal Growth"
            },
            new Genre
            {
                Name="Science Fiction"
            },
            new Genre
            {
                Name="Romance"
            }   
        );
    }
}