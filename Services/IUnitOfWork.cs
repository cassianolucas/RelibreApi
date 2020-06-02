namespace RelibreApi.Services
{
    public interface IUnitOfWork
    {
        void Commit();
         void RollBack();
    }
}