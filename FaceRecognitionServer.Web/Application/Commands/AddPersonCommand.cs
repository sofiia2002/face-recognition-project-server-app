namespace FaceRecognitionServer.Web.Application.Commands
{
    using System.Collections.Generic;
    using System.IO;

    public class AddPersonCommand : ICommand
    {
        public string Name { get; private set; }
        public int Identificator { get; private set; }
        public int Type { get; private set; } // 0 - unknown, 1 - identifiable user (e.g. customer), ...
        public Stream streamImg { get; private set; } // image in form of stream of bytes
    }
}
