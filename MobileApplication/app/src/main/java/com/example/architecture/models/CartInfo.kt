package com.example.architecture.models

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

// Class that describes a user cart
class CartInfo (theProduct: List<ProductInfo>, deliveryAddress: String){
    @SerializedName("products")
    @Expose
    var products: List<ProductInfo>? = null

    @SerializedName("deliveryAddress")
    @Expose
    var countInCart: String? = null

    init{
        products = theProduct;
        countInCart = deliveryAddress;
    }
}