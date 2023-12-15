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
    public class FaceRecognitionServerController : Controller
    {
        private readonly ILogger<FaceRecognitionServerController> logger;
        private readonly IPersonQueriesHandler personQueriesHandler;
        private readonly IStatisticalDataQueriesHandler statisticalDataQueriesHandler;
        private readonly ICommandHandler<AddStatisticalDataCommand> addStatisticalDataCommandHandler;
        private readonly ICommandHandler<AddPersonCommand> addPersonCommandHandler;
        private readonly ICommandHandler<SetPersonNameCommand> setPersonNameCommandHandler;
        private readonly ICommandHandler<SetPersonDetailsCommand> setPersonDetailsCommandHandler;
        private readonly ICommandHandler<SetPersonTypeCommand> setPersonTypeCommandHandler;
        private readonly ICommandHandler<SetPersonIdentificatorCommand> setPersonIdentificatorCommandHandler;

        public FaceRecognitionServerController(
            ILogger<FaceRecognitionServerController> logger, 
            IPersonQueriesHandler personQueriesHandler, 
            ICommandHandler<AddPersonCommand> addPersonCommandHandler,
            ICommandHandler<SetPersonNameCommand> setPersonNameCommandHandler,
            ICommandHandler<SetPersonDetailsCommand> setPersonDetailsCommandHandler,
            ICommandHandler<SetPersonTypeCommand> setPersonTypeCommandHandler,
            ICommandHandler<SetPersonIdentificatorCommand> setPersonIdentificatorCommandHandler,
            IStatisticalDataQueriesHandler statisticalDataQueriesHandler,
            ICommandHandler<AddStatisticalDataCommand> addStatisticalDataCommandHandler
            )
        {
            this.logger = logger;
            this.personQueriesHandler = personQueriesHandler;
            this.statisticalDataQueriesHandler = statisticalDataQueriesHandler;
            this.addPersonCommandHandler = addPersonCommandHandler;
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

        [HttpPost("add-person")]
        public void AddNewPerson([FromBody] AddPersonCommand personCommand)
        {
            addPersonCommandHandler.Handle(personCommand);
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
    }
}