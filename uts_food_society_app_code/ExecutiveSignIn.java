package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

//Sign in activity that leads to a locked activity showing all the reimbursement requests
public class ExecutiveSignIn extends AppCompatActivity{

    private static final String PASSWORD = "foodsoc";

    private String password;

    private EditText passwordInput;

    private Button signIn;


    /**
     * Initialises the elements in the activity layout
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_executive_sign_in);

        passwordInput = (EditText) findViewById(R.id.sign_in_password_input);

        signIn = (Button) findViewById(R.id.sign_in_button);

        //Checks password value and compares it to the password and moves on to the next screen
        signIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                password = passwordInput.getText().toString();

                if (password.equals(PASSWORD)) {
                    Intent viewRequestsIntent = new Intent(ExecutiveSignIn.this, ViewRequests.class);
                    startActivity(viewRequestsIntent);
                    finish();
                }
                else {
                    Toast.makeText(ExecutiveSignIn.this, R.string.password_invalid, Toast.LENGTH_SHORT).show();
                }
            }

        });

    }


}
