using FaceRecognitionServer.Web.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceRecognitionServer.Web.Application.Queries
{
    public interface IRecognizedFacesQueriesHandler
    {
        Task<IEnumerable<RecognizedFaceDto>> GetAllAsync();
        Task<IEnumerable<RecognizedFaceDto>> GetRecognizedFacesByIdAsync(int personId);
        Task<IEnumerable<RecognizedFaceDto>> GetRecognizedFacesByTypeAsync(int type);
        Task<IEnumerable<RecognizedFaceDto>> GetRecognizedFacesByTimeConstraint(long startTime, long endTime);
        Task<IEnumerable<RecognizedFaceDto>> GetRecognizedFacesByTimeConstraintAndPersonId(int personId, long startTime, long endTime);
    }
}
