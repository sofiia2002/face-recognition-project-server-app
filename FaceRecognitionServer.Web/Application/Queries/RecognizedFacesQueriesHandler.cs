namespace FaceRecognitionServer.Web.Application.Queries
{
    using FaceRecognitionServer.Web.Application.Mapper;
    using FaceRecognitionServer.Web.Application.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FaceRecognitionServer.Infrastructure.Repositories;
    using System;

    public class RecognizedFacesQueriesHandler : IRecognizedFacesQueriesHandler
    {
        private readonly IRecognizedFacesRepository recognizedFacesRepository;

        public RecognizedFacesQueriesHandler(IRecognizedFacesRepository recognizedFacesRepository)
        {
            this.recognizedFacesRepository = recognizedFacesRepository;
        }

        async Task<IEnumerable<RecognizedFaceDto>> IRecognizedFacesQueriesHandler.GetAllAsync()
        {
            return (await recognizedFacesRepository.GetAllAsync()).Select(r => r.Map());
        }

        async Task<IEnumerable<RecognizedFaceDto>> IRecognizedFacesQueriesHandler.GetRecognizedFacesByIdAsync(int personId)
        {
            return (await recognizedFacesRepository.GetRecFacesByPersonIdAsync(personId)).Select(r => r.Map());
        }

        async Task<IEnumerable<RecognizedFaceDto>> IRecognizedFacesQueriesHandler.GetRecognizedFacesByTimeConstraint(long startTime, long endTime)
        {
            return (await recognizedFacesRepository.GetRecFacesByTimeConstraint(startTime, endTime)).Select(r => r.Map());
        }

        async Task<IEnumerable<RecognizedFaceDto>> IRecognizedFacesQueriesHandler.GetRecognizedFacesByTimeConstraintAndPersonId(int personId, long startTime, long endTime)
        {
            return (await recognizedFacesRepository.GetRecFacesByTimeConstraintAndPersonId(personId, startTime, endTime)).Select(r => r.Map());
        }

        async Task<IEnumerable<RecognizedFaceDto>> IRecognizedFacesQueriesHandler.GetRecognizedFacesByTypeAsync(int type)
        {
            return (await recognizedFacesRepository.GetRecFacesByTypeAsync(type)).Select(r => r.Map());
        }
    }
}
