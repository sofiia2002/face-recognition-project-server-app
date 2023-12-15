namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using Microsoft.VisualBasic;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStatisticalDataRepository
    {
        Task AddStatisticAsync(StatisticalDataEntity statisticalDataEntity);
        Task<List<StatisticalDataEntity>> GetAllAsync();
        Task<List<StatisticalDataEntity>> GetStatisticsByPersonId(int personId);
        Task<List<StatisticalDataEntity>> GetStatisticsByTimeConstraint(DateAndTime startTime, DateAndTime endTime);
        Task<List<StatisticalDataEntity>> GetStatisticsByTimeConstraintAndPersonId(int personId, DateAndTime startTime, DateAndTime endTime);

    }
}
