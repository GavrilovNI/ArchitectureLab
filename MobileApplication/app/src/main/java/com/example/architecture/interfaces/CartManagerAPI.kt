package com.example.architecture.interfaces

import com.example.architecture.models.CartInfo
import retrofit2.Callback

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

    fun GetProductsInCart(callback: Callback<List<CartInfo>>) {
        service?.GetCart()?.enqueue(callback)
    }
}