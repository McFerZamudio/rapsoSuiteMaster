using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.Models;
using rapsoSuiteMaster.Services;

namespace rapsoSuiteMaster.interfaces
{
    public interface  Icrud 
    {
        // *** Inicializacion ***

        object setRegister<Tinput>(Tinput _newRegister) where Tinput : class;

        // *** Create *** //
        Task<(string, int, Tinput?)> Create<Tinput>(Tinput _newRegister) where Tinput : class;

        // *** Read *** //

        Task<(string, int, List<Tinput>?)> Readlist<Tinput>(DbSet<Tinput> _dbset) where Tinput : class;

        Task<(string, int, Tinput?)> Read<Tinput>(DbSet<Tinput> _dbset, dynamic id) where Tinput : class;

        Task<taskResponse> Update(dynamic _newRegister);


        //  Task<(string, int, Tinput?)> Read<Tinput>(DbSet<Tinput> _dbset, Guid id) where Tinput : class;




    }
}
