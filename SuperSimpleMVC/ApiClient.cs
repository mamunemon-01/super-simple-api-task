using SuperSimpleMVC.Models;

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

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("Employee");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("Employee", employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task<Employee> UpdateEmployeeAsync(string id, Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"Employee/{id}", employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"Employee/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
