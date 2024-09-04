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

        public Task<int> SaveTrainingAsync(Training entrenamiento)
        {
            if (entrenamiento.Id != 0)
            {
                return _database.UpdateAsync(entrenamiento);
            }
            else
            {
                return _database.InsertAsync(entrenamiento);
            }
        }

        public Task<int> DeleteTrainingAsync(Training entrenamiento)
        {
            return _database.DeleteAsync(entrenamiento);
        }

        // Métodos CRUD para TrainingItem
        public Task<List<TrainingItem>> GetTrainingItemsAsync()
        {
            return _database.Table<TrainingItem>().ToListAsync();
        }

        public Task<int> SaveTrainingItemAsync(TrainingItem entrenamientoItem)
        {
            if (entrenamientoItem.Id != 0)
            {
                return _database.UpdateAsync(entrenamientoItem);
            }
            else
            {
                return _database.InsertAsync(entrenamientoItem);
            }
        }

        public Task<int> DeleteTrainingItemAsync(TrainingItem entrenamientoItem)
        {
            return _database.DeleteAsync(entrenamientoItem);
        }

        // Métodos para gestionar las relaciones en TrainingItemXref
        public Task<List<TrainingItem>> GetItemsByTrainingAsync(int entrenamientoId)
        {
            return _database.QueryAsync<TrainingItem>(
                "SELECT ei.* FROM TrainingItem ei " +
                "JOIN TrainingItemXref xref ON ei.Id = xref.IdTrainingItem " +
                "WHERE xref.IdTraining = ?", entrenamientoId);
        }

        public Task<int> AddTrainingItemToTrainingAsync(int entrenamientoId, int entrenamientoItemId)
        {
            var xref = new TrainingItemXref
            {
                IdTraining = entrenamientoId,
                IdTrainingItem = entrenamientoItemId
            };
            return _database.InsertAsync(xref);
        }
    }
}
