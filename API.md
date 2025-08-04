# Auth
- Endpoint: GET api/auth/test

    Description: Checks if the AuthController is functioning correctly.

    Parameters: None

    Returns: 200 OK with a message "AuthController is working!"

- Endpoint: POST api/auth/signup

    Description: Registers a new user with the provided details.

    Parameters:
    - Body parameter: UserSignUpDTO userDto (contains Email, Password, FirstName, LastName, FirmName)

    Returns:
    - 200 OK with a message "Sign-up successful!" if registration is successful.
    - 400 Bad Request if the user data is invalid or if a user with the same email already exists.

- Endpoint: POST api/auth/login

    Description: Authenticates a user and generates a JWT token if the credentials are valid.

    Parameters:
    - Body parameter: UserLoginDto userLoginDto (contains Email, Password)

    Returns:
    - 200 OK with a JWT token if login is successful.
    - 400 Bad Request if the login data is invalid.
    - 401 Unauthorized if the email or password is incorrect.
- Endpoint: POST api/auth/me

    Description: Retrieves the current user's details based on the JWT token.

    Parameters: None (requires authorization)

    Returns:
    - 200 OK with user details if the user is found.
    - 404 Not Found if the user does not exist.

# Customer
- Endpoint: GET api/customers
    Description: Retrieves a list of all customers.

    Parameters: None

    Returns: 200 OK with a list of customers.

- Endpoint: POST api/customers

    Description: Creates a new customer with the provided name and phone number.

    Parameters:
    - Body parameter: CustomerDTO customerDto (contains Name, PhoneNum)

    Returns:
    - 201 Created with the created customer details.
    - 400 Bad Request if the customer data is invalid.

- Endpoint: GET api/customers/{customer_id}

    Description: Retrieves details of a specific customer by their ID.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)

    Returns:
    - 200 OK with the customer details if found.
    - 404 Not Found if the customer does not exist.

- Endpoint: PUT api/customers/{customer_id}

    Description: Updates the details of a specific customer.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)
    - Body parameter: CustomerDTO customerDto (contains updated Name, PhoneNum)

    Returns:
    - 200 OK with the updated customer details if successful.
    - 400 Bad Request if the customer data is invalid.
    - 404 Not Found if the customer does not exist.

- Endpoint: DELETE api/customers/{customer_id}

    Description: Deletes a specific customer by their ID.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)
    
    Returns:
    - 200 OK if the deletion is successful.
    - 404 Not Found if the customer does not exist.

- Endpoint: GET api/customers/{customer_id}/matters

    Description: Retrieves all matters associated with a specific customer.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)
    Returns:
    - 200 OK with a list of matters if the customer is found.
    - 404 Not Found if the customer does not exist.

- Endpoint: POST api/customers/{customer_id}/matters

    Description: Creates a new matter for a specific customer.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)
    - Body parameter: MatterDTO matterDto (contains Title, Description)
    
    Returns:
    - 201 Created with the created matter details.
    - 400 Bad Request if the matter data is invalid.
    - 404 Not Found if the customer does not exist.

- Endpoint: GET api/customers/{customer_id}/matters/{matter_id}

    Description: Retrieves details of a specific matter associated with a customer.

    Parameters:
    - Path parameter: customer_id (the ID of the customer)
    - Path parameter: matter_id (the ID of the matter)

    Returns:
    - 200 OK with the matter details if found.
    - 404 Not Found if the customer or matter does not exist.