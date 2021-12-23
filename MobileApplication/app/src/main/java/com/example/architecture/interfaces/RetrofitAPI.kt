package com.example.architecture.interfaces

import okhttp3.*
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

import java.util.*
import kotlin.collections.ArrayList
import android.app.Activity
import android.content.Context

import android.content.SharedPreferences
import okhttp3.Interceptor
import java.io.IOException


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