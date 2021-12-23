package com.example.architecture.models

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

// Class that describes a user object
class User (theEmail : String, thePassword: String) {

    private val myUsers : Map<String, String> = mapOf("as@gmail.com" to "As12Sa21", "Fem@gmail.com" to "As12Sa21");

    @SerializedName("email")
    @Expose
    var myEmail : String = theEmail;

    @SerializedName("password")
    @Expose
    var myPassword : String = thePassword;

    fun Check() :Boolean
    {
        return if (myEmail.isNotEmpty() && myPassword.isNotEmpty() && myUsers.contains(myEmail) && myUsers[myEmail].equals(myPassword) )
            true;
        else {
            myEmail = "";
            myPassword = ""
            false;
        }
    }

    fun IsLogin() : Boolean
    {
        return (myEmail.isNotEmpty() && myPassword.isNotEmpty())
    }
}