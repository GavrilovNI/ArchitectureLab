package com.example.architecture.ui.order

import androidx.recyclerview.widget.RecyclerView

import android.view.View

import android.view.ViewGroup
import com.example.architecture.components.OrderItem

import com.example.architecture.components.ProductItem
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.models.BoughtProduct
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.CartRepository

class OrderAdapter(theProductsInfo: List<BoughtProduct>?) :
    RecyclerView.Adapter<OrderAdapter.ViewHolder>() {
    // no Context reference needed—can get it from a ViewGroup parameter
    private var myProductsInfo: List<BoughtProduct>
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
        holder.SetOrderItem(myProductsInfo[position]);
    }

    inner class ViewHolder(v: View) : RecyclerView.ViewHolder(v) {
        val customView: OrderItem

        fun SetOrderItem(theProduct: BoughtProduct)
        {
            customView.SetOrderInfo(theProduct);
        }

        init {
            customView = v as OrderItem
        }
    }

    fun SetProductsInfo(theProductsInfo: List<BoughtProduct>?)
    {
        myProductsInfo = theProductsInfo!!;
        this.notifyDataSetChanged();
    }

    init {
        // make own copy of the list so it can't be edited externally
        this.myProductsInfo = ArrayList(theProductsInfo)
    }
}