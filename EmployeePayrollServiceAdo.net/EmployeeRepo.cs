using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollServiceAdo.net
{
    public class EmployeeRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = payroll_service; Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);

        public void checkConnection()
        {
            try
            {
                this.connection.Open();
                Console.WriteLine("connection established");
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void getAllEmployee()
        {
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            employeeModel.EmployeeID = sqlDataReader.GetInt32(0);
                            employeeModel.EmployeeName = sqlDataReader.GetString(1);
                            employeeModel.BasicPay = sqlDataReader.GetDecimal(2);
                            employeeModel.StartDate = sqlDataReader.GetDateTime(3);
                            employeeModel.gender = Convert.ToChar(sqlDataReader.GetString(4));
                            employeeModel.PhoneNumber = sqlDataReader.GetString(5);
                            employeeModel.Department = sqlDataReader.GetString(6);
                            employeeModel.Address = sqlDataReader.GetString(7);
                            employeeModel.Deductions = sqlDataReader.GetDouble(8);
                            employeeModel.TaxablePay = Convert.ToDouble(sqlDataReader.GetFloat(9));
                            employeeModel.Tax = sqlDataReader.GetDouble(10);
                            employeeModel.NetPay = Convert.ToDouble(sqlDataReader.GetFloat(11));
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", employeeModel.EmployeeID, employeeModel.EmployeeName, employeeModel.BasicPay, employeeModel.StartDate, employeeModel.gender, employeeModel.PhoneNumber);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public bool addEmployee(EmployeeModel employeeModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddEmployeeDetails", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeModel.EmployeeID);
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeModel.EmployeeName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", employeeModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", employeeModel.Address);
                    cmd.Parameters.AddWithValue("@Department", employeeModel.Department);
                    cmd.Parameters.AddWithValue("@gender", employeeModel.gender);
                    cmd.Parameters.AddWithValue("@BasicPay", employeeModel.BasicPay);
                    cmd.Parameters.AddWithValue("@Deductions", employeeModel.Deductions);
                    cmd.Parameters.AddWithValue("@TaxablePay", employeeModel.TaxablePay);
                    cmd.Parameters.AddWithValue("@Tax", employeeModel.Tax);
                    cmd.Parameters.AddWithValue("@NetPay", employeeModel.NetPay);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}

