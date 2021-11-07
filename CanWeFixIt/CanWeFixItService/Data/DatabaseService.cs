using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CanWeFixItService.Entities;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace CanWeFixItService.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger _logger;
        private readonly IDbConnection _connection;


        public DatabaseService(IDbConnection connection, ILogger logger)
        {
            // The in-memory database only persists while a connection is open to it. To manage
            // its lifetime, keep one open connection around for as long as you need it.
            _logger = logger;
            _connection = connection;
            _connection.Open();
        }

        public async Task<IEnumerable<Instrument>> Instruments()
        {
            return await _connection.QueryAsync<Instrument>("Select Id, Sedol, Name, Active From Instrument Where Active = 1");
        }

        public async Task<IEnumerable<MarketData>> MarketData()
        {
            return await _connection.QueryAsync<MarketData>("SELECT Id, DataValue, Sedol, Active FROM MarketData WHERE Active = 1");
        }

        public async Task<IEnumerable<MarketValuation>> MarketValuations()
        {
            var marketValuation = await _connection.QueryAsync<MarketValuation>("SELECT 'DataValueTotal' as 'Name', Sum(DataValue) as 'Total' FROM MarketData WHERE Active = 1");
            return marketValuation;
        }

        /// <summary>
        /// This is complete and will correctly load the test data.
        /// It is called during app startup 
        /// </summary>
        public void SetupDatabase()
        {
            const string createInstruments = @"
                CREATE TABLE instrument
                (
                    id     int,
                    sedol  text,
                    name   text,
                    active int
                );
                INSERT INTO instrument
                VALUES (1, 'Sedol1', 'Name1', 0),
                       (2, 'Sedol2', 'Name2', 1),
                       (3, 'Sedol3', 'Name3', 0),
                       (4, 'Sedol4', 'Name4', 1),
                       (5, 'Sedol5', 'Name5', 0),
                       (6, '', 'Name6', 1),
                       (7, 'Sedol7', 'Name7', 0),
                       (8, 'Sedol8', 'Name8', 1),
                       (9, 'Sedol9', 'Name9', 0)";

            _connection.Execute(createInstruments);
            _logger.LogInformation($"Executed :{createInstruments}");
            const string createMarketData = @"
                CREATE TABLE marketdata
                (
                    id        int,
                    datavalue int,
                    sedol     text,
                    active    int
                );
                INSERT INTO marketdata
                VALUES (1, 1111, 'Sedol1', 0),
                       (2, 2222, 'Sedol2', 1),
                       (3, 3333, 'Sedol3', 0),
                       (4, 4444, 'Sedol4', 1),
                       (5, 5555, 'Sedol5', 0),
                       (6, 6666, 'Sedol6', 1)";

            _connection.Execute(createMarketData);
            _logger.LogInformation($"Executed :{createMarketData}");
        }
    }
}