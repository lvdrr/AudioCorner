using Npgsql;

namespace AudioCornerDB
{
    class DataBase
    {
        private NpgsqlConnection _connection;

        public DataBase()
        {
            _connection = new NpgsqlConnection(@"Server = localhost; Port = 5432; User Id = art; Password = 1111; DataBase = AudioCorner;");
        }

        public NpgsqlConnection GetConnection()
        {
            return _connection;
        }

        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}