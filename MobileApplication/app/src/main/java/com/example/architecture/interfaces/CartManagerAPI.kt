package com.example.architecture.interfaces

import com.example.architecture.models.CartInfo
import com.example.architecture.models.ProductInfo
import com.example.architecture.models.User
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

// API for cart repository
class CartManagerAPI : IBaseManagerAPI()  {
    companion object {
        @Volatile
        private var apiManager: CartManagerAPI? = null
        fun GetInstance(): CartManagerAPI? {
            if (apiManager == null) {
                apiManager = CartManagerAPI()
            }
            return apiManager
        }
    }

    fun GetProductsInCart(user: User, callback: Callback<CartInfo>) {
        service?.GetCart(user)?.enqueue(callback)
    }

    fun SetItemCount(user: User, itemId: Int, count: Int, callback: Callback<CartInfo>){
        service?.SetItemCount(user, itemId, count)?.enqueue(callback)
    }

    fun AddItemToCart(user: User, itemId: Int, count: Int, callback: Callback<CartInfo>){
        service?.AddItemToCart(user, itemId, count)?.enqueue(callback)
    }

    fun RemoveItemFromCart(user: User, itemId: Int, count: Int, callback: Callback<CartInfo>){
        service?.RemoveItemFromCart(user, itemId, count)?.enqueue(callback)
    }
}