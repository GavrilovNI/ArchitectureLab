package com.example.architecture.interfaces

// Base API class for service
open class IBaseManagerAPI {
    protected var service: IProjectAPI? = RetrofitAPI.GetService();
}