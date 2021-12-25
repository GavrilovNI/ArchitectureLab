package com.example.architecture.interfaces

import com.example.architecture.models.CartInfo
import com.example.architecture.models.Order
import com.example.architecture.models.ProductInfo
import com.example.architecture.models.User
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

// API for cart repository
class OrderManagerAPI : IBaseManagerAPI()  {
    companion object {
        @Volatile
        private var apiManager: OrderManagerAPI? = null
        fun GetInstance(): OrderManagerAPI? {
            if (apiManager == null) {
                apiManager = OrderManagerAPI()
            }
            return apiManager
        }
    }

    fun GetOrder(user: User, callback: Callback<List<Order>>) {
        service?.GetOrder(user)?.enqueue(callback)
    }
}