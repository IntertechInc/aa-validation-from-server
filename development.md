# Development

## Solution structure

### AATestAPI
A small WebAPI Project for testing purposes

One simple controller which can be called by URL: 
http://localhost:52931/api/Validation/GetValidations?dtoObjectName=ValidationTest&jsonObjectName=ObjectName

### AATestSite
A simple web site.

Can be called at http://localhost:52536/app/index.html

TODO:

* Install required dependencies with bower
* End-to-end tests?

### Intertech.Validation
The actual source of the library.

### Intertech.Validation.Test
The test-project with backend unit-tests