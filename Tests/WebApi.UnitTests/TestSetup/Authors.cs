using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup;
public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        if(context.Authors.Any())
            return;
        
        context.Authors.AddRange
        (
            new Author 
            {
                Name = "Eric",
                LastName = "Ries",
                Birthdate = new DateTime(1978,9,22)
            },
            new Author 
            {
                Name = "Charlotte Perkins",
                LastName = "Gilman",
                Birthdate = new DateTime(1860,7,3)
            },
            new Author 
            {
                Name = "Frank",
                LastName = "Herbert",
                Birthdate = new DateTime(1920,2,11)
            }
        );
    }
}