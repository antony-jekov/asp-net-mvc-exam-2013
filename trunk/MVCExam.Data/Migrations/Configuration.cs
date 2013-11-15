using MVCExam.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MVCExam.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<MVCExam.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MVCExam.Data.ApplicationDbContext context)
        {
            if (!context.PriorityTypes.Any())
            {
                PriorityType low = new PriorityType()
                {
                    Type = "Low"
                };

                PriorityType medium = new PriorityType()
                {
                    Type = "Medium"
                };

                PriorityType high = new PriorityType()
                {
                    Type = "High"
                };

                context.PriorityTypes.Add(low);
                context.PriorityTypes.Add(medium);
                context.PriorityTypes.Add(high);

                context.SaveChanges();

                ApplicationUser testUser = new ApplicationUser() { UserName = "KOBAPHATA KPABA", Points = 10 };
                context.Users.Add(testUser);

                Category cat1 = new Category() { Name = "Front-End" };
                Category cat2 = new Category() { Name = "Back-End" };
                Category cat3 = new Category() { Name = "Core" };
                Category cat4 = new Category() { Name = "Administration" };
                Category cat5 = new Category() { Name = "Support" };

                context.Category.Add(cat1);
                context.Category.Add(cat2);
                context.Category.Add(cat3);
                context.Category.Add(cat4);
                context.Category.Add(cat5);

                context.SaveChanges();

                Ticket ticket1 = new Ticket()
                {
                    Author = testUser,
                    Category = cat1,
                    Priority = low,
                    Title = "Нещо по сайта не работи - ХАААА! :)",
                    ScreenshotUrl = "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg",
                    Description = "Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description v"
                };

                testUser.Points++;

                ticket1.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Да сега пак го видях! Ето го!"
                });

                ticket1.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Сега не го виждам..."
                });

                ticket1.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Аааа, ето го пак!!!!!"
                });

                Ticket ticket2 = new Ticket()
                {
                    Author = testUser,
                    Category = cat1,
                    Priority = low,
                    Title = "Имате картинка, която не се зарежда...",
                    ScreenshotUrl = "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg",
                    Description = "Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description v"
                };

                testUser.Points++;

                ticket2.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Pak vijdam neshtooo!"
                });

                ticket2.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Абе сега май се зареди..."
                });

                ticket2.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Ммм не пак я отняма."
                });

                Ticket ticket3 = new Ticket()
                {
                    Author = testUser,
                    Category = cat1,
                    Priority = high,
                    Title = "Невъзможно е да се регистрира нов потребител, сайта хвърля 500",
                    ScreenshotUrl = "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg",
                    Description = "Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description Maleee description v"
                };

                testUser.Points++;

                ticket3.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Още не мога да се регна!"
                });

                ticket3.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Пешо и той вика, че не може!"
                });

                ticket3.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Айде де!"
                });

                Ticket ticket4 = new Ticket()
                {
                    Author = testUser,
                    Category = cat1,
                    Priority = high,
                    Title = "Не мога да се регистрирам!",
                    ScreenshotUrl = "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg"
                };

                testUser.Points++;

                ticket4.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Виждам, че и други имат същият проблем... да изчакам освен?"
                });

                ticket4.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Още колко горе долу ще трае това?"
                });

                Ticket ticket5 = new Ticket()
                {
                    Author = testUser,
                    Category = cat2,
                    Priority = high,
                    Title = "Запали се сървърното!!!",
                    ScreenshotUrl = "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg"
                };

                testUser.Points++;

                ticket5.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Какво да правя!!!"
                });

                ticket5.Comments.Add(new Comment
                {
                    Author = testUser,
                    Content = "Не се занимавай, тръгвай си."
                });

                context.Tickets.Add(ticket1);
                context.Tickets.Add(ticket2);
                context.Tickets.Add(ticket3);
                context.Tickets.Add(ticket4);
                context.Tickets.Add(ticket5);

                Random rand = new Random();

                int ticketsCount = 300;

                for (int i = 0; i < ticketsCount; i++)
                {
                    context.Tickets.Add(new Ticket
                        {
                            Author = testUser,
                            CategoryId = rand.Next(1, 6),
                            Description = "Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie Opisanie ",
                            PriorityId = rand.Next(1, 4),
                            Title = "Test Ticket",
                            ScreenshotUrl = (rand.Next(0, 101) <= 20 ? "http://d1c739w2xm33i4.cloudfront.net/2.2/top_image.jpg" : "")
                        });

                    testUser.Points++;
                }

                context.SaveChanges();

                int commentsCount = rand.Next(1, 6);

                for (int i = 0; i < commentsCount; i++)
                {
                    foreach (var item in context.Tickets.ToList())
                    {
                        item.Comments.Add(new Comment
                        {
                            Author = testUser,
                            Content = "Test comment... Test comment..."
                        });
                    }
                }

                context.SaveChanges();
            }
        }
    }
}