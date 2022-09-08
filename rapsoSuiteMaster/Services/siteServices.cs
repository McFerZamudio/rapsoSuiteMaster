using Microsoft.EntityFrameworkCore;
using rapsoSuiteMaster.localServices;
using rapsoSuiteMaster.Models;
using System.Security.Claims;

namespace rapsoSuiteMaster.Services
{
    public class siteServices : crudServices
    {

        public readonly tbl_site site;
        private readonly ClaimsPrincipal user;

        public siteServices(rapsoServer_DBContext rapsoServer_DBContext, string idUser, ClaimsPrincipal user) : base(rapsoServer_DBContext)
        {
            this.user = user;
            site = getSite(idUser).Result;
        }

        public siteServices(rapsoServer_DBContext rapsoServer_DBContext, tbl_site site) : base(rapsoServer_DBContext)
        {
            
            site = updateSite(site).Result;
        }


        private async Task<tbl_site> getSite(string idUser)
        {
            tbl_site _getSite = await _context.tbl_sites.FirstOrDefaultAsync(x => x.Id_netUser.Equals(idUser));

            if (_getSite == null)
            {
                _getSite = new()
                {
                    site_name = "Site of " + user.Identity.Name,
                    site_description = "Basic Descripcion Of " + user.Identity.Name,
                    site_RapsoSuitesFamily = false,
                    site_nameDeveloper = user.Identity.Name,
                    site_status = false,
                    Id_netUser = idUser
                };

                await Create(_getSite);
            }

            return _getSite;
        }

        private async Task<tbl_site> updateSite(tbl_site _tbl_site)
        {
            await Update(_tbl_site);

            return _tbl_site;
        }
    }
}
