namespace FaceRecognitionServer.Web.Application.Commands
{
    public class SetPersonNameCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
