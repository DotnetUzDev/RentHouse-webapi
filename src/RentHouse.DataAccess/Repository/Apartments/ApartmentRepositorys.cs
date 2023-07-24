﻿using Dapper;
using RentHouse.DataAccess.Common.Interface;
using RentHouse.DataAccess.Interface;
using RentHouse.DataAccess.Interface.Apartments;
using RentHouse.DataAccess.Utils;
using RentHouse.Domain.Entities.Apartments;

namespace RentHouse.DataAccess.Repository.Apartments;

public class ApartmentRepositorys : BaseRepository, IApartmentRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select count(*) from apartment";
            var result = await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {

            return 0;

        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Apartment entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.apartment(" +
                "description, adress, image_path, common_price, roomcount, created_at, updated_at)" +
                "VALUES (@Describtion, @Adress, @Image_Path, @CommonPrice, @RoomCount, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM apartment WHERE id=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;

        }
        catch
        {

            return 0;

        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Apartment>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM apartment order by id desc " +
                $"offset {/*@params.GetSkipCount()*/1} limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<Apartment>(query)).ToList();
            return result;
        }
        catch
        {

            return new List<Apartment>();

        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
    Task<(int itemsCount, IList<Apartment>)> ISearchable<Apartment>.SearchAsync(string search, PaginationParams paginationParams)
    {
        throw new NotImplementedException();
    }
    public async Task<int> UpdateAsync(long id, Apartment entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.apartment" +
                "SET description=@Description, adress=@Adress, image_path = @ImagePath, common_price=@CommonPrice, roomcount=@RoomCount, created_at=@CreatedAt, updated_at=@UpdatedAt" +
                $"WHERE id={id};";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch 
        {

            return 0;
        }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<Apartment?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM apartment where id = @Id";
            var result = await _connection.QuerySingleAsync<Apartment>(query, new { Id = id}); 
            return result;
        }
        catch
        {

            return null;

        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    
}
