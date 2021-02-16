using SimpleTemplate_Shop.Models;

namespace SimpleTemplate_Shop.Repository.IRepository
{
    public interface IInfoRepository
    {
        Info Add(Info info);
        Info GetInfo(int id);
        Info Update(Info infoUpdate);
    }
}
