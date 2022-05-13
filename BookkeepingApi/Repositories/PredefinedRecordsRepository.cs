using BookkeepingApi.Models;
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
        Task<List<PredefinedRecords>> GetAllPredefinedRecordsByYear(string year);
        Task<Response> UpdatePredefinedRecords(PredefinedRecords model);
    }

    public class PredefinedRecordsRepository : IPredefinedRecordsRepository
    {
        private readonly IConfiguration _config;
        public PredefinedRecordsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<PredefinedRecords>> GetAllPredefinedRecordsByYear(string year)
        {
            List<PredefinedRecords> list = new();
            using (SqlConnection conn = new (_config.GetConnectionString("DefaultConnection")))
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
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@year", Value = year, SqlDbType = SqlDbType.NVarChar });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            PredefinedRecords model = new();

                            list.Add(model);
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

        public async Task<Response> UpdatePredefinedRecords(PredefinedRecords model)
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
    }
}
