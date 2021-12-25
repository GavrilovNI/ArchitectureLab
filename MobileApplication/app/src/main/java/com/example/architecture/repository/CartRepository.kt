package com.example.architecture.repository

import androidx.lifecycle.MutableLiveData
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.User
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CartRepository private constructor(theCartManagerAPI: CartManagerAPI){
    private val myCartManagerAPI: CartManagerAPI = theCartManagerAPI;
    private val myCarts: MutableLiveData<CartInfo?> = MutableLiveData<CartInfo?>();

    fun SetItemCount(itemId: Int, count: Int){
        myCartManagerAPI.SetItemCount(UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)!!.GetUserO(), itemId, count, object : Callback<CartInfo> {
            override fun onResponse(call: Call<CartInfo>?, response: Response<CartInfo>) {
                if (response.isSuccessful) {
                    val body: CartInfo = response.body() as CartInfo
                    myCarts.value = body;
                } else {
                    myCarts.postValue(null)
                }
            }

            override fun onFailure(call: Call<CartInfo>?, t: Throwable?) {
                myCarts.postValue(null)
            }
        })
    }

    fun AddItemToCart(itemId: Int, count: Int){
        myCartManagerAPI.AddItemToCart(UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)!!.GetUserO(), itemId, count, object : Callback<CartInfo> {
            override fun onResponse(call: Call<CartInfo>?, response: Response<CartInfo>) {
                if (response.isSuccessful) {
                    val body: CartInfo = response.body() as CartInfo
                    myCarts.value = body;
                } else {
                    myCarts.postValue(null)
                }
            }

            override fun onFailure(call: Call<CartInfo>?, t: Throwable?) {
                myCarts.postValue(null)
            }
        })
    }

    fun RemoveItemFromCart(itemId: Int, count: Int){
        myCartManagerAPI.RemoveItemFromCart(UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)!!.GetUserO(), itemId, count, object : Callback<CartInfo> {
            override fun onResponse(call: Call<CartInfo>?, response: Response<CartInfo>) {
                if (response.isSuccessful) {
                    val body: CartInfo = response.body() as CartInfo
                    myCarts.value = body;
                } else {
                    myCarts.postValue(null)
                }
            }

            override fun onFailure(call: Call<CartInfo>?, t: Throwable?) {
                myCarts.postValue(null)
            }
        })
    }

    fun GetSumInCart() : String
    {
        val aProducts = GetProductsCart(
            UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)
                ?.GetUserO()!!
        ).value?.products;
        var sum = 0.0;
        if (!aProducts.isNullOrEmpty())
        {
            for (anIndex in aProducts!!.indices)
            {
                sum += aProducts[anIndex].product!!.price!! * aProducts[anIndex].countInCart!!;
            }
        }
        return sum.toString() + "Р";
    }

    fun GetProductsCart(user: User): MutableLiveData<CartInfo?> {
        myCartManagerAPI.GetProductsInCart(user, object : Callback<CartInfo> {
            override fun onResponse(call: Call<CartInfo?>?, response: Response<CartInfo?>) {
                if (response.isSuccessful) {
                    val body: CartInfo = response.body() as CartInfo;
                    myCarts.value = body;
                    if (body != null && myCarts.value != null)
                    {
                        for (anIndex in myCarts.value!!.products?.indices!!)
                        {
                            myCarts.value!!.products?.get(anIndex)?.countInCart?.let {
                                myCarts.value!!.products?.get(anIndex)?.product?.SetCount(
                                    it
                                )
                            }
                        }
                    }
                } else {
                    myCarts.postValue(null);
                }
            }

            override fun onFailure(call: Call<CartInfo?>?, t: Throwable?) {
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