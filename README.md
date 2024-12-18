# Equipment Hosting Service API

## Overview

The Equipment Hosting Service API is a .NET Core Web API application that manages contracts for hosting equipment in various facilities. It provides functionality to create and retrieve equipment-hosting contracts while ensuring validation and handling of business rules, such as facility capacity and equipment type availability.

For the sake of simplicity, I did not hide any sensitive information.

---

## Features

- **Facility and Equipment Type Management**: Retrieve and utilize predefined facilities and equipment types.
- **Contract Management**:
  - Create new contracts for hosting equipment in a facility.
  - Retrieve all existing contracts.
- **Validation**:
  - Verify sufficient space in the facility for the specified equipment quantity.
  - Validate facility and equipment type codes.
- **Global Exception Handling**: Middleware to handle application errors consistently.
- **Unit Testing**: Comprehensive test coverage for services and controllers using XUnit.

---

## Technologies Used

- **Framework**: .NET Core Web API
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Automapper**: For DTO and entity mapping
- **Dependency Injection**: Built-in DI container
- **Testing**: XUnit, Moq
- **Deploying**:  Azure SQL Database, Azure App Service

---

## API Endpoints

### Contracts

1. Get All Contracts

	Request Header: X-Api-Key: 96F54D49-FB67-4A67-90BF-FA76DF0281F4

	GET https://equipmenthostingservice.azurewebsites.net/api/Contracts

	Returns a list of all contracts.

2. Create Contract

	POST https://equipmenthostingservice.azurewebsites.net/api/Contracts

	Request Header: X-Api-Key: 96F54D49-FB67-4A67-90BF-FA76DF0281F4

	Request Body:

	```json
	{
	  "facilityCode": "NF001",
	  "equipmentTypeCode": "ET001",
	  "equipmentQuantity": 1
	}
	```
### Predefined data for testing:  

1. EquipmentType Table

| Code        | Name               | Area   |  
| ----------- | ------------------ | ------ |  
| ET001       | Generator          | 50     |  
| ET002       | Pump               | 30     |  
| ET003       | Compressor         | 60     |  
| ET004       | Transformer        | 80     |  
| ET005       | Boiler             | 100    |  
| ET006       | Packaging machine  | 0      |  
| ET007       | Grinder            | -2     |  

2. Facility Table

| Code        | Name               | StandardArea |  
| ----------- | ------------------ | ------------ |  
| EF004       | East Facility      | 0            |  
| NEF003      | Northeast Facility | 1000         |  
| NF001       | North Facility     | 1000         |  
| SF002       | South Facility     | 1500         |  
| WF005       | West Facility      | -100         |  

---

## Links

- **GitHub Repository**: https://github.com/SerhiiHarashchenko/EquipmentHostingService
- **Deployed API**: https://equipmenthostingservice.azurewebsites.net/api/Contracts
- **Database Connection String**: "Server=equipment-hosting-service.database.windows.net;Database=EquipmentHostingDB;User Id=serhiiharashchenko;Password=letItBe_18;Encrypt=True;"
- **API Key**: 96F54D49-FB67-4A67-90BF-FA76DF0281F4