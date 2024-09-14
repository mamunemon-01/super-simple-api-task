namespace SuperSimpleMVC.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public Guid DeptId { get; set; }
    }
}
