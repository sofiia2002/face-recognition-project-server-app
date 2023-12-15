namespace FaceRecognitionServer.Domain.FaceRecognitionServerAggregate
{
    using FaceRecognitionServer.Domain.SeedWork;
    public class RecognizedFace : Entity
    {
        public int PersonId { get; private set; }
        public float Confidence { get; private set; }
        public long Timestamp { get; private set; }
        public int EyesOpened { get; private set; }
        public string ImageUrl { get; private set; }

        public RecognizedFace(int id, int personId, float confidence, long timestamp, int eyesOpened, string imageUrl) : base(id)
        {
            PersonId = personId;
            Confidence = confidence;
            Timestamp = timestamp;
            EyesOpened = eyesOpened;
            ImageUrl = imageUrl;
        }
    }
}
