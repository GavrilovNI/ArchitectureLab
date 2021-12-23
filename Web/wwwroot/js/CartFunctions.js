function UpdateProduct(itemId) {
    GetProductInfo(itemId, (productInfo) => {
        let idPrefix = "product-" + itemId;
        let count = document.getElementById(idPrefix + "-count");
        let onCartPage = window.location.href.toLowerCase().endsWith('/cart');
        if (productInfo.countInCart == 0 && onCartPage) {
            let product = document.getElementById(idPrefix);
            product.remove();
        }
        else
        {
            let leftCount = document.getElementById(idPrefix + "-leftCount");
            count.value = productInfo.countInCart;
            leftCount.innerHTML = productInfo.product.avaliableAmount - productInfo.countInCart;
        }
    });
}

function AddItem(itemId) {
    let idPrefix = "product-" + itemId;
    let leftCount = document.getElementById(idPrefix + "-leftCount");
    if (leftCount.innerHTML == 0)
        return;
    $.ajax({
        type: "GET",
        url: "/Cart/AddItem",
        data: { 'itemId': itemId, 'count': 1 },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
        },
        error: function (jqXHR, exception) {
            //alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}

function RemoveItem(itemId) {
    let idPrefix = "product-" + itemId;
    let count = document.getElementById(idPrefix + "-count");
    if (count.value == 0)
        return;
    $.ajax({
        type: "GET",
        url: "/Cart/RemoveItem",
        data: { 'itemId': itemId, 'count': 1 },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
        },
        error: function (jqXHR, exception) {
            //alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
            //https://stackoverflow.com/questions/6792878/jquery-ajax-error-function
        }
    });
}

function SetItemCount(itemId) {
    var input = document.getElementsByClassName("quantity");
    var count = parseInt(input[0].value);

    if (count < 0)
        count = 0;

    $.ajax({
        type: "GET",
        url: "/Cart/SetItemCount",
        data: { 'itemId': itemId, 'count': count },
        async: true,
        success: function (html) {
            UpdateProduct(itemId);
        },
        error: function (jqXHR, exception) {
            //alert('error: ' + jqXHR.status + ' : ' + exception + ' : ' + jqXHR.responseText);
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