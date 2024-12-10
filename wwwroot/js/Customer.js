

function loadCustomerDetails(customerID) {
    var option = document.querySelector(`#customers option[value="${customerID}"]`);

    if (option) {
        var customerJSON = option.dataset.customer.replace(/&quot;/g, '"');
        var customer = JSON.parse(customerJSON);

        var displayHTML = `
        <tr>
        <td colspan="2">
            <h1>Loaded Customer<h1>
        </td>
        </tr>
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
        </tr>
        <tr>
            <td colspan="2">
                <button type="submit" value="Confirm Delete" id="confirmDelete" style="width: 100%;">Confirm Delete</button>
            </td>
        </tr>
        `;

        document.getElementById('customerDetails').innerHTML = displayHTML;

        document.getElementById('confirmDelete').addEventListener('click', function () {
            if (isDeleteCustomerFormValid(document.getElementById('deleteCustomerForm'))) {
                // Submit the form
                document.getElementById('deleteCustomerForm').submit();
            }
        });
    }
    else {
        var displayHTML = 'No Customer Found.';
        document.getElementById('customerDetails').innerHTML = displayHTML;
    }

}

function loadCustomerDetailsForm(customerID) {
    var option = document.querySelector(`#customers option[value="${customerID}"]`);

    if (option) {
        var customerJSON = option.dataset.customer.replace(/&quot;/g, '"');
        var customer = JSON.parse(customerJSON);

        var displayHTML = `
        <tr>
        <td colspan="2">
            <h1>Loaded Customer<h1>
        </td>
        </tr>
        <form id="loadedCustomer" method="post">
        <table style="margin-top: 10px;">
        <tr>
            <td>CustomerID:</td>
            <td>
                <input type="text" value="${customer.customerID}" id="visibleCustomerID" readonly />
            </td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td>
                <input type="text" value="${customer.firstName}" id="visibleFirstName" />
            </td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td>
                <input type="text" value="${customer.lastName}" id="visibleLastName" />
            </td>
        </tr>
        <tr>
            <td>Address:</td>
            <td>
                <input type="text" value="${customer.address}" id="visibleAddress" />
            </td>
        </tr>
        <tr>
            <td>City:</td>
            <td>
                <input type="text" value="${customer.city}" id="visibleCity" />
            </td>
        </tr>
        <tr>
            <td>Province:</td>
            <td>
                <input type="text" value="${customer.province}" id="visibleProvince" />
            </td>
        </tr>
        <tr>
            <td>PostalCode:</td>
            <td>
                <input type="text" value="${customer.postalCode}" id="visiblePostalCode" />
            </td>
        </tr>
        </table>
        <tr>
            <td colspan="2">
                <button type="submit" value="Confirm Update" id="confirmUpdate" style="width: 100%;">Confirm Update</button>
            </td>
        </tr>
        </form>
        `;

        document.getElementById('customerDetails').innerHTML = displayHTML;

        document.getElementById('confirmUpdate').addEventListener('click', function () {
            updateHiddenCustomerForm();
            if (isUpdateCustomerFormValid(document.getElementById('updateCustomerForm'))) {
                // Submit the form
                document.getElementById('updateCustomerForm').submit();
            }
        });
    }
    else {
        updateHiddenCustomerFormEmpty();
        var displayHTML = 'No Customer Found.';
        document.getElementById('customerDetails').innerHTML = displayHTML;
    }

}

function updateHiddenCustomerFormEmpty() {
    // Update hidden form with visible form values

    document.getElementById('hiddenCustomerID').value = "";
    document.getElementById('hiddenFirstName').value = "";
    document.getElementById('hiddenLastName').value = "";
    document.getElementById('hiddenAddress').value = "";
    document.getElementById('hiddenCity').value = "";
    document.getElementById('hiddenProvince').value = "";
    document.getElementById('hiddenPostalCode').value = "";
}

function updateHiddenCustomerForm() {
    // Update hidden form with visible form values

    document.getElementById('hiddenCustomerID').value = document.getElementById('visibleCustomerID').value;
    document.getElementById('hiddenFirstName').value = document.getElementById('visibleFirstName').value;
    document.getElementById('hiddenLastName').value = document.getElementById('visibleLastName').value;
    document.getElementById('hiddenAddress').value = document.getElementById('visibleAddress').value;
    document.getElementById('hiddenCity').value = document.getElementById('visibleCity').value;
    document.getElementById('hiddenProvince').value = document.getElementById('visibleProvince').value;
    document.getElementById('hiddenPostalCode').value = document.getElementById('visiblePostalCode').value;

}

function isUpdateCustomerFormValid(form) {

    var isValid = true;

    var customerID = form.CustomerID.value;
    var firstName = form.FirstName.value;
    var lastName = form.LastName.value;
    var address = form.Address.value;
    var city = form.City.value;
    var province = form.Province.value;
    var postalCode = form.PostalCode.value;
    var postalCodeRegex = /^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] [0-9][A-Z][0-9]$/i;

    var validationMessage = "";

    if (!customerID.length > 0) {
        validationMessage += "Ensure Customer was loaded: "
    }

    if (!firstName.length > 0) {
        validationMessage += "First Name is required. ";
        isValid = false;
    }

    if (!lastName.length > 0) {
        validationMessage += "Last Name is required. ";
        isValid = false;
    }

    if (!address.length > 0) {
        validationMessage += "Address is required. ";
        isValid = false;
    }

    if (!city.length > 0) {
        validationMessage += "City is required. ";
        isValid = false;
    }

    if (!province.length > 0) {
        validationMessage += "Province is required. ";
        isValid = false;
    }

    if (!postalCode.length > 0 && !postalCodeRegex.test(postalCode)) {
        validationMessage += "Postal Code is required in the format A1A 1A1.";
        isValid = false;
    }

    document.getElementById('formValidationMessage').textContent = validationMessage;

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}

function isDeleteCustomerFormValid(form) {
    var isValid = true;

    var customerID = form.CustomerID.value;

    if (!customerID.length > 0) {
        document.getElementById('validationMessage').textContent = "CustomerID is required.";
        isValid = false;
    }

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}

function isAddCustomerFormValid(form) {
    var isValid = true;

    var firstName = form.FirstName.value;
    var lastName = form.LastName.value;
    var address = form.Address.value;
    var city = form.City.value;
    var province = form.Province.value;
    var postalCode = form.PostalCode.value;
    var postalCodeRegex = /^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] [0-9][A-Z][0-9]$/i;

    if (!firstName.length > 0) {
        document.getElementById('firstNameValid').textContent = "First Name is required.";
        isValid = false;
    }

    if (!lastName.length > 0) {
        document.getElementById('lastNameValid').textContent = "Last Name is required.";
        isValid = false;
    }

    if (!address.length > 0) {
        document.getElementById('addressValid').textContent = "Address is required.";
        isValid = false;
    }

    if (!city.length > 0) {
        document.getElementById('cityValid').textContent = "City is required.";
        isValid = false;
    }

    if (!province.length > 0) {
        document.getElementById('provinceValid').textContent = "Province is required.";
        isValid = false;
    }

    if (!postalCode.length > 0 && !postalCodeRegex.test(postalCode)) {
        document.getElementById('postalCodeValid').textContent = "Postal Code is required in the format A1A 1A1.";
        isValid = false;
    }

    if (isValid) {
        sessionStorage.clear();
    }

    return isValid;
}