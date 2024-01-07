namespace FaceRecognitionServer.Infrastructure.Repositories
{
    using Dapper;
    using FaceRecognitionServer.Domain.FaceRecognitionServerAggregate;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PersonRepository : IPersonRepository
    {

        public PersonRepository()
        {
        }

        async Task<List<Person>> IPersonRepository.GetAllAsync()
        {
            const string getAllPeopleQuery = @"SELECT * FROM Person";
            var people = new List<Person>();

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getAllPeopleQuery, dbConnection))
                using (DbDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var person = new Person(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"]), Convert.ToString(reader["identificator"]), Convert.ToInt32(reader["type"]), Convert.ToString(reader["Image"]));

                        people.Add(person);
                    }
                }
            }

            return people;
        }

        async Task<Person> IPersonRepository.GetPersonByIdAsync(int personId)
        {
            const string getPersonByIdQuery = @"SELECT * FROM Person WHERE id = @PersonId";
            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                using (var command = new SqlCommand(getPersonByIdQuery, dbConnection))
                {
                    command.Parameters.AddWithValue("@PersonId", personId);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Person(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"]), Convert.ToString(reader["identificator"]), Convert.ToInt32(reader["type"]), Convert.ToString(reader["Image"]));
                        }
                    }
                }
            }
            return null;
        }

        async Task IPersonRepository.SetPersonDetailsAsync(int personId, string name, string identificator, int type)
        {
            const string createPersonQuery = @"
            UPDATE Person
            SET
                name = @Name,
                identificator = @Identificator,
                type = @Type
            WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Name = name, Identificator = identificator, Type = type, PersonId = personId }, transaction);
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

        async Task IPersonRepository.SetPersonIdentificatorAsync(string identificator, int personId)
        {
            const string createPersonQuery = @"
            UPDATE Person
            SET identificator = @Identificator
            WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Identificator = identificator, PersonId = personId }, transaction);
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

        async Task IPersonRepository.SetPersonNameAsync(string name, int personId)
        {
            const string createPersonQuery = @"
            UPDATE Person
            SET name = @Name
            WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Name = name, PersonId = personId }, transaction);
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

        async Task IPersonRepository.SetPersonTypeAsync(int type, int personId)
        {
            const string createPersonQuery = @"
            UPDATE Person
            SET type = @Type
            WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();
                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Type = type, PersonId = personId }, transaction);
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
    }
}
