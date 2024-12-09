

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

