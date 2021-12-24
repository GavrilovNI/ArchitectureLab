package com.example.architecture.repository

import androidx.lifecycle.MutableLiveData
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.CartInfo
import com.example.architecture.models.User
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class UserRepository private constructor(theUserManagerAPI: UserManagerAPI){
    private val myUserManagerAPI: UserManagerAPI = theUserManagerAPI;
    private val myUser: MutableLiveData<User?> = MutableLiveData<User?>();
    private var myUserObj : User = User("", "");

    fun GetUser(): MutableLiveData<User?> {
        myUserManagerAPI.GetUser(object : Callback<User?> {
            override fun onResponse(call: Call<User?>?, response: Response<User?>) {
                if (response.isSuccessful) {
                    val body: User = response.body() as User;
                    myUser.setValue(body);
                } else {
                    myUser.postValue(null);
                }
            }

            override fun onFailure(call: Call<User?>?, t: Throwable?) {
                myUser.postValue(null);
            }
        })
        return myUser;
    }

    fun GetUserO(): User {
        return myUserObj;
    }
    fun LoginUser(user: User) : Boolean
    {
        myUserObj = user;
        return myUserObj.Check();
    }

    fun UnloginUser()
    {
        myUserObj = User("", "");
    }

    fun Authorization(user: User){
        myUserManagerAPI.Authorization(user, object : Callback<User?> {
            override fun onResponse(call: Call<User?>?, response: Response<User?>) {
                if (response.isSuccessful) {
                    val body: User = response.body() as User
                    myUser.value = body;
                } else {
                    myUser.postValue(null)
                }
            }

            override fun onFailure(call: Call<User?>?, t: Throwable?) {
                myUser.postValue(null)
            }
        })
    }

    companion object {
        @Volatile
        private var instance: UserRepository? = null;
        fun GetInstance(theUserManagerAPI: UserManagerAPI): UserRepository? {
            if (instance == null) {
                instance = UserRepository(theUserManagerAPI);
            }
            return instance
        }
    }
}