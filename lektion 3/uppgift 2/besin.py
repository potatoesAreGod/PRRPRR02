pris = int(input("Vad är dagens bensinpris: "))

if pris < 10:
    print("Det var billigt")
elif pris >= 10 and pris < 15:
    print("Tanka full tank")
elif pris >= 15 and pris < 20:
    print("Tanka 10 liter")
else:
    print("Nu börjar jag cykla istället")
