# Monsterspill
Denne oppgaven er besvarelsen til ShaileshS1702 og daDevBoat.

## Introduksjon
Vi fikk i oppgave å lage et monsterspill, noe som vi valgte å løse ved å lage et spill der en spiller tre-på-rad mot et monster (en algortime). Algoritmen er lagd slik og fokusert rundt at det skal være umulig for spilleren å vinne.

## Filer
Dette prosjektet har i alle hovedsak en viktig fil, og det er Program.cs.
Det er andre filer som er inkludert i dette repository-et, men dette er ikke filer vi har redigert i eller opprettet.
Deres funksjon er begrenset til å få C++ til å funke og kjøre riktig.


## Program.cs
Hovedfilen til prosjektet.

### Hvordan programmet brukes
Bruker skriver inn i konsollvinduet koordinatet til der de vil plassere på formen x, y. (0, 0) er øverst til venstre, og (2, 2) er nederst til høyre. Spillet kjører helt til bruker har tapt, algoritmen vinner eller det blir uavgjort. 

### Algoritmerekkefølge
Algoritmen som brukes av monsteret/programmet for å sikre at spilleren aldri vinner er strukturert i grove trekk på følgende måte:

#### Checkwin
Checkwin er en funksjon som sjekker om noen har tre på rad og returnerer null hvis ingen har tre på rad, og returnerer spiller som har vunnet (enten "O" eller "X") hvis noen har tre på rad.


#### FakeAI
Vi har først definert punktet (3,3) som en return-verdi når tingene ikke oppfylles, ettersom, (3,3) ikke finnes. 

Det første vi sjekker er om vi har mulighet til å vinne i dette trekker, og plasser der, hvis det er mulig.
Så sjekker vi om spilleren kan vinne i neste trekk, og plasserer der de skulle plassert for å vinne, slik at spilleren ikke vinner. 
Deretter, hvis ingen kan vinne i neste trekk, sjekker vi for farlige trekk.
Hvis dette heller ikke er farlig, prøver vi å plassere et sted som er fordelaktig for oss, men dette kunne vært mer optimalisert og gitt et bedre resultat.


### Spillogikk
Det er tre hovedproblemer som algoritmen vår må passe seg for. Alle trekkene som gjør, vil hvis ikke de riktige trekkene gjøres, føre til at spilleren får to måter å vinne på i neste trekk. De tre problemene er liten l, stor L og hest.
Vi har definert tre typer felt på brettet.
Hjørne, midten og side. side er feltene ligger ikke-diagonalt mellom to hjørner. Hjørner er røde, midten er grønn og sidene er blå. (farge videre har ikke noe med dette å gjøre).

![image](https://user-images.githubusercontent.com/113507675/210531775-75dfd097-b078-42ae-99c6-f49adcc35d79.png)


#### Liten l
![image](https://user-images.githubusercontent.com/113507675/210532507-b4790c56-9e39-4eaa-8bdb-bccfbf6a91f4.png)
![image](https://user-images.githubusercontent.com/113507675/210532521-c5e1d9cd-7bcb-48d4-8a7f-a99b134a772a.png)
<br>
Hvis spiller (x) neste trekk plassere i 0,0 kan han får to på rad i både horisontalt (0,0), (1,0), (2,0) eller (0,0), (0,1), (0,2).

Løsning:
Plasser i 0,0 (eller tilsvarene hjørne) for å motvirke trekket.

#### Stor L
![image](https://user-images.githubusercontent.com/113507675/210532933-8dc1087b-afc9-46a9-ae6e-17e61693959a.png)
![image](https://user-images.githubusercontent.com/113507675/210532947-cfe1aba7-6fba-46ed-b691-48002675c11c.png)
<br>
Hvis spiller (x) neste trekk plassere i 0,0 kan han får to på rad i både horisontalt (0,0), (1,0), (2,0) eller (0,0), (0,1), (0,2).

Løsning: 
Plasser i en av side, for å tvinge fram spiller til å plassere et sted. Gitt vi har plassert i 1,1 fra før.

#### Hest
Hest (etymologi: et brikke i et sjakkspill; kan kun bevege seg to felt i en retning og et felt i den normale retningen. ): 
![image](https://user-images.githubusercontent.com/113507675/210532397-cf2e14f8-1011-4736-8971-f49e0e47590e.png)
![image](https://user-images.githubusercontent.com/113507675/210532409-a424d089-e4a0-40c6-8ab5-653bee542367.png)
<br>
Hvis spiller (x) neste trekk plassere i 0,0 kan han får to på rad i både horisontalt (0,0), (1,0), (2,0) eller (0,0), (0,1), (0,2).

Løsning:
Plasser i 0,0 (eller tilsvarene hjørne) for å motvirker. OBS: pass på også 1, 2, en annen hest. 

## Videre utvikling
Ved videre utvikling av dette programmet kunne samme algoritme som blir brukt for å se etter liten l, stor L og hest til å finne best mulig posisjon for algoritmen til å plassere. Nå er "målet" til algoritmen å sørge for at spilleren aldri vinner, men ikke så mye fokus på å vinne over spilleren. 

