package com.example.architecture.components

import android.content.Context
import android.util.AttributeSet
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.TextView
import com.example.architecture.R
import com.example.architecture.interfaces.CartManagerAPI
import com.example.architecture.models.User
import com.example.architecture.repository.CartRepository

class UserItem (context: Context?, attrs: AttributeSet?) : LinearLayout(context, attrs) {
    private lateinit var myEmail: TextView;
    private lateinit var mySumInBasket: TextView;
    private lateinit var myRole: TextView;

    private val myRoles : Map<Int, String> = mapOf(1 to "User", 2 to "Manager");
    init {
        inflate(context, R.layout.component_user, this);

        val aTypedArray = context!!.obtainStyledAttributes(attrs, R.styleable.UserAttributes);
        val anEmail = aTypedArray.getString(R.styleable.UserAttributes_Email);
        val aRole = aTypedArray.getInt(R.styleable.UserAttributes_Role, -1);
        val aSubTotal = aTypedArray.getFloat(R.styleable.UserAttributes_SumInBasket, 0f);

        initComponents();

        //! Set data on components
        myEmail.text = myEmail.text.toString().plus("  ").plus(anEmail);
        myRole.text = myRole.text.toString().plus("  ").plus( if(myRoles.containsKey(aRole)) myRoles[aRole] else myRoles[1]);
        mySumInBasket.text = mySumInBasket.text.toString().plus("  ").plus( if(aSubTotal < 0) R.string.ErrorGetPrice.toString() else aSubTotal.toString());

    }

    fun SetData(user: User)
    {
        val aSum = CartRepository.GetInstance(CartManagerAPI.GetInstance()!!)?.GetSumInCart();
        myEmail.text = context.getString(R.string.e_mail).plus("  ").plus(user.myEmail);
        myRole.text = context.getString(R.string.role).plus("  ").plus( if(myRoles.containsKey(1)) myRoles[1] else myRoles[1]);
        mySumInBasket.text = context.getString(R.string.subtotal).plus("  ").plus(aSum);
    }

    private fun initComponents()
    {
        myEmail = findViewById(R.id.Email);
        mySumInBasket = findViewById(R.id.sumInBasket);
        myRole = findViewById(R.id.Role);
    }
}