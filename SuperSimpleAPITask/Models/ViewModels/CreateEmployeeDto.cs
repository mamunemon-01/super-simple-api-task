namespace SuperSimpleAPITask.Models.ViewModels{
    public class CreateEmployeeDto{
        public Guid? Id {get;set;} = Guid.Empty;
        public string? Name {get;set;}
        public string? PhoneNo {get;set;}
        public string? DeptName {get;set;}
    }
}