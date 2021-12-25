package com.example.architecture.ui.user

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.architecture.R
import com.example.architecture.components.UserItem
import com.example.architecture.databinding.FragmentUserBinding
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.repository.UserRepository

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

        val aRepos = UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)!!;
        if(aRepos.GetIsValid())
        {
            val user = aRepos.GetUserO();
            val userItem = root.findViewById<UserItem>(R.id.userItems);
            userItem.SetData(user);
        }
        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        myBinding = null
    }
}