# CodingTest

A collection of REST Web APIs to call, combine and return the result of `jsonplaceholder.typicode.com` mock service.

## Description

* A ASP .Net Core Web Api Project, based on .Net 6.0.
* No MVC pattern or controllers have been implemented to keep it minimal.
* Options can be passed as path parameters in Get requests, purely for practical and simplicity reasons.
* Logging: each request and response is logged to a daily rolling file (max 10MB) inside the `Logs` folder.
* Docker: the solution includes a dockerfile to automatically configure a (Windows) container.
* Documentation: a Swagger page appears by default when running the solution, showing complete documentation about the APIs.
* Actions: a Github action has been implemented (`workflow/build.yml`) to automatically build the project on each push.

## Unit Testing

* A python script (`UnitTest.py`) for Unit Testing is available in the solution folder.

## Getting Started

### Dependencies

* .Net 6.0 
* Any other package is included in the solution, such as `Serilog` for logging, `Swashbuckle` for documentation, `VisualStudio.Containers` for Docker and `Newtonsoft.Json`.

### Installing

* Download or clone the repository.
* Build and run from Visual Studio using any of the available profiles.

### Using the software

* When running, a web page with documentation appears on 
```
https://localhost:7261/swagger/index.html
```
* Default port for Https is `7261` if not edited from Docker, `7262` for http.
* Example of a very basic request from curl
```
curl 'https://localhost:7261/getAllTasks'
```
* Extra parameters to filter by user and/or manage pagination are supported
```
curl 'https://localhost:7261/getTasksByUser?userId=3&limit=10&offset=0'
```
```
curl 'https://localhost:7261/getAllUsers?limit=5'
```
* A json object is returned. It includes: status of the operation (Success/Error), limited list of requested objects and the total amount of them available (for pagination purposes).

### Authors

* [@MatteoDeC](https://github.com/MatteoDeC)
* ~ChatGPT~

