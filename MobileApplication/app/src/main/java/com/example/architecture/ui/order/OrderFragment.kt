package com.example.architecture.ui.order

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer

import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.architecture.R
import com.example.architecture.databinding.FragmentOrderBinding
import com.example.architecture.models.BoughtProduct
import com.example.architecture.models.Order


class OrderFragment : Fragment() {

    private lateinit var myOrderViewModel: OrderViewModel
    private var myBinding: FragmentOrderBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = myBinding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myOrderViewModel = ViewModelProvider(this, OrderViewModelFactory()).get(
            OrderViewModel::class.java
        );

        myBinding = FragmentOrderBinding.inflate(inflater, container, false)
        val root: View = binding.root;

        var products : List<BoughtProduct> = listOf();

        // Create data for recycle view widget
        val anOrderAdapter = OrderAdapter(products);
        val anObserver = Observer<List<Order>?> { theProductsInfo: List<Order>? ->
            if (theProductsInfo != null && !theProductsInfo[0].bouProducts?.isEmpty()!!)
            {
                anOrderAdapter.SetProductsInfo(theProductsInfo[0].bouProducts);
            }
        }

        // TODO: if server on, we can uncomment lines with get data from method GetProducts
        myOrderViewModel.GetBProducts().observe(viewLifecycleOwner, anObserver);
        val anOrderRecycle = root.findViewById<RecyclerView>(R.id.recycleProductItems);
        anOrderRecycle.layoutManager = LinearLayoutManager(context);
        anOrderRecycle.adapter = anOrderAdapter;

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}