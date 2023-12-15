namespace FaceRecognitionServer.Web.Application.Commands
{
    using Microsoft.VisualBasic;

    public class AddExaminationRoomCommand : ICommand
    {
        public int PersonId { get; private set; }
        public string Emotion { get; private set; }
        public DateAndTime TimeOnFrame { get; private set; }
        public DateAndTime TimeOffFrame { get; private set; }
    }
}
