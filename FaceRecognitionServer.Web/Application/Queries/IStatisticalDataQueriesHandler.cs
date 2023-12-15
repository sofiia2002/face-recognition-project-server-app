namespace FaceRecognitionServer.Web.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FaceRecognitionServer.Web.Application.Dtos;

    public interface IStatisticalDataQueriesHandler
    {
        Task<IEnumerable<StatisticalDataDto>> GetAllAsync();
        Task<IEnumerable<StatisticalDataDto>> GetStatisticsByPersonId(int personId);
        Task<IEnumerable<StatisticalDataDto>> GetStatisticsByTimeConstraint(long startTime, long endTime);
        Task<IEnumerable<StatisticalDataDto>> GetStatisticsByTimeConstraintAndPersonId(int personId, long startTime, long endTime);
    }
}
