package com.uts.foodsoc.utsfoodsocietyapp;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.Configuration;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.net.Uri;
import android.provider.MediaStore;
import android.support.annotation.NonNull;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.OnPausedListener;
import com.google.firebase.storage.OnProgressListener;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;

/**
 * Activity that has a form to be filled out by a UTS Food Appreciation Society member who has made
 * a purchase in relation to an event held
 */
public class RequestReimbursement extends AppCompatActivity {

    private static final int RESULT_LOAD_IMAGE = 1;
    private static final int RESULT_CONFIRM_REIMBURSEMENT = 5;
    private static final int MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE = 3;
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
    public static final String EXTRA_EVENT_KEY = "EVENTKEY";

    private String mEventKey;

    private int mReceiptCount;

    private String mFilePath;

    private Bitmap mReceiptPictureBitmap;

    private Event mEvent;

    private TextView mEventName;

    private EditText mReceiptPurchaser;
    private EditText mReceiptBSB;
    private EditText mReceiptAccountNumber;
    private EditText mReceiptAmount;
    private EditText mReceiptGoods;
    private EditText mReceiptCompany;
    private TextView mSelectDate;

    private Spinner mReceiptPaymentMethod;
    private ImageView mReceiptPicture;

    private Button mSubmitBtn;

    private DatabaseReference mEventsDatabaseReference;
    private StorageReference mStorageReference;

    private DatePickerDialog.OnDateSetListener mDateSetListener;

