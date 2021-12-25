package com.example.architecture.components

import android.app.Activity
import android.content.Context
import android.util.AttributeSet
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.TextView
import com.example.architecture.R
import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo
import okhttp3.*
import java.io.IOException

import android.graphics.Bitmap
import android.graphics.BitmapFactory
import java.io.InputStream
import android.os.AsyncTask
import android.widget.Button
import com.bumptech.glide.Glide
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.models.BoughtProduct
import com.example.architecture.repository.ProductRepository


class OrderItem(context: Context?, attrs: AttributeSet?) : LinearLayout(context, attrs) {
    private lateinit var myProductImage: ImageView;
    private lateinit var myProductName: TextView;
    private lateinit var myProductDescription: TextView;
    private lateinit var myProductCount: TextView;
    private lateinit var myProductPrice: TextView;
    private lateinit var myPaidStatus: TextView;
    private lateinit var myProductItem: BoughtProduct;

    init {
        inflate(context, R.layout.component_cart, this);

        initComponents();

        //! Set data on components
        myProductName.text =  R.string.ErrorNameOfProduct.toString()
        myProductDescription.text =  R.string.ErrorOfProductDescription.toString()
        myProductCount.text = "0";
        myProductPrice.text = "0"
        myPaidStatus.text = "Paid}"
    }

    fun SetOrderInfo(theProduct: BoughtProduct)
    {
         myProductItem = theProduct;

        val aProduct  =
            myProductItem.productId?.let {
                ProductRepository.GetInstance(ProductManagerAPI.GetInstance()!!)?.GetProductByID(
                    it
                )
            };
         myProductName.text = if(aProduct!!.value?.product?.name.isNullOrBlank()) R.string.ErrorNameOfProduct.toString(); else aProduct!!.value?.product?.name;
         myProductDescription.text = if(aProduct!!.value?.product?.description.isNullOrBlank()) R.string.ErrorOfProductDescription.toString(); else aProduct!!.value?.product?.description;
         myProductCount.text = if(myProductItem.count!! < 0) R.string.ErrorGetCount.toString() else context.getString(R.string.leftProducts).plus("  ").plus(
             myProductItem.count.toString());
         myProductPrice.text = if(aProduct!!.value?.product?.price!! < 0) R.string.ErrorGetPrice.toString() else context.getString(R.string.price).plus("  ").plus(
             aProduct!!.value?.product?.price.toString());
        myPaidStatus.text = "Paid";

         Glide.with(context)
             .load("http://93.157.254.153/".plus(aProduct!!.value?.product?.linkToImage))
             .into(myProductImage);
    }

    private fun initComponents()
    {
        myProductCount = findViewById(R.id.leftProduct);
        myProductDescription = findViewById(R.id.productDescription);
        myProductImage = findViewById(R.id.productImage);
        myProductName = findViewById(R.id.productName);
        myProductPrice = findViewById(R.id.priceProduct);
        myPaidStatus = findViewById(R.id.paidStatus);
    }
}