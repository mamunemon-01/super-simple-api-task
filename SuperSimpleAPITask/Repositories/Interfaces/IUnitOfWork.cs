namespace SuperSimpleAPITask.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        IDepartmentRepository Department { get; }
        void SaveAsync();
    }
}
