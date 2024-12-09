

function loadItemDetails(ItemNumber) {
    var option = document.querySelector(`#items option[value="${ItemNumber}"]`);

    if (option) {
        var itemJSON = option.dataset.item.replace(/&quot;/g, '"');
        var item = JSON.parse(itemJSON);

        var displayHTML = `
        <h1>Loaded Item<h1>
        <tr>
            <td>Item Number:</td>
            <td>${item.itemNumber}</td>
        </tr>
        <tr>
            <td>Description:</td>
            <td>${item.description}</td>
        </tr>
        <tr>
            <td>Unit Price:</td>
            <td>${item.unitPrice}</td>
        </tr>
        <tr>
            <td>Deleted:</td>
            <td>${item.deleted}</td>
        </tr>
        <tr>
            <td>Quantity on Hand:</td>
            <td>${item.quantityOnHand}</td>
        </tr>
        <tr>
            <input type="submit" value="Confirm Delete" id="confirmDelete"/>
        </tr>
        `;

        document.getElementById('itemDetails').innerHTML = displayHTML;

        document.getElementById('confirmDelete').addEventListener('click', function () {
            document.getElementById('deleteItemForm').submit(); // Submit the form
        });
    }
    else {
        var displayHTML = 'No Item Found.';
        document.getElementById('itemDetails').innerHTML = displayHTML;
    }

}

function loadItemDetailsForm(itemNumber) {
    var option = document.querySelector(`#items option[value="${itemNumber}"]`);

    if (option) {
        var itemJSON = option.dataset.item.replace(/&quot;/g, '"');
        var item = JSON.parse(itemJSON);

        var displayHTML = `
        <h1>Loaded Item<h1>
        <tr>
            <td>Item Number:</td>
            <td>
                <input type="text" value="${item.itemNumber}" id="visibleItemNumber" readonly />
            </td>
        </tr>
        <tr>
            <td>Description:</td>
            <td>
                <input type="text" value="${item.description}" id="visibleDescription" />
            </td>
        </tr>
        <tr>
            <td>Unit Price:</td>
            <td>
                <input type="text" value="${item.unitPrice}" id="visibleUnitPrice" />
            </td>
        </tr>
        <tr>
            <td>Deleted:</td>
            <td>
                <input type="text" value="${item.deleted}" id="visibleDeleted" />
            </td>
        </tr>
        <tr>
            <td>Quantity on Hand:</td>
            <td>
                <input type="text" value="${item.quantityOnHand}" id="visibleQuantityOnHand" />
            </td>
        </tr>
        <tr>
            <input type="submit" value="Confirm Update" id="confirmUpdate"/>
        </tr>
        `;

        document.getElementById('itemDetails').innerHTML = displayHTML;

        document.getElementById('confirmUpdate').addEventListener('click', function () {
            updateHiddenItemForm(); // Submit the form
        });
    }
    else {
        var displayHTML = 'No Item Found.';
        document.getElementById('itemDetails').innerHTML = displayHTML;
    }

}

function updateHiddenItemForm() {
    // Update hidden form with visible form values

    document.getElementById('hiddenItemNumber').value = document.getElementById('visibleItemNumber').value;
    document.getElementById('hiddenDescription').value = document.getElementById('visibleDescription').value;
    document.getElementById('hiddenUnitPrice').value = document.getElementById('visibleUnitPrice').value;
    document.getElementById('hiddenDeleted').value = document.getElementById('visibleDeleted').value;
    document.getElementById('hiddenQuantityOnHand').value = document.getElementById('visibleQuantityOnHand').value;

    // Submit the form
    document.getElementById('updateItemForm').submit();
}