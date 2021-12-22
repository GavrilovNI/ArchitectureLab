package com.example.architecture.ui.products

import androidx.lifecycle.ViewModel

import androidx.lifecycle.ViewModelProvider
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.repository.ProductRepository
import java.lang.IllegalArgumentException


class ProductViewModelFactory : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        return if (modelClass.isAssignableFrom(ProductsViewModel::class.java)) {
            ProductsViewModel(ProductRepository.GetInstance(ProductManagerAPI.GetInstance()!!)) as T
        } else {
            throw IllegalArgumentException("Unknown ViewModel class")
        }
    }

}