package com.example.architecture.ui.cart

import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.architecture.components.CartItem
import com.example.architecture.models.CartInfo

class CartAdapter (theCartInfo: List<CartInfo>?) :
    RecyclerView.Adapter<CartAdapter.ViewHolder>() {
    // no Context reference needed—can get it from a ViewGroup parameter
    private var myCartInfo: List<CartInfo>
    override fun getItemCount(): Int {
        return myCartInfo.size
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        // no need for a LayoutInflater instance—
        // the custom view inflates itself
        val itemView = CartItem(parent.context, null)
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
        holder.SetCartItemsInfo(myCartInfo[position])
    }

    inner class ViewHolder(v: View) : RecyclerView.ViewHolder(v) {
        private val customView: CartItem

        fun SetCartItemsInfo(theCartInfo: CartInfo)
        {
            customView.SetCartInfo(theCartInfo);
        }

        init {
            customView = v as CartItem
        }
    }

    fun SetCartItemInfo(theCartInfoItems: List<CartInfo>?)
    {
        myCartInfo = theCartInfoItems!!;
        this.notifyDataSetChanged();
    }

    init {
        // make own copy of the list so it can't be edited externally
        this.myCartInfo = ArrayList(theCartInfo)
    }
}