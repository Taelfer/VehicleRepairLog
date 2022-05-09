[![Continuous Integration and Deployment](https://github.com/Taelfer/VehicleRepairLog/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Taelfer/VehicleRepairLog/actions/workflows/ci-cd.yml)

# Vehicle Repair Log

## Description
Vehicle Repair Log is a Web API application that allows the user to create a vehicle log and store information related to repairs performed and modifications made. 

Right now user can register account (sensitive data is hashed in DB), add vehicle and things related to it, display vehicles available in DB. The availability of some resources depends on the user's role. User authentication is performed via JWT.

This project serves as a base for me to develop skills related to : C#, ASP.NET, Web API, MS SQL, design patterns, useful libraries. 

## Used Technologies
- ASP.NET Core Web API
- MS SQL Database
- Entity Framework Core
- GitHub Actions

## Used Design Patterns
* Repository
* Mediator
* CQRS
* N-tier architecture

## Used Libraries
* MediatR
* FluentValidation
* AutoMapper
* xUnit
   

## License
[MIT](https://choosealicense.com/licenses/mit/)