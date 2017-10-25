package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.res.Configuration;
import android.graphics.BitmapFactory;
import android.support.annotation.NonNull;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;

import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Activity that displays the details of a single reimbursement request for an executive member to
 * confirm. The use case for this screen is that the executive uses the data here to transfer money
 * to the bank account of the purchaser, then confirms the reimbursement in the app, removing it from
 * the list of requests and marking it as reimbursed on the related receipt to be uploaded to the
 * society treasury spreadsheet.
 */
public class RequestDetails extends AppCompatActivity {

    public static final String EXTRA_PURCHASER = "PURCHASER";
    public static final String EXTRA_BSB = "BSB";
    public static final String EXTRA_ACCOUNT = "ACCOUNT";
    public static final String EXTRA_AMOUNT = "AMOUNT";
    public static final String EXTRA_GOODS = "GOODS";
    public static final String EXTRA_COMPANY = "COMPANY";
    public static final String EXTRA_PAYMENT_METHOD = "PAYMENTMETHOD";
    public static final String EXTRA_FILE_PATH = "FILEPATH";
    public static final String EXTRA_EVENT = "EVENT";
    public static final String EXTRA_PURCHASE_DATE = "PURCHASEDATE";
    public static final String EXTRA_RECEIPT_KEY = "RECEIPTKEY";
    public static final String EXTRA_REQUEST_KEY = "REQUESTKEY";

    private String mPurchaser;
    private String mBsb;
    private String mAccountNo;
    private String mCompany;
    private String mGoods;
    private String mDate;
    private String mPaymentMethod;
    private String mEvent;
    private String mAmount;
    private String mReceiptPath;
    private String mReceiptKey;
    private String mRequestKey;

    private TextView mPurchaserText;
    private TextView mBsbText;
    private TextView mAccountNoText;
    private TextView mGoodsText;
    private TextView mCompanyText;
    private TextView mDateText;
    private TextView mPaymentMethodText;
    private TextView mEventText;
    private TextView mAmountText;
    private TextView mActivateReimburseText;
    private CheckBox mActivateCheckbox;
    private CheckBox mConfirmCheckbox;
    private EditText mActivateEditText;
    private ImageView mReceiptPictureView;
    private Button mConfirmButton;
    private ProgressBar mProgressBar;

