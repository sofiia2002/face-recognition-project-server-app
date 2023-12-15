namespace FaceRecognitionServer.Domain.SeedWork
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public Entity(int id) 
        {
            Id = id;
        }
    }
}
