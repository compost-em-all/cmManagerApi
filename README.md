## Customer Matter Management

### Things to Do / Gameplan

## 1. Database
- Install postgres, pgadmin
- Create database
- Create schema
    - schema will assume only one Firm is using the application
- Create tables
    - Users
         - UserId
         - Email addr
         - Optional: firstname
         - Optional: lastname
         - pw
            - must be hashed
         - Firm name
    - Customers -> clients
        - A customer/client is 1:N with matters
        - Name
        - Phone number
        - CustomerId
    - Matters
        - MatterId
        - CustomerId -> Customers/CustomerId
        - Title
        - Description

## 2. Backend API
- Install dotnet 8
- API
    - EFPowerTools
        - Reverse engineer db to setup context and initial migration
    - APIs
        - Customer / Client
            - GET /api/customers → Retrieve a list of customers
            - POST /api/customers → Create a new customer (name, phone)
            - GET /api/customers/{customer_id} → Retrieve details of a customer
            - PUT /api/customers/{customer_id} → Update a customer
            - DELETE /api/customers/{customer_id} → Delete a customer
        - Matter
            - GET /api/customers/{customer_id}/matters → Retrieve matters for a customer
            - POST /api/customers/{customer_id}/matters → Create a matter
            - GET /api/customers/{customer_id}/matters/{matter_id} → Retrieve matter details
        - Authentication
            - POST /api/auth/signup → Create a new user (email, password, firm name)
            - POST /api/auth/login → Login and receive JWT
            - GET /api/auth/me → Return authenticated user info (JWT protected)

## 3. React/Tailwind Frontend
- Install node/npm
- Create react app
    - Use React with TailwindCSS
    - Build a minimal UI with:
        - A login form
        - A list of customers (clicking a customer should show their matters)
        - A form to create new customers
        - A form to create new matters under a customer
        - Optional: user in corner, clicking calls auth/me

## 4. Other
- README must explain how to run the application and get it setup
- API documentation
    - swagger/openapi

## .env file for env vars

### Extra
- Search & Filtering for customers and matters
- Unit Tests (Jest or Mocha for backend)
- Docker Support (Dockerfile + docker-compose)
- Role-Based Access (e.g., admin vs. standard users)
- Improved UI/UX (Better styling, real-time updates)