    private StorageReference mStorageReference;
    private DatabaseReference mDatabaseReference;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_request_details);

        mStorageReference = FirebaseStorage.getInstance().getReference();
        mDatabaseReference = FirebaseDatabase.getInstance().getReference();

        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                mPurchaser = null;
                mBsb = null;
                mAccountNo = null;
                mGoods = null;
                mCompany = null;
                mAmount = null;
                mDate = null;
                mPaymentMethod = null;
                mReceiptPath = null;
                mEvent = null;
                mReceiptKey = null;
                mRequestKey = null;
            } else {
                mPurchaser = extras.getString(EXTRA_PURCHASER);
                mBsb = extras.getString(EXTRA_BSB);
                mAccountNo = extras.getString(EXTRA_ACCOUNT);
                mGoods = extras.getString(EXTRA_GOODS);
                mAmount = extras.getString(EXTRA_AMOUNT);
                mCompany = extras.getString(EXTRA_COMPANY);
                mDate = extras.getString(EXTRA_PURCHASE_DATE);
                mEvent = extras.getString(EXTRA_EVENT);
                mReceiptPath = extras.getString(EXTRA_FILE_PATH);
                mPaymentMethod = extras.getString(EXTRA_PAYMENT_METHOD);
                mReceiptKey = extras.getString(EXTRA_RECEIPT_KEY);
                mRequestKey = extras.getString(EXTRA_REQUEST_KEY);
            }
        } else {
            mPurchaser = (String) savedInstanceState.getSerializable(EXTRA_PURCHASER);
            mBsb = (String) savedInstanceState.getSerializable(EXTRA_BSB);
            mAmount = (String) savedInstanceState.getSerializable(EXTRA_AMOUNT);
            mAccountNo = (String) savedInstanceState.getSerializable(EXTRA_ACCOUNT);
            mGoods = (String) savedInstanceState.getSerializable(EXTRA_GOODS);
            mCompany = (String) savedInstanceState.getSerializable(EXTRA_COMPANY);
            mReceiptPath = (String) savedInstanceState.getSerializable(EXTRA_FILE_PATH);
            mDate = (String) savedInstanceState.getSerializable(EXTRA_PURCHASE_DATE);
            mEvent = (String) savedInstanceState.getSerializable(EXTRA_EVENT);
            mPaymentMethod = (String) savedInstanceState.getSerializable(EXTRA_PAYMENT_METHOD);
            mReceiptKey = (String) savedInstanceState.getSerializable(EXTRA_RECEIPT_KEY);
            mRequestKey = (String) savedInstanceState.getSerializable(EXTRA_REQUEST_KEY);
        }

        mPurchaserText = (TextView) findViewById(R.id.request_details_purchaser);
        mBsbText = (TextView) findViewById(R.id.request_details_bsb);
        mAccountNoText = (TextView) findViewById(R.id.request_details_acc_no);
        mGoodsText = (TextView) findViewById(R.id.request_details_goods);
        mCompanyText = (TextView) findViewById(R.id.request_details_company);
        mDateText = (TextView) findViewById(R.id.request_details_purchase_date);
        mPaymentMethodText = (TextView) findViewById(R.id.request_details_purchase_method);
        mEventText = (TextView) findViewById(R.id.request_details_event);
        mAmountText = (TextView) findViewById(R.id.request_details_amount);
        mActivateReimburseText = (TextView) findViewById(R.id.request_details_activate_label);
        mReceiptPictureView = (ImageView) findViewById(R.id.request_details_receipt_picture);

        mActivateCheckbox = (CheckBox) findViewById(R.id.request_details_activate_checkbox);
        mConfirmCheckbox = (CheckBox) findViewById(R.id.request_details_checkbox);

        mActivateEditText = (EditText) findViewById(R.id.request_details_activate_amount);

        mProgressBar = (ProgressBar) findViewById(R.id.request_details_progress_bar);

        mConfirmButton = (Button) findViewById(R.id.request_details_button);

        mPurchaserText.setText(getString(R.string.purchaser_colon) + mPurchaser);
        mBsbText.setText(getString(R.string.bsb_colon) + mBsb);
        mAccountNoText.setText(getString(R.string.account_colon) + mAccountNo);
        mGoodsText.setText(getString(R.string.goods_colon) + mGoods);
        mCompanyText.setText(getString(R.string.company_colon) + mCompany);
        mDateText.setText(getString(R.string.purchase_date_colon) + mDate);
        mPaymentMethodText.setText(getString(R.string.payment_method_colon) + mPaymentMethod);
        mEventText.setText(getString(R.string.event_colon) + mEvent);
        mAmountText.setText(R.string.amount_colon + mAmount);

        StorageReference pictureRef = mStorageReference.child(mReceiptPath);
        if (pictureRef != null) {
            final long TWO_MEGABYTE = 2 * 1024 * 1024;
            pictureRef.getBytes(TWO_MEGABYTE).addOnSuccessListener(new OnSuccessListener<byte[]>() {
                @Override
                public void onSuccess(byte[] bytes) {
                    mProgressBar.setVisibility(View.GONE);
                    mReceiptPictureView.setImageBitmap(BitmapFactory.decodeByteArray(bytes,
                            0, bytes.length));
                }
            }).addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception exception) {
                    mProgressBar.setVisibility(View.GONE);
                    // Handle any errors
                }
            });
        }

        mActivateCheckbox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if (mActivateCheckbox.isChecked()) {
                    mActivateEditText.setVisibility(View.VISIBLE);
                    mActivateReimburseText.setVisibility(View.VISIBLE);
                }
                else {
                    mActivateEditText.setVisibility(View.INVISIBLE);
                    mActivateReimburseText.setVisibility(View.INVISIBLE);
                }
            }
        });

        mActivateEditText.setRawInputType(Configuration.KEYBOARD_12KEY);
        mActivateEditText.addTextChangedListener(new TextWatcher() {
            String current = "";

            @Override
            public void afterTextChanged(Editable arg0) {
            }

            @Override
            public void beforeTextChanged(CharSequence s, int start,
                                          int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if (!s.toString().equals(current)) {
                    mActivateEditText.removeTextChangedListener(this);

                    String cleanString = s.toString().replaceAll("[$,.]", "");

                    double parsed = Double.parseDouble(cleanString);
                    String formatted = NumberFormat.getCurrencyInstance().format((parsed/100));

                    current = formatted;
                    mActivateEditText.setText(formatted);
                    mActivateEditText.setSelection(formatted.length());

                    mActivateEditText.addTextChangedListener(this);
                }
            }
        });

        mConfirmButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (mConfirmCheckbox.isChecked()) {

                    DatabaseReference receiptReference = mDatabaseReference.child("receipts").child(mReceiptKey);
                    mDatabaseReference.child("requests").child(mRequestKey).removeValue();

                    Date date = new Date();
                    SimpleDateFormat format = new SimpleDateFormat("dd/MM/yyyy");
                    String dateString = format.format(date);

                    if (mActivateCheckbox.isChecked()) {
                        //Create receipt with activate details
                        receiptReference.child("reimbursed").setValue("Yes");
                        receiptReference.child("activateReimbursed").setValue(mActivateEditText.getText().toString());
                        receiptReference.child("reimburseDate").setValue(dateString);

                        finish();
                    }
                    else {
                        //Create receipt without activate details
                        receiptReference.child("reimbursed").setValue("Yes");
                        receiptReference.child("reimburseDate").setValue(dateString);

                        finish();
                    }
                }
                else {
                    Snackbar s = Snackbar.make(v, getString(R.string.confirm_details_prompt), Snackbar.LENGTH_LONG);
                    s.show();
                }
            }
        });
    }
}
