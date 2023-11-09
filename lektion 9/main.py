import math

def kvadrat(tal):
    print(tal ** 2)

def linjer(antal):
    print("_" * antal)

def maximum(tal1, tal2):
    print(f"Största talet är: {max(tal1, tal2)}")

def nettopris(brutto):
    if brutto < 500:
        print(brutto)
    elif brutto >= 500 and brutto < 1000:
        print(brutto * 0.98)
    else:
        print(brutto * 0.95)

def calc(tal1, tal2, operator):
    if operator == "+":
        print(tal1 + tal2)
    elif operator == "-":
        print(tal1 - tal2)
    elif operator == "*":
        print(tal1 * tal2)
    elif operator == "/":
        print(tal1 / tal2)
    else:
        print("Invalid operator")
    
def cirkel(area):
    print(math.floor(math.sqrt(area / math.pi)))

def fibonacci_a():
    lista = [0, 1]
    for _ in range(2, 40):
        lista.append(lista[-1] + lista[-2])
    print(f"Fiobonaccis talföljd: {lista}")

def fibonacci_b():
    lista = [0, 1]
    for _ in range(2, 40):
        lista.append(lista[-1] + lista[-2])
        print(f"Kvot {lista[-1]} / {lista[-2]}: {lista[-1] / lista[-2]}")

def gyllenesnittet():
    phi = (1 + math.sqrt(5)) / 2
    print(f"Gyllene snittet (phi) är ungefär: {phi}")

def fibonacci_d():
    a, b = 0, 1
    for _ in range(40):
        print(a, end=" ")
        a, b = b, a + b

svar = int(input("Vilken uppgift? "))

match svar:
    case 1:
        kvadrat(int(input("Skriv ett tal för uppgift 1: ")))
    case 2:
        linjer(int(input("Skriv hur många linjer du vill ha: ")))
    case 3:
        maximum(int(input("Skriv in tal 1 att jämföra: ")), int(input("Skriv in tal 2 att jämföra: ")))
    case 4:
        nettopris(int(input("Skriv in nettopriset du vill räkna på: ")))
    case 5:
        tal1 = int(input("Skriv in tal 1: "))
        tal2 = int(input("Skriv in tal 2: "))
        op = input("Skriv in ditt räknesätt (+, -, *, /): ")
        calc(tal1, tal2, op)
    case 6:
        cirkel(int(input("Skriv in cirklens area: ")))
    case 7:
        fibonacci_a()
    case 8:
        fibonacci_b()
    case 9:
        gyllenesnittet()
    case 10:
        fibonacci_d()
