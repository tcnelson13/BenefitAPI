using BenefitsCalculation.API.Entities;
using Microsoft.Data.Sqlite;

namespace BenefitsCalculation.API.Repository
{
	public class EmployeeRepository : IEmployeeRepository
    {
        private string GetConnectionString()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Join(path, "payroll.db");
            string connectionString = string.Format("Data Source={0};", dbPath);
            return connectionString;
        }

        public async Task<int> AddDependentAsync(int employeeId, Dependent dependent)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                INSERT INTO Dependent
                    (EmployeeId, Name, DependentTypeId, BenefitRateId)
                VALUES
                    ($EmployeeId, $Name, $DependentTypeId, $BenefitRateId);
                ";
                command.Parameters.AddWithValue("$EmployeeId", dependent.EmployeeId);
                command.Parameters.AddWithValue("$Name", dependent.Name);
                command.Parameters.AddWithValue("$DependentTypeId", (int)dependent.DependentTypeId);
                command.Parameters.AddWithValue("$BenefitRateId", (int)dependent.BenefitRateId);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return rowsAffected;          
        }

        public void DeleteDependentAsync(int dependentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Dependent>> GetDependentsAsync(int employeeId)
        {
            List<Dependent> dependents = new List<Dependent>();
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT DependentId, EmployeeId, Name, DependentTypeId
                FROM Dependent
                WHERE EmployeeId = $EmployeeId;
                ";
                command.Parameters.AddWithValue("$EmployeeId", employeeId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var dependent = new Dependent();
                        dependent.DependentId = (long)reader["DependentId"];
                        dependent.EmployeeId = (long)reader["EmployeeId"];
                        dependent.Name = reader["Name"].ToString();
                        dependent.DependentTypeId = (long)reader["DependentTypeId"];
                        dependents.Add(dependent);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception
                // ILogger
            }
            return dependents;
        }

        public async Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            Employee employee = new Employee();
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT EmployeeId, Name, AnnualPaycheckCount, AnnualSalary, PayPeriodBenefitCost, BenefitRateId
                FROM Employee
                WHERE EmployeeId = $employeeId;
                ";
                command.Parameters.AddWithValue("$employeeId", employeeId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employee.EmployeeId = (long)reader["EmployeeId"];
                        employee.Name = reader["Name"].ToString();
                        employee.AnnualPaycheckCount = (long)reader["AnnualPaycheckCount"];
                        employee.AnnualSalary = (long)reader["AnnualSalary"];
                        employee.PayPeriodBenefitCost = (double)reader["PayPeriodBenefitCost"];
                        employee.BenefitRateId = (long)reader["BenefitRateId"];
                    }
                }

                employee.Dependents = await GetDependentsAsync(employeeId);
            }
            catch(Exception ex)
            {
                // Log exception
            }
            return employee.EmployeeId > 0 ? employee : null;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT EmployeeId, Name, AnnualPaycheckCount, AnnualSalary, PayPeriodBenefitCost, BenefitRateId
                FROM Employee;
                ";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeId = (long)reader["EmployeeId"];
                        employee.Name = reader["Name"].ToString();
                        employee.AnnualPaycheckCount = (long)reader["AnnualPaycheckCount"];
                        employee.AnnualSalary = (long)reader["AnnualSalary"];
                        employee.PayPeriodBenefitCost = (double)reader["PayPeriodBenefitCost"];
                        employee.BenefitRateId = (long)reader["BenefitRateId"];
                        employees.Add(employee);
                    }
                }

                foreach (var employee in employees)
                {
                    employee.Dependents = await GetDependentsAsync((int)employee.EmployeeId);
                }
            }
            catch (Exception ex)
            {
                // Log exception in APM software
            }

            return employees;
        }

        public decimal PreviewBenefitCosts(int employeeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDependentAsync(Dependent dependent)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateBenefitCost(int employeeId, double payPeriodBenefitCost)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                UPDATE Employee SET
                    PayPeriodBenefitCost = $PayPeriodBenefitCost
                WHERE EmployeeId = $EmployeeId;
                ";
                command.Parameters.AddWithValue("$EmployeeId", employeeId);
                command.Parameters.AddWithValue("$PayPeriodBenefitCost", payPeriodBenefitCost);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                // Log exception
            }
            return rowsAffected;
        }
    }
}

