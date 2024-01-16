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
            Console.WriteLine($"Error 404: Person with person_id {personId} does not exist");
            throw new CustomException($"Not Found Exception: Person with person_id {personId} does not exist", 404);
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

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, throw an exception
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    throw new InvalidOperationException($"Person with id {personId} does not exist.");
                }

                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Update the person information
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Name = name, Identificator = identificator, Type = type, PersonId = personId }, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        // Log success information
                        Console.WriteLine($"Person with id {personId} updated successfully.");
                    }
                    catch (Exception e)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        throw new CustomException("Internal Server Error Something went wrong", 500);
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

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, throw a custom exception
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    throw new CustomException("Person does not exist.", 500);
                }

                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Update the person's identificator
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Identificator = identificator, PersonId = personId }, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        // Log success information
                        Console.WriteLine($"Person with id {personId} identificator updated successfully.");
                    }
                    catch (Exception e)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        throw new CustomException("Internal Server Error: Something went wrong", 500);
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

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, throw a custom exception
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    throw new CustomException("Person does not exist.", 500);
                }

                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Update the person's name
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Name = name, PersonId = personId }, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        // Log success information
                        Console.WriteLine($"Person with id {personId} name updated successfully.");
                    }
                    catch (Exception e)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        throw new CustomException("Internal Server Error: Something went wrong", 500);
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

            // Check if the person with the specified personId exists
            const string checkPersonQuery = "SELECT COUNT(*) FROM Person WHERE id = @PersonId;";

            using (var dbConnection = new SqlConnection(Constants.connectionString))
            {
                await dbConnection.OpenAsync();

                // Check if the person_id exists
                int personCount = await dbConnection.ExecuteScalarAsync<int>(checkPersonQuery, new { PersonId = personId });

                // If the person doesn't exist, throw a custom exception
                if (personCount == 0)
                {
                    // Handle the case where the person_id doesn't exist
                    throw new CustomException("Person does not exist.", 500);
                }

                using (DbTransaction transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Update the person's type
                        await dbConnection.ExecuteAsync(createPersonQuery, new { Type = type, PersonId = personId }, transaction);

                        // Commit the transaction
                        transaction.Commit();

                        // Log success information
                        Console.WriteLine($"Person with id {personId} type updated successfully.");
                    }
                    catch (Exception e)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        throw new CustomException("Internal Server Error: Something went wrong", 500);
                    }
                }
            }
        }
    }
}
