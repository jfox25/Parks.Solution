# Parks API
By James Fox

An api that can handle all crud operations for national and state parks.

## Technologies Used

- C#
- GIT
- DotNet Web Api Architecture 
- MySql
- Entity Framework Core
- Identity
- JSON Web Tokens
- Swagger

## Info

A web api for interacting with state and national parks. Uses Identity for authorization, JWT for authentication, and Swagger for documentation. 

## Project Requirements

1. .Net 5 SDK
2. MySQL
3. MySQLWorkbench

## Project Setup

1. Clone this repository to your desktop using git clone

```
git clone https://github.com/jfox25/Parks.Solution
```

2. cd into Directive

```
cd Parks.Solution
```

3. cd into the Project

```
cd Parks
```

4. Run dotnet restore in the terminal

```
dotnet restore
```

### Setting up the Database

1. In Sql WorkBench, create a new schema.
2. Create an appsettings.json in the Parks.Solution/Parks directive.
3. Configure the following code snippet for your database, then add it to your appsettings.json file

```

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database={DATABASE NAME};uid=root;pwd={PASSWORD};"
  }
}

```

4. Run this command.

```

dotnet ef Migrations add Initial

```

5. then run this command.

```

dotnet ef database update

```

### To Run the Project

1. Make sure you are in the Parks.Solution/Park directive
2. Make sure you have your mySql server configured
3. Start the project

```

dotnet run

```

4. Go to the localhost and you should be able to view the application

### PostMan

I have included a collection of Postman Requests to help interact with the api.
1. Simply import the ParkApi.postmanCollection.json into your Postman.
2. You should now be able to make requests to the server.
3. *Note* the url is defaulted to "http://localhost:5000" & some of the requests use a variable {{token}} for the Bearer JWT token.

### Documentation
1. Swagger documentation is avalible at the root address of the api.
2. Go to the root url of the api ex. "http://localhost:5000" to view swagger docs.

### Endpoints
All endpoints for the API
#### Authorization
* (POST) /api/register
Requires Register Credentials(Email, Password, Confirm Password), returns valid JWT token
* (POST) /api/login
Requires Login Credentials(Email, Password), returns valid JWT token
#### NationalParks
* (GET) /api/nationalparks
Does not require credentials, returns a collection of all national parks in db
* (GET) /api/nationalparks/nationalParkId
Does not require credentials, returns an specific national park based on id
* (POST) /api/nationalparks
Does require credentials(Token) & a national park schema(Name, NumberOfVisitors, StateName), returns newly created national park id as an object {id : newlyCreatedNationalParkId}
* (PUT) /api/nationalparks/nationalParkId
Does require credentials(Token) & a national park model(NationalParkId, Name, NumberOfVisitors, StateName, UserId) & the nationalParkId as a parameter, returns empty result
* (DELETE) /api/nationalparks/nationalParkId
Does require credentials(Token) & the nationalParkId as a parameter, returns empty result
#### StateParks
* (GET) /api/stateparks
Does not require credentials, returns a collection of all state parks in db
* (GET) /api/stateparks/stateParkId
Does not require credentials, returns an specific state park based on id
* (POST) /api/stateparks
Does require credentials(Token) & a state park schema(Name, NumberOfVisitors, StateName), returns newly created state park id as an object {id : newlyCreatedStateParkId}
* (PUT) /api/stateparks/stateParkId
Does require credentials(Token) & a state park model(StateParkId, Name, NumberOfVisitors, StateName, UserId) & the stateparkId as a parameter, returns empty result
* (DELETE) /api/stateparks/stateParkId
Does require credentials(Token) & the stateparkId as a parameter, returns empty result
#### Further Objectives
* Authorization with Identity
* Authentication with JSON Web Tokens
* Swagger Documentation

## GitHub Link

[Link to Code on GitHub](https://github.com/jfox25/Parks.Solution)

## Bugs

No known bugs at this time.

## Future Improvements

- Plan to add better authorization.

## License

MIT
Copyright (c) 2022 James Fox

```

```
