using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognitionServer.Domain.Models;

namespace FaceRecognitionServer.Interfaces.Repositories
{
    public interface IFaceAPIClient
    {
        Task CreatePersonAsync(string name, string description, byte[] imgdata);
        Task AddFaceAsync(Guid personId, byte[] imgdata);
        Task<List<PersonViewModel>> RecognizeAsync(byte[] imgdata);
        Task<List<PersonViewModel>> GetAllAsync();
    }
}
