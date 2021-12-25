package com.example.architecture.ui.order

import androidx.lifecycle.ViewModel

import androidx.lifecycle.ViewModelProvider
import com.example.architecture.interfaces.OrderManagerAPI
import com.example.architecture.interfaces.ProductManagerAPI
import com.example.architecture.repository.OrderRepository
import com.example.architecture.repository.ProductRepository
import java.lang.IllegalArgumentException


class OrderViewModelFactory : ViewModelProvider.Factory {
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        return if (modelClass.isAssignableFrom(OrderViewModel::class.java)) {
            OrderViewModel(OrderRepository.GetInstance(OrderManagerAPI.GetInstance()!!)) as T
        } else {
            throw IllegalArgumentException("Unknown ViewModel class")
        }
    }

}