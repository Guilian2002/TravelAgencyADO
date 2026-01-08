using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyADO.Domain.Repositories;
using TravelAgencyADO.Domain.Entities;

namespace TravelAgencyADO.DAL.Repositories
{
    public class SqlDestinationRepo : IDestinationRepo
    {
        private readonly string _connectionString;

        public SqlDestinationRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection CreateConnection() => new SqlConnection(_connectionString);

        public ICollection<Destination> GetAll()
        {
            var list = new List<Destination>();

            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT * FROM [Destination];";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapDestination(reader));
            }

            return list;
        }

        public Destination? GetById(Guid destinationId)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT * FROM [Destination]
                WHERE Id = @destinationId;
            ";

            AddParameter(command, "@destinationId", destinationId);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;

            return MapDestination(reader);
        }

        public void Insert(Destination destination)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO [Destination] ([Id], [Country], [City], [Description])
                VALUES (@Id, @Country, @City, @Description);
            ";

            AddParameter(command, "@Id", destination.Id);
            AddParameter(command, "@Country", destination.Country);
            AddParameter(command, "@City", destination.City);
            AddParameter(command, "@Description", destination.Description);

            command.ExecuteNonQuery();
        }

        public bool Update(Destination destination)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                UPDATE [Destination]
                SET Country = @Country, City = @City, Description = @description
                WHERE Id = @destinationId;
            ";

            AddParameter(command, "@Country", destination.Country);
            AddParameter(command, "@City", destination.City);
            AddParameter(command, "@Description", destination.Description);
            AddParameter(command, "@destinationId", destination.Id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid destinationId)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                DELETE FROM [Destination]
                WHERE Id = @destinationId;
            ";

            AddParameter(command, "@destinationId", destinationId);

            return command.ExecuteNonQuery() > 0;
        }

        private static Destination MapDestination(IDataRecord record)
        {
            return new Destination(
                id: (Guid)record["Id"],
                country: (string)record["Country"],
                city: (string)record["City"],
                description: (string)record["Description"]
            );
        }

        private static void AddParameter(DbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }
    }
}
