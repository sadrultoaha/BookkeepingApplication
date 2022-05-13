using BookkeepingApi.Models;
using BookkeepingApi.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookkeepingApi.Services
{
    public interface IReconciliationRecordsService
    {
        Task<List<ReconciliationRecords>> GetAllReconciliationsByYear(int year);
        Task<Response> Reconcile(List<ReconciliationRecords> model);
    }
    public class ReconciliationRecordsService:IReconciliationRecordsService
    {
        private readonly IReconciliationRecordsRepository _reconciliationRecordsRepository;

        public ReconciliationRecordsService(IReconciliationRecordsRepository reconciliationRecordsRepository)
        {
            _reconciliationRecordsRepository = reconciliationRecordsRepository;
        }

        public async Task<List<ReconciliationRecords>> GetAllReconciliationsByYear(int year)
        {
            var list = await _reconciliationRecordsRepository.GetAllReconciliationsByYear(year);
            return list;
        }

        public async Task<Response> Reconcile(List<ReconciliationRecords> list)
        {
            Response IsUpdated = await _reconciliationRecordsRepository.Reconcile(list);
            return IsUpdated;
        }
    }
}
