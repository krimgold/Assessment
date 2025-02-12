There are a lot of things that can be improved in this service. 
From the safety point of view it requires adding authentication/authorization. Possibly using jwt tokens and claims/roles for different groups of users (admins/registered users).
Also it does not have any logger service. This needs to be added to make it possible to store logs in files or at the cloud. 
Logs are required for debugging and should contain error description, traceid of requests and other necessary information. Errors can be logged in the "/error" endpoint I created.
We also do not have "appsettings" for Staging and Prod environments. We need to add those to store environment specific settings.
Might be useful to implement storing of secrets such as database connection strings or other sensitive data.
The current database obviously is not production-ready, so a proper SQL database with proper schema should be created.
Another suggestion is to introduce the ORM tool to make querying the database more testable, maintainable and efficient. 
We can use Entity Framework, if the queries are simple as the one in the example, or Dapper for more complex queries.
If we address the scalability of this service it raises a number of topics for discussion, eg.:
- using message queue to process orders by variable number of services.
- data synchronization between service instances or database partitioning, etc.