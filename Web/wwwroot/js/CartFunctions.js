function UpdateProduct(itemId) {
    GetProductInfo(itemId, (productInfo) => {
        let idPrefix = "product-" + itemId;
        let elem = document.getElementById(idPrefix + "-count");
        elem.value = productInfo.countInCart;
    });
}

function AddItem(itemId) {
    $.ajax({
        type: "GET",
        url: "/Cart/AddItem",
        data: { 'itemId': itemId, 'count': 1 },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
            ChangeInputValuePlus();
        },
        error: function (jqXHR, exception) {
            alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}

function RemoveItem(itemId) {
    $.ajax({
        type: "GET",
        url: "/Cart/RemoveItem",
        data: { 'itemId': itemId, 'count': 1 },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
            ChangeInputValueMinus();
        },
        error: function (jqXHR, exception) {
            alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}

function SetItemCount(itemId) {
    var input = document.getElementsByClassName("quantity");
    var count = parseInt(input[0].value);
    count = (count < 0) ? 0 : count;
    $.ajax({
        type: "GET",
        url: "/Cart/SetItemCount",
        data: { 'itemId': itemId, 'count': count },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
        },
        error: function (jqXHR, exception) {
            alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}


function GetProductInfo(itemId, callback) {
    $.ajax({
        type: "GET",
        url: "/api/Product/Info/" + itemId,
        data: {},
        async: true,
        success: function (productInfo) {
            callback(productInfo)
        },
        error: function (jqXHR, exception) {
            alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}

/*function ChangeInputValueMinus() {
    var input = document.getElementsByClassName("quantity");
    var count = parseInt(input[0].value) - 1;
    count = count < 0 ? 0 : count;
    input[0].value = count;
}
function ChangeInputValuePlus() {
    var input = document.getElementsByClassName("quantity");
    var count = parseInt(input[0].value) + 1;
    input[0].value = count;
}*/