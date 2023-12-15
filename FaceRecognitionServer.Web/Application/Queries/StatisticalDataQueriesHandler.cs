namespace FaceRecognitionServer.Web.Application.Queries
{
    using FaceRecognitionServer.Web.Application.Mapper;
    using FaceRecognitionServer.Web.Application.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FaceRecognitionServer.Infrastructure.Repositories;
    using System;

    public class StatisticalDataQueriesHandler : IStatisticalDataQueriesHandler
    {
        private readonly IStatisticalDataRepository statisticalDataRepository;

        public StatisticalDataQueriesHandler(IStatisticalDataRepository statisticalDataRepository)
        {
            this.statisticalDataRepository = statisticalDataRepository;
        }

        public async Task<IEnumerable<StatisticalDataDto>> GetAllAsync()
        {
            return (await statisticalDataRepository.GetAllAsync()).Select(r => r.Map());
        }

        public async Task<IEnumerable<StatisticalDataDto>> GetStatisticsByPersonId(int personId)
        {
            return (await statisticalDataRepository.GetStatisticsByPersonId(personId)).Select(r => r.Map());
        }

        public async Task<IEnumerable<StatisticalDataDto>> GetStatisticsByTimeConstraint(long startTime, long endTime)
        {
            return (await statisticalDataRepository.GetStatisticsByTimeConstraint(startTime, endTime)).Select(r => r.Map());
        }

        public async Task<IEnumerable<StatisticalDataDto>> GetStatisticsByTimeConstraintAndPersonId(int personId, long startTime, long endTime)
        {
            return (await statisticalDataRepository.GetStatisticsByTimeConstraintAndPersonId(personId, startTime, endTime)).Select(r => r.Map());
        }
    }
}
