package com.uts.foodsoc.utsfoodsocietyapp;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.File;

/**
 * Activity that takes the data from the previous 'RequestReimbursement' activity and displays it
 * to the user to confirm before it is uploaded to the Firebase database.
 */
public class ConfirmReimbursement extends AppCompatActivity {

    public static final String RESULT_CONFIRM = "result";

    public static final String EXTRA_PURCHASER = "PURCHASER";
    public static final String EXTRA_BSB = "BSB";
    public static final String EXTRA_ACCOUNT = "ACCOUNT";
    public static final String EXTRA_AMOUNT = "AMOUNT";
    public static final String EXTRA_GOODS = "GOODS";
    public static final String EXTRA_COMPANY = "COMPANY";
    public static final String EXTRA_PAYMENT_METHOD = "PAYMENTMETHOD";
    public static final String EXTRA_PURCHASE_DATE = "PURCHASEDATE";
    public static final String EXTRA_FILE_PATH = "FILEPATH";

    private String mPurchaser;
    private String mBsb;
    private String mAccountNumber;
    private String mAmount;
    private String mGoods;
    private String mCompany;
    private String mPaymentMethod;
    private String mPurchaseDate;
    private String mFilePath;

    private TextView mPurchaserText;
    private TextView mBsbText;
    private TextView mAccountNumberText;
    private TextView mAmountText;
    private TextView mGoodsText;
    private TextView mCompanyText;
    private TextView mPaymentMethodText;
    private TextView mPurchaseDateText;
    private ImageView mReceiptImage;

    private CheckBox mCheckbox;
    private Button mSubmitBtn;

    /**
     * Takes extras from the previous activities and initialises them into the views in the layout
     * @param savedInstanceState saved state from before the app was previously closed
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_confirm_reimbursement);

        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                mPurchaser = null;
                mBsb = null;
                mAccountNumber = null;
                mAmount = null;
                mGoods = null;
                mCompany = null;
                mPaymentMethod = null;
                mPurchaseDate = null;
                mFilePath = null;
            } else {
                mPurchaser = extras.getString(EXTRA_PURCHASER);
                mBsb = extras.getString(EXTRA_BSB);
                mAccountNumber = extras.getString(EXTRA_ACCOUNT);
                mAmount = extras.getString(EXTRA_AMOUNT);
                mGoods = extras.getString(EXTRA_GOODS);
                mCompany = extras.getString(EXTRA_COMPANY);
                mPurchaseDate = extras.getString(EXTRA_PURCHASE_DATE);
                mPaymentMethod = extras.getString(EXTRA_PAYMENT_METHOD);
                mFilePath = extras.getString(EXTRA_FILE_PATH);
            }
        } else {
            mPurchaser = (String) savedInstanceState.getSerializable(EXTRA_PURCHASER);
            mBsb = (String) savedInstanceState.getSerializable(EXTRA_BSB);
            mAccountNumber = (String) savedInstanceState.getSerializable(EXTRA_ACCOUNT);
            mAmount = (String) savedInstanceState.getSerializable(EXTRA_AMOUNT);
            mGoods = (String) savedInstanceState.getSerializable(EXTRA_GOODS);
            mPurchaseDate = (String) savedInstanceState.getSerializable(EXTRA_PURCHASE_DATE);
            mCompany = (String) savedInstanceState.getSerializable(EXTRA_COMPANY);
            mPaymentMethod = (String) savedInstanceState.getSerializable(EXTRA_PAYMENT_METHOD);
        }

        mPurchaserText = (TextView) findViewById(R.id.confirm_reimbursement_purchaser);
        mBsbText = (TextView) findViewById(R.id.confirm_reimbursement_bsb);
        mAccountNumberText = (TextView) findViewById(R.id.confirm_reimbursement_account);
        mGoodsText = (TextView) findViewById(R.id.confirm_reimbursement_goods);
        mCompanyText = (TextView) findViewById(R.id.confirm_reimbursement_company);
        mPaymentMethodText = (TextView) findViewById(R.id.confirm_reimbursement_payment_method);
        mAmountText = (TextView) findViewById(R.id.confirm_reimbursement_amount);
        mPurchaseDateText = (TextView) findViewById(R.id.confirm_reimbursement_purchase_date);
        mReceiptImage = (ImageView) findViewById(R.id.confirm_reimbursement_picture);
        mCheckbox = (CheckBox) findViewById(R.id.confirm_reimbursement_checkbox);
        mSubmitBtn = (Button) findViewById(R.id.confirm_reimbursement_submit);

        mPurchaserText.setText(getString(R.string.purchaser_colon) + mPurchaser);
        mBsbText.setText(getString(R.string.bsb_colon) + mBsb.substring(0, 3) + "-" + mBsb.substring(3));
        mAccountNumberText.setText(getString(R.string.account_colon) + mAccountNumber);
        mAmountText.setText(getString(R.string.amount_colon) + mAmount);
        mGoodsText.setText(getString(R.string.goods_colon) + mGoods);
        mCompanyText.setText(getString(R.string.company_colon) + mCompany);
        mPurchaseDateText.setText(getString(R.string.purchase_date_colon) + mPurchaseDate);
        mPaymentMethodText.setText(getString(R.string.payment_method_colon) + mPaymentMethod);

        File imgFile = new File(mFilePath);

        if(imgFile.exists()){
            Bitmap imageBmp = BitmapFactory.decodeFile(imgFile.getAbsolutePath());
            mReceiptImage.setImageBitmap(imageBmp);
        }

        //checks whether the confirm box is ticked, then confirms the validity of the data to the previous activity
        mSubmitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                boolean correctDetails = mCheckbox.isChecked();

                if (correctDetails) {
                    Intent confirm = new Intent();

                    confirm.putExtra(RESULT_CONFIRM, correctDetails);
                    setResult(Activity.RESULT_OK, confirm);

                    finish();
                }
                else {
                    Snackbar s = Snackbar.make(v, getString(R.string.confirm_details_prompt), Snackbar.LENGTH_LONG);
                    s.show();
                }
            }
        });

    }
}
