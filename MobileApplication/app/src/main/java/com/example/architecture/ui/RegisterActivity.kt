package com.example.architecture.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.EditText
import com.example.architecture.R

class RegisterActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.fragment_register)
        val email = intent.getStringExtra("email");
        if (email.toString().isNotEmpty())
            findViewById<EditText>(R.id.editTextTextEmailAddress).setText(email.toString());
    }

    fun Register(view: View) {
        //! Add password validation
        this.finish();
    }
}