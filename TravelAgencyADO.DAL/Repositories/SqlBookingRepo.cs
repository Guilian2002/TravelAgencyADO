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
    public class SqlBookingRepo : IBookingRepo
    {
        private readonly string _connectionString;

        public SqlBookingRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private DbConnection CreateConnection() => new SqlConnection(_connectionString);

        public ICollection<Booking> GetAll()
        {
            var list = new List<Booking>();
            var bookingListIncomplete = new List<Booking>();

            using var connection = CreateConnection();
            connection.Open();

            var bookingIds = new List<Guid>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM [Booking]";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookingListIncomplete.Add(MapBooking(reader));
                }

            }
            foreach (Booking booking in bookingListIncomplete)
            {
                var activityIds = GetActivitiesIdsForBooking(connection, booking.Id);
                list.Add(MapBookingAndActivities(booking, activityIds));
            }

            return list;
        }

        public Booking? GetById(Guid bookingId)
        {
            using var connection = CreateConnection();
            connection.Open();

            Booking booking;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT * FROM [Booking]
                    WHERE Id = @bookingId;
                ";

                AddParameter(command, "@bookingId", bookingId);

                using var reader = command.ExecuteReader();
                if (!reader.Read()) return null;

                booking = MapBooking(reader);
            }

            var activityIds = GetActivitiesIdsForBooking(connection, bookingId);

            return MapBookingAndActivities(booking, activityIds);
        }

        private List<Guid> GetActivitiesIdsForBooking(
            DbConnection connection,
            Guid bookingId)
        {
            var activityIds = new List<Guid>();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT ActivityId
                FROM ActivityBooked
                WHERE BookId = @bookingId;
            ";

            AddParameter(command, "@bookingId", bookingId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                activityIds.Add((Guid)reader["ActivityId"]);
            }

            return activityIds;
        }

        public void Insert(Booking booking)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                InsertBooking(connection, transaction, booking);

                foreach (var activityId in booking.ActivityIds)
                {
                    InsertBookingActivity(
                        connection,
                        transaction,
                        booking.Id,
                        activityId);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private void InsertBooking(DbConnection connection, DbTransaction transaction, Booking booking)
        {
            using var command = connection.CreateCommand();
            command.Transaction = transaction;

            command.CommandText = @"
            INSERT INTO Booking (Id, BookingDate, ClientName)
            VALUES (@Id, @BookingDate, @ClientName)";

            AddParameter(command, "@Id", booking.Id);
            AddParameter(command, "@BookingDate", booking.BookingDate!);
            AddParameter(command, "@ClientName", booking.ClientName);

            command.ExecuteNonQuery();
        }

        private void InsertBookingActivity(DbConnection connection, DbTransaction transaction, Guid bookingId, Guid activityId)
        {
            using var command = connection.CreateCommand();
            command.Transaction = transaction;

            command.CommandText = @"
            INSERT INTO ActivityBooked ([BookId], [ActivityId])
            VALUES (@BookingId, @ActivityId)";

            AddParameter(command, "@BookingId", bookingId);
            AddParameter(command, "@ActivityId", activityId);

            command.ExecuteNonQuery();
        }


        public bool Update(Booking booking)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                UPDATE [Booking]
                SET [BookingDate] = @BookingDate, [ClientName] = @ClientName
                WHERE Id = @bookingId;
            ";

            AddParameter(command, "@BookingDate", booking.BookingDate!);
            AddParameter(command, "@ClientName", booking.ClientName);
            AddParameter(command, "@bookingId", booking.Id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(Guid bookingId)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                DELETE FROM [Booking]
                WHERE Id = @bookingId;
            ";

            AddParameter(command, "@bookingId", bookingId);

            return command.ExecuteNonQuery() > 0;
        }

        private static Booking MapBooking(
            IDataRecord record)
        {
            return new Booking(
                id: (Guid)record["Id"],
                bookingDate: (DateTime?)record["BookingDate"],
                clientName: (string)record["ClientName"]
            );
        }

        private static Booking MapBookingAndActivities(
            Booking booking,
            ICollection<Guid> activityIds)
        {
            return new Booking(
                id: booking.Id,
                bookingDate: booking.BookingDate,
                clientName: booking.ClientName,
                activityIds: activityIds
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

