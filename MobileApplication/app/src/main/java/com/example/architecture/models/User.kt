package com.example.architecture.models

import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.repository.UserRepository
import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

// Class that describes a user object
class User (theEmail : String, thePassword: String) {

    @SerializedName("email")
    @Expose
    var myEmail : String = theEmail;

    @SerializedName("password")
    @Expose
    var myPassword : String = thePassword;

    fun Check() :Boolean
    {
        return if (myEmail.isNotEmpty() && myPassword.isNotEmpty())
            true;
        else {
            myEmail = "";
            myPassword = ""
            false;
        }
    }

    fun IsLogin() : Boolean
    {
        return (myEmail.isNotEmpty() && myPassword.isNotEmpty() && UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)?.GetIsValid()!!);
    }
}