package com.example.architecture

import android.os.Bundle
import android.system.Os.read
import android.webkit.WebView
import androidx.appcompat.app.AppCompatActivity
import java.io.BufferedInputStream
import java.io.ByteArrayOutputStream
import java.io.IOException
import java.io.InputStream
import java.net.HttpURLConnection
import java.net.MalformedURLException
import java.net.URL

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val myWebView: WebView = findViewById(R.id.webview)
        myWebView.loadUrl("https://localhost:7006/")
        myWebView.settings.javaScriptEnabled = true

        /*var urlConnection: HttpURLConnection? = null

        try {
            val url = URL("http://localhost:7006")
            urlConnection = url.openConnection() as HttpURLConnection
            val `in`: InputStream = BufferedInputStream(urlConnection.getInputStream())
            val myString: String = readStream(`in`)
            var otherString = myString
            otherString = "$otherString "
        } catch (e: MalformedURLException) {
            // TODO Auto-generated catch block
            e.printStackTrace()
        } catch (e: IOException) {
            e.printStackTrace()
        } finally {
            urlConnection?.disconnect()
        }*/
    }

    //private fun readStream(inputStream: InputStream): String {
        /*return try {
            val bo = ByteArrayOutputStream()
            var i: Int = InputStream.read()
            while (i != -1) {
                bo.write(i)
                i = InputStream.read()
            }
            bo.toString()
        } catch (e: IOException) {
            "" + e
        }*/
    //}
}