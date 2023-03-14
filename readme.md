# Paylocity Coding Challenge - Thomas Nelson | https://github.com/tcnelson13 | tcnelson13@gmail.com

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

