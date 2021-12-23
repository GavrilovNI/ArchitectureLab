package com.example.architecture.models

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class Order {
    @SerializedName("id")
    @Expose
    var id: Int? = null;

    @SerializedName("userId")
    @Expose
    var userId: String? = null;

    @SerializedName("time")
    @Expose
    var time: String? = null;

    @SerializedName("boughtProducts")
    @Expose
    var bouProducts: List<BoughtProduct>? = null;

    @SerializedName("boughtProducts")
    @Expose
    var deliveryAddres: String? = null;

}

class BoughtProduct(
    theId: Int,
    theCount: Int,
    thePrice: Float,
    theStatus: Int) {

    @SerializedName("productId")
    @Expose
    var productId: Int? = null

    @SerializedName("price")
    @Expose
    var price: Double? = null

    @SerializedName("count")
    @Expose
    var count: Int? = null

    @SerializedName("paidStatus")
    @Expose
    var status: Int? = null

    init {
        productId = theId;
        price = thePrice.toDouble();
        count = theCount;
        status = theStatus;
    }
}