    /**
     * Initialises the layout elements in the activity and sets listeners
     * @param savedInstanceState saved state of the application from when it was previously closed
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_request_reimbursement);

        mEventsDatabaseReference = FirebaseDatabase.getInstance().getReference();
        mStorageReference = FirebaseStorage.getInstance().getReference();

        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                mEventKey = null;
            } else {
                mEventKey = extras.getString(EXTRA_EVENT_KEY);
            }
        } else {
            mEventKey = (String) savedInstanceState.getSerializable(EXTRA_EVENT_KEY);
        }

        mEventName = (TextView) findViewById(R.id.request_reimbursement_event_name);

        mReceiptPurchaser = (EditText) findViewById(R.id.request_reimbursement_purchaser);
        mReceiptBSB = (EditText) findViewById(R.id.request_reimbursement_bsb);
        mReceiptAccountNumber = (EditText) findViewById(R.id.request_reimbursement_account_number);
        mReceiptAmount = (EditText) findViewById(R.id.request_reimbursement_amount);
        mReceiptGoods = (EditText) findViewById(R.id.request_reimbursement_goods);
        mReceiptCompany = (EditText) findViewById(R.id.request_reimbursement_vendor);
        mSelectDate = (TextView) findViewById(R.id.request_reimbursement_select_date);

        mReceiptPaymentMethod = (Spinner) findViewById(R.id.request_reimbursement_spinner);
        mReceiptPicture = (ImageView) findViewById(R.id.request_reimbursement_add_image);

        mSubmitBtn = (Button) findViewById(R.id.request_reimbursement_submit);

        //checks input for completion and validity before passing data to the next activity for confirmation
        mSubmitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (mReceiptPurchaser.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.request_purchaser_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptBSB.getText().length() != 6) {
                    Snackbar s = Snackbar.make(v, R.string.request_bsb_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptAccountNumber.getText().length() < 8) {
                    Snackbar s = Snackbar.make(v, R.string.request_account_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptAmount.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.request_receipt_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptGoods.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.request_goods_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptCompany.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.request_company_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mReceiptPictureBitmap == null) {
                    Snackbar s = Snackbar.make(v, R.string.request_image_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else {
                        String purchaser = mReceiptPurchaser.getText().toString();
                        String bsb = mReceiptBSB.getText().toString();
                        String accountNumber = mReceiptAccountNumber.getText().toString();
                        String amount = mReceiptAmount.getText().toString();
                        String goods = mReceiptGoods.getText().toString();
                        String company = mReceiptCompany.getText().toString();
                        String date = mSelectDate.getText().toString();
                        String paymentMethod = mReceiptPaymentMethod.getSelectedItem().toString();
                        ByteArrayOutputStream receiptImage = new ByteArrayOutputStream();

                        if (mReceiptPictureBitmap != null) {
                            mReceiptPictureBitmap.compress(Bitmap.CompressFormat.PNG, 50, receiptImage);
                        }
                        Intent confirmEventIntent = new Intent(RequestReimbursement.this, ConfirmReimbursement.class);

                        confirmEventIntent.putExtra(EXTRA_PURCHASER, purchaser);
                        confirmEventIntent.putExtra(EXTRA_BSB, bsb);
                        confirmEventIntent.putExtra(EXTRA_ACCOUNT, accountNumber);
                        confirmEventIntent.putExtra(EXTRA_AMOUNT, amount);
                        confirmEventIntent.putExtra(EXTRA_GOODS, goods);
                        confirmEventIntent.putExtra(EXTRA_COMPANY, company);
                        confirmEventIntent.putExtra(EXTRA_PAYMENT_METHOD, paymentMethod);
                        confirmEventIntent.putExtra(EXTRA_PURCHASE_DATE, date);
                        confirmEventIntent.putExtra(EXTRA_FILE_PATH, mFilePath);

                        startActivityForResult(confirmEventIntent, RESULT_CONFIRM_REIMBURSEMENT);
                }
            }
        });

        //Gets the receipt count for the related event from the Firebase database to set the ID for this receipt
        DatabaseReference eventReference = mEventsDatabaseReference.child("events").child(mEventKey);
        eventReference.addListenerForSingleValueEvent(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                mEvent = dataSnapshot.getValue(Event.class);
                mEventName.setText(mEvent.getName() + getString(R.string.reimbursement_request_space));
                mReceiptCount = mEvent.getReceiptCount();
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });

        //Opens a date picker
        mSelectDate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Calendar cal = Calendar.getInstance();
                int year = cal.get(Calendar.YEAR);
                int month = cal.get(Calendar.MONTH);
                int day = cal.get(Calendar.DAY_OF_MONTH);

                DatePickerDialog dialog = new DatePickerDialog(RequestReimbursement.this, R.style.Theme_AppCompat_Dialog_MinWidth,
                        mDateSetListener, year, month, day);
                dialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
                dialog.show();
            }
        });
        mDateSetListener = new DatePickerDialog.OnDateSetListener() {

            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                Calendar cal = Calendar.getInstance();
                cal.set(year, month, dayOfMonth);

                SimpleDateFormat simpledateformat = new SimpleDateFormat("dd/MM/yyyy");
                String date = simpledateformat.format(cal.getTime());
                mSelectDate.setText(date);
            }
        };

        //Uploads a picture from the gallery
        mReceiptPicture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (checkSelfPermission(android.Manifest.permission.READ_EXTERNAL_STORAGE)
                        != PackageManager.PERMISSION_GRANTED) {

                    // Should we show an explanation?
                    if (shouldShowRequestPermissionRationale(
                            android.Manifest.permission.READ_EXTERNAL_STORAGE)) {
                        // Explain to the user why we need to read the contacts
                    }

                    requestPermissions(new String[]{android.Manifest.permission.READ_EXTERNAL_STORAGE},
                            MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE);

                    // MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE is an
                    // app-defined int constant that should be quite unique

                    return;
                }

                Intent pickImageIntent = new Intent(Intent.ACTION_PICK);
                pickImageIntent.setType("image/*");
                startActivityForResult(pickImageIntent, RESULT_LOAD_IMAGE);
            }
        });

        //Parses input for the text view to be currency
        mReceiptAmount.setRawInputType(Configuration.KEYBOARD_12KEY);
        mReceiptAmount.addTextChangedListener(new TextWatcher() {
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
                    mReceiptAmount.removeTextChangedListener(this);

                    String cleanString = s.toString().replaceAll("[$,.]", "");

                    double parsed = Double.parseDouble(cleanString);
                    String formatted = NumberFormat.getCurrencyInstance().format((parsed/100));

                    current = formatted;
                    mReceiptAmount.setText(formatted);
                    mReceiptAmount.setSelection(formatted.length());

                    mReceiptAmount.addTextChangedListener(this);
                }
            }
        });
    }

    /**
     * Handles data passed back from other activities, either uploading receipt image or
     * confirming request data
     * @param reqCode either upload image code or confirm data code
     * @param resultCode whether there is data to be handled
     * @param data actual data to handle
     */
    @Override
    protected void onActivityResult(int reqCode, int resultCode, Intent data) {
        super.onActivityResult(reqCode, resultCode, data);

        //Uploads the picture from gallery to the image view in the layout
        if (reqCode == RESULT_LOAD_IMAGE) {
            if (resultCode == RESULT_OK) {
                try {
                    final Uri imageUri = data.getData();
                    final InputStream imageStream = getContentResolver().openInputStream(imageUri);
                    final Bitmap selectedImage = BitmapFactory.decodeStream(imageStream);
                    mFilePath = getRealPathFromURI(imageUri);
                    mReceiptPicture.setImageBitmap(selectedImage);
                    mReceiptPictureBitmap = selectedImage;
                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                    Toast.makeText(RequestReimbursement.this, getString(R.string.image_error), Toast.LENGTH_LONG).show();
                }

            }
        }

        //uploads request data to the Firebase database upon confirmation from the next activity
        if (reqCode == RESULT_CONFIRM_REIMBURSEMENT) {
            if (data != null) {
                if (data.getExtras().getBoolean(RESULT_CONFIRM)) {
                    if (mReceiptPictureBitmap != null) {

                        //formats string for the receipt ID
                        NumberFormat formatter = new DecimalFormat("00");
                        String s = formatter.format(mReceiptCount + 1);

                        String receiptId = mEvent.getId() + "-" + s;

                        //Constructs receipt to be uploaded
                        Receipt newReceipt = new Receipt(receiptId, mReceiptAmount.getText().toString(),
                                mReceiptCompany.getText().toString(), mReceiptGoods.getText().toString(),
                                mReceiptPaymentMethod.getSelectedItem().toString(), mSelectDate.getText().toString(),
                                mReceiptPurchaser.getText().toString());

                        //Uploads receipt to Firebase database
                        DatabaseReference newReceiptReference = mEventsDatabaseReference.child("receipts").push();
                        String newReceiptId = newReceiptReference.getKey();
                        mEventsDatabaseReference.child("receipts").child(newReceiptId).setValue(newReceipt);

                        //Creates request at Firebase databse
                        DatabaseReference newRequestReference = mEventsDatabaseReference.child("requests").push();
                        String newRequestId = newRequestReference.getKey();

                        //Sets storage path for the receipt picture
                        String storageRefPath = "events/" + mEvent.getName() + "/" + mEvent.getName().toString()
                                + " " + receiptId + ".bmp";
                        StorageReference storageRef = mStorageReference.child(storageRefPath);

                        //Converts the image view content to a bitmap to upload
                        ByteArrayOutputStream receiptImage = new ByteArrayOutputStream();
                        mReceiptPictureBitmap.compress(Bitmap.CompressFormat.PNG, 50, receiptImage);

                        //Uploads the image to Firebase storage
                        UploadTask uploadTask = storageRef.putBytes(receiptImage.toByteArray());
                        uploadTask.addOnProgressListener(new OnProgressListener<UploadTask.TaskSnapshot>() {
                            @Override
                            public void onProgress(UploadTask.TaskSnapshot taskSnapshot) {
                                @SuppressWarnings("VisibleForTests")
                                double progress = (100.0 * taskSnapshot.getBytesTransferred()) / taskSnapshot.getTotalByteCount();
                            }
                        }).addOnPausedListener(new OnPausedListener<UploadTask.TaskSnapshot>() {
                            @Override
                            public void onPaused(UploadTask.TaskSnapshot taskSnapshot) {
                            }
                        }).addOnFailureListener(new OnFailureListener() {
                            @Override
                            public void onFailure(@NonNull Exception exception) {
                                Toast.makeText(getApplicationContext(), getString(R.string.receipt_upload_fail), Toast.LENGTH_SHORT).show();
                                // Handle unsuccessful uploads
                            }
                        }).addOnSuccessListener(new OnSuccessListener<UploadTask.TaskSnapshot>() {
                            @Override
                            public void onSuccess(UploadTask.TaskSnapshot taskSnapshot) {
                                // Handle successful uploads on complete
                                @SuppressWarnings("VisibleForTests") Uri downloadUrl = taskSnapshot.getMetadata().getDownloadUrl();
                                Toast.makeText(getApplicationContext(), getString(R.string.receipt_upload_success), Toast.LENGTH_SHORT).show();
                            }
                        });

                        //Constructs a reimbursement request with the data
                        ReimbursementRequest newRequest = new ReimbursementRequest(mReceiptPurchaser.getText().toString(),
                                (mReceiptBSB.getText().toString()), (mReceiptAccountNumber.getText().toString()),
                                mReceiptAmount.getText().toString(), mReceiptGoods.getText().toString(),
                                mReceiptCompany.getText().toString(), mReceiptPaymentMethod.getSelectedItem().toString(),
                                mSelectDate.getText().toString(), storageRefPath, mEventKey, newReceiptReference.getKey(),
                                newRequestId);

                        newRequest.setEventName(mEvent.getName());
                        newRequest.setEventId(mEvent.getId());

                        //Uploads the reimbursement data to the Firebase database
                        mEventsDatabaseReference.child("requests").child(newRequestId).setValue(newRequest);

                        mEventsDatabaseReference.child("events").child(mEventKey).child("receiptCount").setValue(mReceiptCount + 1);

                    }

                    finish();

                }
            }
        }
    }

    /**
     * Borrowed from a StackOverflow answer
     * Gets the filepath of the file chosen in the gallery so that in can be passed on to the next
     * activity rather than the full image file
     * @param uri image resource
     * @return image file path
     */
    public String getRealPathFromURI(Uri uri) {
        String[] projection = { MediaStore.Images.Media.DATA };
        @SuppressWarnings("deprecation")
        Cursor cursor = managedQuery(uri, projection, null, null, null);
        int column_index = cursor
                .getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        return cursor.getString(column_index);
    }
}
