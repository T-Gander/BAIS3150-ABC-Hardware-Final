function displayItemsInStorage() {
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'))

    var index = 0;
    var displayHTML = '<tr><th>Item Number</th><th>Description</th><th>Quantity</th><th>Unit Price</th><th>Item Total</th></tr>';

    if (itemsArray != null) {
        for (index = 0; index <= itemsArray.length - 1; index++) {
            var item = itemsArray[index][0];
            var quantity = itemsArray[index][1];
            var itemTotal = itemsArray[index][2];
            displayHTML += `
            <tr>
                <td>${item.itemNumber}</td>
                <td>${item.description}</td>
                <td>${quantity}</td> <!--Input quantity-->
                <td>$${item.unitPrice}</td>
                <td>$${itemTotal}</td> <!--Calculated item total-->
            </tr>`;
        }
    }
    else {
        displayHTML += `<tr><td colspan="4">No items have been added.</td></tr>`;
    }

    window.document.getElementById('saleItemsTable').innerHTML = displayHTML;
}

function storeItem(ItemNumber, quantity) {
    var option = document.querySelector(`#items option[value="${ItemNumber}"]`);
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'))

    if (itemsArray == null) {
        itemsArray = [];
    }

    if (option) {
        var itemJSON = option.dataset.item.replace(/&quot;/g, '"');
        var item = JSON.parse(itemJSON);
        var itemTotal = item.unitPrice * quantity

        itemsArray.push([item, quantity, itemTotal]);
        sessionStorage.setItem('ItemsArray', JSON.stringify(itemsArray));
        displayItemsInStorage();
    }
}

function submit() {
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'))
    var saleItemArray = [];
    var index = 0;

    for (index = 0; index <= itemsArray.length - 1; index++) {
        var item = itemsArray[index][0];
        var quantity = +itemsArray[index][1];
        var itemTotal = itemsArray[index][2];

        const saleItem = {
            SaleItemID: 0,
            SaleNumber: 0,
            ItemNumber: item.itemNumber,
            Quantity: quantity,
            ItemTotal: itemTotal
        }

        saleItemArray.push(saleItem);
    }

    document.getElementById('SaleItems').value = JSON.stringify(saleItemArray);
}