package com.example.architecture.ui.order

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.example.architecture.models.Order
import com.example.architecture.repository.OrderRepository

// TODO: uncomment line with get data from repository
class OrderViewModel(theProductRepos: OrderRepository?) : ViewModel() {

    private lateinit var myOrder: MutableLiveData<List<Order>?>;
    private val myOrderRepos : OrderRepository = theProductRepos!!;

    fun GetBProducts() : MutableLiveData<List<Order>?>
    {
        myOrder = myOrderRepos.GetOrder();
        //myProducts = MutableLiveData(listOf(ProductInfo(Product(1, "sss", 12f, "ssss", 3, "/ada"), 6),
        //    ProductInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6), ProductInfo(Product(3, "sss", 12f, "ssss", 3, "/ada"), 6),
        //    ProductInfo(Product(10, "aaa", 12f, "dscsa", 12, "/ada"), 6)));
        return myOrder;
    }

}