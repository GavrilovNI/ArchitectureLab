package com.example.architecture.ui.cart

import androidx.lifecycle.ViewModel

import androidx.lifecycle.ViewModelProvider
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.repository.CartRepository
import java.lang.IllegalArgumentException


class CartViewModelFactory : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        return if (modelClass.isAssignableFrom(CartViewModel::class.java)) {
            CartViewModel(CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)) as T
        } else {
            throw IllegalArgumentException("Unknown ViewModel class")
        }
    }

}