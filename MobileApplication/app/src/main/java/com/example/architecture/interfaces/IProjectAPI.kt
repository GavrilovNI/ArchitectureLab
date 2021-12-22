package com.example.architecture.interfaces

import com.example.architecture.models.CartInfo
import com.example.architecture.models.ProductInfo
import com.example.architecture.models.User
import retrofit2.http.GET
import retrofit2.Call
import retrofit2.http.POST
import retrofit2.http.Path

// TODO: Split on some interfaces!
interface IProjectAPI {

    // Account
    @POST("api/Account/CreateToken")
    fun PostCreateToken(theEmail: String, thePassword: String)

    // Image part

    // Order part
    //@GET("api/Order/Index")
    //fun GetOrder(): Call<List<Order>>

    // Product part
    @GET("api/Product/Index")
    fun GetProducts(): Call<List<ProductInfo>>

    @GET("api/Product/Info/{itemId}")
    fun GetProductByID(@Path("itemId") id: Int): Call<ProductInfo>

//    @GET("api/Product/Edit")
//    fun EditProduct(): Call<List<ProductInfo>>

    // Cart Part
    @GET("api/Cart/Index")
    fun GetCart(): Call<List<CartInfo>>

    // User part
    @GET("user")
    fun GetUser(): Call<User?>?
}