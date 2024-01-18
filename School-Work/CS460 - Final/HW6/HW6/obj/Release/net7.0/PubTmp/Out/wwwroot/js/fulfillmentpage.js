document.addEventListener('DOMContentLoaded', displayStations);

async function displayStations()
{
    const stationsTemplate = document.getElementById("stationsTemplate");
    const stationsResults = document.getElementById("stationsResults");
    stationsResults.replaceChildren();

    const url = `/api/stations`;
    const response = await fetch(url);

    if(response.ok)
    {
        var stations = await response.json();

        stations.forEach(station => {
            const clone = stationsTemplate.content.cloneNode(true);
            const stationTitle = clone.querySelector('.stationTitle');
            stationTitle.setAttribute('onclick', `displayStationContent(${station.id})`);
            stationTitle.style.cursor = "pointer";
            var h5Element = clone.querySelector('h5');
            h5Element.innerText = station.name;
            stationsResults.appendChild(clone);
        });
    }
    else
    {
        const errorMessage = await response.text();
        stationsResults.innerText = "Error displaying stations, please try again later. Error: " + errorMessage;
    }
}


async function displayStationContent(stationId)
{
    var stationView = document.getElementById('stationView');
    stationView.style.display = "block";
    var h5Element = stationView.querySelector('h5');

    const url = `/api/station/${stationId}`;
    const response = await fetch(url);

    if(response.ok)
    {
        var station = await response.text();
        
        h5Element.innerText = station;
        displayStationItems(stationId);
    }
    else
    {
        const errorMessage = await response.text();
        stationView.innerText = "Error displaying station content, please try again later. Error: " + errorMessage;
    }
}

async function displayStationItems(stationId)
{
    const stationOrdersTemplate = document.getElementById("ordersTemplate");
    const stationOrdersResults = document.getElementById("stationOrdersResults");
    stationOrdersResults.replaceChildren();

    const url = `/api/stationItems/${stationId}`;
    const response = await fetch(url);
    if(response.ok)
    {
        var stationItems = await response.json();
        if(stationItems.length == 0)
        {
            stationOrdersResults.innerText = "No orders to display";
        }
        else
        {
            const clone = stationOrdersTemplate.content.cloneNode(true);
            const items = clone.querySelector(".items");
            stationItems.forEach(item => {
                const div = document.createElement("div");
                div.className = "currentOrdersRow";
                const firstRowItem = document.createElement("div");
                const secondRowItem = document.createElement("div");
                const thirdRowItem = document.createElement("button");
                thirdRowItem.setAttribute("onclick", `completeOrderItem(${item.id}, ${stationId})`);
                firstRowItem.className = "rowItem";
                secondRowItem.className = "rowItem";
                thirdRowItem.className = "rowItem";
    
                firstRowItem.innerText = item.quantity;
                secondRowItem.innerText = item.name;
                thirdRowItem.innerText = "Complete 1";
    
                div.appendChild(firstRowItem);
                div.appendChild(secondRowItem);
                div.appendChild(thirdRowItem);
                items.appendChild(div);
            });
    
            stationOrdersResults.appendChild(clone);
        }
    }
    else
    {
        const errorMessage = await response.text();
        stationOrdersResults.innerText = "Error displaying results, please try again later. Error: " + errorMessage;
    }
}

async function completeOrderItem(id, stationId)
{
    const url = `/api/completeOrderItem/${id}`;
    const response = await fetch(url, {method: 'PUT'});

    if(response.ok)
    {
        displayStationItems(stationId);
    }
    else
    {
        console.error(`Error: ${response.status} ${response.statusText}`);
    }
}