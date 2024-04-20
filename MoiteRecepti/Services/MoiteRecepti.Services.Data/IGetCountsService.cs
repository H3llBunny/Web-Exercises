namespace MoiteRecepti.Services.Data
{
    using MoiteRecepti.Services.Data.Models;

    public interface IGetCountsService
    {
        // 1. You can use the view model <- lazy but convenient if you are short on time.
        // 2. You can create a Dto and create the view model in the controller by mapping the Dto to it <- best practice
        // 3. You can use Tuples <- not preffered.
        CountsDto GetCounts();
    }
}
