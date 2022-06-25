## overview(external) architecture
![Image alt text](./images/clean_ddd.png)

## overview(internal) architecture
![Image alt text](./images/clean_arch.png)

---


## TODO 

- [ ] dockerize
- [ ] docker compose
- [ ] structed logging + elasticsearch
- [ ] usecases
- [ ] unit tests
- [ ] functional tests
- [ ] fluent command validation
- [ ] rest
- [ ] mediatR
- [ ] clean architecture
- [ ] mongo
- [ ] mongo add constraint for blockeduser
- [ ] masstransit
- [ ] masstransit unit tests
- [ ] rabbitmq
- [ ] tech stack olarak awesome readme koy (masstransit,rabbitmq,mongo,dotnet6.0)


## Get test coverage result

```sh
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov tests/MessageService.UnitTests/MessageService.UnitTests.csproj
```

```sh
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov tests/MessageService.FunctionalTests/MessageService.FunctionalTests.csproj
```