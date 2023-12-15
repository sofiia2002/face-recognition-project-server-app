namespace FaceRecognitionServer.Web.Application.Commands
{
    public class SetPersonTypeCommand : ICommand
    {
        public int Id { get; set; }
        public int Type { get; set; }
    }
}
