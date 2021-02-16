using SimpleTemplate_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface IAppDataRepository
    {
        AppSocialAddress Add(AppSocialAddress data);
        IQueryable<AppSocialAddress> AppSocialAddress { get; }
        AppSocialAddress GetInfo(int id);
        AppSocialAddress Update(AppSocialAddress data);
        Task<AppSocialAddress> Delete(int id);
        IEnumerable<AppSocialAddress> GetAllAppSocialAddress();
    }
}
