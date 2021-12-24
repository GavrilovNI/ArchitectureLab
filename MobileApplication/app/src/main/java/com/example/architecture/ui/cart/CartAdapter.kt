package com.example.architecture.ui.cart

import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.architecture.components.CartItem
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.CartRepository

class CartAdapter (theCartInfo: List<ProductInfo>?) :
    RecyclerView.Adapter<CartAdapter.ViewHolder>() {
    // no Context reference needed—can get it from a ViewGroup parameter
    private var myCartInfo: List<ProductInfo>
    override fun getItemCount(): Int {
        return myCartInfo.size!!
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
        myCartInfo.get(position)?.let { holder.SetCartItemsInfo(it) }
        holder.customView.GetButtonAdd().setOnClickListener(View.OnClickListener { // When you're inside the click listener interface,
            // you can access the position using the ViewHolder.
            // We'll store the position in the member variable in this case.
            CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.AddItemToCart(myCartInfo[position].product?.id!!, 1)
        })
        holder.customView.GetButtonRemove().setOnClickListener(View.OnClickListener { // When you're inside the click listener interface,
            // you can access the position using the ViewHolder.
            // We'll store the position in the member variable in this case.
            CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.RemoveItemFromCart(
                myCartInfo[position].product?.id!!, 1)
        })
    }

    inner class ViewHolder(v: View) : RecyclerView.ViewHolder(v) {
        val customView: CartItem

        fun SetCartItemsInfo(theCartInfo: ProductInfo)
        {
            customView.SetCartInfo(theCartInfo);
        }

        init {
            customView = v as CartItem
        }
    }

    fun SetCartItemInfo(theCartInfoItems: List<ProductInfo>?)
    {
        myCartInfo = theCartInfoItems!!;
        this.notifyDataSetChanged();
    }

    init {
        // make own copy of the list so it can't be edited externally
        this.myCartInfo = theCartInfo!!
    }
}