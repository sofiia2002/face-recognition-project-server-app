namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public interface IStatisticalDataRepository
    {
        Task AddStatisticAsync(int person_id, long time_on_frame, long time_off_frame);
        Task<List<StatisticalDataEntity>> GetAllAsync();
        Task<List<StatisticalDataEntity>> GetStatisticsByPersonId(int personId);
        Task<List<StatisticalDataEntity>> GetStatisticsByTimeConstraint(long startTime, long endTime);
        Task<List<StatisticalDataEntity>> GetStatisticsByTimeConstraintAndPersonId(int personId, long startTime, long endTime);

    }
}
