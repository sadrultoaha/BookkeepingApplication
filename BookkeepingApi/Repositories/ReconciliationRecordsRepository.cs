using BookkeepingApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookkeepingApi.Repository
{
    public interface IReconciliationRecordsRepository
    {
        Task<List<ReconciliationRecords>> GetAllReconciliationsByYear(int year);
        Task<Response> Reconcile(List<ReconciliationRecords> list);
    }

    public class ReconciliationRecordsRepository : IReconciliationRecordsRepository
    {
        private readonly IConfiguration _config;
        public ReconciliationRecordsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ReconciliationRecords>> GetAllReconciliationsByYear(int year)
        {
            List<ReconciliationRecords> list = new();
            using (SqlConnection conn = new (_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @"SELECT 
                                     RR.[Id]
                                    ,RR.[TypeId]
                                    ,UPPER(RT.[ActionName]) as Action
                                    ,UPPER(RT.[TypeName]) as Details
                                    ,@year as Year
                                    ,SUM(RR.[Jan]) as Jan
                                    ,SUM(RR.[Feb]) as Feb
                                    ,SUM(RR.[Mar]) as Mar
                                    ,SUM(RR.[Apr]) as Apr
                                    ,SUM(RR.[May]) as May
                                    ,SUM(RR.[Jun]) as Jun
                                    ,SUM(RR.[Jul]) as Jul
                                    ,SUM(RR.[Aug]) as Aug
                                    ,SUM(RR.[Sep]) as Sep
                                    ,SUM(RR.[Oct]) as Oct
                                    ,SUM(RR.[Nov]) as Nov
                                    ,SUM(RR.[Dec]) as Dec
                                FROM [ReconciliationRecords] RR 
                                INNER JOIN [RecordTypes] RT on RT.[Id] = RR.[TypeId]
                                WHERE RR.[Year] = @year
                                GROUP BY RR.[TypeId], RT.[ActionName], RT.[TypeName], RR.[Id]
                                ORDER BY RT.[ActionName] DESC, RT.[TypeName];";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@year", Value = year, SqlDbType = SqlDbType.Int });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            ReconciliationRecords model = new();

                            model.Id = Convert.ToInt32(dr["Id"]);
                            model.TypeId = Convert.ToInt32(dr["TypeId"]);
                            model.Action = Convert.ToString(dr["Action"]);
                            model.Details = Convert.ToString(dr["Details"]);
                            model.Year = Convert.ToInt32(dr["Year"]);
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

        public async Task<Response> Reconcile(List<ReconciliationRecords> list)
        {
            StringBuilder sql = new();

            foreach (var model in list)
            {
                if (model.Id > 0)
                {
                    string updateSql = @" UPDATE [dbo].[ReconciliationRecords]
                                       SET
                                        [Jan] = '" + model.Jan
                                  + "' ,[Feb] = '" + model.Feb
                                  + "' ,[Mar] = '" + model.Mar
                                  + "' ,[Apr] = '" + model.Apr
                                  + "' ,[Feb] = '" + model.May
                                  + "' ,[Mar] = '" + model.Jun
                                  + "' ,[Apr] = '" + model.Jul
                                  + "' ,[Feb] = '" + model.Aug
                                  + "' ,[Mar] = '" + model.Sep
                                  + "' ,[Apr] = '" + model.Oct
                                  + "' ,[Mar] = '" + model.Nov
                                  + "' ,[Apr] = '" + model.Dec

                                  + "' WHERE [Id] = " + model.Id.ToString()
                                  + ";";

                    sql.Append(updateSql);
                }
                else
                {
                    string createSql = @" INSERT INTO [dbo].[ReconciliationRecords] 
                                       (TypeId, Year, Jan, Feb, Mar, May, Jun, Jul, Aug, Sep, Nov, Dec)
                                       VALUES

                                     (  " + model.TypeId.ToString()
                                   + ", " + model.Year.ToString()
                                   + ",'" + model.Jan
                                   + "','" + model.Feb
                                   + "','" + model.Mar
                                   + "','" + model.Apr
                                   + "','" + model.May
                                   + "','" + model.Jun
                                   + "','" + model.Jul
                                   + "','" + model.Aug
                                   + "','" + model.Sep
                                   + "','" + model.Oct
                                   + "','" + model.Nov
                                   + "','" + model.Dec
                                   + "' ); ";

                    sql.Append(createSql);
                }

            }
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    using (SqlCommand cmd = new(sql.ToString(), conn))
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
