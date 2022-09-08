using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.Models;

namespace rapsoSuiteMaster.localServices
{
    public class crudServices : Icrud
    {
        public readonly rapsoServer_DBContext _context;

        public crudServices(rapsoServer_DBContext context)
        {
            _context = context;
        }

        public object setRegister<Tinput>(Tinput _newRegister) where Tinput : class
        {
            AspNetRole _roletmp = (AspNetRole)Convert.ChangeType(_newRegister, typeof(AspNetRole));
            IdentityRole _IdentityRole = new(_roletmp.Name);

            // *** ToDO: Pasar a Mapper *** //
            Models.AspNetRole _NetRole = new()
            {
                Id = _IdentityRole.Id,
                Name = _IdentityRole.Name,
                ConcurrencyStamp = _IdentityRole.ConcurrencyStamp,
                NormalizedName = _IdentityRole.Name.ToUpper()
            };

            return _NetRole;
        }

        public async Task<(string, int, Tinput?)> Create<Tinput>(Tinput _newRegister) where Tinput : class
        {
            try
            {
                _context.Add(_newRegister);
                var _saved = await _context.SaveChangesAsync();
                return ("ok", _saved, _newRegister);
            }
            catch (Exception ex)
            {
                return (ex.Message, -1, _newRegister);
            }
        }

        #region "Read"
        public async Task<(string, int, List<Tinput>?)> Readlist<Tinput>(DbSet<Tinput> _dbset) where Tinput : class
        {
            try
            {
                List<Tinput> read = await _dbset.ToListAsync();
                if (read == null)
                {
                    return ("ok", 0, null);
                }
                else
                {
                    return ("ok", read.Count(), read);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, -1, null);
            }
        }
   
        public async Task<(string, int, Tinput)> Read<Tinput>(DbSet<Tinput> _dbset, dynamic id) where Tinput : class
        {
            int _resultRead = 0;
            try
            {
                var read = await _dbset.FindAsync(id);

                if (read != null) _resultRead = 1;

                return ("ok", _resultRead, read);
            }
            catch (Exception ex)
            {
                return (ex.Message, -1, null);
            }
        }
        #endregion

        #region "Update"
        public async Task<taskResponse> Update(dynamic _newRegister) 
        {
            try
            {
                _context.Update(_newRegister);
                var _result = await _context.SaveChangesAsync();
                return (new taskResponse("Update", "Ok", "Update is made !"));
            }
            catch (Exception ex)
            {
                return (new taskResponse("Update", "Fail", "Error: " + ex.Message));
            }

        }
        #endregion

    }
}
