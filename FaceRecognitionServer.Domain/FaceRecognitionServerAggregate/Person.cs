namespace FaceRecognitionServer.Domain.FaceRecognitionServerAggregate
{
    using FaceRecognitionServer.Domain.SeedWork;
    using System;

    public class Person : Entity
    {
        public string Name { get; private set; }
        public string Identificator { get; private set; }
        public int Type {get; private set; } // 0 - unknown, 1 - identifiable user (e.g. customer), ...
        public string ImgUrl { get; private set; }

        public Person(int id, string name, string identificator, int type, string imgUrl) : base(id)
        {
            Identificator = identificator;
            Name = name;
            Type = type;
            ImgUrl = imgUrl;
        }

        public Person(int id) : base(id)
        {
            Type = 0;
        }
    }
}
