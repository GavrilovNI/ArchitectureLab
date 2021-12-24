package com.example.architecture.ui.products

import androidx.recyclerview.widget.RecyclerView

import android.view.View

import android.view.ViewGroup

import com.example.architecture.components.ProductItem
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.CartRepository

class ProductAdapter(theProductsInfo: List<ProductInfo>?) :
    RecyclerView.Adapter<ProductAdapter.ViewHolder>() {
    // no Context reference needed—can get it from a ViewGroup parameter
    private var myProductsInfo: List<ProductInfo>
    override fun getItemCount(): Int {
        return myProductsInfo.size
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        // no need for a LayoutInflater instance—
        // the custom view inflates itself
        val itemView = ProductItem(parent.context, null)
        // manually set the CustomView's size
        itemView.setLayoutParams(
            ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MATCH_PARENT,
                ViewGroup.LayoutParams.WRAP_CONTENT
            )
        )
        return ViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.SetProductInfo(myProductsInfo[position]);
        holder.customView.GetButtonAdd().setOnClickListener(View.OnClickListener { // When you're inside the click listener interface,
            // you can access the position using the ViewHolder.
            // We'll store the position in the member variable in this case.
            CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.AddItemToCart(myProductsInfo[position].product?.id!!, 1)
        })
        holder.customView.GetButtonRemove().setOnClickListener(View.OnClickListener { // When you're inside the click listener interface,
            // you can access the position using the ViewHolder.
            // We'll store the position in the member variable in this case.
            CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.RemoveItemFromCart(
                myProductsInfo[position].product?.id!!, 1)
        })
    }

    inner class ViewHolder(v: View) : RecyclerView.ViewHolder(v) {
        val customView: ProductItem

        fun SetProductInfo(theProductInfo: ProductInfo)
        {
            customView.SetProductsInfo(theProductInfo);
        }

        init {
            customView = v as ProductItem
        }
    }

    fun SetProductsInfo(theProductsInfo: List<ProductInfo>?)
    {
        myProductsInfo = theProductsInfo!!;
        this.notifyDataSetChanged();
    }

    init {
        // make own copy of the list so it can't be edited externally
        this.myProductsInfo = ArrayList(theProductsInfo)
    }
}