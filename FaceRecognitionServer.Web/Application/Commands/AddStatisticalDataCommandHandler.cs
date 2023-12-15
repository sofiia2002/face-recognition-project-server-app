namespace FaceRecognitionServer.Web.Application.Commands
{
    using FaceRecognitionServer.Infrastructure.Repositories;
    public class AddStatisticalDataCommandHandler : ICommandHandler<AddStatisticalDataCommand>
    {
        private readonly IStatisticalDataRepository statisticalDataRepository;

        public AddStatisticalDataCommandHandler(IStatisticalDataRepository statisticalDataRepository)
        {
            this.statisticalDataRepository = statisticalDataRepository;
        }

        void ICommandHandler<AddStatisticalDataCommand>.Handle(AddStatisticalDataCommand command)
        {
            statisticalDataRepository.AddStatisticAsync(command.PersonId, command.Emotion, command.TimeOnFrame, command.TimeOffFrame);
        }
    }

}
