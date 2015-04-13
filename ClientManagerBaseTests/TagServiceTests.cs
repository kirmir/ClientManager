using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ClientManagerBase.Models.Clients;
using ClientManagerBase.Repository;
using ClientManagerBase.Services;
using Moq;
using NUnit.Framework;

namespace ClientManagerBaseTests
{
    /// <summary>
    /// Tests for <see cref="TagService"/>.
    /// </summary>
    [TestFixture]
    public class TagServiceTests
    {
        /// <summary>
        /// Test for GetTagsFromString method with valid string in argument.
        /// </summary>
        [Test]
        public void GetTagsFromValidStringTest()
        {
            // Setup
            var tags = new List<Tag>
                       {
                           new Tag("USA") { Id = 1 },
                           new Tag("Ukraine") { Id = 2 },
                           new Tag(".NET") { Id = 3 },
                           new Tag("New") { Id = 4 }
                       };

            var repMock = new Mock<IDataRepository>();
            repMock.Setup(m => m.Single(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Returns((Expression<Func<Tag, bool>> predicate) => tags.FirstOrDefault(predicate.Compile()));

            var tagService = new TagService(repMock.Object);

            var tagsString = "Old client, USA, , Old client, Active 777";
            
            // Execute
            var letters = tagService.GetTagsFromString(tagsString);

            // Verify
            Assert.AreEqual(3, letters.Count());
            Assert.True(letters.FirstOrDefault(t => t.Value == "Old client") != null);
            Assert.True(letters.FirstOrDefault(t => t.Value == "USA") != null);
            Assert.True(letters.FirstOrDefault(t => t.Value == "Active 777") != null);
            Assert.True(letters.FirstOrDefault(t => t.Value == "Old client").Id == 0);
            Assert.True(letters.FirstOrDefault(t => t.Value == "USA").Id == 1);
            Assert.True(letters.FirstOrDefault(t => t.Value == "Active 777").Id == 0);
        }

        /// <summary>
        /// Test for GetTagsFromString method with <c>null</c> and empty string in argument.
        /// </summary>
        [Test]
        public void GetTagsFromNullStringTest()
        {
            // Setup
            var repMock = new Mock<IDataRepository>();
            var tagService = new TagService(repMock.Object);
            
            // Execute
            var fromNull = tagService.GetTagsFromString(null);
            var fromEmpty = tagService.GetTagsFromString(string.Empty);

            // Verify
            Assert.IsNull(fromNull);
            Assert.IsNull(fromEmpty);
        }

        /// <summary>
        /// Test for GetTagsFromString method with invalid string in argument.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTagsFromInvalidStringTest()
        {
            // Setup
            var repMock = new Mock<IDataRepository>();
            var tagService = new TagService(repMock.Object);
            var invalidNames = @"New, !Stup1d@, &Act|ve, L%L";

            // Execute
            tagService.GetTagsFromString(invalidNames);
        }
    }
}
