namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class SetPersonIdentificatorCommandHandler : ICommandHandler<SetPersonIdentificatorCommand>
    {
        private readonly IPersonRepository personRepository;

        public SetPersonIdentificatorCommandHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        void ICommandHandler<SetPersonIdentificatorCommand>.Handle(SetPersonIdentificatorCommand command)
        {
            personRepository.SetPersonIdentificatorAsync(command.Identificator, command.Id);
        }
    }

}
