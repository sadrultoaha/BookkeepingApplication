using BookkeepingApi.Models;
using BookkeepingApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Services
{
    public interface IPredefinedRecordsService
    {
        Task<List<PredefinedRecords>> GetAllPredefinedRecordsByYear(string year);
        Task<Response> UpdatePredefinedRecords(PredefinedRecords model);
    }
    public class PredefinedRecordsService:IPredefinedRecordsService
    {
        private readonly IPredefinedRecordsRepository _predefinedRecordsRepository;

        public PredefinedRecordsService(IPredefinedRecordsRepository PredefinedRecordsRepository)
        {
            _predefinedRecordsRepository = PredefinedRecordsRepository;
        }

        public async Task<List<PredefinedRecords>> GetAllPredefinedRecordsByYear(string year)
        {
            var list = await _predefinedRecordsRepository.GetAllPredefinedRecordsByYear(year);
            return list;

        }

        public async Task<Response> UpdatePredefinedRecords(PredefinedRecords model)
        {
            Response IsUpdated = await _predefinedRecordsRepository.UpdatePredefinedRecords(model);

            return IsUpdated;
        }
    }
}
