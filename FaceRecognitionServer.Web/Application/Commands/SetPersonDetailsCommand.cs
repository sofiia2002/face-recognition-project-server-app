namespace FaceRecognitionServer.Web.Application.Commands
{
    public class SetPersonDetailsCommand : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identificator { get; set; }
        public int Type { get; set; } // 0 - unknown, 1 - identifiable user (e.g. customer), ...
        // public Stream streamImg { get; private set; } // image in form of stream of bytes
    }
}
