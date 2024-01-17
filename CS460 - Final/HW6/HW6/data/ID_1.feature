# -- This file is best viewed with a Gherkin syntax highlighter, e.g. Cucumber (Gherkin) Full Support extension in VS Code

Feature: View Home Page

    As a user
    I want to view the home page
    So that I can see what the site is about

    Scenario: View Home Page
        Given I am on the "home" page
        Then I should see "Welcome to Doughnut Dreams & Brewed Beans!"
    
Feature: View Current Orders

    As a user
    I want to view all current orders
    So that I can see what orders are currently being processed

    Scenario: View a new order
        Given I am on the "current orders" page
        When a new order is placed for:     
            | store | name   | delivery |
            | 1     | Xavier | 1        |
        And the order contains items:
            | id | quantity |
            | 7  |    2     |
            | 1  |    3     |
            | 4  |    2     |
        Then I should see an order for "Xavier" 
        And the order should contain:
            | quantity | item_name         | status      |
            | 2        | Espresso Shot     | In Progress |
            | 3        | Caramel Macchiato | In Progress |
            | 2        | Mocha Frappuccino | In Progress |
        And I should see a total price of "$30.93"
        And I should see a delivery location of "main counter"
