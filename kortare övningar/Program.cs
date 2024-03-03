Console.WriteLine("Del 1: For-loop");

for (int i = 0; i < 10; i++)
    Console.WriteLine(i);

Console.WriteLine("\nDel 2: Foreach-loop");

string[] arr = ["A", "B", "C"];
foreach (string s in arr) Console.WriteLine(s);

Console.WriteLine("\nDel 3: FizzBuzz");

for (int j = 1; j < 16; j++)
{
    if (j % 15 == 0) Console.WriteLine("FizzBuzz");
    else if (j % 3 == 0) Console.WriteLine("Fizz");
    else if (j % 5 == 0) Console.WriteLine("Buzz");
    else Console.WriteLine(j);
}

Console.WriteLine("\nDel 4: Variabelbyte");

int x = 12;
int y = 31;

Console.WriteLine("x = {0}, y = {1}", x, y);
Console.WriteLine("Byter variablerna med varandra");

int temp;
temp = x;
x = y;
y = temp;

Console.WriteLine("x = {0}, y = {1}", x, y);
Console.WriteLine("Byter variablerna igen");

(x, y) = (y, x);

Console.WriteLine("x = {0}, y = {1}", x, y);
