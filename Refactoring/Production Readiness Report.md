There are a lot of things that can be improved in this service. 
From the safety point of view it requires adding authentication/authorization. Possibly using jwt tokens and claims/roles for different groups of users (admins/registered users).
Also it does not have any logger service. This needs to be added to make it possible to store logs in files or at the cloud. 
Logs are required for debugging and should contain error description, traceid of requests and other necessary information. Errors can be logged in the "/error" endpoint I created.
We also do not have a "launchsettings" or "appsettings" for other environments. We need to add those to store environment specific settings.
Might be useful to implement storing of secrets such as database connection strings or other sensitive data.
The current database obviously is not production-ready, so a proper SQL database with proper schema should be created.
If we address the scalability of this service it arises a number of topics for discussion, eg.:
- using message queue to process orders by variable number of services.
- data synchronization between service instances or database partitioning, etc.
These are just first ideas that come to mind.