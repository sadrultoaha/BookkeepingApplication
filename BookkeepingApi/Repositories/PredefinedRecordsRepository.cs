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
        Task<List<PredefinedRecords>> GetAllPredefinedRecords();
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
                                        RT.[ActionName] as ActionName,
                                        YEAR(PR.[Date]) as Year,
                                        SUBSTRING(DATENAME(m,PR.[Date]), 1, 3) as Month,
                                        SUM(PR.[Amount]) as Amount
                                    INTO #IncomeCost
                                    FROM [dbo].[PredefinedRecords] PR
                                    INNER JOIN [dbo].[RecordTypes] RT ON RT.Id=PR.TypeId
                                    WHERE YEAR(PR.[Date]) = @year 
                                    GROUP BY Year(PR.[Date]), DATENAME(m,PR.[Date]), RT.[ActionName]

                                    SELECT 
                                        [Year],	
	                                    (CASE WHEN [ActionName] = 'income' THEN 'Income' ELSE 'Cost' END) as ActionName,
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
                            if(dr["ActionName"].ToString() == "Income")
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

        public async Task<List<PredefinedRecords>> GetAllPredefinedRecords()
        {
            List<PredefinedRecords> list = new();
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @" SELECT 
                                         PR.[Id]
                                        ,PR.[TypeId]
                                        ,PR.[Date]
                                        ,PR.[Amount]
                                        ,RT.[ActionName] as ActionName
                                        ,RT.[TypeName] as TypeName
                                    FROM [dbo].[PredefinedRecords] PR
                                    INNER JOIN [dbo].[RecordTypes] RT ON RT.Id=PR.TypeId;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            PredefinedRecords model = new();

                            model.Id = Convert.ToInt32(dr["Id"]);
                            model.TypeId = Convert.ToInt32(dr["TypeId"]);
                            model.Date = Convert.ToDateTime(dr["Date"]);
                            model.Amount = Convert.ToDecimal(dr["Amount"]);
                            model.ActionName = Convert.ToString(dr["ActionName"]);
                            model.TypeName = Convert.ToString(dr["TypeName"]);
                            
                            list.Add(model);
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
                    string sql = @" SELECT 
                                         [Id]
                                        ,[TypeId]
                                        ,[Date]
                                        ,[Amount]
                                    FROM [dbo].[PredefinedRecords]
                                    WHERE Id = @id;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            model.Id = Convert.ToInt32(dr["Id"]);
                            model.TypeId = Convert.ToInt32(dr["TypeId"]);
                            model.Date = Convert.ToDateTime(dr["Date"]);
                            model.Amount = Convert.ToDecimal(dr["Amount"]);
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
                    string sql = @"INSERT INTO [dbo].[PredefinedRecords] ([TypeId], [Date], [Amount]) VALUES (@TypeId, @Date, @Amount);";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@TypeId", Value = model.TypeId, SqlDbType = SqlDbType.Int });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Date", Value = model.Date , SqlDbType = SqlDbType.DateTime });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Amount", Value = model.Amount, SqlDbType = SqlDbType.Decimal });

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
                    string sql = @"UPDATE [dbo].[PredefinedRecords]
                                   SET
                                       [TypeId] = @TypeId
                                      ,[Date] = @Date
                                      ,[Amount] = @Amount
                                   WHERE Id = @Id;";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Id", Value = model.Id, SqlDbType = SqlDbType.Int });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@TypeId", Value = model.TypeId, SqlDbType = SqlDbType.Int });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Date", Value = model.Date, SqlDbType = SqlDbType.DateTime });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Amount", Value = model.Amount, SqlDbType = SqlDbType.Decimal });

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
                    string sql = @"Delete FROM [dbo].[PredefinedRecords] WHERE Id = @id;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });
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

            model.Year = Convert.ToInt32(dr["Year"] as int?);
            model.ActionName = Convert.ToString(dr["ActionName"]);
            model.Jan = Convert.ToDecimal(dr["Jan"] as decimal?);
            model.Feb = Convert.ToDecimal(dr["Feb"] as decimal?);
            model.Mar = Convert.ToDecimal(dr["Mar"] as decimal?);
            model.Apr = Convert.ToDecimal(dr["Apr"] as decimal?);
            model.May = Convert.ToDecimal(dr["May"] as decimal?);
            model.Jun = Convert.ToDecimal(dr["Jun"] as decimal?);
            model.Jul = Convert.ToDecimal(dr["Jul"] as decimal?);
            model.Aug = Convert.ToDecimal(dr["Aug"] as decimal?);
            model.Sep = Convert.ToDecimal(dr["Sep"] as decimal?);
            model.Oct = Convert.ToDecimal(dr["Oct"] as decimal?);
            model.Nov = Convert.ToDecimal(dr["Nov"] as decimal?);
            model.Dec = Convert.ToDecimal(dr["Dec"] as decimal?);
            model.TypeName = model.ActionName;

            return model;
        }

       
    }
}
