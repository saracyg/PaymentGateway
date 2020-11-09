# PaymentGateway

## Solution
This is a sample app simulating payment gateway between a merchant and acquiring bank
Projects:
- BankApiMock: Simple web app with a single endpoint, mocking bank card payment API. It returns a pseudorandom id and operation result.
- PaymentGateway: Web app for processing payments, works as the intermediary between merchants and bank API. It has two endpoints:
	- endpoint accepting payments to process 
	- endpoint allowing to get info about past payments.
	
	Incoming payments data is validated, then passed to bank API. When the bank API responds, the payment info (with the masked card number and without CVV code) with the request result is stored in MongoDB. Then, info about payment id and status is returned to the client.
	
	Requests for payment details are processed by querying mongo for the provided id. If the record is found, it's returned to the client.
- PaymentGateway.Contract: Classes defining communication data format. It serves as a contract between PaymentGateway and BankApiMock as well as between PaymentGateway and the client. 
- PaymentGateway.Tests: Unit tests for PaymentGateway, for processing payment requests, getting payment info, and masking card number.

Framework: .NET Core 3.1

Data storage: MongoDB

Running: Docker

## How to run
In \PaymentGateway directory run `docker-compose build` and then `docker-compose up`
For running without docker, make sure MongoDB is available at the address from connection string in file PaymentGateway\appsettings.json

## Available endpoints:
 - POST 
	
	Address: http://localhost:5002/payment
	
	Headers: Content-Type: application/json
	
	Body example:
```
	{
		"cardNumber":"5280470691255066",
		"cardholderName": "Test Person",
		"expiryMonth": 10,
		"expiryYear": 2021,
		"amount": 30,
		"currency": "USD",
		"cvv": 123
	}
```
 - GET 
	
	Address: http://localhost:5002/payment/{id}

## Space for future work
- Authentication
- Enable https
- Improve exception handling - decide what information should be disclosed to the client and in what form
- Add integration tests 
- Improve input data validation
- Tackle payment data security 
