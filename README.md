# Opdracht Testing

## Navigatie
- [Informatie](#informatie)
- [Structuur](#structuur)
- [Interfaces](#interfaces)
- [Unit Tests](#unit-tests)
- [Integration Tests](#integration-tests)
- [Acceptance Tests](#acceptance-tests)

---

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

- **`IGameService`**: Vergelijkbaar met `IUserService`, wordt deze interface gebruikt door zowel de `GameService` als de `GameServiceApi`. Dit maakt het eenvoudig voor testing door de meest geschikte implementatie te kiezen op basis van de testomgeving.

- **`IDiscountService`**: Deze interface maakt het mogelijk om de logica voor het berekenen van kortingen te variëren. Dit maakt unit testing eenvoudig, omdat de kortingenlogica kan worden gemockt zonder de echte implementatie.

- **`IActionHandler`**: Deze interface definieert acties op basis van beslissingen. Het maakt unit testing mogelijk doordat de acties kunnen worden gemockt en getest zonder ze daadwerkelijk uit te voeren.

---

## Unit Tests
- **MakeDecision_ShouldReturnApproved_WhenUserAgeIsGreaterThanOrEqualToAgeRating**
  - Controleert of de beslissing "Approved" wordt wanneer de gebruiker oud genoeg is voor de leeftijdsvereisten van het spel.

- **MakeDecision_ShouldReturnRejected_WhenUserAgeIsLessThanAgeRating**
  - Controleert of de beslissing "Rejected" wordt wanneer de gebruiker te jong is voor de leeftijdsvereisten van het spel.

- **MakeDecision_ShouldApplyDiscount_WhenUserAgeIsGreaterThanOrEqualToGameDiscountAge**
  - Verifieert dat een korting wordt toegepast wanneer de gebruiker in aanmerking komt voor een leeftijdsgebonden korting.

- **MakeDecision_ShouldThrowArgumentException_WhenGameIsNull**
  - Controleert of een fout wordt gegenereerd wanneer een ongeldig game-ID wordt meegegeven.

- **MakeDecision_ShouldThrowArgumentException_WhenUserIsNull**
  - Controleert of een fout wordt gegenereerd wanneer een ongeldig gebruiker-ID wordt meegegeven.

- **MakeDecision_ShouldThrowGenericException_WhenUnexpectedErrorOccurs**
  - Controleert of een algemene foutmelding wordt gegenereerd wanneer er een onverwachte fout optreedt.

### Uitleg van Unit Tests
De unittests zijn gericht op het valideren van de functionaliteit van de DecisionModule onder verschillende omstandigheden. Hierbij maak ik gebruik van mocking om de afhankelijkheden van de module, zoals IGameService, IUserService, IDiscountService, en IActionHandler, te simuleren. Door mocks te gebruiken, kan ik het gedrag van deze afhankelijkheden controleren en specifieke scenario's creëren, zonder dat ik afhankelijk ben van hun daadwerkelijke implementaties.

---

## Integration Tests
- **MakeDecision_ShouldReturnApproved_WhenUserAgeIsGreaterThanOrEqualToAgeRating**
  - Valideert dat de beslissing "Approved" wordt teruggegeven wanneer de leeftijd van de gebruiker voldoet aan de leeftijdsvereisten van het spel. De test gebruikt Mockoon API’s voor gebruikers- en spelgegevens.

- **MakeDecision_ShouldReturnRejected_WhenUserAgeIsLessThanAgeRating**
  - Controleert dat de beslissing "Rejected" wordt teruggegeven wanneer de gebruiker te jong is volgens de leeftijdsvereisten van het spel.

- **MakeDecision_ShouldApplyDiscount_WhenUserAgeIsGreaterThanOrEqualToGameDiscountAge**
  - Controleert of de juiste korting wordt toegepast voor gebruikers die in aanmerking komen op basis van hun leeftijd.

- **MakeDecision_ShouldThrowArgumentException_WhenGameIdIsInvalid**
  - Test dat een `ArgumentException` wordt gegenereerd wanneer een ongeldig spel-ID wordt gebruikt.

- **MakeDecision_ShouldThrowArgumentException_WhenUserIdIsInvalid**
  - Test dat een `ArgumentException` wordt gegenereerd wanneer een ongeldig gebruiker-ID wordt gebruikt.

- **MakeDecision_ShouldThrowException_WhenAnErrorOccurs**
  - Verifieert dat een algemene uitzondering wordt gegenereerd wanneer een fout optreedt in de API of service.

### Uitleg van Integratie Tests
De integratietests valideren het gedrag van de `DecisionModule` in een realistische omgeving waarin de afhankelijkheden samenwerken. Dit omvat het gebruik van:

- **IGameService** en **IUserService**: Deze services communiceren met Mockoon API’s om gegevens over spellen en gebruikers op te halen.
- **IDiscountService**: Controleert of een gebruiker in aanmerking komt voor een korting.
- **IActionHandler**: Voert acties uit op basis van de beslissing.

In tegenstelling tot unit tests worden hier geen mocks gebruikt. De echte implementaties van de services worden getest, wat een vollediger beeld geeft van hoe de module zich gedraagt in een productie-achtige omgeving.

---

## Acceptance Tests

### Making decisions based on user age, game age rating, and discount eligibility
#### Scenario 1 - Approve decision when user age meets or exceeds game age rating
- Valideert dat de beslissing "Approved" wordt teruggegeven wanneer de leeftijd van de gebruiker voldoet aan de leeftijdsvereisten van het spel.

#### Scenario 2 - Reject decision when user age is less than game age rating
- Controleert dat de beslissing "Rejected" wordt teruggegeven wanneer de gebruiker te jong is volgens de leeftijdsvereisten van het spel.

#### Scenario 3 - Apply discount when user is eligible for discount
- Controleert of de juiste korting wordt toegepast voor gebruikers die in aanmerking komen op basis van hun leeftijd.

### Handling failure scenarios in decision making
#### Scenario 1 - Throw ArgumentException when game ID is invalid
- Test dat een `ArgumentException` wordt gegenereerd wanneer een ongeldig spel-ID wordt gebruikt.

#### Scenario 2 - Throw ArgumentException when user ID is invalid
- Test dat een `ArgumentException` wordt gegenereerd wanneer een ongeldig gebruiker-ID wordt gebruikt.

#### Scenario 3 - Throw Exception when an error occurs
- Verifieert dat een algemene uitzondering wordt gegenereerd wanneer een fout optreedt in de API of service.

### Uitleg van Acceptatie Tests
De acceptatietests zijn geschreven met behulp van de `Xunit.Gherkin.Quick` bibliotheek en verifiëren de functionaliteit van het systeem door middel van scenario's die zijn gedefinieerd in Gherkin-syntax. 
Deze tests zijn gericht op het valideren van de beslissingen die worden genomen door de `DecisionModule` op basis van verschillende gebruikers- en gamegegevens. 

Net zoals integratie tests maken de tests gebruik van echte API-responses via Mockoon. Dit zorgt ervoor dat de tests de werkelijke interacties in het systeem valideren, wat een realistischer beeld geeft van hoe het systeem zich gedraagt in een productieomgeving. Door gebruik te maken van Mockoon kunnen we API-responses simuleren.
