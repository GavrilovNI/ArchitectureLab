package com.example.architecture.repository

import androidx.lifecycle.MutableLiveData
import com.example.architecture.interfaces.OrderManagerAPI
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.Order
import com.example.architecture.models.User
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class OrderRepository private constructor(theOrderManagerAPI: OrderManagerAPI){
    private val myIrderManagerAPI: OrderManagerAPI = theOrderManagerAPI;
    private val myOrder: MutableLiveData<List<Order>?> = MutableLiveData<List<Order>?>();

    fun GetOrder(): MutableLiveData<List<Order>?> {
        val user = UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)?.GetUserO()!!;
        myIrderManagerAPI.GetOrder(user, object : Callback<List<Order>> {
            override fun onResponse(call: Call<List<Order>?>?, response: Response<List<Order>?>) {
                if (response.isSuccessful) {
                    val body: List<Order> = response.body() as List<Order>;
                    myOrder.value = body;
                } else {
                    myOrder.postValue(null);
                }
            }

            override fun onFailure(call: Call<List<Order>?>?, t: Throwable?) {
                myOrder.postValue(null)
            }
        });
        return myOrder;
    }

    companion object {
        @Volatile
        private var instance: OrderRepository? = null;
        fun GetInstance(theOrderManagerAPI: OrderManagerAPI): OrderRepository? {
            if (instance == null) {
                instance = OrderRepository(theOrderManagerAPI);
            }
            return instance;
        }
    }
}