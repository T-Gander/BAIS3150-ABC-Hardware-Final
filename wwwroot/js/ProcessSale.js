function displayItemsInStorage() {
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'));
    var subtotal = +JSON.parse(sessionStorage.getItem('Subtotal'));
    var gst = +JSON.parse(sessionStorage.getItem('GST'));
    var saleTotal = +JSON.parse(sessionStorage.getItem('SaleTotal'));

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
                <td>$${item.unitPrice.toFixed(2)}</td>
                <td>$${itemTotal.toFixed(2)}</td> <!--Calculated item total-->
            </tr>`;
        }
    }
    else {
        displayHTML += `<tr><td colspan="4">No items have been added.</td></tr>`;
    }

    window.document.getElementById('saleItemsTable').innerHTML = displayHTML;
    window.document.getElementById('subtotal').textContent = `$${subtotal.toFixed(2)}`;
    window.document.getElementById('gst').textContent = `$${gst.toFixed(2)}`;
    window.document.getElementById('saleTotal').textContent = `$${saleTotal.toFixed(2)}`;
}

function storeItem(ItemNumber, quantity) {
    var option = document.querySelector(`#items option[value="${ItemNumber}"]`);
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'));
    var subtotal = +JSON.parse(sessionStorage.getItem('Subtotal'));
    var gst = +JSON.parse(sessionStorage.getItem('GST'));
    var saleTotal = +JSON.parse(sessionStorage.getItem('SaleTotal'));

    if (itemsArray == null) {
        itemsArray = [];
    }

    if (subtotal == null) {
        subtotal = 0;
    }

    if (gst == null) {
        gst = 0;
    }

    if (saleTotal == null) {
        saleTotal = 0;
    }

    if (option) {
        var itemJSON = option.dataset.item.replace(/&quot;/g, '"');
        var item = JSON.parse(itemJSON);

        document.getElementById('itemValid').textContent = "";
        document.getElementById('quantityValid').textContent = "";
        document.getElementById('itemNumberValid').textContent = "";

        var validItem = checkSaleItem(item, ItemNumber, +quantity);

        if (validItem) {

            var itemTotal = item.unitPrice * quantity
            subtotal += itemTotal;
            gst = subtotal * 0.05;
            saleTotal = subtotal + gst;

            itemsArray.push([item, quantity, itemTotal]);
            sessionStorage.setItem('ItemsArray', JSON.stringify(itemsArray));
            sessionStorage.setItem('Subtotal', JSON.stringify(subtotal));
            sessionStorage.setItem('GST', JSON.stringify(gst));
            sessionStorage.setItem('SaleTotal', JSON.stringify(saleTotal));

            displayItemsInStorage();
        }
    }
    else {
        document.getElementById('itemNumberValid').textContent = "Invalid item selected.";
    }
}

function checkSaleItem(item, itemNumber, quantity) {

    var isValid = true;

    document.getElementById('itemValid').textContent = "";

    if (!item.description || item.description.length == 0 || item.quantityOnHand <= 0 || item.unitPrice < 0) {
        //Validation
        document.getElementById('itemValid').textContent = "Invalid item detected, ensure selected item exists and in stock.";
        isValid = false;
    }

    if (!itemNumber || itemNumber.length == 0) {
        //Validation
        document.getElementById('itemNumberValid').textContent = "Invalid itemNumber detected, ensure selected item number exists."
        isValid = false;
    }

    if (quantity <= 0) {
        //Validation
        document.getElementById('quantityValid').textContent = "Invalid quantity detected, ensure quantity is valid and not negative."
        isValid = false;
    }

    return isValid;
}

function submit() {
    var itemsArray = JSON.parse(sessionStorage.getItem('ItemsArray'))
    var subtotal = +JSON.parse(sessionStorage.getItem('Subtotal'));
    var gst = +JSON.parse(sessionStorage.getItem('GST'));
    var saleTotal = +JSON.parse(sessionStorage.getItem('SaleTotal'));

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
    document.getElementById('SubTotal').value = subtotal;
    document.getElementById('SaleTotal').value = saleTotal;
    document.getElementById('GST').value = gst;

    if (isValid) {
        sessionStorage.clear();
    }

    return true;
}

function loadCustomerDetails(customerID) {
    var option = document.querySelector(`#customers option[value="${customerID}"]`);

    if (option) {
        var customerJSON = option.dataset.customer.replace(/&quot;/g, '"');
        var customer = JSON.parse(customerJSON);

        var displayHTML = `
        <h3>Loaded Customer<h3>
        <tr>
            <td>CustomerID:</td>
            <td>${customer.customerID}</td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td>${customer.firstName}</td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td>${customer.lastName}</td>
        </tr>
        <tr>
            <td>Address:</td>
            <td>${customer.address}</td>
        </tr>
        <tr>
            <td>City:</td>
            <td>${customer.city}</td>
        </tr>
        <tr>
            <td>Province:</td>
            <td>${customer.province}</td>
        </tr>
        <tr>
            <td>PostalCode:</td>
            <td>${customer.postalCode}</td>
        </tr>`;

        document.getElementById('customerDetails').innerHTML = displayHTML;
        sessionStorage.setItem('CustomerID', JSON.stringify(customer.customerID));
    }
    else {
        var displayHTML = 'No Customer Found.';
        document.getElementById('customerDetails').innerHTML = displayHTML;
        sessionStorage.removeItem('CustomerID');
    }

}

function loadSalespersonDetails(salespersonID) {
    var option = document.querySelector(`#salespeople option[value="${salespersonID}"]`);

    if (option) {
        var salespersonJSON = option.dataset.salesperson.replace(/&quot;/g, '"');
        var salesperson = JSON.parse(salespersonJSON);

        var displayHTML = `
        <h3>Loaded Salesperson<h3>
        <tr>
            <td>Salesperson ID:</td>
            <td>${salesperson.salespersonID}</td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td>${salesperson.firstName}</td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td>${salesperson.lastName}</td>
        </tr>
        `;

        document.getElementById('salespersonDetails').innerHTML = displayHTML;
        sessionStorage.setItem('SalespersonID', JSON.stringify(salesperson.salespersonID));
    }
    else {
        var displayHTML = 'No Salesperson Found.';
        document.getElementById('salespersonDetails').innerHTML = displayHTML;
        sessionStorage.removeItem('SalespersonID');
    }

}

function isProcessSaleFormValid(form) {

    var isValid = true;
    var subtotal = +JSON.parse(sessionStorage.getItem('Subtotal'));
    var gst = +JSON.parse(sessionStorage.getItem('GST'));
    var saleTotal = +JSON.parse(sessionStorage.getItem('SaleTotal'));
    var salespersonID = JSON.parse(sessionStorage.getItem('SalespersonID'));
    var customerID = JSON.parse(sessionStorage.getItem('CustomerID'));

    var validationMessage = "";

    if (!salespersonID) {
        validationMessage += "SalespersonID is required. Make sure you loaded their details. ";
        isValid = false;
    }

    if (!customerID) {
        validationMessage += "CustomerID is required. Make sure you loaded their details. ";
        isValid = false;
    }

    if (!subtotal || !gst || !saleTotal) {
        validationMessage += "Sale was unable to be parsed. Cancelling form submission. If there are no other validation messages and there are items in your order, Contact Support. "
        isValid = false;
    }

    document.getElementById('processFormValid').textContent = validationMessage;

    return isValid;
}

function submitValidation() {
    if (isProcessSaleFormValid(document.getElementById('processSaleForm'))) {
        return submit();
    }
    else {
        return false; //Sorry I was getting lazy at this point.
    }
}