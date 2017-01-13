﻿using MongoDB.Driver;
using System;
using System.Data.Entity.Design.PluralizationServices;

namespace MongoDbRepository
{
    public class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// Get connection string from Mongo Client
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Get database name from Mongo Client
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// Pluralize document name on server
        /// Default value = false
        /// </summary>
        public bool PluralizeDocumentName { get; set; }

        private IMongoClient client;
        private IMongoDatabase database;

        public MongoDbContext()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString), "Cannot be null or empty !");
        }

        public MongoDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            DatabaseName = MongoUrl.Create(ConnectionString).DatabaseName;
            client = new MongoClient(ConnectionString);
            database = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> Collection<T>() where T : MongoEntity
        {
            //Pluralize class name
            var pluralizeService = PluralizationService.CreateService(new System.Globalization.CultureInfo("en-US"));
            string tableName = PluralizeDocumentName ? pluralizeService.Pluralize(typeof(T).Name) : typeof(T).Name;

            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}