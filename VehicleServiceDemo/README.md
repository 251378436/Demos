## Introduction
Demo background: The customers want to know the status of the vehicle including location, humidity, light, weight, and other
information. The standard customers can only get location information. Premium customers can get other sensor data. The most important 
domain logics are in this file https://github.com/251378436/Demos/blob/main/VehicleServiceDemo/VehicleService.Domain/UseCases/Sensor/SensorUseCase.cs.

This is a real demo project about how to use ServiceBus and Kafka to handle requests for vehicle information.

The purposes of this demostration include:
* What is fragile test
* How refactoring breaks the unit tests
* Why it needs to decouple production code and tests code
* What is the difference betweem unit tests / component tests / integratin tests
* How to use Moq and Dependecy Injection in tests
* Why SOLID principles are important to production code and tests code
* How proxy and factory design patterns used in the project and how they help with tests


## Warning
Not all the code in this project is following the best pracetices. 

Please do not use those code in production code directly.

## Projects
* VehicleService.Domain - include all the domain business logics, e.g. useCases
* VehicleService.Infrastructure - include the code how to interact with ServiceBus and Kafka
* VehicleService - this is a real asp.net service running with code from domain and infrastructure projects
* VehicleReplayService.Domain - this asp.net service is used to handle replay messages
* Tests/UnitTestExample - use Moq to test the classes in xUnit
* Tests/ComponentTestExample - use DependencyInjection to test the classes in xUnit
* Tests/IntegrationTestWithSubstituteExample - use WebApplicationFactory for tests but substitute receivers and producers
* Tests/IntegrationTestExample (TODO) - use WebApplicationFactory for tests in near real environment with TestContainer or FluentDocker

## How to generate Kafka avro schemas
inteall avro tools
```sh
dotnet tool install --global Apache.Avro.Tools
```
generate schemas
```sh
avrogen -s SensorMessageValue.asvc ./Generated --skip-directories
```

## How to run it locally
1. make sure Kafka and ServiceBus running
2. Run or Debug project `VehicleService` locally
3. Send message to ServiceBus with the payload below.
```
{"VehicleId":"82ed666a-2b2e-4380-bfd5-cfb5d72d6d8c","CustomerId":"954cbf0c-b594-4d19-828c-6f3bbedec728"}
```