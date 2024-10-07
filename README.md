# Estoque App

Componentes e frameworks utilizados
- .Net 8.0
- EF Core
- Postgresql
- Redis
- Circuit breaker com Polly
- FluentValidation
- Docker e docker compose para subir a aplicação, o BD e o Redis


Arquitetura em camadas aderente ao Clean Architecture e ao DDD
- Camada do presentation onde fica o web api
- Camada de aplicação
- Camada de domínio
- Camada de infra
- Camada de crosscutting

# Como subir o ambiente local
Basta dar um docker compose up que todos os componentes necessários e a api ficarão de pé.


