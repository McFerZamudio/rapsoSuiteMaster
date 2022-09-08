using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.localServices;
using rapsoSuiteMaster.Models;

namespace rapsoSuiteMaster.Services
{
    public class rolesServices : crudServices
    {


        public rolesServices(rapsoServer_DBContext rapsoServer_DBContext) : base(rapsoServer_DBContext)
        {

        }

        public async Task<(string, int, List<AspNetRole>?)> Readlist_orderRole()
        {
            var tmp = await Readlist(_context.AspNetRoles);

            var read = tmp.Item3.OrderBy(x => x.Name).ToList();
            return ("ok", tmp.Item3.Count(), read);
        }



    }
}