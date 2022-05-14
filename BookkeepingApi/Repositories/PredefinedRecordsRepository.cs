using BookkeepingApi.Models;
using BookkeepingApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Repository
{
    public interface IPredefinedRecordsRepository
    {
        Task<PredefinedIncomeCostDto> GetAllPredefinedRecordsByYear(int year);
        Task<PredefinedRecords> GetPredefinedRecordById(int id);
        Task<Response> CreatePredefinedRecord(PredefinedRecords model);
        Task<Response> UpdatePredefinedRecord(PredefinedRecords model);
        Task<Response> DeletePredefinedRecord(int id);
    }

    public class PredefinedRecordsRepository : IPredefinedRecordsRepository
    {
        private readonly IConfiguration _config;
        public PredefinedRecordsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PredefinedIncomeCostDto> GetAllPredefinedRecordsByYear(int year)
        {
            PredefinedIncomeCostDto list = new();
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @"
                                    IF OBJECT_ID('tempdb..#IncomeCost') IS NOT NULL
                                    DROP TABLE #IncomeCost
                                    
                                    SELECT
                                        RT.[ActionName] as Action,
                                        YEAR(PR.[Date]) as Year,
                                        SUBSTRING(DATENAME(m,PR.[Date]), 1, 3) as Month,
                                        SUM(PR.[Amount]) as Amount
                                    INTO #IncomeCost
                                    FROM PredefinedRecords PR
                                    INNER JOIN RecordTypes RT ON RT.Id=PR.TypeId
                                    WHERE YEAR(PR.[Date]) = @year 
                                    GROUP BY Year(PR.[Date]), DATENAME(m,PR.[Date]), RT.[ActionName]

                                    SELECT 
                                        [Year],	
	                                    (CASE WHEN [Action] = 'income' THEN 'Income' ELSE 'Cost' END) as Action,
                                        [Jan], [Feb], [Mar], [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec]
                                    FROM #IncomeCost

                                    PIVOT
                                    (
                                        SUM(Amount)
                                        FOR [Month]
                                        IN ([Jan], [Feb], [Mar], [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec])
                                    ) as MonthWiseIncomeCost;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@year", Value = year, SqlDbType = SqlDbType.Int });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            if(dr["Action"].ToString() == "Income")
                            {
                                list.Income = LoadIncomeCostData(dr);
                            }
                            else
                            {
                                list.Cost = LoadIncomeCostData(dr);
                            }

                        }

                        await dr.CloseAsync();
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
            return list;
        }
        public async Task<PredefinedRecords> GetPredefinedRecordById(int id)
        {
            PredefinedRecords model = new();
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {

                        }

                        await dr.CloseAsync();
                    }

                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
            return model;
        }
        public async Task<Response> CreatePredefinedRecord(PredefinedRecords model)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    return new Response(StatusCodes.Status500InternalServerError, false, ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
            return new Response(StatusCodes.Status200OK, true, "Saved Successfully.");
        }
        public async Task<Response> UpdatePredefinedRecord(PredefinedRecords model)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    return new Response(StatusCodes.Status500InternalServerError, false, ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
            return new Response(StatusCodes.Status200OK, true, "Updated Successfully.");
        }
        public async Task<Response> DeletePredefinedRecord(int id)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    return new Response(StatusCodes.Status500InternalServerError, false, ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        await conn.CloseAsync();
                    }
                }
            }
            return new Response(StatusCodes.Status200OK, true, "Deleted Successfully.");
        }

        private IncomeCostDto LoadIncomeCostData(SqlDataReader dr)
        {
            IncomeCostDto model = new();

            model.Year = Convert.ToInt32(dr["Year"]);
            model.Action = Convert.ToString(dr["Action"]);
            model.Jan = Convert.ToDecimal(dr["Jan"]);
            model.Feb = Convert.ToDecimal(dr["Feb"]);
            model.Mar = Convert.ToDecimal(dr["Mar"]);
            model.Apr = Convert.ToDecimal(dr["Apr"]);
            model.May = Convert.ToDecimal(dr["May"]);
            model.Jun = Convert.ToDecimal(dr["Jun"]);
            model.Jul = Convert.ToDecimal(dr["Jul"]);
            model.Aug = Convert.ToDecimal(dr["Aug"]);
            model.Sep = Convert.ToDecimal(dr["Sep"]);
            model.Oct = Convert.ToDecimal(dr["Oct"]);
            model.Nov = Convert.ToDecimal(dr["Nov"]);
            model.Dec = Convert.ToDecimal(dr["Dec"]);
            model.Details = model.Action;

            return model;
        }

       
    }
}
