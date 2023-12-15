namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using Dapper;
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticalDataRepository : IStatisticalDataRepository
    {

        public StatisticalDataRepository()
        { 
        }

        Task IStatisticalDataRepository.AddStatisticAsync(StatisticalDataEntity statisticalDataEntity)
        {
            throw new NotImplementedException();
        }

        Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByPersonId(int personId)
        {
            throw new NotImplementedException();
        }

        Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByTimeConstraint(DateAndTime startTime, DateAndTime endTime)
        {
            throw new NotImplementedException();
        }

        Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByTimeConstraintAndPersonId(int personId, DateAndTime startTime, DateAndTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}
