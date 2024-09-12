using SuperSimpleAPITask.Repositories.Interfaces;

namespace SuperSimpleAPITask.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmployeeRepository Employee { get; }
        public IDepartmentRepository Department { get; }
        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
            Employee = new EmployeeRepository(_context);
            Department = new DepartmentRepository(_context);
        }

        public async void SaveAsync()
        {
            _context.SaveChanges();
        }
    }
}
