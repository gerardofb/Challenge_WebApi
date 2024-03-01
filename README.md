# Challenge_TL
## This repository is the final result of a challenge for a position as Tech Lead in .NET Technology for a software engineering company.

> N5 company requests a Web API for registering user permissions, to carry out this task it is necessary to comply with the following steps:
- [x] Create tables to manage employees , permission and permission types.
- [x] Your system must allow that you have employess with "N" count of permissions type.
- [x] Create a Web API using net core on Visual Studio and persist data on SQL Server.
- [x] Make use of EntityFramework.
- [x] The Web API must have 3 services “Request Permission”, “Modify Permission” and “Get Permissions”. Every service should persist a permission registry in an elasticsearch index, the register inserted in elasticsearch must contains the same structure of database table “permission”.
- [x] Create apache kafka in local environment and create new topic where persist every operation a message with the next dto structure:
- [x] Id: random Guid
- [x] Name operation: “modify”, “request” or “get”.
- (desired)
- [x] Making use of repository pattern and Unit of Work and CQRS pattern(Desired). Bear in mind that is required to stick to a proper service architecture so that creating different layers and dependency injection is a must-have.
- [x] Add information logs in every api endpoint and log the name of operation using serilog as log library.
- [x] Create Unit Testing and Integration Testing to call the three of the services.
- [x] Use good practices as much as possible.
- [x] Prepare the solution to be containerized in a docker image.
- [ ] Prepare the solution to be deployed in kubernetes. (desired)
- [x] Upload exercise to some repository (github, gitlab,etc).

> [!TIP]
> The tasks marked completed are so far the result.

The challenge was made for collaboration with the company contacted by @workana/team [Workana](https://www.workana.com/es)

> [!NOTE]
> To run the solution from the docker image, just run from the root of the solution:
```
docker-compose up --force-recreate --build
```
> [!TIP]
> For proper working of the api, you should seed the database using only once the .exe in the project ConsoleSeedChallenge_TL
> Lastly, navigate to http://localhost:6400/swagger/index.html and test the API. The project ConsoleConsumerTopics is just for checking the queues in kafka.

> [!IMPORTANT]
> If you wish not to use the docker image generated in the solution the only way to run this solution is building independently the docker images and ajusting configuration of projects:
- [Bitnami Kafka](https://hub.docker.com/r/bitnami/kafka/#!)
- [Microsoft SQL Server](https://hub.docker.com/_/microsoft-mssql-server)
- [Elastic Search](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html)

The challenge follows the Unit Of Work and CQRS Design Patterns:
(Reference)
- [CQRS](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Unit of Work](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/june/the-unit-of-work-pattern-and-persistence-ignorance)
