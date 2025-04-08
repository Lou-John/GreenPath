﻿using GreenPath.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace GreenPath.Services
{
   public class PlantsService
   {
      private readonly IMongoCollection<Plant> _plantsCollection;

      public PlantsService(
          IOptions<DatabaseSettings> databaseSettings)
      {
         var mongoClient = new MongoClient(
             databaseSettings.Value.ConnectionString);

         var mongoDatabase = mongoClient.GetDatabase(
             databaseSettings.Value.DatabaseName);

         _plantsCollection = mongoDatabase.GetCollection<Plant>(
             databaseSettings.Value.PlantsCollectionName);
         Console.WriteLine($"Connecting to MongoDB at: {databaseSettings.Value.ConnectionString}");
         Console.WriteLine($"Using database: {databaseSettings.Value.DatabaseName}");
         
      }

      public async Task<List<Plant>> GetAsync()
      {
         try
         {
            return await _plantsCollection.Find(_ => true).ToListAsync();
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Error fetching plants: {ex.Message}");
            throw;
         }
      }

      public async Task<Plant?> GetAsync(string id) =>
          await _plantsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();


      public async Task<Plant?> GetByExternalIdAsync(string externalId) =>
          await _plantsCollection.Find(x => x.ExternalId == externalId).FirstOrDefaultAsync();

      public async Task CreateAsync(Plant newPlant) =>
          await _plantsCollection.InsertOneAsync(newPlant);

      public async Task UpdateAsync(string id, Plant updatedPlant) =>
          await _plantsCollection.ReplaceOneAsync(x => x.Id == id, updatedPlant);

      public async Task RemoveAsync(string id) =>
          await _plantsCollection.DeleteOneAsync(x => x.Id == id);
   }
}
