namespace FaceRecognitionServer.Domain.FaceRecognitionServerAggregate
{
    using FaceRecognitionServer.Domain.SeedWork;
    using Microsoft.VisualBasic;
    using System;

    public class StatisticalDataEntity : Entity
    {
        public int PersonId { get; private set; }
        public DateTime TimeOnFrame { get; private set; }
        public DateTime TimeOffFrame { get; private set; }

        public StatisticalDataEntity(int id, int personId, DateTime timeOnFrame, DateTime timeOffFrame) : base(id)
        {
            PersonId = personId;
            TimeOnFrame = timeOnFrame;
            TimeOffFrame = timeOffFrame;
        }
    }
}
