## Prerequisites

- Create a payroll.db sqlite database file. These snippets will create some tables and seed data
  From command prompt run: sqlite3
  -- Create payroll database
  CREATE DATABASE payroll;

        -- Create Employee Table
        CREATE TABLE Employee
        (
          EmployeeId INTEGER PRIMARY KEY,
          Name TEXT NOT NULL,
          AnnualSalary INTEGER NOT NULL,
          AnnualPaycheckCount INTEGER NOT NULL,
          PayPeriodBenefitCost DECIMAL(10,2) NOT NULL,
          BenefitRateId INTEGER NOT NULL
        );

        -- Populate some test data into Employee table
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Thomas Nelson', 52000, 26, 38.46, 1);
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Joseph Anderson', 52000, 26, 38.46, 1);
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Bill Kidd', 52000, 26, 38.46, 1);
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Samuel Andrews', 52000, 26, 38.46, 1);
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Denise Smith', 52000, 26, 38.46, 1);
        INSERT INTO Employee(Name, AnnualSalary, AnnualPaycheckCount, PayPeriodBenefitCost, BenefitRateId) VALUES('Davey Jones', 52000, 26, 38.46, 1);

        SELECT * FROM Employee;

        -- Create Dependent Table
        CREATE TABLE Dependent
        (
          DependentId  INTEGER PRIMARY KEY,
          EmployeeId INTEGER NOT NULL,
          Name TEXT NOT NULL,
          DependentTypeId INTEGER NOT NULL,
          BenefitRateId INTEGER NOT NULL,
          FOREIGN KEY (EmployeeId) REFERENCES Employee (EmployeeId) ON DELETE CASCADE ON UPDATE NO ACTION
        );

        INSERT INTO Dependent(EmployeeId, Name, DependentTypeId, BenefitRateId) VALUES(1, 'Denise Nelson', 1, 2);
        INSERT INTO Dependent(EmployeeId, Name, DependentTypeId, BenefitRateId) VALUES(1, 'William Nelson', 2, 2);
        INSERT INTO Dependent(EmployeeId, Name, DependentTypeId, BenefitRateId) VALUES(1, 'Annabelle Nelson', 2, 2);


        -- Create BenefitType Table
        CREATE TABLE BenefitRate
        (
          BenefitRateId  INTEGER PRIMARY KEY,
          Name TEXT NOT NULL,
          AnnualBenefitCost DECIMAL(10,2) NOT NULL,
          FOREIGN KEY (BenefitRateId) REFERENCES Employee (BenefitRateId) ON DELETE CASCADE ON UPDATE NO ACTION
          FOREIGN KEY (BenefitRateId) REFERENCES Dependent (BenefitRateId) ON DELETE CASCADE ON UPDATE NO ACTION
        );

        INSERT INTO BenefitRate(Name, AnnualBenefitCost) VALUES('Employee', 1000.00);
        INSERT INTO BenefitRate(Name, AnnualBenefitCost) VALUES('Dependent', 500.00);

##

I am running the database on a Macbook at Users/[username]/.local/share

## Endpoints and Functionality

### Swagger viewable at: https://localhost:7096/swagger/index.html

## Employees Endpoint

1. GET - Get Employees - Returns an array of EmployeeModels as JSON for all Employee records in the database
   https://localhost:7096/api/employees
2. GET - Get Employee By Id
   https://localhost:7096/api/employees/1
3. POST - Add a Dependent
   https://localhost:7096/api/employees/1
   Takes a JSON payload like:
   ```json
   {
     "dependentId": 0,
     "employeeId": 1,
     "name": "Jack Johnson",
     "dependentTypeId": 2,
     "benefitRateId": 2
   }
   ```
4. PUT - Updates a dependent [NOT IMPLEMENTED]
   https://localhost:7096/api/employees/1/2
5. DELETE - Deletes a dependent [NOT IMPLEMENTED]
   https://localhost:7096/api/employees/1/2

## PayrollAdmin Endpoint

1. GET - Returns current benefit rates https://localhost:7096/api/admin
2. PUT - Updates a benefit rate with the following JSON payload https://localhost:7096/api/admin/123/benefitrateid/1
   ```json
   {
     "benefitRateId": 1,
     "name": "Employee",
     "annualBenefitCost": 1250
   }
   ```
3. GET - Allows user to preview payroll of the employees in the database based on the current benefit rates https://localhost:7096/api/admin/123/
4. POST - Processes payroll applying the most recent benefit rates in the system to the employee record - P https://localhost:7096/api/admin/123/

## Unit Tests

1. A couple of unit tests to test some calculations
