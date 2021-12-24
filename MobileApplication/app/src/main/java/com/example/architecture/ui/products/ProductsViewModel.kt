package com.example.architecture.ui.products

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.ProductRepository

// TODO: uncomment line with get data from repository
class ProductsViewModel(theProductRepos: ProductRepository?) : ViewModel() {

    var click: Int = -1;
    private lateinit var myProducts: MutableLiveData<List<ProductInfo>?>;
    private lateinit var myProductByID: MutableLiveData<ProductInfo?>;
    private val myProductRepos : ProductRepository = theProductRepos!!;

    fun GetProducts() : MutableLiveData<List<ProductInfo>?>
    {
        //myProducts = myProductRepos.GetProducts();
        myProducts = MutableLiveData(listOf(ProductInfo(Product(1, "sss", 12f, "ssss", 3, "/ada"), 6),
            ProductInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6), ProductInfo(Product(3, "sss", 12f, "ssss", 3, "/ada"), 6),
            ProductInfo(Product(10, "aaa", 12f, "dscsa", 12, "/ada"), 6)));
        return myProducts;
    }

    fun GetProductsByID(theID: Int) : MutableLiveData<ProductInfo?>
    {
        //myProductByID = myProductRepos.GetProductByID(theID);

        myProductByID = MutableLiveData(ProductInfo(Product(2, "sss", 12f, "ssss", 3, "/ada"), 6));
        return myProductByID;
    }
}