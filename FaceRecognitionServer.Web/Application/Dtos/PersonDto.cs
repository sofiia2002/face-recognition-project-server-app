namespace FaceRecognitionServer.Web.Application.Dtos
{
    using System.Collections.Generic;

    public class PersonDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Identificator { get; set; }
        public int Type { get; set; }

        public PersonDto(int id, string name, string identificator, int type)
        {
            id = id;
            Name = name;
            Identificator = identificator;
            Type = type;
        }
    }
   
}
