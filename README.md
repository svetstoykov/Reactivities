

# Reactivities


## Table of Contents





- [Build](#build)
- [Description](#description)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Identity](#identity)
- [WebAPIGenerator](#webAPIGenerator)


---
## Build
1. Configure connection string in the ```appsettings.Development.json```
2. If you want to use the image upload service, you need an account at **Cloudinary**. Once you have an account there you can add the Cloudinary settings in an ```appsettings.json``` file (create one).
```
  "Cloudinary": {
    "CloudName": "name",
    "ApiKey": "apiKey",
    "ApiSecret": "apiSecret"
  }
```
3. Build and run the project. An automated db migration logic (defined in ```Program.cs```) will run the necessary migrations and seed some dummy data (can be found in ```Seed.cs```)

---
## Description
---


An Activities app created using React &amp; ASP.NET Core. The application allows the user to create an account, manage activities, follow/unfollow other users and much more.  


---
## Technologies
---
- .NET 6
- ASP.NET
- React 17
- EF Core 6


---
## Architecture
---
The application implements **Domain-Driven-Design** (DDD) as well as the **Onion architecture**:
<img src="docs\onion_architecture.jpg" width="420" height="350" />


The main structure for the projects is folder-by-feature. All projects include a folder for a **feature** (i.e., activities) and (usually) a **common** folder which holds common entities/services. Then each feature-folder declares its sub-folders for any services, models etc.

<img src="docs\folderStructure.PNG"/>


The core layer is divided into 3 main layers: **Domain**, **Application**, **Infrastructure**.


1. ***Domain*** - As per DDD the Domain layer has no foreign dependencies - this includes other projects or NuGet packages. It includes all of the domain models and the logic, which pairs with them. Encapsulation is used to restrict the way these domain models are manipulated.
2. ***Application*** - Contains the service that provides the application logic. This includes commands and queries since the Application layer communicates via CQRS with the Web layer. This layer depends only on the Domain layer, the DataServices interfaces are declared here, however the implementations are in the Infrastructure.
3. ***Infrastructure*** - This layer manipulates the infrastructure part of the project - Identity, FileOperations, DataServices. It communicates with the database via EF Core and provides implementations for the DataServices, which is used from the Application layer. This layer also contains the identity, which I will discuss further later.


Besides the **Core** layer, we also have several other projects.


1. ***API*** - The *Web* part of the application. This project exposes an API, from which the client can communicate with the app. It does this by using controllers or endpoints.
2. ***WebAPIGenerator*** - A tool which is used to generate TypeScript models, which are then used by the client.
3. ***Common/Models*** - Provides common models - Result, Global Constants etc.


---
## Identity
---
I've created a seperate section for Identity since using ASP.NET Identity along with DDD is actually very inconvenient. In general, the **user** should be a domain model.However, using the **IdentityUser** class requires you to pollute your domain layer with a couple of NuGet packages.Therefore, I have divided the User <> Profile models to address different parts of the application.


1. ***User*** - This class implements the Microsoft **IdentityUser** interface and is defined in the Infrastructure layer, where we already use EF.The sole purpose of this class is the log in and register users. It has no relation to the Domain entity **Profile**.
2. ***Profile*** - This is the domain entity which defines the user. When you register for the application, you use all of the built-in logic from the ASP.NET Identity (registration, password hashing, and so on). This creates an **IdentityUser**, but the application creates a **Profile** domain model for the user. This is the actual model which is used for all of the business logic. Unfortunately, no solution is perfect and this approach has its cons - duplication of *email*, *username* in the DB Tables ASP.NET User & Profiles.

---
## WebAPIGenerator
---
This is a simple console app, that uses a NuGet package called **TypeScriptModelsGenerator** by José A. Saldaña Perez. You can check the project's website [here](https://www.typescriptmodelsgenerator.com/).

The idea is that your request/response/view models should be consistent across your client and server. Changing a model in the C# code means that I have to keep in mind to change it on the client-side ```.ts``` model as well. This tool helps me generate models, which will always be consistent across my app.

When you build the project, an on-build (post-build) trigger is triggered, which runs the ```GenerateModels.cmd``` batch file. This batch file will basically just ```dotnet run``` the project. The application will take care of cleaning up all of the ```.ts``` files and generating new ones. If you need a model to be generated, you simply have to inherit the ```BaseApiModel.cs``` (for the Web Layer) or ```BaseAppModel.cs``` (for the Application layer). The tool uses reflection to locate the models.
