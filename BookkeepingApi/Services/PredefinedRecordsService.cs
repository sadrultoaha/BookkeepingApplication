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
            list.CumulativeIncome = CumulativeSum(list.Income);
            list.CumulativeCost = CumulativeSum(list.Cost);
            list.Result = IncomeCostResult(list.Income, list.Cost);

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

        private IncomeCostDto CumulativeSum(IncomeCostDto model)
        {
            IncomeCostDto cumulativeAmount = new();

            cumulativeAmount.Year = model.Year;
            cumulativeAmount.Action = model.Action;
            cumulativeAmount.Details = "Cumulative " + model.Action.ToString();
            cumulativeAmount.Jan = model.Jan;
            cumulativeAmount.Feb = model.Jan + model.Feb;
            cumulativeAmount.Mar = model.Jan + model.Feb + model.Mar;
            cumulativeAmount.Apr = model.Jan + model.Feb + model.Mar + model.Apr;
            cumulativeAmount.May = model.Jan + model.Feb + model.Mar + model.Apr + model.May;
            cumulativeAmount.Jun = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun;
            cumulativeAmount.Jul = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul;
            cumulativeAmount.Aug = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul + model.Aug;
            cumulativeAmount.Sep = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul + model.Aug + model.Sep;
            cumulativeAmount.Oct = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul + model.Aug + model.Sep + model.Oct;
            cumulativeAmount.Nov = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul + model.Aug + model.Sep + model.Oct + model.Nov;
            cumulativeAmount.Dec = model.Jan + model.Feb + model.Mar + model.Apr + model.May + model.Jun + model.Jul + model.Aug + model.Sep + model.Oct + model.Nov + model.Dec;

            return cumulativeAmount;
        }

        private IncomeCostDto IncomeCostResult(IncomeCostDto income, IncomeCostDto cost)
        {
            IncomeCostDto result = new();

            result.Year = income.Year;
            result.Action = "Income - Cost";
            result.Details = "Result";
            result.Jan = income.Jan - cost.Jan;
            result.Feb = income.Feb - cost.Feb; 
            result.Mar = income.Mar - cost.Mar; 
            result.Apr = income.Apr - cost.Apr; 
            result.May = income.May - cost.May; 
            result.Jun = income.Jun - cost.Jun; 
            result.Jul = income.Jul - cost.Jul; 
            result.Aug = income.Aug - cost.Aug; 
            result.Sep = income.Sep - cost.Sep; 
            result.Oct = income.Oct - cost.Oct; 
            result.Nov = income.Nov - cost.Nov; 
            result.Dec = income.Dec - cost.Dec; 

            return result;
        }
    }
}
