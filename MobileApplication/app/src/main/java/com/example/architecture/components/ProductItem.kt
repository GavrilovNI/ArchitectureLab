package com.example.architecture.components

import android.content.Context
import android.util.AttributeSet
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.TextView
import com.example.architecture.R
import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo

// Class for create item for product
class ProductItem(context: Context?, attrs: AttributeSet?) : LinearLayout(context, attrs) {
    private lateinit var myProductName: TextView;
    private lateinit var myProductDescription: TextView;
    private lateinit var myProductCount: TextView;
    private lateinit var myProductPrice: TextView;
    private lateinit var myProductImage: ImageView;
    private lateinit var myCountInCart: TextView;
    private var myProductInfo: ProductInfo


    init {
        inflate(context, R.layout.component_product, this);

        val aTypedArray = context!!.obtainStyledAttributes(attrs, R.styleable.ProductAttributes);
        val anID = aTypedArray.getInt(R.styleable.ProductAttributes_Id, -1);
        val aName = aTypedArray.getString(R.styleable.ProductAttributes_Name);
        val aDescription = aTypedArray.getString(R.styleable.ProductAttributes_Description);
        val aCount = aTypedArray.getInt(R.styleable.ProductAttributes_Count, -1);
        val aPrice = aTypedArray.getFloat(R.styleable.ProductAttributes_Price, 0f);
        val anImage = aTypedArray.getString(R.styleable.ProductAttributes_Image);
        val aCountInCart = aTypedArray.getInt(R.styleable.ProductAttributes_CountInCart, -1);

        myProductInfo = ProductInfo(Product(anID, aName.toString(), aPrice, aDescription.toString(), aCount, anImage.toString()), aCountInCart);
        initComponents();

        //! Set data on components
        myProductName.text = if(aName.isNullOrBlank()) R.string.ErrorNameOfProduct.toString(); else aName;
        myProductDescription.text = if(aDescription.isNullOrBlank()) R.string.ErrorOfProductDescription.toString(); else aDescription;
        myProductCount.text = if(aCount < 0) R.string.ErrorGetCount.toString() else aCount.toString();
        myProductPrice.text = if(aPrice < 0) R.string.ErrorGetPrice.toString() else aPrice.toString();
        myCountInCart.text = if(aCount < 0) R.string.ErrorGetCount.toString() else aCountInCart.toString();

        // TODO: For image create additional method for set image by file name and path
        myProductImage.setImageResource(R.drawable.apple);
    }

    private fun initComponents()
    {
        myProductCount = findViewById(R.id.leftProduct);
        myProductDescription = findViewById(R.id.productDescription);
        myProductImage = findViewById(R.id.productImage);
        myProductName = findViewById(R.id.productName);
        myProductPrice = findViewById(R.id.priceProduct);
        myCountInCart = findViewById(R.id.productCountInCart);
    }

    fun SetProductsInfo(theProductInfo: ProductInfo)
    {
        myProductInfo = theProductInfo;

        myProductName.text = if(myProductInfo.product?.name.isNullOrBlank()) R.string.ErrorNameOfProduct.toString(); else myProductInfo.product?.name;
        myProductDescription.text = if(myProductInfo.product?.description.isNullOrBlank()) R.string.ErrorOfProductDescription.toString(); else myProductInfo.product?.description;
        myProductCount.text = if(myProductInfo.product?.avaliableAmount!! < 0) R.string.ErrorGetCount.toString() else myProductInfo.product?.avaliableAmount.toString();
        myProductPrice.text = if(myProductInfo.product?.price!! < 0) R.string.ErrorGetPrice.toString() else myProductInfo.product?.price.toString();
        myCountInCart.text = if(myProductInfo.countInCart!! < 0) R.string.ErrorGetCount.toString() else myProductInfo.countInCart.toString();

        // TODO: For image create additional method for set image by file name and path
        myProductImage.setImageResource(R.drawable.apple);
    }

    fun GetProduct() : ProductInfo
    {
        return myProductInfo;
    }
}