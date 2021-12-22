package com.example.architecture.ui.cart

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.example.architecture.models.CartInfo
import com.example.architecture.models.Product
import com.example.architecture.repository.CartRepository

// TODO: uncomment line with get data from repository
class CartViewModel(getInstance: CartRepository?) : ViewModel() {

    private lateinit var myCartInfo: MutableLiveData<List<CartInfo>?>;
    private lateinit var myCartRepository: CartRepository;

    fun GetCart() : MutableLiveData<List<CartInfo>?>
    {
        //myCart = myCartRepository.GetProducts();
        myCartInfo = MutableLiveData(listOf(
            CartInfo(Product(2, "sss", 12f, "ssss", 3, "/ada"), 6),
            CartInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6), CartInfo(Product(2, "sss", 12f, "ssss", 3, "/ada"), 6),
            CartInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6)));
        return myCartInfo;
    }

    // Send data on server
    fun SetCartRepos(theCartRepository: CartRepository)
    {
        myCartRepository = theCartRepository;
    }

    private val myText = MutableLiveData<String>().apply {
        value = " 100Р"; //TODO: Add function for compute sum of bought!
    }
    val text: LiveData<String> = myText
}
