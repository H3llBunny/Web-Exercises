namespace MoiteRecepti.Services.Data.Tests
{
    using MoiteRecepti.Data.Common.Repositories;
    using MoiteRecepti.Data.Models;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnlyOneVoteShouldBeCount()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback(
                (Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 1);
            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "1", 5);

            Assert.Equal(1, list.Count);
            Assert.Equal(5, list.First().Value);
        }

        [Fact]
        public async Task When2UsersVoteForTheSameRecipeTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback(
                (Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(2, "Pesho", 1);
            await service.SetVoteAsync(2, "Niki", 5);
            await service.SetVoteAsync(2, "Niki", 2);

            mockRepo.Verify(x => x.All(), Times.Exactly(3));
            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Exactly(2));

            Assert.Equal(2, list.Count);
            Assert.Equal(1.5, service.GetAverageVotes(2));
        }
    }
}
