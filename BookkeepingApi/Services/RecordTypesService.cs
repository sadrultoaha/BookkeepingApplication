using BookkeepingApi.Models;
using BookkeepingApi.Models.Dtos;
using BookkeepingApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Services
{
    public interface IRecordTypesService
    {
        Task<List<RecordTypes>> GetAllRecordTypes();
        Task<RecordTypes> GetRecordTypeById(int id);
        Task<Response> CreateRecordType(RecordTypes model);
        Task<Response> UpdateRecordType(RecordTypes model);
        Task<Response> DeleteRecordType(int id);
    }
    public class RecordTypesService:IRecordTypesService
    {
        private readonly IRecordTypesRepository _recordTypesRepository;

        public RecordTypesService(IRecordTypesRepository recordTypesRepository)
        {
            _recordTypesRepository = recordTypesRepository;
        }
        public async Task<List<RecordTypes>> GetAllRecordTypes()
        {
            List<RecordTypes> list = await _recordTypesRepository.GetAllRecordTypes();
            return list;
        }
        public async Task<RecordTypes> GetRecordTypeById(int id)
        {
            RecordTypes model = await _recordTypesRepository.GetRecordTypeById(id);
            return model;
        }
        public async Task<Response>CreateRecordType(RecordTypes model)
        {
            Response IsCreated = await _recordTypesRepository.CreateRecordType(model);
            return IsCreated;
        }
        public async Task<Response> UpdateRecordType(RecordTypes model)
        {
            Response IsUpdated = await _recordTypesRepository.UpdateRecordType(model);
            return IsUpdated;
        }
        public async Task<Response> DeleteRecordType(int id)
        {
            Response IsDeleted = await _recordTypesRepository.DeleteRecordType(id);
            return IsDeleted;
        }
    }
}
