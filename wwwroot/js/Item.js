

function loadItemDetails(ItemNumber) {
    var option = document.querySelector(`#items option[value="${ItemNumber}"]`);

    if (option) {
        var itemJSON = option.dataset.item.replace(/&quot;/g, '"');
        var item = JSON.parse(itemJSON);

        var displayHTML = `
        <tr>
            <td colspan="2">
                <h1>Loaded Item<h1>
            </td>
        </tr>
        <table style="margin-top: 10px;">
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
            <td>${item.unitPrice.toFixed(2)}</td>
        </tr>
        <tr>
            <td>Deleted:</td>
            <td>${item.deleted}</td>
        </tr>
        <tr>
            <td>Quantity on Hand:</td>
            <td>${item.quantityOnHand.toFixed(0)}</td>
        </tr>
        <table>
        <tr>
            <td colspan="2">
                <button type="submit" value="Confirm Update" id="confirmDelete" style="width: 100%;">Confirm Delete</button>
            </td>
        </tr>
        `;

        document.getElementById('itemDetails').innerHTML = displayHTML;

        document.getElementById('confirmDelete').addEventListener('click', function () {
            if (isDeleteItemFormValid(document.getElementById('deleteItemForm'))) {
                document.getElementById('deleteItemForm').submit(); // Submit the form
            }
            
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
        <tr>
            <td colspan="2">
                <h1>Loaded Item<h1>
            </td>
        </tr>
        <table style="margin-top: 10px;">
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
                <input type="text" value="${item.unitPrice.toFixed(2)}" id="visibleUnitPrice" />
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
                <input type="text" value="${item.quantityOnHand.toFixed(0)}" id="visibleQuantityOnHand" />
            </td>
        </tr>
        <table>
        <tr>
            <td colspan="2">
                <button type="submit" value="Confirm Update" id="confirmUpdate" style="width: 100%;">Confirm Update</button>
            </td>
        </tr>
        `;

        document.getElementById('itemDetails').innerHTML = displayHTML;

        document.getElementById('confirmUpdate').addEventListener('click', function () {
            updateHiddenItemForm();
            if (isUpdateItemFormValid(document.getElementById('updateItemForm'))) {

                // Submit the form
                document.getElementById('updateItemForm').submit();
            }
        });
    }
    else {
        updateHiddenItemFormEmpty();
        var displayHTML = 'No Item Found.';
        document.getElementById('itemDetails').innerHTML = displayHTML;
    }
}

function updateHiddenItemFormEmpty() {
    // Update hidden form with visible form values

    document.getElementById('hiddenItemNumber').value = "";
    document.getElementById('hiddenDescription').value = "";
    document.getElementById('hiddenUnitPrice').value = "";
    document.getElementById('hiddenDeleted').value = "";
    document.getElementById('hiddenQuantityOnHand').value = "";
}

function updateHiddenItemForm() {
    // Update hidden form with visible form values

    document.getElementById('hiddenItemNumber').value = document.getElementById('visibleItemNumber').value;
    document.getElementById('hiddenDescription').value = document.getElementById('visibleDescription').value;
    document.getElementById('hiddenUnitPrice').value = document.getElementById('visibleUnitPrice').value;
    document.getElementById('hiddenDeleted').value = document.getElementById('visibleDeleted').value;
    document.getElementById('hiddenQuantityOnHand').value = document.getElementById('visibleQuantityOnHand').value;

}

function isUpdateItemFormValid(form) {
    var isValid = true;

    var itemNumber = form.ItemNumber.value;
    var description = form.Description.value;
    var deleted = form.Deleted.value;
    var unitPrice = form.UnitPrice.value;
    var quantityOnHand = form.QuantityOnHand.value;

    var validationMessage = "";

    if (!itemNumber.length > 0) {
        validationMessage += "Item Number is required. ";
        isValid = false;
    }

    if (!description.length > 0) {
        validationMessage += "Description is required. ";
        isValid = false;
    }

    if (!(deleted == "true" || deleted == "false")) {
        validationMessage += "Deleted is required and must be true or false. ";
        isValid = false;
    }

    unitPrice = +unitPrice;

    if (!unitPrice && unitPrice >= 0) {
        validationMessage += "Unit Price is required, must be a decimal and greater or equal to 0. ";
        isValid = false;
    }

    quantityOnHand = +quantityOnHand;

    if (!quantityOnHand && quantityOnHand >= 0) {
        validationMessage += "Quantity on Hand is required. And must be a positive integer.";
        isValid = false;
    }

    document.getElementById('formValidationMessage').textContent = validationMessage;

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}

function isAddItemFormValid(form) {
    var isValid = true;

    var itemNumber = form.ItemNumber.value;
    var description = form.Description.value;
    var unitPrice = form.UnitPrice.value;
    var quantityOnHand = form.QuantityOnHand.value;

    if (!itemNumber.length > 0) {
        document.getElementById('itemNumberValid').textContent = "Item Number is required.";
        isValid = false;
    }

    if (!description.length > 0) {
        document.getElementById('descriptionValid').textContent = "Description is required.";
        isValid = false;
    }

    unitPrice = +unitPrice;

    if (!unitPrice && unitPrice >= 0) {
        document.getElementById('unitPriceValid').textContent = "Unit Price is required, must be a decimal and greater or equal to 0.";
        isValid = false;
    }

    quantityOnHand = +quantityOnHand;

    if (!quantityOnHand && quantityOnHand >= 0) {
        document.getElementById('quantityOnHandValid').textContent = "Quantity on Hand is required. And must be a positive integer.";
        isValid = false;
    }

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}

function isDeleteItemFormValid(form) {
    var isValid = true;

    var itemNumber = form.ItemNumber.value;

    if (!itemNumber.length > 0) {
        document.getElementById('validationMessage').textContent = "ItemNumber is required.";
        isValid = false;
    }

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}