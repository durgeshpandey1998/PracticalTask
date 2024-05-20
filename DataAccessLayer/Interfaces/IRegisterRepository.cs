using DataAccessLayer.DataBaseTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRegisterRepository
    {
        Task<IEnumerable<TblUserRegistration>> GetTblUserRegistrationsAsync();
        Task<TblUserRegistration> AddTblUserRegisterationAsync(TblUserRegistration record);
        Task<IEnumerable<TblState>> GetTblStateAsync();
        Task<IEnumerable<TblCity>> GetTblCityAsync(int stateId);
        Task<IEnumerable<TblUserRegistration>> GetUserById(int userId);
        Task<bool> UserDeletedAsync(int userId);
    }
}
