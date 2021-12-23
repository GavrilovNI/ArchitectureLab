package com.example.architecture.ui.login

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.architecture.databinding.FragmentLoginBinding

//! Class describing login page
class LoginFragment : Fragment() {

    private lateinit var myLoginViewModel: LoginViewModel
    private var myBinding: FragmentLoginBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = myBinding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        myLoginViewModel =
            ViewModelProvider(this).get(LoginViewModel::class.java)

        myBinding = FragmentLoginBinding.inflate(inflater, container, false)
        val root: View = binding.root

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}