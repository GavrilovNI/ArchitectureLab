package com.example.architecture.ui.login

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import com.example.architecture.R
import com.example.architecture.databinding.FragmentLoginBinding
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.User
import com.example.architecture.repository.UserRepository

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
        if (UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)?.GetIsValid()!!)
        {
            root.findViewById<EditText>(R.id.editTextTextEmailAddress).isEnabled = false;
            root.findViewById<EditText>(R.id.editTextTextPassword).isEnabled = false;
            root.findViewById<Button>(R.id.Login).visibility = View.GONE;
            root.findViewById<Button>(R.id.RegisterPage).visibility = View.GONE;
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}