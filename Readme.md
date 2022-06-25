## overview(external) architecture
![Image alt text](./images/clean_ddd.png)

## overview(internal) architecture
![Image alt text](./images/clean_arch.png)

---


## TODO 

- [x] dockerize
- [x] docker compose
- [ ] structed logging + elasticsearch
- [ ] global exception handling
- [ ] fluent command validation
- [ ] usecases
- [ ] unit tests
- [ ] functional tests
- [x] rest
- [ ] mediatR
- [x] clean architecture
- [x] mongo
- [ ] mongo add constraint for blockeduser
- [x] masstransit
- [ ] masstransit unit tests
- [x] rabbitmq
- [ ] tech stack olarak awesome readme koy (masstransit,rabbitmq,mongo,dotnet6.0)


## Get test coverage result

```sh
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov tests/MessageService.UnitTests/MessageService.UnitTests.csproj
```

```sh
 dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov tests/MessageService.FunctionalTests/MessageService.FunctionalTests.csproj
```