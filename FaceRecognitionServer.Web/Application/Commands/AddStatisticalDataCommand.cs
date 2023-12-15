namespace FaceRecognitionServer.Web.Application.Commands
{
    using Microsoft.VisualBasic;
    using System;

    public class AddStatisticalDataCommand : ICommand
    {
        public int PersonId { get; set; }
        public string Emotion { get; set; }
        public int TimeOnFrame { get; set; }
        public int TimeOffFrame { get; set; }
    }
}
