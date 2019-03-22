using System;

namespace DesignPatterns.Examples.Chain_Of_Responsibility
{
    public class Example1
    {
        public static void Demo()
        {
            var william = new ExpenseHandler(new Employee("William Worker", Decimal.Zero));
            var mary = new ExpenseHandler(new Employee("Mary Manager", 1000));
            var victor = new ExpenseHandler(new Employee("Victor Vicepres", 5000));
            var paula = new ExpenseHandler(new Employee("William Worker", 20000));

            william
                .RegisterNext(mary)
                .RegisterNext(victor)
                .RegisterNext(paula);

            if (ConsoleInput.TryReadDecimal("Expense report amount:", out var expenseReportAmount))
            {
                IExpenseReport expense = new ExpenseReport(expenseReportAmount);
                ApprovalResponse response = william.Approve(expense);
                Console.WriteLine("The request was {0}", response);
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

    public interface IExpenseHandler
    {
        ApprovalResponse Approve(IExpenseReport expenseReport);
        IExpenseHandler RegisterNext(IExpenseHandler next);
    }

    public class ExpenseHandler : IExpenseHandler
    {
        private readonly IExpenseApprover _approver;
        private IExpenseHandler _next;

        public ExpenseHandler(IExpenseApprover expenseApprover)
        {
            _approver = expenseApprover;
            _next = EndOfChainExpenseHandler.Instance;
        }

        public ApprovalResponse Approve(IExpenseReport expenseReport)
        {
            var response = _approver.ApproveExpense(expenseReport);

            if (response == ApprovalResponse.BeyondApprovalLimit)
            {
                return _next.Approve(expenseReport);
            }

            return response;
        }

        public IExpenseHandler RegisterNext(IExpenseHandler next)
        {
            _next = next;
            return _next;
        }

        public class EndOfChainExpenseHandler : IExpenseHandler
        {
            private EndOfChainExpenseHandler() { }

            public static EndOfChainExpenseHandler Instance => _instance;
            public ApprovalResponse Approve(IExpenseReport expenseReport)
            {
                return ApprovalResponse.Denied;
            }

            public IExpenseHandler RegisterNext(IExpenseHandler next)
            {
                throw new NotImplementedException("It's the end of the chain!");
            }

            private static readonly EndOfChainExpenseHandler _instance = new EndOfChainExpenseHandler();
        }
    }
}
