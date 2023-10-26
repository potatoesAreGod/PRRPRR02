print("Uppgift 1 **********")
antal = int(input("Hur många tal vill du mata in? "))
lista = []
for i in range(antal):
    lista.append(input("Skriv in ditt tal: "))

summa = 0

for i in range(antal):
    summa = summa + int(lista[i - 1])

medel = summa / antal

print(f"Summan av talen är: {summa}")
print(f"Medelvärdet är: {medel}")

print("Uppgift 2 **********")

svar = 0
while svar < 1 or svar > 12:
    svar = int(input("Skriv in månadsnummer: "))

arr = ["Januari", "Febuari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "Septemper", "Oktober", "November", "December"]

print(arr[svar - 1])
