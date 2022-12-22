# NUnit Main Features
Repository to practice the main features of NUnit framework

Some of the Tests are based on the exercises proposed on this course: https://testautomationu.applitools.com/nunit-tutorial

## Course repository
The mentioned course has a repository which was used as base for the current repo, It was not forked because the idea was to implement all the classes by hand.
https://github.com/brendanconnolly/TestingWithNUnit


## Project Structure

* **NUnitBasicFeatures.Tests**: Project that contains mock tests with simple assertions
* **RestfulBooker.Api**: Library that contains the classes to call the RestfulBooker API
* **RestfulBooker.UI**: Library that contains the page objects and needed components to interact with the RestfulBooker application using Selenium
* **TestingWithNUnit.Tests**: Contains some of the exercises proposes on the mentioned course with some minor modifications
  * Includes examples of DataDriven testing attributes like `[Values]`, `[Pairwise]`, `[Sequential]`, `[Range]`, `[TestCase]`, ` [ValueSource]`, `[TestCaseSource]`
  * Also examples of additional or optional attributes like `[Description]`, `[Category]`, `[Order]`, `[Platform]`, `[Explicit]`, `[Ignore]`
  * It also has an example of how to load test data from a JSON file
  * API testing using the previous library
  * UI testing using the previous library

## Application under test

https://automationintesting.online/

## API documentation
https://restful-booker.herokuapp.com/apidoc/index.html
