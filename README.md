# Bookstore API Test Automation Framework

This is an API automation testing framework built with **C#**, **RestSharp**, **NUnit**, and integrated with **GitHub Actions** for continuous integration.  
It covers full CRUD scenarios for both **Books** and **Authors** endpoints using [Fake REST API](https://fakerestapi.azurewebsites.net/).

---

## Technologies Used

- **C# .NET**
- **RestSharp** - HTTP client for API requests
- **NUnit** - Test framework
- **GitHub Actions** - CI/CD pipeline
- **Newtonsoft.Json** - JSON parsing
- **appsettings.json** - Configuration management

---

## Project Structure


```
BookstoreApiTests/
│
├── Clients/           # Contains API clients (BookClient.cs, AuthorClient.cs)
├── Models/            # POCOs (Book.cs, Author.cs)
├── TestData/          # Test data generators (BookGenerator.cs, AuthorGenerator.cs)
├── Tests/             # Test classes (BookTests.cs, AuthorTests.cs)
└── BookstoreApiTests.csproj
```

---

## Features

- Full **CRUD tests** for both `/Books` and `/Authors` endpoints
- Reusable **client classes** for API interaction
- Random test data using generators (`AuthorGenerator`, `BookGenerator`)
- Uses **NUnit** with clear assertions
- Fully integrated with **GitHub Actions CI**: run tests on push
- Uses **GitHub Secrets** to inject `BASE_URL` securely during CI runs
- Built-in **timeout configuration** for API response handling
- Clear separation of concerns via **BaseTest** class for shared setup
- Uploads detailed **test result artifacts** (`.trx`) after each run

---

## How to Setup and Run the Project

### What you need

- [.NET 8 SDK]
- IDE like **Visual Studio**
- Git

---

### Local Setup & Run

1. **Clone the repository**

2. **Open the solution in Visual Studio**

3.  **Update the BASE_URL in appsettings.json for local run:**

   ```
  "BaseUrl": "https://fakerestapi.azurewebsites.net/api/v1/",
   ```

4. **Build the solution**

5. **Open the Test Explorer and Run the tests**:

    (Test > Test Explorer)

    Click Run All Tests

---

## GitHub Actions CI/CD

### Workflow File: `.github/workflows/tests.yml`

- Runs the test suite on every `push` to `master` branch
- Uses `dotnet test` to run tests and show console output
- GitHub Actions generates a **.trx test report**, automatically uploaded as an artifact for test result tracking and debugging

---

### Screenshots of test results/reports/artifact

![GitHub Actions Test Result/Report and Artifact](images/github-actions-report.PNG)

![Local Test Results/Report](images/local-results.PNG)

## Author

Stefan Josimovski

---