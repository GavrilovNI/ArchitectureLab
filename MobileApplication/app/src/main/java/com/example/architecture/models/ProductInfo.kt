package com.example.architecture.models

import com.google.gson.annotations.Expose

import com.google.gson.annotations.SerializedName

class ProductInfo(theProduct: Product, theCountInCart: Int) {
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

class Product(
    theId: Int,
    theName: String,
    thePrice: Float,
    theDescr: String,
    theCount: Int,
    theImage: String
) {
    @SerializedName("id")
    @Expose
    var id: Int? = null

    @SerializedName("name")
    @Expose
    var name: String? = null

    @SerializedName("price")
    @Expose
    var price: Double? = null

    @SerializedName("description")
    @Expose
    var description: String? = null

    @SerializedName("avaliableAmount")
    @Expose
    var avaliableAmount: Int? = null

    @SerializedName("linkToImage")
    @Expose
    var linkToImage: String? = null

    init {
        id = theId;
        name = theName;
        price = thePrice.toDouble();
        description = theDescr;
        avaliableAmount = theCount;
        linkToImage = theImage;
    }
}
