package com.example.architecture.ui.cart

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.architecture.R
import com.example.architecture.databinding.FragmentCartBinding
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.ProductInfo
import com.example.architecture.repository.ProductRepository
import com.example.architecture.ui.products.ProductAdapter

// Class describing cart page
class CartFragment : Fragment() {

    private lateinit var myCartViewModel: CartViewModel
    private var myBinding: FragmentCartBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = myBinding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myCartViewModel =
            ViewModelProvider(this, CartViewModelFactory()).get(CartViewModel::class.java);

        myBinding = FragmentCartBinding.inflate(inflater, container, false)
        val root: View = binding.root

        var products : List<ProductInfo> = listOf();

        // Create data for recycle view widget
        val aCartAdapter= CartAdapter(products);
        val anObserver = Observer<CartInfo?> { theProductsInfo: CartInfo? ->
            if (theProductsInfo != null && !theProductsInfo.products?.isEmpty()!!)
            {
                aCartAdapter.SetCartItemInfo(theProductsInfo.products);
            }
        }

        // TODO: if server on, we can uncomment lines with get data from method GetProducts
        myCartViewModel.GetCart().observe(viewLifecycleOwner, anObserver);
        val aCartRecycle = root.findViewById<RecyclerView>(R.id.recycleCartItems);
        aCartRecycle.layoutManager = LinearLayoutManager(context);
        aCartRecycle.adapter = aCartAdapter;

        val firstPartOfMessage = context?.resources?.getString(R.string.subtotal);
        val textView: TextView = binding.sumOfProducts
        myCartViewModel.text.observe(viewLifecycleOwner, Observer {
            textView.text = firstPartOfMessage + it;
        })

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}