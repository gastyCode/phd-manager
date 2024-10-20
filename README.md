# PhDManager
Táto aplikácia je tvorená ako bakalárska práca. Výledkom bude informačný sytém pre správu doktorantského štúdia.

## Spustenie aplikácie
**Pre správne fungovanie je potrebné do User Secrets v projekte PhDManager.Api pridať hodnoty zo súboru sample-secrets.json a prepísať Username a Password na Vaše meno a heslo do systému LDAP.**
Pre spustenie je potrebné mať nainštalovaný Docker. Projekt je potom spustiteľný buď cez Visual Studio alebo cez príkaz `docker compose up --build`.
## Pistup k viacerým funkciám
Nakoľko sa ešte v aplikácií nenachádza systém rozdeľovania rolí, je potrebné v databáze prepísať rolu Vášho profilu na "Admin". Následne sa odhlásiť a znova prihlásiť. Po prihlásení do aplikácie budú dostupné všetky momentálne implementované funkcionality.

## O aplikácií
### Prihlasovanie pomocou LDAP
Do aplikácie je možné sa prihlásiť pomocou LDAP účtu.
### Systém rolí
Role pozostávajú zo študenta, učiteľa a admina. Každá rola má svoje práva prístupu.
### Autorizácia pomocou JWT tokenu
Po úspešnom prihlásení na strane klienta, server pridelí klientovi JWT token, ktorý je platný po dobu 30 minút.
### Rozloženie aplikácie
1. serverová časť - ASP.NET Core Web API
2. klientská časť - Blazor Web App
3. databáza - PostgreSQL
