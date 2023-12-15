namespace FaceRecognitionServer.Domain.FaceRecognitionServerAggregate
{
    using FaceRecognitionServer.Domain.SeedWork;
    using System;

    public class Person : Entity
    {
        public string Name { get; private set; }
        public int Identificator { get; private set; }
        public int Type {get; private set;} // 0 - unknown, 1 - identifiable user (e.g. customer), ...

        public Person(int id, string name, int identificator, int type) : base(id)
        {
            Identificator = identificator;
            Name = name;
            Type = type;
        }

        public Person(int id) : base(id)
        {
            Type = 0;
        }
    }
}
