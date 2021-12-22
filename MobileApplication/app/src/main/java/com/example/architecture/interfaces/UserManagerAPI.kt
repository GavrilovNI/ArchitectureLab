package com.example.architecture.interfaces

import com.example.architecture.models.User
import retrofit2.Callback

// API for user repository
class UserManagerAPI : IBaseManagerAPI() {

    companion object {
        @Volatile
        private var apiManager: UserManagerAPI? = null
        fun GetInstance(): UserManagerAPI? {
            if (apiManager == null) {
                apiManager = UserManagerAPI()
            }
            return apiManager
        }
    }

    fun GetUser(callback: Callback<User?>?) {
        service?.GetUser()?.enqueue(callback)
    }
}