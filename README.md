## Customer Matter Management
A simple example of what a client/matter management application may look like. Uses a .NET backend API and a React front end.

This was a fun learning experience as it was the first time to that I had an opportunity to write dotnet on a Linux machine and with vscode. This project also afforded me the opportunity to try several new things I've never used before, such as vite, the efcore powertools CLI and Tailwindcss. I also got very familiar with the dotnet CLI 🙃

## Things to Do / Gameplan

### 1. ✅ Database
- ✅ Install postgres, pgadmin
- ✅ Create database
- ✅ Create schema
- ✅ Create tables
    - Users
         - UserId
         - Email addr
         - Optional: firstname
         - Optional: lastname
         - pw
            - must be hashed
         - Firm name
    - Customers -> clients
        - (A customer/client is 1:N with matters)
        - Name
        - Phone number
        - CustomerId
    - Matters
        - (CustomerId -> Customers/CustomerId Fk)
        - MatterId
        - Title
        - Description
- ✅ Initial database migration
    - ✅ Tested migrations with dummy db

### 2. ✅ Backend API
- ✅ Install dotnet 8
- ✅ API
    - ✅ EFPowerTools
        - ✅ Reverse engineer db to setup context and initial migration
    - ✅ (Unit of Work/Repo pattern)
    - APIs
        - ✅ (Basic error handling)
            - Middleware?
        - ✅ Customer / Client
            - GET /api/customers → Retrieve a list of customers
            - POST /api/customers → Create a new customer (name, phone)
            - GET /api/customers/{customer_id} → Retrieve details of a customer
            - PUT /api/customers/{customer_id} → Update a customer
            - DELETE /api/customers/{customer_id} → Delete a customer
        - ✅ Matter
            - GET /api/customers/{customer_id}/matters → Retrieve matters for a customer
            - POST /api/customers/{customer_id}/matters → Create a matter
            - GET /api/customers/{customer_id}/matters/{matter_id} → Retrieve matter details
        - ✅ Authentication
            - POST /api/auth/signup → Create a new user (email, password, firm name)
            - POST /api/auth/login → Login and receive JWT
            - GET /api/auth/me → Return authenticated user info (JWT protected)
    - ✅ JWT
        - can generate tokens
        - can validate tokens

### 3. React/Tailwind Frontend
- ✅ Install node/npm
- ✅ Create react app - using vite
    - (Basic error handling)
    - ✅ Use React with TailwindCSS
    - ✅ Build a minimal UI with:
        - A login form
        - A list of customers (clicking a customer should show their matters)
        - A form to create new customers
        - A form to create new matters under a customer
        - Optional: user in corner, clicking calls auth/me

### 4. Other
- ✅ README must explain how to run the application and get it setup
- ✅ API documentation
    - swagger/openapi

### ✅ .env file for env vars

### Extra
- Search & Filtering for customers and matters
- Unit Tests (Jest or Mocha for backend)
- Docker Support (Dockerfile + docker-compose)
- Role-Based Access (e.g., admin vs. standard users)
- Improved UI/UX (Better styling, real-time updates)

### Things I'd do if I had more time (not all inclusive)
- the validation is only partially done in the UI project
- genericize the calls to the API from the UI project so they are reusable
- split up the different components that are all piled into the App.tsx file
- routing since the components would be on different pages
- annotate the routes in the API to surface more useful information in swagger
- rework the returns for a few of the "Create" endpoints, I don't particularly like that I am saving a record and then turning back around and retrieving it from the db to return
- better styling, i am not super familiar with Tailwind but took it as a learning opportunity
- search with pagination for customers and matters
- displaying who you are logged in as
- ability to log out
- login is not persisted across refreshes
- request logging middlware/exception middleware (eg. Serilog)
- containerization

## Setup
### Postgres/Database
(Uses .NET CLI - should be installed if you have .NET Core >3.1 SDK installed)
- Install postgres if it is not already installed
- Install [pgAdmin](www.pgadmin.org)
- Create new server registration in pgAdmin that points to localhost
- Using pgAdmin, create a new postgres blank database with a name of your choice
- In the CLI, install the dotnet-ef tool
```sh
dotnet tool install --global dotnet-ef
```
- To scaffold the database, we will need to run the intial db migation. In the CLI, navigate to CustomerMatterManagementAPI and run
``` sh
# run the first migration
dotnet ef database update --connection "host=localhost;port=5432;database=YOURDBNAME;username=postgres;password=YOURPASSWORD"
```
### API
(Uses the .NET CLI)
- Inside CustomerMatterManagementaPI, create a .env file next to the .env.example file
- Inside CustomerMatterManagementAPI, find .env.example and copy it's contents into .env, making sure to set your connection string, your JWT Issuer and JWT Key
- Run `dotnet build` or build the solution, either should be fine
- For hot-reloading, run `dotnet watch`; for just running the API, run `dotnet run`
- In your browser, navigate to `http://localhost:5182/swagger/index.html` to confirm API is running

### UI
- In the CLI, navigate to CustomerMatterManagerUI
- From inside the directory, run `npm install` (if package-lock.json is already present, run `npm ci`)
- Navigate to the /environments directory and create a .env.local file
- Copy the contents of .env.example into .env.local and save
- Navigate back to CustomerMatterManagerUI and run `npm run dev`; the UI project should load in the browser
