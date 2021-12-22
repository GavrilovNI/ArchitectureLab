package com.example.architecture.interfaces

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object RetrofitAPI {
    private val retrofit = Retrofit.Builder()
        .baseUrl("http://93.157.254.153/")
        .addConverterFactory(GsonConverterFactory.create())
        .build();

    private var service: IProjectAPI? = retrofit.create(IProjectAPI::class.java);

    fun GetService(): IProjectAPI? {
        return service;
    }
}