import random

uppg = int(input("Vilken uppgift? "))

def lotto():
    for _ in range(7):
        print(random.randint(1, 35))

def stryktips():
    rad = random.choices(population=['1', 'X', '2'], weights=[.5, .25, .25], k=13)

    print("Stryktipsrad:")
    for r in rad:
        if r == "1":
            print(r)
        elif r == "X":
            print(f"\t{r}")
        elif r == "2":
            print(f"\t\t{r}")
    print()

def slump_1000():
    tal = [random.randint(1, 1000) for _ in range(100)]
    print(f"Största tal: {max(tal)}")
    print(f"Minsta tal: {min(tal)}")
    print(f"Medelvärde: {sum(tal) / len(tal)}")

def monthy_hall():
    stay_wins, switch_wins = 0, 0

    for _ in range(1000):
        bil = random.randint(1, 3)
        deltagare = random.randint(1, 3)

        dörrar = [door for door in range(1, 4) if door != deltagare and door != bil]
        host = random.choice(dörrar)
        switch_val = [door for door in range(1, 4) if door != deltagare and door != host][0]

        if switch_val == bil:
            switch_wins += 1
        if stay_wins == bil:
            stay_wins += 1
        
    print(f"Vinst utan att byta dörr: {stay_wins}")
    print(f"Vinst genom att byta dörr: {switch_wins}")


match uppg:
    case 1:
        lotto()
    case 2:
        stryktips()
    case 3:
        slump_1000()
    case 4:
        monthy_hall()
