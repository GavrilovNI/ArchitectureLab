package com.example.architecture.ui.home

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class HomeViewModel : ViewModel() {

    private val myText = MutableLiveData<String>().apply {
        value = "This is Home page"
    }
    val text: LiveData<String> = myText
}