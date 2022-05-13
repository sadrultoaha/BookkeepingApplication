using BookkeepingApi.Models;
using BookkeepingApi.Models.Dtos;
using BookkeepingApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Services
{
    public interface IPredefinedRecordsService
    {
        Task<PredefinedIncomeCostDto> GetAllPredefinedRecordsByYear(int year);
        Task<PredefinedRecords> GetPredefinedRecordById(int id);
        Task<Response> CreatePredefinedRecord(PredefinedRecords model);
        Task<Response> UpdatePredefinedRecord(PredefinedRecords model);
        Task<Response> DeletePredefinedRecord(int id);
    }
    public class PredefinedRecordsService:IPredefinedRecordsService
    {
        private readonly IPredefinedRecordsRepository _predefinedRecordsRepository;

        public PredefinedRecordsService(IPredefinedRecordsRepository PredefinedRecordsRepository)
        {
            _predefinedRecordsRepository = PredefinedRecordsRepository;
        }
        public async Task<PredefinedIncomeCostDto> GetAllPredefinedRecordsByYear(int year)
        {
            PredefinedIncomeCostDto list = await _predefinedRecordsRepository.GetAllPredefinedRecordsByYear(year);
            return list;
        }
        public async Task<PredefinedRecords> GetPredefinedRecordById(int id)
        {
            PredefinedRecords model = await _predefinedRecordsRepository.GetPredefinedRecordById(id);
            return model;
        }
        public async Task<Response>CreatePredefinedRecord(PredefinedRecords model)
        {
            Response IsCreated = await _predefinedRecordsRepository.CreatePredefinedRecord(model);
            return IsCreated;
        }
        public async Task<Response> UpdatePredefinedRecord(PredefinedRecords model)
        {
            Response IsUpdated = await _predefinedRecordsRepository.UpdatePredefinedRecord(model);
            return IsUpdated;
        }
        public async Task<Response> DeletePredefinedRecord(int id)
        {
            Response IsDeleted = await _predefinedRecordsRepository.DeletePredefinedRecord(id);
            return IsDeleted;
        }
    }
}
