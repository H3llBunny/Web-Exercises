namespace MoiteRecepti.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MoiteRecepti.Data.Common.Repositories;
    using MoiteRecepti.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepostiroty;

        public VotesService(IRepository<Vote> votesRepostiroty)
        {
            this.votesRepostiroty = votesRepostiroty;
        }

        public double GetAverageVotes(int recipeId)
        {
            return this.votesRepostiroty.All().Where(x => x.RecipeId == recipeId).Average(x => x.Value);
        }

        public async Task SetVoteAsync(int recipeId, string userId, byte value)
        {
            var vote = this.votesRepostiroty.All()
                .FirstOrDefault(x => x.RecipeId == recipeId && x.UserId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                };

                await this.votesRepostiroty.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepostiroty.SaveChangesAsync();
        }
    }
}
