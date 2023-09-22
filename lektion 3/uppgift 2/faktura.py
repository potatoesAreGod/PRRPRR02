ålder = int(input("Hur gammal är du: "))
lön = int(input("Vad är din årsinkomst: "))
anmärkning = input("Har du betalningsanmärkningar: ")

if ålder >= 18 and lön >= 120000 and anmärkning == "nej":
    print("Fakturabetalning beviljas")
else:
    print("Tyvärr, du måste betala ditt köp med bankkort")
