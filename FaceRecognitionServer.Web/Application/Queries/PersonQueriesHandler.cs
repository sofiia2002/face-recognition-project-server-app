namespace FaceRecognitionServer.Web.Application.Queries
{
    using FaceRecognitionServer.Web.Application.Mapper;
    using FaceRecognitionServer.Web.Application.Dtos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FaceRecognitionServer.Infrastructure.Repositories;
    using System;

    public class PersonQueriesHandler : IPersonQueriesHandler
    {
        private readonly IPersonRepository personRepository;

        public PersonQueriesHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonDto>> GetAllAsync()
        {
            return (await personRepository.GetAllAsync()).Select(r => r.Map());
        }

        async Task<PersonDto> IPersonQueriesHandler.GetPersonByIdAsync(int personId)
        {
            return (await personRepository.GetPersonByIdAsync(personId)).Map();
        }
    }
}
