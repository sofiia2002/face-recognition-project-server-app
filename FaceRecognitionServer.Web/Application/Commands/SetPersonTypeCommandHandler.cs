namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class SetPersonTypeCommandHandler : ICommandHandler<SetPersonTypeCommand>
    {
        private readonly IPersonRepository personRepository;

        public SetPersonTypeCommandHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        void ICommandHandler<SetPersonTypeCommand>.Handle(SetPersonTypeCommand command)
        {
            personRepository.SetPersonTypeAsync(command.Type, command.Id);
        }
    }

}
