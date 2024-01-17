
Feature: Order fulfillment

    As an employee working at one of the fulfillment stations 
    I need to be able to see items ordered from my station 
    so I can make them

    Scenario: See new items after an order
        Given I am on the "fulfillment" page
        When a new order is placed for:     
            | store | name   | delivery |
            | 1     | Xavier | 1        |
        And the order contains items:
            | id | quantity |
            | 7  |    2     |
            | 1  |    3     |
            | 4  |    2     |
        And I select the "espresso station"
        Then I should see 3 new items for fulfillment
        And the items should contain:
            | quantity | item_name         |
            | 2        | Espresso Shot     |
            | 3        | Caramel Macchiato |
            | 2        | Mocha Frappuccino |

    Scenario: Fulfill an item
        Given I am on the "fulfillment" page
        When a new order is placed for:     
            | store | name   | delivery |
            | 1     | Xavier | 1        |
        And the order contains items:
            | id | quantity |
            | 7  |    2     |
            | 1  |    3     |
            | 4  |    2     |
        And I select the "espresso station" 
        And I fulfill the "Espresso Shot" item
        Then I should see one fewer items for fulfillment
        And the items should not contain:
            | quantity | item_name         |
            | 2        | Espresso Shot     |
