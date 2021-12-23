package com.example.architecture.ui.products

import androidx.recyclerview.widget.RecyclerView

import android.view.View

import android.view.ViewGroup

import com.example.architecture.components.ProductItem
import com.example.architecture.models.ProductInfo


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
        holder.SetProductInfo(myProductsInfo[position])
    }

    inner class ViewHolder(v: View) : RecyclerView.ViewHolder(v) {
        private val customView: ProductItem

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