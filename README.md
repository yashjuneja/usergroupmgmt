# UserGroupmanagement Solution

# Overview
Sample solution for managing user groups
It uses Asp.NET Core Web ApI for backend and Blozor WebAssembly for client-side frontend.

Solution Structure
UserGroupManagement.API # ASP.NET Core Web API 
UserGroupManagement.Client # Blazor WebAssembly Client
UserGroupManagement.Common # Shared models & utilities
UserGroupManagement.Service # Business logic layer
UserGroupManagement.Repository # Data access layer
Database/ # Database script

# UserGroupManagement.API
- The backend Web API.
- Handles HTTP requests, authentication, routing, and connects to services & repository layers.

# UserGroupManagement.Client
- Blazor WebAssembly frontend.
- Communicates with the API.
- Provides UI for users to manage groups.

# UserGroupManagement.Common
- Shared DTOs, constants, helpers.
- Used by API, Client, Services, Repository.

# UserGroupManagement.Service
- Contains business logic.
- Implements services used by API controllers.

# UserGroupManagement.Repository
- Handles database operations.
- Contains repository classes, DbContext, migrations, etc.

# Database
- Contains SQL scripts for creating the database and seeding data.
- The script need to be run in blaank environment and connection string to be updated in UserGroupManagement.API appSettings.json file

#Set up Instructions
Clone the repo
git clone https://github.com/yashjuneja/usergroupmgmt.git
cd UserGroupManagement
run db script
Update connection string to point to teh db created

Run the API
dotnet run --project UserGroupManagement.API

Run Blazor client
dotnet run --project UserGroupManagement.Client

