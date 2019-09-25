namespace DL.Migrations
{
    using DL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DL.Entities.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DL.Entities.Model1 context)
        {
            Users user = new Users() { Name = "User1", Email = "User1@mail.ru" };
            context.Users.Add(user);
            Users user2 = new Users() { Name = "User2", Email = "User2@mail.ru" };
            context.Users.Add(user2);

            Authors author1 = new Authors() { FirstName = "Author1FirstName", LastName = "Author1LastName" };
            context.Authors.Add(author1);
            Authors author2 = new Authors() { FirstName = "Author2FirstName", LastName = "Author2LastName" };
            context.Authors.Add(author2);

            Genre genre1 = new Genre() { Name = "Genre1" };
            context.Genre.Add(genre1);
            Genre genre2 = new Genre() { Name = "Genre2" };
            context.Genre.Add(genre2);
            context.SaveChanges();

            Books book1 = new Books() { AuthorId = author1.Id, GenreId = genre1.Id, Title = "Book1", Pages = 100, Price = 1000 };
            context.Books.Add(book1);
            Books book2 = new Books() { AuthorId = author2.Id, GenreId = genre2.Id, Title = "Book2", Pages = 200, Price = 2000 };
            context.Books.Add(book2);

            context.SaveChanges();
        }
    }
}
