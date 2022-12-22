# NUnit Main Features
Repository to practice the main features of NUnit framework

Some of the Tests are based on the exercises proposed on this course: https://testautomationu.applitools.com/nunit-tutorial

## Course repository
The mentioned course has a repository which was used as base for the current repo, It was not forked because the idea was to implement all the classes by hand.
https://github.com/brendanconnolly/TestingWithNUnit


## Project Structure

* **RestfulBooker.Api**: Library that contains the classes to call the RestfulBooker API
* **RestfulBooker.UI**: Library that contains the page objects and needed components to interact with the RestfulBooker application using Selenium
* **TestingWithNUnit.Tests**: Contains some of the exercises proposes on the mentioned course with some minor modifications
  * NUnitCore:
    * Examples of the Basic features that NUnit provides `[Test]`, `[Setup]`, `[TearDown]`, Assertions, Assumptions.
    * Includes examples of DataDriven testing attributes like `[Values]`, `[Pairwise]`, `[Sequential]`, `[Range]`, `[TestCase]`, ` [ValueSource]`, `[TestCaseSource]`
    * Also examples of additional or optional attributes like `[Description]`, `[Category]`, `[Order]`, `[Platform]`, `[Explicit]`, `[Ignore]`
    * It also has an example of how to load test data from a JSON file
    * TestContext and TestResults
    * Parallel execution
  * Api:
    * Contains API tests using the *RestfulBooker.Api* Library
  * UI
    * Contains UI tests using the * RestfulBooker.UI* Library

## Application under test

https://automationintesting.online/

## API documentation
https://restful-booker.herokuapp.com/apidoc/index.html


## Running the test by console


### Run all tests
`dotnet test`

### Running a specific class

`dotnet test --filter FullyQualifiedName~<ClassName>`

### Running the tests related to a specific category

`dotnet test --filter TestCategory=<Category>`

## Generating reports
(Requires *NunitXml.TestLogger* dependency)
` dotnet test --test-adapter-path:. --logger:nunit`
