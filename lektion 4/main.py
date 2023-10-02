if int(input("Hur gammal är du? ")) < 18:
    if input("Vill du ta körkort? ") == "j":
        if input("Har du börjat övningsköra? ") == "j":
            print("Lycka till")
        else:
            print("Tack för din medverkan i denna enkät")
    else:
        print("Tack för din medverkan i denna enkät")
else:
    if input("Har du tagit körkort? ") == "j":
        if input("Har du egen bil? ") == "j":
            bil = input("Vilken modell? ")
            print(f"Du har fyllt 18 och du har tagit körkort och du kör en egen bil av modellen {bil}")
        else:
            lån = input("Vems bil lånar du? ")
            print(f"Du har fyllt 18 och du har tagit körkort och du lånar en bil som tillhör {lån}")
    else:
        print("Tack för din medverkan i denna enkät")
 