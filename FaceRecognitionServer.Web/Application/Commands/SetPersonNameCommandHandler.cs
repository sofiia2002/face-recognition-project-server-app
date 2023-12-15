namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class SetPersonNameCommandHandler : ICommandHandler<SetPersonNameCommand>
    {
        private readonly IPersonRepository personRepository;

        public SetPersonNameCommandHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        void ICommandHandler<SetPersonNameCommand>.Handle(SetPersonNameCommand command)
        {
            personRepository.SetPersonNameAsync(command.Name, command.Id);
        }
    }

}
