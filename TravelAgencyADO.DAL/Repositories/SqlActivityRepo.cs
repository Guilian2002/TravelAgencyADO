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
    public class SqlActivityRepo : IActivityRepo
    {
        private readonly string _connectionString;

        public SqlActivityRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection CreateConnection() => new SqlConnection(_connectionString);

        public ICollection<Activity> GetAll(Guid destinationId)
        {
            var list = new List<Activity>();

            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT * FROM [Activity] WHERE DestinationId = @destinationId;";

            AddParameter(command, "@destinationId", destinationId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapActivity(reader));
            }

            return list;
        }

        public Activity? GetById(Guid activityId)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                SELECT * FROM [Activity]
                WHERE Id = @activityId;
            ";

            AddParameter(command, "@activityId", activityId);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;

            return MapActivity(reader);
        }

        public void Insert(Activity activity)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO [Activity] ([Id], [Title], [Price], [Description], [DestinationId])
                VALUES (@Id, @Title, @Price, @Description, @DestinationId);
            ";

            AddParameter(command, "@Id", activity.Id);
            AddParameter(command, "@Title", activity.Title);
            AddParameter(command, "@Price", activity.Price);
            AddParameter(command, "@Description", activity.Description);
            AddParameter(command, "@DestinationId", activity.DestinationId);

            command.ExecuteNonQuery();
        }

        public bool Update(Activity activity)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                UPDATE [Activity]
                SET Title = @title, Price = @price, Description = @description
                WHERE Id = @activityId;
            ";

            AddParameter(command, "@title", activity.Title);
            AddParameter(command, "@price", activity.Price);
            AddParameter(command, "@description", activity.Description);
            AddParameter(command, "@activityId", activity.Id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid activityId)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                DELETE FROM [Activity]
                WHERE Id = @activityId;
            ";

            AddParameter(command, "@activityId", activityId);

            return command.ExecuteNonQuery() > 0;
        }

        private static Activity MapActivity(IDataRecord record)
        {
            return new Activity(
                id: (Guid)record["Id"],
                title: (string)record["Title"],
                price: (decimal)record["Price"],
                description: (string)record["Description"],
                destinationId: (Guid)record["DestinationId"]
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
