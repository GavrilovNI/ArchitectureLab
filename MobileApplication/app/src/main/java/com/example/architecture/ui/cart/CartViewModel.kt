package com.example.architecture.ui.cart

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModel
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.CartRepository
import com.example.architecture.repository.UserRepository

// TODO: uncomment line with get data from repository
class CartViewModel(theCartRepository: CartRepository?) : ViewModel() {

    private lateinit var myCartInfo: MutableLiveData<CartInfo?>;
    private val myCartRepository: CartRepository = theCartRepository!!;
    private val myUserRepository = UserRepository.GetInstance(UserManagerAPI.GetInstance()!!);

    fun GetCart() : MutableLiveData<CartInfo?>
    {
        myCartInfo = myCartRepository.GetProductsCart(myUserRepository?.GetUserO()!!);
        //myCartInfo = MutableLiveData(listOf(
        //    CartInfo(Product(2, "sss", 12f, "ssss", 3, "/ada"), 6),
        //    CartInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6), CartInfo(Product(2, "sss", 12f, "ssss", 3, "/ada"), 6),
        //    CartInfo(Product(2, "aaa", 12f, "dscsa", 12, "/ada"), 6)));
        return myCartInfo;
    }

    // Send data on server
    fun SetCartRepos(theCartRepository: CartRepository)
    {
        //myCartRepository = theCartRepository;
    }

    private val myText = MutableLiveData<String>().apply {
        val aProducts = GetCart().value?.products;
        var sum = 0.0;
        if (!aProducts.isNullOrEmpty())
        {
            for (anIndex in aProducts!!.indices)
            {
                sum += aProducts[anIndex].product!!.price!! * aProducts[anIndex].countInCart!!;
            }
        }
        value = sum.toString() + "Р";
    }
    val text: LiveData<String> = myText
}
