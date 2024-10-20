# PhDManager
Táto aplikácia je tvorená ako bakalárska práca. Výledkom bude informačný sytém pre správu doktorantského štúdia.
## Prihlasovanie pomocou LDAP
Do aplikácie je možné sa prihlásiť pomocou LDAP účtu.
## Systém rolí
Role pozostávajú zo študenta, učiteľa a admina. Každá rola má svoje práva prístupu.
## Autorizácia pomocou JWT tokenu
Po úspešnom prihlásení na strane klienta, server pridelí klientovi JWT token, ktorý je platný po dobu 30 minút.

## Rozloženie aplikácie
1. serverová časť - ASP.NET Core Web API
2. klientská časť - Blazor Web App
3. databáza - PostgreSQL
