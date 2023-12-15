namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class SetPersonDetailsCommandHandler : ICommandHandler<SetPersonDetailsCommand>
    {
        private readonly IPersonRepository personRepository;

        public SetPersonDetailsCommandHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        void ICommandHandler<SetPersonDetailsCommand>.Handle(SetPersonDetailsCommand command)
        {
            personRepository.SetPersonDetailsAsync(command.Id, command.Name, command.Identificator, command.Type);
        }
    }

}
