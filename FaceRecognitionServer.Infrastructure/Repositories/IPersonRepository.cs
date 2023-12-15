namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person, Stream imgStream);
        Task<List<Person>> GetAllAsync();
        Task<Person> GetPersonById(int personId);

    }
}
