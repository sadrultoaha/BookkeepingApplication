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
    public interface IRecordTypesRepository
    {
        Task<List<RecordTypes>> GetAllRecordTypes();
        Task<RecordTypes> GetRecordTypeById(int id);
        Task<Response> CreateRecordType(RecordTypes model);
        Task<Response> UpdateRecordType(RecordTypes model);
        Task<Response> DeleteRecordType(int id);
    }

    public class RecordTypesRepository : IRecordTypesRepository
    {
        private readonly IConfiguration _config;
        public RecordTypesRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<RecordTypes>> GetAllRecordTypes()
        {
            List<RecordTypes> list = new();
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @"SELECT [Id], [ActionName], [TypeName] FROM [dbo].[RecordTypes];";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            RecordTypes model = new();

                            model.Id = Convert.ToInt32(dr["Id"]);
                            model.ActionName = Convert.ToString(dr["ActionName"]);
                            model.TypeName = Convert.ToString(dr["TypeName"]);

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
        public async Task<RecordTypes> GetRecordTypeById(int id)
        {
            RecordTypes model = new();
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "SELECT [Id], [ActionName], [TypeName] FROM [dbo].[RecordTypes] WHERE Id = @id;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@id", Value = id, SqlDbType = SqlDbType.Int });

                        SqlDataReader dr = await cmd.ExecuteReaderAsync();

                        while (await dr.ReadAsync())
                        {
                            model.Id = Convert.ToInt32(dr["Id"]);
                            model.ActionName = Convert.ToString(dr["ActionName"]);
                            model.TypeName = Convert.ToString(dr["TypeName"]);
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
        public async Task<Response> CreateRecordType(RecordTypes model)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = "INSERT INTO [dbo].[RecordTypes] ([ActionName], [TypeName]) VALUES(@ActionName, @TypeName);";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@ActionName", Value = model.ActionName, SqlDbType = SqlDbType.NVarChar });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@TypeName", Value = model.TypeName, SqlDbType = SqlDbType.NVarChar });

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
        public async Task<Response> UpdateRecordType(RecordTypes model)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @"UPDATE [dbo].[RecordTypes]
                                   SET 
                                   [ActionName] = @ActionName,
                                   [TypeName] = @TypeName
                                   WHERE Id = @Id;";

                    using (SqlCommand cmd = new(sql, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@Id", Value = model.Id, SqlDbType = SqlDbType.Int });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@ActionName", Value = model.ActionName, SqlDbType = SqlDbType.NVarChar });
                        cmd.Parameters.Add(new SqlParameter { ParameterName = "@TypeName", Value = model.TypeName, SqlDbType = SqlDbType.NVarChar });

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
        public async Task<Response> DeleteRecordType(int id)
        {
            using (SqlConnection conn = new(_config.GetConnectionString("DefaultConnection")))
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }
                try
                {
                    string sql = @"DELETE FROM [dbo].[RecordTypes] WHERE Id = @id;";

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
    }
}
