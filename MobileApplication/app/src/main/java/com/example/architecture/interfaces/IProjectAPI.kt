package com.example.architecture.interfaces

import com.example.architecture.models.CartInfo
import com.example.architecture.models.Order
import com.example.architecture.models.ProductInfo
import com.example.architecture.models.User
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.http.*

// TODO: Split on some interfaces!
interface IProjectAPI {

    // Account
    @POST("api/Account/CreateToken")
    fun PostCreateToken(theEmail: String, thePassword: String)

    // Order part
    @POST("api/Order/Index")
    fun GetOrder(@Body user: User): Call<List<Order>>

    @POST("api/Order/PayForOrder/{cartId}")
    fun PayOrder(@Body cartId: Int, @Path("cartId") id: Int): Call<Order>

    // Product part
    @GET("api/Product/Index")
    fun GetProducts(): Call<List<ProductInfo>>

    @GET("api/Product/Info/{itemId}")
    fun GetProductByID(@Path("itemId") id: Int): Call<ProductInfo>

    // Cart Part
    @POST("api/Cart/Index")
    fun GetCart(@Body user: User): Call<CartInfo>

    @POST("api/Cart/SetItemCount/{itemId}/{count}")
    fun SetItemCount(@Body user: User, @Path("itemId") itemId: Int, @Path("count") count: Int): Call<CartInfo>

    @POST("api/Cart/AddItem/{itemId}/{count}")
    fun AddItemToCart(@Body user: User, @Path("itemId") itemId: Int, @Path("count") count: Int): Call<CartInfo>

    @POST("api/Cart/RemoveItem/{itemId}/{count}")
    fun RemoveItemFromCart(@Body user: User, @Path("itemId") itemId: Int, @Path("count") count: Int): Call<CartInfo>

    //@POST("api/Cart/Checkout")
    //fun Checkout()

    // User part
    @GET("user")
    fun GetUser(): Call<User?>?

    @POST("/api/Account/IsValid")
    fun Authorization(@Body user: User): Call<Boolean?>

    //Image part
    @GET("{PathImg}")
    fun LoadImage(@Path("PathImg") path: String)

}