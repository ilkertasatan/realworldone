# realworld one Backend Developer Test
This is a technical test that I developed for realworld one. All information that you need and instructions on how you can test have been introduced the following.

# Technologies
Aside from .NET Core, numerous technologies I used within this solution including:

- NET 5 and Clean Architecture
- Web API using ASP.NET Core
- Data access with EF Core
- CQRS with MediatR
- Security using JWT Token
- Validation with FluentValidation
- Testing with XUnit, XBehave, Moq and FluentAssertions

## Infrastructure
I focused on building microservice architecture so you will see two separate projects in the solution like in the real life scenario. This architecture can also be extended by adding an API Gateway and monitoring.

### Microservices

#### Kitten Generator API
Kitten Generator service is being user for getting random kitty image via https://cataas.com/. This Api is secured and you will need a token to use it.

##### User Management API
User Management service is responsible for registering a new user, list them, and generate a JWT token to access protected resources. There are two PowerShell scripts to register a new user and acquire an access token.
Those scripts are located under the root project folder named `new-user.ps1` and `login-user.ps1`. When you run these scripts, you will be prompt user information. As soon as you enter, User Management API will be triggered.

After you get the token, you should add your token into the request header as a Bearer token to access the protected resource otherwise you will get a 401 Unauthorized response.

Token lifetime is limited to five minutes.

A sample curl:

```
curl --location --request GET 'http://localhost:5000/api/kitten-images/random' \
--header 'Authorization: Bearer {your_token}'
```

## API Interfaces
Swagger documentation has been added to the project to test the APIs, but for the sake of security, I'd recommend you using the Powershell script that I've added for registering and login user described above.

#### Kitten Generator API Interfaces
Use this link: http://localhost:5000/swagger/index.html to reach Swagger document.

#### User Management API Interfaces
Use this link: http://localhost:5001/swagger/index.html to reach Swagger document.

## How To Build
All project was dockerized. You need to run `build-all.ps1` PowerShell script in the root directory of the project.

After open any terminal, go to the project directory and type in the command below.

`./build-all.ps1`

In case you might get a security error when running the PowerShell script, you should run the following;

`Set-ExecutionPolicy RemoteSigned`

Afterward, you should see the project started to be built.

## Conclusion
I would like to thank you for giving me this chance to show what I can do. I hope that you will like my solution when you review it.