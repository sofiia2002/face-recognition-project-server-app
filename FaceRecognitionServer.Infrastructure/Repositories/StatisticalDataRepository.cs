namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using Dapper;
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using FaceRecognitionServer.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class StatisticalDataRepository : IStatisticalDataRepository
    {

        public StatisticalDataRepository()
        { 
        }

        async Task IStatisticalDataRepository.AddStatisticAsync(int person_id, long time_on_frame, long time_off_frame)
        {
            // Check if the person_id exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE person_id = @PersonId;";

            // Your insert query remains the same
            const string createPersonQuery = @"
                INSERT INTO StatisticalData (person_id, time_on_frame, time_off_frame)
                VALUES (@PersonId, @TimeOnFrame, @TimeOffFrame);";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = person_id });

                // If the person doesn't exist, you may handle it accordingly (e.g., throw an exception, log, etc.)
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    Console.WriteLine("Error 400: Person with such person_id does not exist.");
                    throw new InvalidOperationException("Person with such person_id does not exist.");
                }

                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Insert data into the StatisticalData table
                        await dbConnection.ExecuteAsync(createPersonQuery, new { PersonId = person_id, TimeOnFrame = time_on_frame, TimeOffFrame = time_off_frame }, transaction);

                        // Commit the transaction
                        transaction.Commit();
                        Console.WriteLine($"Success 204: Statisctic for person with id {person_id} Added Successfully");
                    }
                    catch (Exception e)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        Console.WriteLine("Error 500: Error while connecting to the database");
                        throw new CustomException("Internal Server Error", 500);
                    }
                }
            }
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetAllAsync()
        {
            const string getAllPeopleQuery = @"SELECT * FROM StatisticalData";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            try
            {
                using (var dbConnection = new SqlConnection(Constants.connectionString))
                {
                    await dbConnection.OpenAsync();

                    using (var command = new SqlCommand(getAllPeopleQuery, dbConnection))
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var statDataEntity = new StatisticalDataEntity(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_on_frame"])).ToLocalTime(),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_off_frame"])).ToLocalTime()
                            );

                            statData.Add(statDataEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error 500: Something went wrong - {ex.Message}");

                // Throw a custom exception with code 500 and message Internal Server Error
                throw new CustomException("Internal Server Error: Something went wrong", 500);
            }

            return statData;
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByPersonId(int personId)
        {
            const string getStatisticalDataByPersonIdQuery = @"SELECT * FROM StatisticalData WHERE person_id = @PersonId";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE person_id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, you may handle it accordingly (e.g., throw an exception, log, etc.)
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    throw new InvalidOperationException("Perso with such person_id does not exist.");
                }

                using (var command = new SqlCommand(getStatisticalDataByPersonIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var statDataEntity = new StatisticalDataEntity(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_on_frame"])).ToLocalTime(),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_off_frame"])).ToLocalTime()
                            );

                            statData.Add(statDataEntity);
                        }
                    }
                }
            }

            return statData;
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByTimeConstraint(long startTime, long endTime)
        {
            const string getStatisticalDataByPersonIdQuery = @"SELECT * FROM StatisticalData WHERE time_on_frame > @StartTime AND time_off_frame < @EndTime";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getStatisticalDataByPersonIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var statDataEntity = new StatisticalDataEntity(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["person_id"]),
                            dateTime.AddSeconds(Convert.ToInt32(reader["time_on_frame"])).ToLocalTime(),
                            dateTime.AddSeconds(Convert.ToInt32(reader["time_off_frame"])).ToLocalTime()
                            );

                            statData.Add(statDataEntity);
                        }
                    }
                }
            }

            return statData;
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByTimeConstraintAndPersonId(int personId, long startTime, long endTime)
        {
            const string getStatisticalDataByPersonIdQuery = @"
                SELECT * 
                FROM StatisticalData 
                WHERE time_on_frame > @StartTime AND time_off_frame < @EndTime AND person_id = @PersonId";

            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE person_id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, you may handle it accordingly (e.g., throw an exception, log, etc.)
                if (personCount == 0)
                {
                    Console.WriteLine("Error 400: Person with such person id does not exist.");
                    // Handle the case where the person_id doesn't exist
                    throw new InvalidOperationException("Person with such person id does not exist.");
                }

                using (var command = new SqlCommand(getStatisticalDataByPersonIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@StartTime", startTime);
                    command.Parameters.AddWithValue("@EndTime", endTime);
                    command.Parameters.AddWithValue("@PersonId", personId);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var statDataEntity = new StatisticalDataEntity(
                                Convert.ToInt32(reader["id"]),
                                Convert.ToInt32(reader["person_id"]),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_on_frame"])).ToLocalTime(),
                                dateTime.AddSeconds(Convert.ToInt32(reader["time_off_frame"])).ToLocalTime()
                            );

                            statData.Add(statDataEntity);
                        }
                    }
                }
            }

            return statData;

        }
    }
}
