# ID 1
<hr>

## Title

*As a DD&BB manager or employee I want to have a modern web application for our internal operations where we can see all current orders for one store.*

## Description

This user story will get us going with our internal order fulfillment system.  We don't have anything at all yet.  We want a main home page with our company title and logo and then a "current orders" page.  This page will show the live orders at one of our stores (in a later story we'll have a way to select which store we're viewing but for now we can start with just our flagship store).  On the "current orders" page we need to see all orders that have been placed and not yet fulfilled.  This page will be used by our servers to check that orders are complete before calling out the customer's name, delivering it to them or having it ready after the customer receives a mobile notification.  It will be used by the store manager to see the current order load, allocate resources and estimate how well the team is performing.

Orders are ready to be sent from a separate system we've already developed.  All we have to do is turn that system on.  See details below.  This means we don't have to worry about the system where the orders originate.  This story is only about receiving orders, adding them to our system and displaying them until they're fulfilled.

### Details

1. An order is received as a POST request from our ordering system.  Here is sample data for 5 separate requests:
    ```json
    {"store": 1, "dlvy": 1, "name": "Catherine", "items": [{"id": 7, "qty": 1}]}
    {"store": 1, "dlvy": 2, "name": "Xavier", "items": [{"id": 7, "qty": 2}, {"id": 1, "qty": 3}, {"id": 4, "qty": 2}]}
    {"store": 1, "dlvy": 3, "name": "St\u00e9phanie", "items": [{"id": 5, "qty": 2}, {"id": 9, "qty": 1}, {"id": 10, "qty": 3}]}
    {"store": 1, "dlvy": 2, "name": "Jeffrey", "items": [{"id": 8, "qty": 3}, {"id": 7, "qty": 2}, {"id": 3, "qty": 1}]}
    {"store": 1, "dlvy": 1, "name": "Angela", "items": [{"id": 1, "qty": 2}, {"id": 11, "qty": 1}, {"id": 3, "qty": 1}, {"id": 16, "qty": 3}, {"id": 9, "qty": 2}]}
    ```
    Here, Catherine ordered one item, an Espresso shot from the main counter.  Xavier ordered 2 Espresso shots, 3 Caramel Macchiato's and 2 Mocha Frappuccino from the drive-through.  They're either going to be absolutely wired or have some happy friends!  Stéphanie ordered a bunch of items and will pick it up at the quick walk-in counter.

    For clarification, "store" is the id of the store where this order was directed, "dlvy" is the delivery location, "name" is the customer's name and "items" is a list of items in the order.  Within the item list is the "id" of the item and "qty" is the quantity of that item ordered.  Names must support internationalization as the orders arrive with unicode escaped characters (see the third order above).

    The possibly delivery locations so far are: 1 main counter, 2 drive-through, and 3 walk-in counter.

    Orders arrive at random times and at random intervals which can be configured for testing purposes (see `order_generator.py`)

2. When displaying the current orders, we need to see:
    - the oldest order at the top
    - the customer's name and each of the items ordered (name and quantity, no ids)
    - the total order price
    - the delivery location
    - and some indicator next to each item that tells us its fulfillment status (red/green would be a good choice)

3. Update the current orders at least every 10 seconds

4. Orders are no longer current when all items in them have their fulfillment status set to complete, i.e. they've been made and delivered.

### Implementation Details

1. Use an ApiController with a standard REST endpoint to receive the orders
2. Use AJAX for the current orders page so it's smooth and has no page reloads
3. We don't yet have authentication/authorization capabilities so do not worry about that in this assignment

### Acceptance Criteria
See the associated `.feature` file

### Assumptions/Preconditions
We have a way to get orders into the system for development and testing.

### Dependencies
None

### Effort Points
4

### Owner
Zoe Robb

### Git feature branch name
`BBDD-US-1`

### Modeling and Other Documents

1. Order generator for development and testing: [order_generator.py](order_generator.py)
2. Table of items [items.md](items.md)
3. Database design/model [DbDiagram.io link](ID_1_DBDiagram.png)
4. Front page design/sketch [Front page](ID_1_home_page.png)
5. Current orders page design/sketch [Current Orders](ID_1_current_orders.png)
6. UML diagram of repository/service layer to build [ID_1_UML.md](ID_1_UML_Diagram.png)

### Tasks
1. Create an empty MVC app in a subfolder using the dotnet template
2. Create NUnit testing project
3. Remove Privacy page. Keep home page.
4. Add a current orders page
5. Add the welcoming message and title to the home page
6. Do some data modeling and set up a database in Azure
8. Reverse engineer models and DbContext
9. Use Dr. Morserepostiory and service classes with methods
11. Run the order_generator python script to generate random orders
12. Build the interface for the current orders page
13. Use JavaScript and API Controllers to display information on the current orders page
14. Add Moq to the test project
15. Create Unit tests for any model validation or methods
16. Create Unit tests for any functionality
17. Manually test the interface and functionality