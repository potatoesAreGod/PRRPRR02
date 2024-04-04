abstract class Employee(string name)
{
    public string Name { get; set; } = name;

    // This will never be called and can therefore be abstract
    public abstract double CalculateSalary();
}

class Salesman(string name, double sales, double provision) : Employee(name)
{
    public double Sales { get; set; } = sales;
    public double Provision { get; set; } = provision;

    public override double CalculateSalary()
    {
        return Sales * Provision / 100;
    }
}

class Consultant(string name, double workedHours, double hourlyWage) : Employee(name)
{
    public double WorkedHours { get; set; } = workedHours;
    public double HourlyWage { get; set; } = hourlyWage;

    public override double CalculateSalary()
    {
        return WorkedHours * HourlyWage;
    }
}

class Clerk(string name, double monthlySalary) : Employee(name)
{
    public double MonthlySalary { get; set; } = monthlySalary;

    public override double CalculateSalary()
    {
        return MonthlySalary;
    }
}

class EmployeeManager
{
    // List to store all employees in
    private readonly List<Employee> employees = [];

    public void RegisterEmployee()
    {
        Console.Clear();

        // Ask user for employee name
        Console.WriteLine("Enter employee name:");
        string? name = Console.ReadLine()?.Trim();

        // Disallow empty names
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid name for employee. Please try again");
            Console.WriteLine("Press any key to return to menu");
            Console.ReadKey();
            return;
        }

        // Ask for what type of employee the user wants to add
        Console.WriteLine("Select employee role:");
        Console.WriteLine("1. Salesman");
        Console.WriteLine("2. Consultant");
        Console.WriteLine("3. Clerk");
        Console.Write("> ");

        // Different scenario for each role as they have different salary calculations
        switch (Console.ReadLine())
        {
            case "1":
                double sales;
                double provision;
                // Ask for amount of sales and provision
                if (ParseDouble("Enter sales: ", out sales) && ParseDouble("Enter provision percentage: ", out provision))
                {
                    // Add salesman to the list
                    employees.Add(new Salesman(name, sales, provision));
                    Console.WriteLine("Salesman registered successfully.");
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                }
                break;
            case "2":
                double workedHours;
                double hourlyWage;
                if (ParseDouble("Enter worked hours: ", out workedHours) && ParseDouble("Enter hourly wage: ", out hourlyWage))
                {
                    employees.Add(new Consultant(name, workedHours, hourlyWage));
                    Console.WriteLine("Consultant registered successfully.");
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                }
                break;
            case "3":
                double monthlySalary;
                if (ParseDouble("Enter monthly salary: ", out monthlySalary))
                {
                    employees.Add(new Clerk(name, monthlySalary));
                    Console.WriteLine("Clerk registered successfully.");
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                }
                break;
            default:
                Console.WriteLine("Invalid employee type. Please try again.");
                Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
                break;
        }
    }

    public void ViewAllEmployees()
    {
        Console.Clear();
        Console.WriteLine("All Employees:");

        // Loop through all employees
        foreach (var employee in employees)
        {
            // Determine type of employee
            string employeeType = employee switch
            {
                Salesman => "Salesman",
                Consultant => "Consultant",
                Clerk => "Clerk",
                _ => "Unknown Type" // Default case for unknown types
            };

            // Display employee name and role
            Console.WriteLine("Name: {0}, Role: {1}", employee.Name, employeeType);
        }
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
    }

    public void ViewPayouts()
    {
        Console.Clear();
        Console.WriteLine("Payouts:");

        double totalPay = 0;

        // Loop through all employees
        foreach (var employee in employees)
        {
            // Determine type of employee
            string employeeType = employee switch
            {
                Salesman => "Salesman",
                Consultant => "Consultant",
                Clerk => "Clerk",
                _ => "Unknown Type" // Default case for unknown types
            };

            // Display employee name, role and pay
            Console.WriteLine("Name: {0} ({1}), Salary: {2}", employee.Name, employeeType, employee.CalculateSalary());
            totalPay += employee.CalculateSalary();
        }
        // Write out total pay for all employees
        Console.WriteLine("Total pay: {0}", totalPay);

        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
    }

    // Function for checking numbers entered by the user
    private static bool ParseDouble(string message, out double result)
    {
        // Ask the user
        Console.WriteLine(message);
        // If user input is convertable, return true along with the parsed result
        return double.TryParse(Console.ReadLine(), out result);
    }
}

class Program
{
    static void Main()
    {
        // Create an instance to manage employees with
        EmployeeManager employeeManager = new();

        while (true)
        {
            // Display a menu
            Console.Clear();
            Console.WriteLine("1. Register Employee");
            Console.WriteLine("2. View All Employees");
            Console.WriteLine("3. View Payouts");
            Console.WriteLine("4. Exit");
            Console.Write("> ");

            switch (Console.ReadLine())
            {
                case "1":
                    employeeManager.RegisterEmployee();
                    break;
                case "2":
                    employeeManager.ViewAllEmployees();
                    break;
                case "3":
                    employeeManager.ViewPayouts();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
    }
}
