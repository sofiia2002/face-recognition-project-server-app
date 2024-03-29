﻿using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FaceRecognitionServer.Infrastructure.Repositories
{
    public class RecognizedFacesRepository : IRecognizedFacesRepository
    {
        public async Task<List<RecognizedFace>> GetAllAsync()
        {
            const string getAllPeopleQuery = @"SELECT * FROM RecognizedFaces";
            var people = new List<RecognizedFace>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getAllPeopleQuery, dbConnection))
                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var person = new RecognizedFace(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["person_id"]), float.Parse(reader["confidence"].ToString()), long.Parse(reader["timestamp"].ToString()), Convert.ToInt32(reader["eyes_opened"]), Convert.ToString(reader["image_url"]));
                        people.Add(person);
                    }
                }
            }

            return people;
        }

        public async Task<List<RecognizedFace>> GetRecFacesByPersonIdAsync(int personId)
        {
            const string getPersonByIdQuery = @"SELECT * FROM RecognizedFaces WHERE person_id = @PersonId";
            var people = new List<RecognizedFace>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getPersonByIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if there are no records
                        if (!reader.HasRows)
                        {
                            // Handle the case where there are no records (throw a custom exception with 404)
                            throw new CustomException("Not Found Error: No records found for the specified personId.", 404);
                        }

                        while (await reader.ReadAsync())
                        {
                            var person = new RecognizedFace(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                float.Parse(reader["confidence"].ToString()),
                                long.Parse(reader["timestamp"].ToString()),
                                Convert.ToInt32(reader["eyes_opened"]),
                                Convert.ToString(reader["image_url"])
                            );

                            people.Add(person);
                        }
                    }
                }
            }

            return people;

        }

        public async Task<List<RecognizedFace>> GetRecFacesByTimeConstraint(long startTime, long endTime)
        {
            const string getPersonByIdQuery = @"SELECT * FROM RecognizedFaces WHERE timestamp > @StartTime AND timestamp < @EndTime";
            var people = new List<RecognizedFace>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getPersonByIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var person = new RecognizedFace(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["person_id"]), float.Parse(reader["confidence"].ToString()), long.Parse(reader["timestamp"].ToString()), Convert.ToInt32(reader["eyes_opened"]), Convert.ToString(reader["image_url"]));
                            people.Add(person);
                        }
                    }
                }
            }
            return people;
        }

        public async Task<List<RecognizedFace>> GetRecFacesByTimeConstraintAndPersonId(int personId, long startTime, long endTime)
        {
            const string getPersonByIdQuery = @"
                SELECT * 
                FROM RecognizedFaces 
                WHERE timestamp > @StartTime AND timestamp < @EndTime AND person_id = @PersonId";

            var people = new List<RecognizedFace>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getPersonByIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if there are no records
                        if (!reader.HasRows)
                        {
                            // Handle the case where there are no records (throw a custom exception with 404)
                            throw new CustomException("No records found for the specified criteria.", 404);
                        }

                        while (await reader.ReadAsync())
                        {
                            var person = new RecognizedFace(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                float.Parse(reader["confidence"].ToString()),
                                long.Parse(reader["timestamp"].ToString()),
                                Convert.ToInt32(reader["eyes_opened"]),
                                Convert.ToString(reader["image_url"])
                            );

                            people.Add(person);
                        }
                    }
                }
            }

            return people;

        }

        public async Task<List<RecognizedFace>> GetRecFacesByTypeAsync(int type)
        {
            const string getPersonByIdQuery = @"
                SELECT RecognizedFaces.*, Person.type 
                FROM RecognizedFaces 
                JOIN Person ON RecognizedFaces.person_id = Person.id 
                WHERE Person.type = @Type";

            var people = new List<RecognizedFace>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getPersonByIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@Type", type);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        // Check if there are no records
                        if (!reader.HasRows)
                        {
                            // Handle the case where there are no records (throw a custom exception with 404)
                            throw new CustomException("No records found for the specified type.", 404);
                        }

                        while (await reader.ReadAsync())
                        {
                            var person = new RecognizedFace(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                float.Parse(reader["confidence"].ToString()),
                                long.Parse(reader["timestamp"].ToString()),
                                Convert.ToInt32(reader["eyes_opened"]),
                                Convert.ToString(reader["image_url"])
                            );

                            people.Add(person);
                        }
                    }
                }
            }

            return people;
        }
    }
}
