// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener('DOMContentLoaded', displayOrders);

// Run the method every 10 seconds
setInterval(displayOrders, 10000);

async function displayOrders()
{
    const updateUrl = `/api/updateOrders`;
    const updateResponse = await fetch(updateUrl, {
        method: 'put'
    });

    const currentOrdersTemplate = document.getElementById("currentOrdersTemplate");
    const currentOrdersResults = document.getElementById("currentOrdersResults");
    currentOrdersResults.replaceChildren();

    const url = `/api/orders`;
    const response = await fetch(url);
    console.log(response);

    if(response.ok)
    {
        var orders = await response.json();
        if(orders.length == 0)
        {
            currentOrdersResults.innerText = "No orders to display";
        }
        else
        {
            for(const order of orders)
            {
                await displaySingleOrder(order);
            }
        }
    } 
    else
    {
        const errorMessage = await response.text();
        currentOrdersResults.innerText = "Error displaying orders, please try again later. Error: " + errorMessage;
    }
}

async function displaySingleOrder(order)
{
    const clone = currentOrdersTemplate.content.cloneNode(true);
    const title = clone.querySelector(".rowTitle");
    const totalPrice = clone.querySelector(".totalPrice");
    const location = clone.querySelector(".location");
    const items = clone.querySelector(".items");
    
    title.innerText = order.name;
    totalPrice.innerHTML = "<strong>Total Price: $" + order.totalPrice + "</strong>";
    location.innerHTML = "<strong>" + order.deliveryName + "</strong>";

    const orderedItemsUrl = `/api/orderedItems/${order.id}`;
    const orderedItemsResponse = await fetch(orderedItemsUrl);
    
    if(orderedItemsResponse.ok)
    {
        var ordereredItems = await orderedItemsResponse.json();
        ordereredItems.forEach(item => {
            const div = document.createElement("div");
            div.className = "currentOrdersRow";
            const firstRowItem = document.createElement("div");
            const secondRowItem = document.createElement("div");
            const thirdRowItem = document.createElement("div");
            firstRowItem.className = "rowItem";
            secondRowItem.className = "rowItem";
            thirdRowItem.className = "rowItem";

            firstRowItem.innerText = item.quantity;
            secondRowItem.innerText = item.name;
            if(item.complete)
            {
                thirdRowItem.innerText = "Complete";
                thirdRowItem.classList.add('green');
            }
            else
            {
                thirdRowItem.innerText = "In Progress";
                thirdRowItem.classList.add('red');
            }

            div.appendChild(firstRowItem);
            div.appendChild(secondRowItem);
            div.appendChild(thirdRowItem);
            items.appendChild(div);
        });
    }
    else
    {
        const errorMessage = await orderedItemsResponse.text();
        items.innerText = "Error displaying items, please try again later. Error: " + errorMessage;

    }
    
    currentOrdersResults.appendChild(clone);
}