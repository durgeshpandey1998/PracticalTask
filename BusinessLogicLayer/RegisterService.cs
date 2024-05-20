using DataAccessLayer.DataBaseTable;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RegisterService : IRegisterUserService
    {
        private readonly IRegisterRepository _context;
        public RegisterService(IRegisterRepository context)
        {
            _context = context;
        }
        public async Task<TblUserRegistration> AddTblUserRegisterationAsync(TblUserRegistration record)
        {
            return await _context.AddTblUserRegisterationAsync(record);
        }

        public async Task<IEnumerable<TblCity>> GetTblCityAsync(int stateId)
        {
            return await _context.GetTblCityAsync(stateId);
        }

        public async Task<IEnumerable<TblState>> GetTblStateAsync()
        {
            return await _context.GetTblStateAsync();
        }

        public async Task<IEnumerable<TblUserRegistration>> GetTblUserRegistrationsAsync()
        {
            return await _context.GetTblUserRegistrationsAsync();
        }

        public async Task<IEnumerable<TblUserRegistration>> GetUserById(int userId)
        {
            return await _context.GetUserById(userId);
        }

        public async Task<bool> UserDeleteAsync(int userId)
        {
            return await _context.UserDeletedAsync(userId);
        }
    }
}
