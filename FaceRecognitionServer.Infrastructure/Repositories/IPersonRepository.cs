namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public interface IPersonRepository
    {
        Task SetPersonNameAsync(string name, int personId); 
        Task SetPersonIdentificatorAsync( string identificator, int personId);
        Task SetPersonTypeAsync(int type, int personId);
        Task SetPersonDetailsAsync(int personId, string name, string identificator, int type);
        Task<List<Person>> GetAllAsync();
        Task<Person> GetPersonByIdAsync(int personId);

    }
}
