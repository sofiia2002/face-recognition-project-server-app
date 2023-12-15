namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IRecognizedFacesRepository
    {
        Task<List<RecognizedFace>> GetAllAsync();
        Task<List<RecognizedFace>> GetRecFacesByPersonIdAsync(int personId);
        Task<List<RecognizedFace>> GetRecFacesByTypeAsync(int type);
        Task<List<RecognizedFace>> GetRecFacesByTimeConstraint(long startTime, long endTime);
        Task<List<RecognizedFace>> GetRecFacesByTimeConstraintAndPersonId(int personId, long startTime, long endTime);

    }
}
