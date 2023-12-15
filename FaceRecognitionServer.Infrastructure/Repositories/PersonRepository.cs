namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using Dapper;
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonRepository : IPersonRepository
    {

        public PersonRepository()
        {
        }

        Task IPersonRepository.AddPersonAsync(Person person, Stream imgStream)
        {
            throw new NotImplementedException();
        }

        Task<List<Person>> IPersonRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Person> IPersonRepository.GetPersonById(int personId)
        {
            throw new NotImplementedException();
        }
    }
}
