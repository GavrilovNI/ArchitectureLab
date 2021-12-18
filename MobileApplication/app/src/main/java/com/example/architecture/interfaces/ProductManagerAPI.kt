package com.example.architecture.interfaces

import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo
import retrofit2.Call
import retrofit2.Callback


// API for product repository
class ProductManagerAPI : IBaseManagerAPI() {

    companion object {
        @Volatile
        private var apiManager: ProductManagerAPI? = null
        fun GetInstance(): ProductManagerAPI? {
            if (apiManager == null) {
                apiManager = ProductManagerAPI()
            }
            return apiManager
        }
    }

    fun GetProducts(callback: Callback<List<ProductInfo>>) {
        val aProducts: Call<List<ProductInfo>>? = service?.GetProducts();
        aProducts?.enqueue(callback)
    }

    fun GetProductByID(theID: Int, callback: Callback<ProductInfo>) {
        val aProduct: Call<ProductInfo>? = service?.GetProductByID(theID);
        aProduct?.enqueue(callback)
    }
}