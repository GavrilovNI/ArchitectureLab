package com.example.architecture.repository

import androidx.lifecycle.MutableLiveData
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.models.CartInfo
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CartRepository private constructor(theCartManagerAPI: CartManagerAPI){
    private val myCartManagerAPI: CartManagerAPI = theCartManagerAPI;
    private val myCarts: MutableLiveData<List<CartInfo>?> = MutableLiveData<List<CartInfo>?>();

    fun GetProductsCart(): MutableLiveData<List<CartInfo>?> {
        myCartManagerAPI.GetProductsInCart(object : Callback<List<CartInfo>> {
            override fun onResponse(call: Call<List<CartInfo>?>?, response: Response<List<CartInfo>?>) {
                if (response.isSuccessful) {
                    val body: List<CartInfo> = response.body() as List<CartInfo>;
                    myCarts.setValue(body);
                } else {
                    myCarts.postValue(null);
                }
            }

            override fun onFailure(call: Call<List<CartInfo>?>?, t: Throwable?) {
                myCarts.postValue(null)
            }
        });
        return myCarts;
    }

    companion object {
        @Volatile
        private var instance: CartRepository? = null;
        fun GetInstance(theCartManagerAPI: CartManagerAPI): CartRepository? {
            if (instance == null) {
                instance = CartRepository(theCartManagerAPI);
            }
            return instance;
        }
    }
}