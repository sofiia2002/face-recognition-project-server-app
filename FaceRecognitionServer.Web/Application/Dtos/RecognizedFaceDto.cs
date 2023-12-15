namespace FaceRecognitionServer.Web.Application.Dtos
{
    public class RecognizedFaceDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public float Confidence { get; set; }
        public long Timestamp { get; set; }
        public int EyesOpened { get; set; }
        public string ImageUrl { get; set; }

        public RecognizedFaceDto(int id, int personId, float confidence, long timestamp, int eyesOpened, string imageUrl)
        {
            Id = id;
            PersonId = personId;
            Confidence = confidence;
            Timestamp = timestamp;
            EyesOpened = eyesOpened;
            ImageUrl = imageUrl;
        }
    }
}
