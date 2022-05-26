# File Processor
Please make sure you have installed following tools before running the code.
1. Node.js
2. .Net Core 3.1 SDK (latest)

## Project Walk Through
UI: 
- Create simple UI to handle loading and reading the .csv file from local folder.
- Add some restriction on file type and size to make sure the service and process the data smoothly.
- Can only click 'Save' button when there is file detected.

note: From assignment I don't see any specific requirement about the way load and read file so I decided to have UI to handle this part.

Backend:
- Create RESTFul Api for front end  to consume to pass the file content to backend to do further process.
- Sample sales.csv file is csv extension, however considering service may extend in the future that will take other file extension to process also, I created 'FileProcessorFctory' so that it can create different processor instance based on input 'FileType'. Currently, only 'CsvFileProcessor' is available.
- Put all business logic into business service project and each call will have its own responsibility to make sure we handle business logic in single place (single responsibility principle).
- Use DI(dependency injection) in while application by registering those in startup.cs to decrease coupling between classes and their dependencies. Additionally, it will be able to allow me to do full unit testing.
- Put all shared object or method into common project so that it can be used by entire application. E,g string extension method.
- Base on schema.sql sample Data project is created which uses entityframework core to call database, and it is registered as Microsoft Sql DB in startup.cs.
- Create sample test project to unit test my logic, since I don't have database I use in memory database to test if file content can be saved to DB successfully. And 'InMemoryDbBuilder' can be generic method to create different database with different name.
# file_processor
