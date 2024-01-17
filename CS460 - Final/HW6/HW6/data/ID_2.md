# ID 2
<hr>

## Title

*As an employee working at one of the fulfillment stations I need to be able to see items ordered from my station so I can make them*

## Description

An employee is working at a fulfillment station, let's say the espresso station.  Orders come in from the order system that may contain espresso orders.  Each item that needs to be made at my station should be displayed on my screen as it comes in.  I'll see that, pull shots of espresso as needed to make the coffee item, send it off to the integration area, and then use the screen to check off that the item has been made and delivered.  Once checked off it disappears from my screen, keeping the display showing only items that I need to make. 

### Details

1. We have multiple stations so the fulfillment page needs to have a way to select which station the employee is working at.
2. Items need to be displayed as soon as they come in
3. Show the item name, description and quantity
4. Items need to be able to be checked off in a very simple manner to keep things moving efficiently, i.e. one click
5. Items must of course match the station being displayed (can't have a chocolate donut being displayed at the soft drink station!)

### Implementation Details

1. Use AJAX to display the orders
2. Update the page frequently

### Acceptance Criteria
See the associated `.feature` file

### Assumptions/Preconditions
We need to know the possible fulfillment stations and which items go with which station

### Dependencies
ID_1

### Effort Points
4

### Owner
Zoe Robb

### Git feature branch name
`BBDD-US-2`

### Modeling and Other Documents

1. UI sketch [Fulfillment_page_sketch](Fulfillment_page_sketch.png)
2. Table showing fulfillment stations and items that go to each station (Table_of_stations)[Table_of_stations.md](Table_of_stations.md), updated item list [items.md](items.md)
3. Updated database model [DbDiagram.io link](ID_1_DBDiagram.png) **Didn't really need updated, MenuItems already have a station ID and the station table exists**
4. UML diagram of repository/service layer to build [ID_2_UML.md](ID_2_UML.png)

### Tasks

1. Build a repository for stations
2. Scaffold any new model information
3. Update the Database seed to properly include the different stations and link the items to them through the station id
4. Write methods to get a station's name and methods to link up the items and stations
5. Build the fulfillment page and use thE API Controller and JavaScript to display the needed content and add functions to button clicks
6. Add to the unit test project file any tests needed for the fulfillment page
7. Create the tests
8. Manually test the interface and functionality