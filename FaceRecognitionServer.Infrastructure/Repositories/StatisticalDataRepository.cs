namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using Dapper;
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using FaceRecognitionServer.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class StatisticalDataRepository : IStatisticalDataRepository
    {

        public StatisticalDataRepository()
        { 
        }

        async Task IStatisticalDataRepository.AddStatisticAsync(int person_id, string emotion, long time_on_frame, long time_off_frame)
        {
            const string createPersonQuery = @"
            INSERT INTO StatisticalData (person_id, emotion, time_on_frame, time_off_frame)
            VALUES (@PersonId, @Emotion, @TimeOnFrame, @TimeOffFrame);";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        await dbConnection.ExecuteAsync(createPersonQuery, new { PersonId = person_id, Emotion = emotion, TimeOnFrame = time_on_frame, TimeOffFrame = time_off_frame }, transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetAllAsync()
        {
            const string getAllPeopleQuery = @"SELECT * FROM StatisticalData";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

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

            return statData;
        }

        async Task<List<StatisticalDataEntity>> IStatisticalDataRepository.GetStatisticsByPersonId(int personId)
        {
            const string getStatisticalDataByPersonIdQuery = @"SELECT * FROM StatisticalData WHERE person_id = @PersonId";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

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
            const string getStatisticalDataByPersonIdQuery = @"SELECT * FROM StatisticalData WHERE time_on_frame > @StartTime AND time_off_frame < @EndTime AND person_id = @PersonId";
            var statData = new List<StatisticalDataEntity>();
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

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
