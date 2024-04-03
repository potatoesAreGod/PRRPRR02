abstract class Employee(string name)
{
    string Name = name;

    public abstract double CalculateSalary();
}

class Salesman(string name, double sales, double provision) : Employee(name)
{
    double Sales = sales;
    double Provision = provision;

    public override double CalculateSalary()
    {
        return Sales * Provision / 100;
    }
}

class Consultant(string name, double workedHours, double hourlyWage) : Employee(name)
{
    double WorkedHours = workedHours;
    double HourlyWage = hourlyWage;

    public override double CalculateSalary()
    {
        return WorkedHours * HourlyWage;
    }
}

class Clerk(string name, double monthlySalary) : Employee(name)
{
    double MonthlySalary = monthlySalary;

    public override double CalculateSalary()
    {
        return MonthlySalary;
    }
}

class EmployeeRegister
{
    private readonly List<Employee> employees;
    public EmployeeRegister() { employees = []; }
    private readonly string[] ValidEmployeeTitles = ["salesman", "consultant", "clerk"];

    public void RegisterPay()
    {
        Console.Clear();
        string type = "";

        while (true)
        {
            Console.Write("What type of employee do you want to add? (salesman, consultant, clerk): ");
            type = Console.ReadLine().Trim().ToLower();
            if (Array.Exists(ValidEmployeeTitles, e => e == type))
                break;
            else
                Console.WriteLine("Please enter a valid role.");
        }

        // Ask for name
        Console.Write("Enter employee name: ");
        string name = Console.ReadLine().Trim();

        // Disallow whitespace and null
        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid name");
            Console.Write("Enter employee name: ");
            name = Console.ReadLine().Trim();
        }

        if (type == "salesman")
        {
            Console.Write("Enter provision for {0} ({1}): ", name, type);
            double provision;

            // Make sure user enters a valid provision
            while (!double.TryParse(Console.ReadLine()?.Trim(), out provision) || provision <= 0)
            {
                Console.WriteLine("Please enter a valid provision");
                Console.Write("Enter provision: ");
            }

            Console.Write("Enter amount of sales for {0} ({1}): ", name, type);
            double sales;

            // Make sure user enters a valid sales number
            while (!double.TryParse(Console.ReadLine()?.Trim(), out sales) || sales <= 0)
            {
                Console.WriteLine("Please enter a valid sales value");
                Console.Write("Enter sales: ");
            }
            Salesman newSalesman = new(name, provision, sales);
        }
        else if (type == "consultant")
        {
            Console.Write("Enter amount of hours worked for {0} ({1}): ", name, type);
            double hoursWorked;

            // Make sure user enters a valid number of hours worked
            while (!double.TryParse(Console.ReadLine()?.Trim(), out hoursWorked) || hoursWorked <= 0)
            {
                Console.WriteLine("Please enter a valid amount of hours worked");
                Console.Write("Enter hours worked: ");
            }

            Console.Write("Enter hourly wage for {0} ({1}): ", name, type);
            double hourlyWage;

            // Make sure user enters a valid hourly wage
            while (!double.TryParse(Console.ReadLine()?.Trim(), out hourlyWage) || hourlyWage <= 0)
            {
                Console.WriteLine("Please enter a valid hourly wage");
                Console.Write("Enter hourly wage: ");
            }
            Consultant newConsultant = new(name, hoursWorked, hourlyWage);
        }
        else if (type == "clerk")
        {
            Console.Write("Enter monthly salary for {0} ({1}): ", name, type);
            double monthlySalary;

            // Make sure user enters a valid monthly salary
            while (!double.TryParse(Console.ReadLine()?.Trim(), out monthlySalary) || monthlySalary <= 0)
            {
                Console.WriteLine("Please enter a valid monthly salary");
                Console.Write("Enter monthly salary: ");
            }
            Clerk newClerk = new(name, monthlySalary);
        }
    }

    public void ViewEmployees()
    {
        Console.WriteLine("Salesmen:");

        foreach (var salesman in salesmen)
        {
            Console.WriteLine("{0}", salesman);
        }

    }

    public void ViewPayouts()
    {

    }
}

class Program
{
    static void Main()
    {
        // Create a class to store employees in
        EmployeeRegister employeeRegister = new();

        while (true)
        {
            // Display a menu
            Console.Clear();
            Console.WriteLine("1. Register pay");
            Console.WriteLine("2. View employees");
            Console.WriteLine("3. View payouts");
            Console.WriteLine("4. Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    employeeRegister.RegisterPay();
                    break;
                case "2":
                    employeeRegister.ViewEmployees();
                    break;
                case "3":
                    employeeRegister.ViewPayouts();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again");
                    break;
            }
        }
    }
}