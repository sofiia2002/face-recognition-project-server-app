namespace FaceRecognitionServer.Web.Controllers
{
    using FaceRecognitionServer.Web.Application.Commands;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    public class FaceRecognitionServerController : ControllerBase
    {
        private readonly ILogger<FaceRecognitionServerController> logger;
        private readonly ICommandHandler<AddPersonCommand> addExaminationRoomCommandHandler;

        
    }
}