using DataAccessLayer.Data;
using DataAccessLayer.DataBaseTable;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class UserRegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRegisterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region`Add Record With Stored Procedure
        public async Task<TblUserRegistration> AddTblUserRegisterationAsync(TblUserRegistration record)
        {
            #region Without Stored Procedure
            //if (record.Id > 0)
            //{
            //    var userData = await GetUserById(record.Id);
            //    if (userData != null)
            //    {
            //        foreach (var item in userData)
            //        {
            //            item.StateId = record.StateId;
            //            item.CityId = record.CityId;
            //            item.Email = record.Email;
            //            item.Address = record.Address;
            //            item.Name = record.Name;
            //            item.Phone = record.Phone;
            //            _context.tblUserRegistration.Update(item);
            //            _context.SaveChanges();
            //        }
            //    }
            //    return record;
            //}
            //else
            //{
            //    _context.tblUserRegistration.Add(record);
            //    _context.SaveChanges();
            //    return record;
            //}
            #endregion

            #region With Stored Procedure
            if (record.Id > 0)
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_UpdateUserRegistration @Id, @StateId, @CityId, @Email, @Address, @Name, @Phone",
                    new SqlParameter("@Id", record.Id),
                    new SqlParameter("@StateId", record.StateId),
                    new SqlParameter("@CityId", record.CityId),
                    new SqlParameter("@Email", record.Email),
                    new SqlParameter("@Address", record.Address),
                    new SqlParameter("@Name", record.Name),
                    new SqlParameter("@Phone", record.Phone));

                return result == 1 ? record : null;
            }
            else
            {
                var result = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_InsertUserRegistration @StateId, @CityId, @Email, @Address, @Name, @Phone",
                    new SqlParameter("@StateId", record.StateId),
                    new SqlParameter("@CityId", record.CityId),
                    new SqlParameter("@Email", record.Email),
                    new SqlParameter("@Address", record.Address),
                    new SqlParameter("@Name", record.Name),
                    new SqlParameter("@Phone", record.Phone));

                if (result > 0)
                {                  
                    record.Id = Convert.ToInt32(result);
                    return record;
                }
                else
                {
                    return null;
                }
            }
            #endregion
        }
        #endregion

        #region Get All City Based on State
        public async Task<IEnumerable<TblCity>> GetTblCityAsync(int stateId)
        {
            return await _context.tblCity.Where(x => x.StateId == stateId).ToListAsync();
        }
        #endregion

        #region Get All State
        public async Task<IEnumerable<TblState>> GetTblStateAsync()
        {
            return await _context.tblState.ToListAsync();
        }
        #endregion
        
        #region Get All Registerd Data 
        public async Task<IEnumerable<TblUserRegistration>> GetTblUserRegistrationsAsync()
        {
            return await _context.tblUserRegistration.ToListAsync();
        }
        #endregion
        
        #region Get Registered Data based on Id
        public async Task<IEnumerable<TblUserRegistration>> GetUserById(int userId)
        {
            return await _context.tblUserRegistration.Where(x => x.Id == userId).ToListAsync();
        }
        #endregion

        #region Delete Record With Stored Procedure
        public async Task<bool> UserDeletedAsync(int userId)
        { 
            //By using Stored Procedure
            var result = await _context.Database.ExecuteSqlRawAsync("EXEC sp_deleteDecision @p0", userId);
            //return result == 1;
            var data = await GetUserById(userId);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            //Without Stored Procedure
            //foreach (var item in data)
            //{
            //    _context.tblUserRegistration.Remove(item);
            //    _context.SaveChanges();
            //    return true;
            //}
            //return false;

        }

        #endregion 
    }
}
