using AEPersonalTrainerApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEPersonalTrainerApp.Data
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Training>().Wait();
            _database.CreateTableAsync<TrainingItem>().Wait();
            _database.CreateTableAsync<TrainingItemXref>().Wait();
        }


        // Métodos CRUD para Training
        public Task<List<Training>> GetTrainingsAsync()
        {
            return _database.Table<Training>().ToListAsync();
        }

        public Task<int> SaveTrainingAsync(Training training)
        {
            if (training.Id != 0)
            {
                return _database.UpdateAsync(training);
            }
            else
            {
                return _database.InsertAsync(training);
            }
        }

        public Task<int> DeleteTrainingAsync(Training training)
        {
            return _database.DeleteAsync(training);
        }

        // Métodos CRUD para TrainingItem
        public Task<List<TrainingItem>> GetTrainingItemsAsync()
        {
            return _database.Table<TrainingItem>().ToListAsync();
        }

        public Task<int> SaveTrainingItemAsync(TrainingItem trainingItem)
        {
            if (trainingItem.Id != 0)
            {
                return _database.UpdateAsync(trainingItem);
            }
            else
            {
                return _database.InsertAsync(trainingItem);
            }
        }

        public Task<int> DeleteTrainingItemAsync(TrainingItem trainingItem)
        {
            return _database.DeleteAsync(trainingItem);
        }

        // Métodos para gestionar las relaciones en TrainingItemXref
        public Task<List<TrainingItem>> GetItemsByTrainingAsync(int trainingId)
        {
            return _database.QueryAsync<TrainingItem>(
                "SELECT ei.* FROM TrainingItem ei " +
                "JOIN TrainingItemXref xref ON ei.Id = xref.IdTrainingItem " +
                "WHERE xref.IdTraining = ?", trainingId);
        }

        public Task<int> AddTrainingItemToTrainingAsync(int trainingId, int trainingItemId)
        {
            var xref = new TrainingItemXref
            {
                IdTraining = trainingId,
                IdTrainingItem = trainingItemId
            };
            return _database.InsertAsync(xref);
        }
    }
}
