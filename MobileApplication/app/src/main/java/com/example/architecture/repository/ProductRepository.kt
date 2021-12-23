package com.example.architecture.repository

import androidx.lifecycle.MutableLiveData
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.models.ProductInfo
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ProductRepository private constructor(theProductManagerAPI: ProductManagerAPI) {
    private val myProductManagerAPI: ProductManagerAPI = theProductManagerAPI;
    private val myProducts: MutableLiveData<List<ProductInfo>?> = MutableLiveData<List<ProductInfo>?>();
    private val myProductByID: MutableLiveData<ProductInfo?> = MutableLiveData<ProductInfo?>();

    fun GetProducts(): MutableLiveData<List<ProductInfo>?> {
        myProductManagerAPI.GetProducts(object : Callback<List<ProductInfo>> {
            override fun onResponse(call: Call<List<ProductInfo>?>?, response: Response<List<ProductInfo>?>) {
                if (response.isSuccessful) {
                    val body: List<ProductInfo> = response.body() as List<ProductInfo>
                    myProducts.setValue(body)
                } else {
                    myProducts.postValue(null)
                }
            }

            override fun onFailure(call: Call<List<ProductInfo>?>?, t: Throwable?) {
                myProducts.postValue(null)
            }
        })
        return myProducts
    }

    fun GetProductByID(theID: Int): MutableLiveData<ProductInfo?> {
        myProductManagerAPI.GetProductByID(theID, object : Callback<ProductInfo> {
            override fun onResponse(call: Call<ProductInfo>?, response: Response<ProductInfo>) {
                if (response.isSuccessful) {
                    val body: ProductInfo = response.body() as ProductInfo
                    myProductByID.setValue(body)
                } else {
                    myProductByID.postValue(null)
                }
            }

            override fun onFailure(call: Call<ProductInfo>?, t: Throwable?) {
                myProductByID.postValue(null)
            }
        })
        return myProductByID;
    }

    companion object {
        @Volatile
        private var instance: ProductRepository? = null
        fun GetInstance(theProductManagerAPI: ProductManagerAPI): ProductRepository? {
            if (instance == null) {
                instance = ProductRepository(theProductManagerAPI)
            }
            return instance
        }
    }
}