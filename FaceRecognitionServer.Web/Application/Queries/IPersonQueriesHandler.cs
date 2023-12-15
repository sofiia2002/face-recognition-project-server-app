namespace FaceRecognitionServer.Web.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FaceRecognitionServer.Web.Application.Dtos;

    public interface IPersonQueriesHandler
    {
        Task<IEnumerable<PersonDto>> GetAllAsync();
        Task<PersonDto> GetPersonByIdAsync(int personId);
    }
}
