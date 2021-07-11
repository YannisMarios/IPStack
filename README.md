# ipstack.com Demo Project

This project is using .NET Core (v.3.1) as back-end API in order
to fetch and store IP address information from https://ipstack.com/

## Usage
1. Right click on Solution in Solution Explorer, click on "Select Startup Projects".
2. Start the app in Visual Studio without debugging (Ctrl + F5)
   and under "Single startup project" set "Start" for "IPStack.WebApi" project.

## Notes:
This project already includes a local database file (MSSQLLocalDB) with several 
IP addresses details already inserted and a json file with IP addresses details
(in SolutionItems folder) for you to experiment.

Swagger is included so you can easily try the API.
