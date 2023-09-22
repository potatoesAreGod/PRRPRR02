print("Skriv in dina prov!")

prov1 = float(input("Prov 1: "))
prov2 = float(input("Prov 2: "))
prov3 = float(input("Prov 3: "))

medel = (prov1 + prov2 + prov3) / 3

print(f"Ditt medelbetyg är {medel} poäng")

if medel >= 90:
    print("Bra jobbat!")
