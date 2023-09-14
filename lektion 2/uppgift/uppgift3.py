ordPris = float(input("Skriv in ordinarie pris(kr): "))
rabatt = float(input("Skriv in rabatt(%): "))

pris = ordPris - ordPris * rabatt / 100

print(f"""
*-----------------------------*
Extrapriset blir: {pris} kr
*-----------------------------*
""")
