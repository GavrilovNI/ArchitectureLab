package com.example.architecture.ui.user

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class UserViewModel : ViewModel() {

    private val myText = MutableLiveData<String>().apply {
        value = "This User Page"
    }
    val text: LiveData<String> = myText
}