package com.example.architecture.models

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

// Class that describes a user cart
class CartInfo (theProduct: Product, theCountInCart: Int){
    @SerializedName("product")
    @Expose
    var product: Product? = null

    @SerializedName("countInCart")
    @Expose
    var countInCart: Int? = null

    init{
        product = theProduct;
        countInCart = theCountInCart;
    }
}