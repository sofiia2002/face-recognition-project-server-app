namespace FaceRecognitionServer.Web.Application.Dtos
{
    using System.Collections.Generic;

    public class StatisticalDataDto
    {
        public int Id { get; private set; }
        public int PersonId { get; private set; }
        public long TimeOnFrame { get; private set; }
        public long TimeOffFrame { get; private set; }

        public StatisticalDataDto(int id, int personId, long timeOnFrame, long timeOffFrame)
        {
            Id = id;
            PersonId = personId;
            TimeOnFrame = timeOnFrame;
            TimeOffFrame = timeOffFrame;
        }
    }
   
}
