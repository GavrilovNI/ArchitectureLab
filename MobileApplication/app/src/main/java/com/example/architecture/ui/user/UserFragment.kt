package com.example.architecture.ui.user

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.architecture.databinding.FragmentUserBinding

// Class describing cart page
class UserFragment : Fragment() {

    private lateinit var myUserViewModel: UserViewModel
    private var myBinding: FragmentUserBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = myBinding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myUserViewModel =
            ViewModelProvider(this).get(UserViewModel::class.java)

        myBinding = FragmentUserBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textView: TextView = binding.textUser
        myUserViewModel.text.observe(viewLifecycleOwner, Observer {
            textView.text = it;
        })
        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}