package com.uts.foodsoc.utsfoodsocietyapp;

import android.app.DatePickerDialog;
import android.app.TimePickerDialog;
import android.content.Intent;
import android.content.pm.PackageManager;
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
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.OnPausedListener;
import com.google.firebase.storage.OnProgressListener;
import com.google.firebase.storage.StorageReference;
import com.google.firebase.storage.UploadTask;

import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

/**
 * Form that the user inputs to create an event to upload to the Firebase database
 */
public class CreateEvent extends AppCompatActivity {

    private static final int RESULT_LOAD_IMAGE = 1;
    private static final int RESULT_CONFIRM_EVENT = 2;
    private static final int MY_PERMISSIONS_REQUEST_READ_EXTERNAL_STORAGE = 3;
    public static final String RESULT_CONFIRM = "result";

    public static final String EXTRA_NAME = "NAME";
    public static final String EXTRA_ID = "ID";
    public static final String EXTRA_DATE = "DATE";
    public static final String EXTRA_TIME = "TIME";
    public static final String EXTRA_LOCATION = "LOCATION";
    public static final String EXTRA_DESCRIPTION = "DESCRIPTION";
    public static final String EXTRA_FILE_PATH = "IMAGEFILEPATH";

    private DatabaseReference mEventsDatabaseReference;
    private StorageReference mStorageReference;

    private EditText mEventName;
    private EditText mEventId;
    private TextView mEventDate;
    private TextView mEventTime;
    private EditText mEventLocation;
    private EditText mEventDescription;
    private Button mSubmitBtn;
    private ImageView mEventImage;
    private Date mDate;
    private Bitmap mEventImageBitmap;
    private String mFilePath;

    private DatePickerDialog.OnDateSetListener mDateSetListener;
    private TimePickerDialog.OnTimeSetListener mTimeSetListener;

