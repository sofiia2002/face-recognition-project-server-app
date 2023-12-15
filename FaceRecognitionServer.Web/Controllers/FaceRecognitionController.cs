namespace FaceRecognitionServer.Web.Controllers
{
    using FaceRecognitionServer.Web.Application.Commands;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using FaceRecognitionServer.Web.Application.Dtos;
    using FaceRecognitionServer.Web.Application.Queries;

    [ApiController]
    public class FaceRecognitionController : Controller
    {
        private readonly ILogger<FaceRecognitionController> logger;
        private readonly IPersonQueriesHandler personQueriesHandler;
        private readonly IStatisticalDataQueriesHandler statisticalDataQueriesHandler;
        private readonly IRecognizedFacesQueriesHandler recognizedFacesQueriesHandler;
        private readonly ICommandHandler<AddStatisticalDataCommand> addStatisticalDataCommandHandler;
        private readonly ICommandHandler<SetPersonNameCommand> setPersonNameCommandHandler;
        private readonly ICommandHandler<SetPersonDetailsCommand> setPersonDetailsCommandHandler;
        private readonly ICommandHandler<SetPersonTypeCommand> setPersonTypeCommandHandler;
        private readonly ICommandHandler<SetPersonIdentificatorCommand> setPersonIdentificatorCommandHandler;

        public FaceRecognitionController(
            ILogger<FaceRecognitionController> logger, 
            IPersonQueriesHandler personQueriesHandler, 
            ICommandHandler<SetPersonNameCommand> setPersonNameCommandHandler,
            ICommandHandler<SetPersonDetailsCommand> setPersonDetailsCommandHandler,
            ICommandHandler<SetPersonTypeCommand> setPersonTypeCommandHandler,
            ICommandHandler<SetPersonIdentificatorCommand> setPersonIdentificatorCommandHandler,
            IStatisticalDataQueriesHandler statisticalDataQueriesHandler,
            ICommandHandler<AddStatisticalDataCommand> addStatisticalDataCommandHandler,
            IRecognizedFacesQueriesHandler recognizedFacesQueriesHandler
            )
        {
            this.logger = logger;
            this.personQueriesHandler = personQueriesHandler;
            this.statisticalDataQueriesHandler = statisticalDataQueriesHandler;
            this.recognizedFacesQueriesHandler = recognizedFacesQueriesHandler;
            this.setPersonNameCommandHandler = setPersonNameCommandHandler;
            this.setPersonDetailsCommandHandler = setPersonDetailsCommandHandler;
            this.setPersonTypeCommandHandler = setPersonTypeCommandHandler;
            this.setPersonIdentificatorCommandHandler = setPersonIdentificatorCommandHandler;
            this.addStatisticalDataCommandHandler = addStatisticalDataCommandHandler;
        }


        [HttpGet("people-data")]
        public async Task<IEnumerable<PersonDto>> GetAllPeopleAsync()
        {
            return await personQueriesHandler.GetAllAsync();
        }

        [HttpGet("person-data-by-id")]
        public async Task<PersonDto> GetPersonById([FromQuery] int personId)
        {
            // Add 404 Exception if no person was found, right now it is 204
            return await personQueriesHandler.GetPersonByIdAsync(personId);
        }

        [HttpPut("set-person-details/{id}")]
        public void SetPersonDetails(int id, [FromBody] SetPersonDetailsCommand personCommand)
        {
            personCommand.Id = id;
            setPersonDetailsCommandHandler.Handle(personCommand);
        }

        [HttpPut("set-person-name/{id}")]
        public void SetPersonName(int id, [FromBody] SetPersonNameCommand personCommand)
        {
            personCommand.Id = id;
            setPersonNameCommandHandler.Handle(personCommand);
        }

        [HttpPut("set-person-type/{id}")]
        public void SetPersonType(int id, [FromBody] SetPersonTypeCommand personCommand)
        {
            personCommand.Id = id;
            setPersonTypeCommandHandler.Handle(personCommand);
        }

        [HttpPut("set-person-identificator/{id}")]
        public void SetPersonIdentificator(int id, [FromBody] SetPersonIdentificatorCommand personCommand)
        {
            personCommand.Id = id;
            setPersonIdentificatorCommandHandler.Handle(personCommand);
        }

        // ----------------------------- Statistical Data -----------------------------

        [HttpGet("statistical-data")]
        public Task<IEnumerable<StatisticalDataDto>> GetAllStatisticalDataAsync()
        {
            return statisticalDataQueriesHandler.GetAllAsync();
        }

        [HttpPost("add-statistical-data")]
        public void AddStatisticalData([FromBody] AddStatisticalDataCommand statisticCommand)
        {
            addStatisticalDataCommandHandler.Handle(statisticCommand);
        }

        [HttpGet("statistical-data-by-person-id")]
        public Task<IEnumerable<StatisticalDataDto>> GetAllStatisticalDataByPeronIdAsync([FromQuery] int personId)
        {
            return statisticalDataQueriesHandler.GetStatisticsByPersonId(personId);
        }

        [HttpGet("statistical-data-by-time-constraints")]
        public Task<IEnumerable<StatisticalDataDto>> GetAllStatisticalDataByTimeConstraintsAsync([FromQuery] long timeStart, [FromQuery] long timeEnd)
        {
            return statisticalDataQueriesHandler.GetStatisticsByTimeConstraint(timeStart, timeEnd);
        }


        [HttpGet("statistical-data-by-time-constraints-and-person-id")]
        public Task<IEnumerable<StatisticalDataDto>> GetAllStatisticalDataByTimeConstraintAndPersonIAsync([FromQuery] int personId, [FromQuery] long timeStart, [FromQuery] long timeEnd)
        {
            return statisticalDataQueriesHandler.GetStatisticsByTimeConstraintAndPersonId(personId, timeStart, timeEnd);
        }

        // ----------------------------- Recognized Faces -----------------------------


        [HttpGet("recognized-faces")]
        public async Task<IEnumerable<RecognizedFaceDto>> GetAllRecognizedFacesAsync()
        {
            return await recognizedFacesQueriesHandler.GetAllAsync();
        }

        [HttpGet("recognized-faces-by-person-id")]
        public async Task<IEnumerable<RecognizedFaceDto>> GetAllRecognizedFacesByPeronIdAsync([FromQuery] int personId)
        {
            return await recognizedFacesQueriesHandler.GetRecognizedFacesByIdAsync(personId);
        }

        [HttpGet("recognized-faces-by-type")]
        public async Task<IEnumerable<RecognizedFaceDto>> GetAllRecognizedFacesByTypeAsync([FromQuery] int type)
        {
            return await recognizedFacesQueriesHandler.GetRecognizedFacesByTypeAsync(type);
        }

        [HttpGet("recognized-faces-by-time-constraints")]
        public async Task<IEnumerable<RecognizedFaceDto>> GetAllRecognizedFacesByTimeConstraintsAsync([FromQuery] long timeStart, [FromQuery] long timeEnd)
        {
            return await recognizedFacesQueriesHandler.GetRecognizedFacesByTimeConstraint(timeStart, timeEnd);
        }


        [HttpGet("recognized-faces-by-time-constraints-and-person-id")]
        public async Task<IEnumerable<RecognizedFaceDto>> GetAllRecognizedFacesByTimeConstraintAndPersonIAsync([FromQuery] int personId, [FromQuery] long timeStart, [FromQuery] long timeEnd)
        {
            return await recognizedFacesQueriesHandler.GetRecognizedFacesByTimeConstraintAndPersonId(personId, timeStart, timeEnd);
        }
    }
}