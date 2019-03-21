using System;
using System.Collections.Generic;

namespace DesignPatterns.Examples.Chain_Of_Responsibility
{
    public class Example1
    {
        public static void Demo()
        {
            List<Employee> managers = new List<Employee>
            {
                new Employee("William Worker", Decimal.Zero),
                new Employee("Mary Manager", 1000),
                new Employee("Victor Vicepres", 5000),
                new Employee("William Worker", 20000)
            };

            while (ConsoleInput.TryReadDecimal("Expense report amount:", out var expenseReportAmount))
            {
                var expense = new ExpenseReport(expenseReportAmount);
                bool expenseProcessed = false;
                foreach (var approver in managers)
                {
                    var response = approver.ApproveExpense(expense);
                    if (response != ApprovalResponse.BeyondApprovalLimit)
                    {
                        Console.WriteLine("The request was {0}.", response);
                        expenseProcessed = true;
                        break;
                    }
                }

                if (!expenseProcessed)
                {
                    Console.WriteLine("No one was able to approve your expense.");
                }
            }
        }
    }

    public class ConsoleInput
    {
        public static bool TryReadDecimal(string message, out decimal expenseReportAmount)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return decimal.TryParse(input, out expenseReportAmount);
        }
    }

    public interface IExpenseReport
    {
        Decimal Total { get; }
    }

    public interface IExpenseApprover
    {
        ApprovalResponse ApproveExpense(IExpenseReport expenseReport);
    }

    public enum ApprovalResponse
    {
        Denied,
        Approved,
        BeyondApprovalLimit
    }

    public class ExpenseReport : IExpenseReport
    {
        public ExpenseReport(Decimal total)
        {
           Total = total;
        }

        public decimal Total { get; private set; }
    }

    public class Employee : IExpenseApprover
    {
        public Employee(string name, Decimal approvalLimit)
        {
            Name = name;
            _approvalLimit = approvalLimit;
        }

        private readonly Decimal _approvalLimit;

        public string Name { get; private set; }

        public ApprovalResponse ApproveExpense(IExpenseReport expenseReport)
        {
            return expenseReport.Total > _approvalLimit
                ? ApprovalResponse.BeyondApprovalLimit
                : ApprovalResponse.Approved;
        }
    }
}