    /**
     * Initialises the views in the layout and sets listeners for each interactable view element
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_event);

        mEventsDatabaseReference = FirebaseDatabase.getInstance().getReference();
        mStorageReference = FirebaseStorage.getInstance().getReference();

        mEventName = (EditText) findViewById(R.id.create_event_name);
        mEventId = (EditText) findViewById(R.id.create_event_id);
        mEventDate = (TextView) findViewById(R.id.create_event_date);
        mEventTime = (TextView) findViewById(R.id.create_event_time);
        mEventLocation = (EditText) findViewById(R.id.create_event_location);
        mEventDescription = (EditText) findViewById(R.id.create_event_description);
        mSubmitBtn = (Button) findViewById(R.id.create_event_submit);
        mEventImage = (ImageView) findViewById(R.id.create_event_image);

        //Checks fields for completion and validity before passing data to the next activity to confirm details
        mSubmitBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (mEventName.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.event_name_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventId.getText().length() != 3) {
                    Snackbar s = Snackbar.make(v, R.string.event_id_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventLocation.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.event_location_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventDate.getText().equals(getString(R.string.select_date))) {
                    Snackbar s = Snackbar.make(v, R.string.event_date_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventTime.getText().equals(R.string.select_time)) {
                    Snackbar s = Snackbar.make(v, R.string.event_time_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventDescription.getText().length()<1) {
                    Snackbar s = Snackbar.make(v, R.string.event_description_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else if (mEventImageBitmap == null) {
                    Snackbar s = Snackbar.make(v, R.string.event_image_error, Snackbar.LENGTH_LONG);
                    s.show();
                }
                else {
                    String eventName = mEventName.getText().toString();
                    String eventId = mEventId.getText().toString();
                    String eventDateStr = mEventDate.getText().toString();
                    String eventTime = mEventTime.getText().toString();
                    String eventLocation = mEventLocation.getText().toString();
                    String eventDescription = mEventDescription.getText().toString();
                    ByteArrayOutputStream eventImage = new ByteArrayOutputStream();

                    if (mEventImageBitmap != null) {
                        mEventImageBitmap.compress(Bitmap.CompressFormat.PNG, 50, eventImage);
                    }
                    Intent confirmEventIntent = new Intent(CreateEvent.this, ConfirmEvent.class);

                    confirmEventIntent.putExtra(EXTRA_NAME, eventName);
                    confirmEventIntent.putExtra(EXTRA_ID, eventId);
                    confirmEventIntent.putExtra(EXTRA_DATE, eventDateStr);
                    confirmEventIntent.putExtra(EXTRA_TIME, eventTime);
                    confirmEventIntent.putExtra(EXTRA_LOCATION, eventLocation);
                    confirmEventIntent.putExtra(EXTRA_DESCRIPTION, eventDescription);
                    confirmEventIntent.putExtra(EXTRA_FILE_PATH, mFilePath);

                    startActivityForResult(confirmEventIntent, RESULT_CONFIRM_EVENT);
                }
            }
        });

        //Uploads an image from the gallery
        mEventImage.setOnClickListener(new View.OnClickListener() {
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

        //Opens a calendar picker
        mEventDate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Calendar cal = Calendar.getInstance();
                int year = cal.get(Calendar.YEAR);
                int month = cal.get(Calendar.MONTH);
                int day = cal.get(Calendar.DAY_OF_MONTH);

                DatePickerDialog dialog = new DatePickerDialog(CreateEvent.this, R.style.Theme_AppCompat_Dialog_MinWidth,
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

                SimpleDateFormat simpledateformat = new SimpleDateFormat("EEEE, MMM d");
                String date = simpledateformat.format(cal.getTime());
                mEventDate.setText(date);
                mDate = cal.getTime();
            }
        };

        //Opens a time picker
        mEventTime.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                TimePickerDialog dialog = new TimePickerDialog(CreateEvent.this, R.style.Theme_AppCompat_Dialog_MinWidth,
                        mTimeSetListener, 0, 0, false);
                dialog.getWindow().setBackgroundDrawable(new ColorDrawable(Color.TRANSPARENT));
                dialog.show();
            }
        });
        mTimeSetListener = new TimePickerDialog.OnTimeSetListener() {

            @Override
            public void onTimeSet(TimePicker view, int hourOfDay, int minute) {
                String time = new DecimalFormat("00").format(hourOfDay) + ":" +
                        new DecimalFormat("00").format(minute);
                mEventTime.setText(time);
            }
        };
    }

    /**
     * Handles results from the image upload and confirm activities
     * @param reqCode either the code for uploading the image or confirming the event details
     * @param resultCode confirms that data has been passed back
     * @param data the actual data that is being handled
     */
    @Override
    protected void onActivityResult(int reqCode, int resultCode, Intent data) {
        super.onActivityResult(reqCode, resultCode, data);

        //sets the image view to the image selected in the gallery
        if (reqCode == RESULT_LOAD_IMAGE) {
            if (resultCode == RESULT_OK) {
                try {
                    final Uri imageUri = data.getData();
                    final InputStream imageStream = getContentResolver().openInputStream(imageUri);
                    final Bitmap selectedImage = BitmapFactory.decodeStream(imageStream);
                    mFilePath = getRealPathFromURI(imageUri);
                    mEventImage.setImageBitmap(selectedImage);
                    mEventImageBitmap = selectedImage;
                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                    Toast.makeText(CreateEvent.this, getString(R.string.image_error), Toast.LENGTH_LONG).show();
                }

            } else {
                //Toast.makeText(PostImage.this, "You haven't picked Image", Toast.LENGTH_LONG).show();
            }
        }

        //uploads the data to the Firebase database upon confirmation from the next activity
        if (reqCode == RESULT_CONFIRM_EVENT) {
            if (data != null) {
                if (data.getExtras().getBoolean(RESULT_CONFIRM)) {
                    if (mEventImageBitmap != null) {

                        //Sets path for banner image to be uploaded
                        String storageRefPath = "events/" + mEventName.getText().toString() + "/" + mEventName.getText().toString() + ".bmp";
                        StorageReference storageRef = mStorageReference.child(storageRefPath);

                        //Gets the bitmap from the image view
                        ByteArrayOutputStream eventImage = new ByteArrayOutputStream();
                        mEventImageBitmap.compress(Bitmap.CompressFormat.PNG, 50, eventImage);

                        //Uplaods the image to the Firebase storage
                        UploadTask uploadTask = storageRef.putBytes(eventImage.toByteArray());
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
                                Toast.makeText(getApplicationContext(), getString(R.string.banner_upload_fail), Toast.LENGTH_SHORT).show();
                                // Handle unsuccessful uploads
                            }
                        }).addOnSuccessListener(new OnSuccessListener<UploadTask.TaskSnapshot>() {
                            @Override
                            public void onSuccess(UploadTask.TaskSnapshot taskSnapshot) {
                                // Handle successful uploads on complete
                                @SuppressWarnings("VisibleForTests") Uri downloadUrl = taskSnapshot.getMetadata().getDownloadUrl();
                                Toast.makeText(getApplicationContext(), getString(R.string.banner_upload_success), Toast.LENGTH_SHORT).show();
                            }
                        });

                        //Constructs and event with the data
                        Event newEvent = new Event(mEventName.getText().toString(), mDate, mEventTime.getText().toString(),
                                mEventDescription.getText().toString(), mEventLocation.getText().toString(), storageRefPath,
                                mEventId.getText().toString());

                        //Uploads event data to Firebase database
                        DatabaseReference newEventReference = mEventsDatabaseReference.child("events").push();
                        String newEventId = newEventReference.getKey();
                        mEventsDatabaseReference.child("events").child(newEventId).setValue(newEvent);
                    } else {
                        Event newEvent = new Event(mEventName.getText().toString(), mDate, mEventTime.getText().toString(),
                                mEventDescription.getText().toString(), mEventLocation.getText().toString(), null,
                                mEventId.getText().toString());
                        DatabaseReference newEventReference = mEventsDatabaseReference.child("events").push();
                        String newEventId = newEventReference.getKey();
                        mEventsDatabaseReference.child("events").child(newEventId).setValue(newEvent);
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
