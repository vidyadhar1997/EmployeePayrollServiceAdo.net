using System;

namespace EmployeePayrollServiceAdo.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcomr to the Employee payroll service ado .net program");
            EmployeeRepo employeeRepo = new EmployeeRepo();
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeID = 1;
            employeeModel.EmployeeName = "dhiraj";
            employeeModel.BasicPay = 22000;
            employeeModel.gender = 'M';
            employeeModel.PhoneNumber = "8149713160";
            employeeModel.Department = "Sale";
            employeeModel.Address = "Tawarja";
            employeeModel.Deductions = 1500;
            employeeModel.TaxablePay = 200;
            employeeModel.Tax = 300;
            employeeModel.NetPay = 2500;
            employeeRepo.checkConnection();
            employeeRepo.getAllEmployee();
            employeeRepo.addEmployee(employeeModel);
            employeeRepo.getParticularEmployee();
            employeeRepo.getParticularEmployeeBetweenDate();
        }
    }
}
