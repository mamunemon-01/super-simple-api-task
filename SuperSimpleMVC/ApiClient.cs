using SuperSimpleMVC.Models.ViewModels;

namespace SuperSimpleMVC
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiSettings:BaseApiUrl"]);
        }

        public async Task<IEnumerable<ReadEmployeeDto>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("Employee");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReadEmployeeDto>>();
        }

        public async Task<CreateEmployeeDto> AddEmployeeAsync(CreateEmployeeDto employee)
        {
            var response = await _httpClient.PostAsJsonAsync("Employee", employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CreateEmployeeDto>();
        }

        public async Task<CreateEmployeeDto> UpdateEmployeeAsync(string id, CreateEmployeeDto employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"Employee/{id}", employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CreateEmployeeDto>();
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"Employee/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<ReadEmployeeDto>> SearchByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"Employee/search/name/{name}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReadEmployeeDto>>();
        }

        public async Task<IEnumerable<ReadEmployeeDto>> SearchByPhoneNoAsync(string phoneNo)
        {
            var response = await _httpClient.GetAsync($"Employee/search/phoneNo/{phoneNo}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReadEmployeeDto>>();
        }

        public async Task<IEnumerable<ReadEmployeeDto>> SearchByDeptNameAsync(string deptName)
        {
            var response = await _httpClient.GetAsync($"Employee/search/department/{deptName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ReadEmployeeDto>>();
        }
    }
}
