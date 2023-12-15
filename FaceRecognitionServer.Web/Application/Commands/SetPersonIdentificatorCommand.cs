namespace FaceRecognitionServer.Web.Application.Commands
{
    public class SetPersonIdentificatorCommand : ICommand
    {
        public int Id { get; set; }
        public string Identificator { get; set; }
    }
}
