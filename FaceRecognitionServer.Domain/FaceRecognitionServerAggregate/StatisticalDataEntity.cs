namespace FaceRecognitionServer.Domain.FaceRecognitionServerAggregate
{
    using FaceRecognitionServer.Domain.SeedWork;
    using Microsoft.VisualBasic;

    public class StatisticalDataEntity : Entity
    {
        public int PersonId { get; private set; }
        public string Emotion { get; private set; }
        public DateAndTime TimeOnFrame { get; private set; }
        public DateAndTime TimeOffFrame { get; private set; }

        public StatisticalDataEntity(int id, int personId, DateAndTime timeOnFrame, DateAndTime timeOffFrame) : base(id)
        {
            PersonId = personId;
            TimeOnFrame = timeOnFrame;
            TimeOffFrame = timeOffFrame;
        }

        public StatisticalDataEntity(int id, int personId, DateAndTime timeOnFrame, DateAndTime timeOffFrame, string emotion) : base(id)
        {
            PersonId = personId;
            TimeOnFrame = timeOnFrame;
            TimeOffFrame = timeOffFrame;
            Emotion = emotion;
        }
    }
}
