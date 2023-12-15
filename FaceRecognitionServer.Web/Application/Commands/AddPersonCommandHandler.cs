namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class AddPersonCommandHandler : ICommandHandler<AddPersonCommand>
    {
        private readonly IPersonRepository personRepository;

        public AddPersonCommandHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        void ICommandHandler<AddPersonCommand>.Handle(AddPersonCommand command)
        {
            personRepository.AddPersonAsync(command.Name, command.Identificator, command.Type);
        }
    }

}
