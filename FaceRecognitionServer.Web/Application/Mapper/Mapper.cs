namespace FaceRecognitionServer.Web.Application.Mapper
{
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using FaceRecognitionServer.Web.Application.Dtos;
    using System;

    public static class Mapper
    {
        public static StatisticalDataDto Map(this StatisticalDataEntity statisticalData)
        {
            if (statisticalData == null)
                return null;

            return new StatisticalDataDto
            (
                statisticalData.Id,
                statisticalData.PersonId,
                ((DateTimeOffset)statisticalData.TimeOnFrame).ToUnixTimeSeconds(),
                ((DateTimeOffset)statisticalData.TimeOffFrame).ToUnixTimeSeconds(),
                statisticalData.Emotion
            );
        }

        public static PersonDto Map(this Person person)
        {
            if (person == null)
                return null;

            return new PersonDto
            (
                person.Id,
                person.Name,
                person.Identificator,
                person.Type
            );
        }
    }
}
