# Opdracht Testing

## Informatie
Het project is iets uitgebreider dan de overkoepelende oefening; ik hoop dat dit oké is (ik heb me een beetje laten gaan). 
Het idee is een soort winkel waar we videogames kopen en de aankoop wordt geautomatiseerd. 
Elke videogame heeft een leeftijdsgrens (Age Rating), wat betekent dat de gebruiker deze leeftijd of ouder moet zijn om het spel te kunnen spelen of kopen.

Als extra functionaliteit heb ik ervoor gezorgd dat elke videogame een korting-leeftijd heeft. 
Als de gebruiker deze leeftijd heeft bereikt of ouder is, wordt er automatisch een korting toegepast op de aankoop.

In dit project worden Unit Tests, Integration Tests en Acceptance Tests gemaakt om te testen of bij het aankopen van een game de gebruiker "Approved" of "Rejected" wordt, afhankelijk van of de leeftijd voldoet aan de vereisten van het spel. 
Hetzelfde wordt gedaan met de korting om te testen of de gebruiker aan de korting-leeftijd voldoet en of de korting correct wordt toegepast.

---

## Structuur
- De klasse **`DecisionModule`** neemt de beslissing om een aankoop goed te keuren op basis van leeftijd en kortingen. De structuur is modulair en maakt gebruik van aparte services voor flexibiliteit en testbaarheid.
  
- De klasse **`ActionHandler`** voert acties uit op basis van de beslissingen van de **`DecisionModule`**, zoals het verwerken van aankopen en het toepassen van kortingen.
  
- De klasse **`DiscountService`** berekent kortingen op basis van de leeftijd van de gebruiker, wat zorgt voor een gepersonaliseerde ervaring.

- De klasse **`GameService`** is verantwoordelijk voor het ophalen van game-informatie. De klasse heeft een `GameUrl` property die de URL van de game bevat en een `GetGame` methode die, op basis van een `id`, de bijbehorende game opzoekt (momenteel nog niet geïmplementeerd).

- De klasse **`GameServiceApi`** haalt game-informatie op via een API-aanroep, waarbij gebruik wordt gemaakt van Mockoon voor het simuleren van API-responses. Deze klasse biedt dezelfde functionaliteit als de **`GameService`**, maar maakt gebruik van Mockoon om de game-informatie op te halen vanuit een mock API.

- De klasse **`UserService`** is verantwoordelijk voor het ophalen van gebruikersinformatie. Het bevat een `UserUrl` property en een `GetUser` methode die een gebruiker ophaalt op basis van een `id` (momenteel nog niet geïmplementeerd).

- De klasse **`UserServiceApi`** haalt gebruikersinformatie op via een API-aanroep, met behulp van Mockoon voor het simuleren van API-responses. De functionaliteit is vergelijkbaar met de **`UserService`**, maar deze maakt gebruik van Mockoon om de gebruikersinformatie op te halen vanuit een mock API.

- De klasse **`Game`** definieert de eigenschappen van een videogame, waaronder: **`Id`** (de unieke identificatie van het spel), **`GameTitle`** (de naam van het spel), **`AgeRating`** (de leeftijdsbeoordeling van het spel), en **`IsEligibleForDiscount`** (een waarde die aangeeft of de game in aanmerking komt voor een korting, afhankelijk van de leeftijd van de gebruiker).

- De klasse **`User`** definieert de eigenschappen van een gebruiker, waaronder: **`Id`** (de unieke identificatie van de gebruiker), **`UserName`** (de naam van de gebruiker), en **`UserAge`** (de leeftijd van de gebruiker, die gebruikt wordt om te bepalen of de gebruiker in aanmerking komt voor de aankoop van een game en de korting).

---

## Interfaces

- **`IUserService`**: Vergelijkbaar met `IGameService`, wordt deze interface gebruikt door zowel de `UserService` als de `UserServiceApi`. Dit maakt het eenvoudig voor testing door de meest geschikte implementatie te kiezen op basis van de testomgeving.

- **`IGameService`**: Vergelijkbaar met `IUserService`, wordt deze interface gebruikt door zowel de `GameService` als de `GameServiceApi` Dit maakt het eenvoudig voor testing door de meest geschikte implementatie te kiezen op basis van de testomgeving.

- **`IDiscountService`**: Deze interface maakt het mogelijk om de logica voor het berekenen van kortingen te variëren. Dit maakt unit testing eenvoudig, omdat de kortingenlogica kan worden gemockt zonder de echte implementatie.

- **`IActionHandler`**: Deze interface definieert acties op basis van beslissingen. Het maakt unit testing mogelijk doordat de acties kunnen worden gemockt en getest zonder ze daadwerkelijk uit te voeren.

---
