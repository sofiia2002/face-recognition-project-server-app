﻿namespace FaceRecognitionServer.Web.Application.Mapper
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
                ((DateTimeOffset)statisticalData.TimeOffFrame).ToUnixTimeSeconds()
            );
        }

        public static RecognizedFaceDto Map(this RecognizedFace recFace)
        {
            if (recFace == null)
                return null;

            return new RecognizedFaceDto
            (
                recFace.Id,
                recFace.PersonId,
                recFace.Confidence,
                recFace.Timestamp,
                recFace.EyesOpened,
                recFace.ImageUrl
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
                person.Type,
                person.ImgUrl
            );
        }
    }
}
