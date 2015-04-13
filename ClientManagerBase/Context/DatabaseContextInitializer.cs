using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Contacts;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;

namespace ClientManagerBase.Context
{
    /// <summary>
    /// Adds some initial data to database when it drops and creates.
    /// </summary>
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(DatabaseContext context)
        {
            using (var repository = new DatabaseRepository(context, false))
            {
                // Add settings.
                var generateDateSetting = new Setting("LastGenerate", DateTime.MinValue.ToString());
                var smtpHostSetting = new Setting("SmtpHost", "localhost");

                repository.Add(generateDateSetting);
                repository.Add(smtpHostSetting);

                // Add tags.
                var tagRich = new Tag("Rich");
                var tagStupid = new Tag("Stupid");
                var tagLazy = new Tag("Lazy");

                // Add users.
                var userService = new UserService(repository);
                var userTest = userService.CreateUser("Test", "123456", "ecymah-599@yopmail.com", "Test sender");
                var userAdmin = userService.CreateUser("Admin", "qwerty", "oreqirra-22@yopmail.com", "Admin sender");

                // Add client types.
                var typeHot = new ClientType("Hot");
                var typeCold = new ClientType("Cold");
                var typePotential = new ClientType("Potential");

                // Add clients.
                var clientHomer = new Client
                                  {
                                      Title = "Big angry guy",
                                      Details = "Possibly a very rich client with stupid ideas",
                                      FirstName = "Homer",
                                      SecondName = "Simpson",
                                      CompanyName = "Stupid Things Inc.",
                                      Description = "Company that develops things which no one use.",
                                      MainEmail = "gybotaj-100@yopmail.com",
                                      Emails = new Collection<Email>
                                               {
                                                   new Email("otynuss-116@yopmail.com"),
                                                   new Email("ybyhuttammo-568@yopmail.com"),
                                                   new Email("gybotaj-100@yopmail.com"),
                                                   new Email("ibimiz-531@yopmail.com")
                                               },
                                      Addresses = new Collection<Address>
                                                  {
                                                      new Address("USA, New-York, Main street, 666"),
                                                      new Address("Russia, Moscow, First street, 17")
                                                  },
                                      PhoneNumbers = new Collection<PhoneNumber>
                                                     {
                                                         new PhoneNumber("123-456-789"),
                                                         new PhoneNumber("+3 (777) 111-777-900")
                                                     },
                                      Websites = new Collection<Website>
                                                 {
                                                     new Website("http://rutracker.org/"),
                                                     new Website("http://microsoft.com")
                                                 },
                                      Type = typeHot,
                                      CreationDate = DateTime.Now.AddDays(-15),
                                      Tags = new Collection<Tag>
                                             {
                                                 tagRich,
                                                 tagStupid
                                             },
                                      User = userTest
                                  };

                var clientHarry = new Client
                                  {
                                      Title = "Magic guy",
                                      Details = "A very powerful hero",
                                      FirstName = "Harry",
                                      SecondName = "Potter",
                                      CompanyName = "Nowhere Inc.",
                                      Description = "Company that do nothing.",
                                      MainEmail = "qilinazi-74@yopmail.com",
                                      Emails = new Collection<Email>
                                               {
                                                   new Email("mazappinn-634@yopmail.com"),
                                                   new Email("qilinazi-74@yopmail.com"),
                                                   new Email("callajedd-445@yopmail.com")
                                               },
                                      Addresses = new Collection<Address>
                                                  {
                                                      new Address("Ukraine, Donetsk, 777"),
                                                      new Address("North Pole, 1")
                                                  },
                                      PhoneNumbers = new Collection<PhoneNumber>
                                                     {
                                                         new PhoneNumber("101"),
                                                         new PhoneNumber("+3 (123) 123-321"),
                                                         new PhoneNumber("+1 (2345) 67-89")
                                                     },
                                      Websites = new Collection<Website>
                                                 {
                                                     new Website("http://habrahabr.ru/"),
                                                     new Website("http://trash.box")
                                                 },
                                      Type = typeCold,
                                      CreationDate = DateTime.Now.AddDays(-10),
                                      Tags = new Collection<Tag>
                                             {
                                                 tagRich,
                                                 tagLazy
                                             },
                                      User = userAdmin
                                  };

                var clientKenny = new Client
                {
                    Title = "Kenny",
                    Details = "Poor client",
                    FirstName = "Kenny",
                    SecondName = "McCormick",
                    CompanyName = "South-Park",
                    Description = "Company that like to stuck in the trouble.",
                    MainEmail = "kenny-100@yopmail.com",
                    Emails = new Collection<Email>
                                               {
                                                   new Email("kenny-100@yopmail.com")
                                               },
                    Addresses = new Collection<Address>
                                                  {
                                                      new Address("USA, New-York, South Park, 999")
                                                  },
                    PhoneNumbers = new Collection<PhoneNumber>
                                                     {
                                                         new PhoneNumber("321-456-789")
                                                     },
                    Websites = new Collection<Website>
                                                 {
                                                     new Website("http://south-park.org/")
                                                 },
                    Type = typeCold,
                    CreationDate = DateTime.Now.AddDays(-20),
                    Tags = new Collection<Tag>
                                             {
                                                 tagLazy
                                             },
                    User = userTest
                };

                var clientKira = new Client
                {
                    Title = "Kira",
                    Details = string.Empty,
                    FirstName = "Yagami",
                    SecondName = "Light",
                    CompanyName = "NotesFactory",
                    Description = "Company that make's notebooks.",
                    MainEmail = "kira-books@mail.com",
                    Emails = new Collection<Email>
                                               {
                                                   new Email("kira-books@mail.com")
                                               },
                    Addresses = new Collection<Address>
                                                  {
                                                      new Address("Japan, Tokyo")
                                                  },
                    PhoneNumbers = new Collection<PhoneNumber>
                                                     {
                                                         new PhoneNumber("543-456-789")
                                                     },
                    Websites = new Collection<Website>
                                                 {
                                                     new Website("http://kira-books.org/")
                                                 },
                    Type = typeHot,
                    CreationDate = DateTime.Now.AddDays(-18),
                    Tags = new Collection<Tag>
                                             {
                                                 tagRich
                                             },
                    User = userTest
                };

                var clientFury = new Client
                {
                    Title = "FLCL",
                    Details = string.Empty,
                    FirstName = "Haruko",
                    SecondName = "Haruhara",
                    CompanyName = "Furi-Kury",
                    Description = string.Empty,
                    MainEmail = "flcl@mail.com",
                    Emails = new Collection<Email>
                                               {
                                                   new Email("flcl@mail.com")
                                               },
                    Addresses = new Collection<Address>
                                                  {
                                                      new Address("Japan, Tokyo")
                                                  },
                    PhoneNumbers = new Collection<PhoneNumber>
                                                     {
                                                         new PhoneNumber("543-456-780")
                                                     },
                    Websites = new Collection<Website>
                                                 {
                                                     new Website("http://flcl.com/")
                                                 },
                    Type = typePotential,
                    CreationDate = DateTime.Now.AddDays(-30),
                    Tags = new Collection<Tag>
                                             {
                                                 tagStupid, tagLazy
                                             },
                    User = userTest
                };

                repository.Add(clientHomer);
                repository.Add(clientHarry);
                repository.Add(clientKenny);
                repository.Add(clientKira);
                repository.Add(clientFury);

                // Add templates.
                var templateForRichAndLazy = new Template
                                             {
                                                 Name = "Notification for rich clients",
                                                 Description = "Email with notification to give us $1 000 000",
                                                 LetterSubject = "Money, money, money",
                                                 LetterContent =
                                                     "Dear {second-name} {first-name}, we are waiting your money!",
                                                 DaysCount = 10,
                                                 Tags = new Collection<Tag>
                                                        {
                                                            tagRich,
                                                            tagLazy
                                                        }
                                             };

                var templateForStupid = new Template
                                        {
                                            Name = "Rude mail template for stupid clients",
                                            Description = "Email stupid clients",
                                            LetterSubject = "Brains!",
                                            LetterContent =
                                                "Hello, {first-name}! We suggest you to buy some brains first!",
                                            DaysCount = 15,
                                            Tags = new Collection<Tag>
                                                   {
                                                       tagStupid,
                                                       tagLazy
                                                   }
                                        };

                repository.Add(templateForRichAndLazy);
                repository.Add(templateForStupid);

                // Add e-mails to the history
                var hist1 = new Letter
                                {
                                    ActualSentDate = new DateTime(2011, 8, 2),
                                    Client = clientHomer,
                                    Content = "We are regular readers of your blog and love your thoughts on marketing and branding. That’s why we wanted to inform you about a new study that will be released in the next days. The study covers 64 brands and is based on surveying more than 1,000 consumers in the US during the first quarter of 2010.",
                                    Subject = "Dear Jery",
                                    ExpectedSendDate = new DateTime(2011, 8, 2),
                                    FromNameEmail = "binary <test@com.ua>",
                                    IsCanceled = false,
                                    Template = null
                                };

                var hist2 = new Letter
                                {
                                    ActualSentDate = new DateTime(2011, 8, 29),
                                    Client = clientHomer,
                                    Content =
                                        "It was very enjoyable to speak with you today about the assistant account executive position at the Smith Agency. The job seems to be an excellent match for my skills and interests. The creative approach to account management that you described confirmed my desire to work with you.",
                                    Subject = "Thanks letter",
                                    ExpectedSendDate = new DateTime(2011, 8, 29),
                                    FromNameEmail = "binary <test@com.ua>",
                                    IsCanceled = false,
                                    Template = null
                                };

                var hist3 = new Letter
                                {
                                    ActualSentDate = new DateTime(2011, 9, 2),
                                    Client = clientHomer,
                                    Content =
                                        "In addition to my enthusiasm, I will bring to the position strong writing skills, assertiveness, and the ability to encourage others to work cooperatively with the department.",
                                    Subject = "Additional letter",
                                    ExpectedSendDate = new DateTime(2011, 9, 2),
                                    FromNameEmail = "binary <test@com.ua>",
                                    IsCanceled = false,
                                    Template = null
                                };

                var hist4 = new Letter
                                {
                                    ActualSentDate = DateTime.Now,
                                    Client = clientHomer,
                                    Content =
                                        "I appreciate the time you took to interview me. I am very interested in working for you and look forward to hearing from you regarding this position.",
                                    Subject = "Appreciate letter",
                                    ExpectedSendDate = DateTime.Now,
                                    FromNameEmail = "binary <test@com.ua>",
                                    IsCanceled = false,
                                    Template = null
                                };

                repository.Add(hist1);
                repository.Add(hist2);
                repository.Add(hist3);
                repository.Add(hist4);

                // Additional types unused in clients records.
                repository.Add(typeCold);
                repository.Add(typePotential);

                repository.SaveChanges();
            }
        }
    }
}
