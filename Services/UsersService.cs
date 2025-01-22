﻿using GreenPath.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace GreenPath.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<Plant> _usersCollection;

        public UsersService(
            IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Plant>(
                databaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<Plant>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Plant?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Plant newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, Plant updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}