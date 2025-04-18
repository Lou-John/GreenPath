﻿namespace GreenPath.Models
{
   public class DatabaseSettings
   {
      public string ConnectionString { get; set; } = null!;

      public string DatabaseName { get; set; } = null!;

      public string PlantsCollectionName { get; set; } = null!;
      public string UsersCollectionName { get; set; } = null!;
      public string HousesCollectionName { get; set; } = null!;

      public string ApiKey { get; set; } = null!;
      public string BaseUrl { get; set; } = null!;


   }
}
