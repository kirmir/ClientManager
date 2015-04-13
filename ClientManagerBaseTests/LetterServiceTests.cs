using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using ClientManagerBase.Models;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Models.Letters;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;
using Moq;
using NUnit.Framework;

namespace ClientManagerBaseTests
{
    /// <summary>
    /// Tests for <see cref="LetterService"/>.
    /// </summary>
    [TestFixture]
    public class LetterServiceTests
    {
        /// <summary>
        /// Test for CreateFromTemplate method.
        /// </summary>
        [Test]
        public void CreateFromTemplateTest()
        {
            // Setup
            var template = new Template
                           {
                               DaysCount = 10,
                               Description = "Test template",
                               LetterContent = "Hello, {second-name} {first-name}! How are you? Good-bye, {first-name}.",
                               LetterSubject = "Greetings"
                           };

            var user = new User
                       {
                           Name = "Harry Potter",
                           Email = "harry@potters.com"
                       };

            var client = new Client
                         {
                             FirstName = "Homer",
                             SecondName = "Simpson",
                             CreationDate = new DateTime(2000, 10, 1),
                             MainEmail = "homer@simpsons.com",
                             User = user
                         };

            // Execute
            var letter = LetterService.CreateFromTemplate(template, client, user);

            // Verify
            Assert.AreSame(client, letter.Client);
            Assert.AreSame(template, letter.Template);
            Assert.AreEqual("Greetings", letter.Subject);
            Assert.AreEqual("Hello, Simpson Homer! How are you? Good-bye, Homer.", letter.Content);
            Assert.AreEqual(new DateTime(2000, 10, 11), letter.ExpectedSendDate);
            Assert.IsNull(letter.ActualSentDate);
        }

        /// <summary>
        /// Test for GenerateNewLetters method.
        /// </summary>
        [Test]
        [Ignore]
        public void GenerateNewLettersTest()
        {
            // Setup
            var tagRich = new Tag("Rich");
            var tagStupid = new Tag("Stupid");
            var tagLazy = new Tag("Lazy");

            var userTest = new User
                           {
                               Email = "test@mail.com",
                               Representation = "Test sender"
                           };

            var clients = new List<Client>
                          {
                              new Client
                              {
                                  Id = 1,
                                  FirstName = "Homer",
                                  SecondName = "Simpson",
                                  MainEmail = "homer@mail.com",
                                  CreationDate = DateTime.Now.AddDays(-7),
                                  Tags = new Collection<Tag>
                                         {
                                             tagRich,
                                             tagStupid
                                         },
                                  User = userTest
                              },

                              new Client
                              {
                                  Id = 2,
                                  FirstName = "Harry",
                                  SecondName = "Potter",
                                  MainEmail = "harry@mail.com",
                                  CreationDate = DateTime.Now.AddDays(-20),
                                  Tags = new Collection<Tag>
                                         {
                                             tagRich,
                                             tagLazy
                                         },
                                  User = userTest
                              }
                          };

            var templates = new List<Template>
                            {
                                new Template
                                {
                                    Name = "Notification for rich clients",
                                    Description = "Email with notification to give us $1 000 000",
                                    LetterSubject = "Money, money, money",
                                    LetterContent =
                                        "Dear {second-name} {first-name}, we are waiting your money!",
                                    DaysCount = 5,
                                    Tags = new Collection<Tag>
                                           {
                                               tagRich,
                                               tagStupid
                                           }
                                },
                                new Template
                                {
                                    Name = "Rude mail template for stupid clients",
                                    Description = "Email stupid clients",
                                    LetterSubject = "Brains!",
                                    LetterContent =
                                        "Hello, {first-name}! We suggest you to buy some brains first!",
                                    DaysCount = 10,
                                    Tags = new Collection<Tag>
                                           {
                                               tagStupid,
                                               tagLazy
                                           }
                                }
                            };

            var letters = new List<Letter>
                          {
                              new Letter
                              {
                                  ClientId = 1,
                                  Template = templates[1]
                              }
                          };

            tagLazy.Templates = new List<Template> { templates[1] };
            tagRich.Templates = new List<Template> { templates[0] };
            tagStupid.Templates = new List<Template> { templates[1] };

            List<Letter> added = null;

            var repositoryMock = new Mock<IDataRepository>();
            repositoryMock.Setup(r => r.All<Client>()).Returns(clients.AsQueryable());
            repositoryMock.Setup(r => r.All(It.IsAny<Expression<Func<Letter, bool>>>()))
                .Returns((Expression<Func<Letter, bool>> predicate) => letters.Where(predicate.Compile()).AsQueryable());
            repositoryMock.Setup(r => r.Add(It.IsAny<IEnumerable<Letter>>()))
                .Callback((IEnumerable<Letter> newLetters) => added = newLetters.ToList());
            
            // Execute
            var service = new LetterService(repositoryMock.Object);
            service.GenerateNewLetters(DateTime.Now.AddDays(-30));

            // Verify
            Assert.IsTrue(added.Count == 3);

            Assert.AreEqual(clients[0], added[0].Client);
            Assert.AreEqual(templates[0], added[0].Template);
            Assert.AreEqual(DateTime.Now.AddDays(-2).Date, added[0].ExpectedSendDate.Date);

            Assert.AreEqual(clients[1], added[1].Client);
            Assert.AreEqual(templates[0], added[1].Template);
            Assert.AreEqual(DateTime.Now.AddDays(-15).Date, added[1].ExpectedSendDate.Date);

            Assert.AreEqual(clients[1], added[2].Client);
            Assert.AreEqual(templates[1], added[2].Template);
            Assert.AreEqual(DateTime.Now.AddDays(-10).Date, added[2].ExpectedSendDate.Date);
        }
    }
}
