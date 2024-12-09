﻿

function loadCustomerDetails(customerID) {
    var option = document.querySelector(`#customers option[value="${customerID}"]`);

    if (option) {
        var customerJSON = option.dataset.customer.replace(/&quot;/g, '"');
        var customer = JSON.parse(customerJSON);

        var displayHTML = `
        <h1>Loaded Customer<h1>
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
            <input type="submit" value="Confirm Delete" id="confirmDelete"/>
        </tr>
        `;

        document.getElementById('customerDetails').innerHTML = displayHTML;

        document.getElementById('confirmDelete').addEventListener('click', function () {
            document.getElementById('deleteCustomerForm').submit(); // Submit the form
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
        <h1>Loaded Customer<h1>
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
        <tr>
            <input type="submit" value="Confirm Update" id="confirmUpdate"/>
        </tr>
        `;

        document.getElementById('customerDetails').innerHTML = displayHTML;

        document.getElementById('confirmUpdate').addEventListener('click', function () {
            updateHiddenCustomerForm(); // Submit the form
        });
    }
    else {
        var displayHTML = 'No Customer Found.';
        document.getElementById('customerDetails').innerHTML = displayHTML;
    }

}

function updateHiddenCustomerForm() {
    // Update hidden form with visible form values
    var test = document.getElementById('visibleCustomerID').value;

    document.getElementById('hiddenCustomerID').value = document.getElementById('visibleCustomerID').value;
    document.getElementById('hiddenFirstName').value = document.getElementById('visibleFirstName').value;
    document.getElementById('hiddenLastName').value = document.getElementById('visibleLastName').value;
    document.getElementById('hiddenAddress').value = document.getElementById('visibleAddress').value;
    document.getElementById('hiddenCity').value = document.getElementById('visibleCity').value;
    document.getElementById('hiddenProvince').value = document.getElementById('visibleProvince').value;
    document.getElementById('hiddenPostalCode').value = document.getElementById('visiblePostalCode').value;

    // Submit the form
    document.getElementById('updateCustomerForm').submit();
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

    return isValid;
}

function isUpdateCustomerFormValid(form) {
    var test = 0;
}