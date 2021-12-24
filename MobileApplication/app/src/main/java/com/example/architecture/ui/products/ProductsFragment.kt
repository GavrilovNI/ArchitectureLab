package com.example.architecture.ui.products

import android.os.Bundle
import android.util.AttributeSet
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import com.example.architecture.databinding.FragmentProductsBinding

import androidx.core.widget.ContentLoadingProgressBar

import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.architecture.R
import com.example.architecture.components.ProductItem
import com.example.architecture.interfaces.IProjectAPI
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.interfaces.RetrofitAPI
import com.example.architecture.models.Product
import com.example.architecture.models.ProductInfo



class ProductsFragment : Fragment() {

    private lateinit var myProductsViewModel: ProductsViewModel
    private var myBinding: FragmentProductsBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = myBinding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myProductsViewModel = ViewModelProvider(this, ProductViewModelFactory()).get(
            ProductsViewModel::class.java
        );

        myBinding = FragmentProductsBinding.inflate(inflater, container, false)
        val root: View = binding.root;

        var products : List<ProductInfo> = listOf();

        // Create data for recycle view widget
        val aProductAdapter = ProductAdapter(products);
        val anObserver = Observer<List<ProductInfo>?> { theProductsInfo: List<ProductInfo>? ->
            if (theProductsInfo != null && theProductsInfo.isNotEmpty())
            {
                aProductAdapter.SetProductsInfo(theProductsInfo);
            }
        }

        // TODO: if server on, we can uncomment lines with get data from method GetProducts
        myProductsViewModel.GetProducts().observe(viewLifecycleOwner, anObserver);
        val aProductRecycle = root.findViewById<RecyclerView>(R.id.recycleProductItems);
        aProductRecycle.layoutManager = LinearLayoutManager(context);
        aProductRecycle.adapter = aProductAdapter;

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}