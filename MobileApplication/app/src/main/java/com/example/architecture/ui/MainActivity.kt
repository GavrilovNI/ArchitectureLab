package com.example.architecture.ui

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.EditText
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.setupWithNavController
import com.example.architecture.R
import com.example.architecture.databinding.ActivityMainBinding
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.interfaces.IProjectAPI
import com.example.architecture.interfaces.RetrofitAPI
import com.example.architecture.interfaces.UserManagerAPI
import com.example.architecture.models.User
import com.example.architecture.repository.CartRepository
import com.example.architecture.repository.UserRepository
import com.google.android.material.bottomnavigation.BottomNavigationView

class MainActivity : AppCompatActivity() {

    private lateinit var myBinding: ActivityMainBinding

    companion object {
        lateinit var myService: IProjectAPI
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        myService = RetrofitAPI.GetService()!!;

        myBinding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(myBinding.root)

        val navView: BottomNavigationView = myBinding.navView

        val navController = findNavController(R.id.nav_host_fragment_activity_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        val appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.navigation_home, R.id.navigation_products, R.id.navigation_cart, R.id.navigation_login, R.id.navigation_user
            )
        )
        //setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)
    }


    fun RemoveProduct(view: View){

        CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.RemoveItemFromCart(1, 1)
    }

    fun AddProduct(view: View){
        CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.AddItemToCart(1, 1)
    }

    fun ToRegister(view: View) {

        val registerPage = Intent(this, RegisterActivity::class.java);
        val email = findViewById<EditText>(R.id.editTextTextEmailAddress).text;
        if (email.isNotEmpty())
            registerPage.putExtra("email", email.toString());
        startActivity(registerPage);
    }

    fun Login(view: View) {
        val email = findViewById<EditText>(R.id.editTextTextEmailAddress).text;
        val password = findViewById<EditText>(R.id.editTextTextPassword).text;
        if (UserRepository.GetInstance(UserManagerAPI.GetInstance()!!)
                ?.LoginUser(User(email.toString(), password.toString())) == true
        )
        {
            findViewById<Button>(R.id.Login).visibility = View.GONE;
        }

    }
}