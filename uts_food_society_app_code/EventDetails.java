package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.Intent;
import android.graphics.BitmapFactory;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;

/**
 * Activity that displays the details of a single event held by UTS Food Appreciation Society
 */
public class EventDetails extends AppCompatActivity {

    public static final String EXTRA_EVENT_KEY = "EVENTKEY";
    public static final String EXTRA_NAME = "NAME";
    public static final String EXTRA_DATE = "DATE";
    public static final String EXTRA_LOCATION = "LOCATION";
    public static final String EXTRA_DESCRIPTION = "DESCRIPTION";
    public static final String EXTRA_FILE_PATH = "IMAGEFILEPATH";

    private StorageReference mStorageReference;

    private String mEventName;
    private String mEventDateTime;
    private String mEventLocation;
    private String mEventDescription;
    private String mEventBannerPath;
    private String mEventKey;

    private TextView mEventNameText;
    private TextView mEventDateTimeText;
    private TextView mEventLocationText;
    private TextView mEventDescriptionText;
    private ImageView mEventImageView;
    private ProgressBar mEventProgressBar;

    private Button mRequestButton;

    /**
     * Takes extras from the event passed in the list from the home page and displays them in the
     * initialised views in the layout
     * @param savedInstanceState saved state from before the app was previously closed
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_event_details);

        mStorageReference = FirebaseStorage.getInstance().getReference();

        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                mEventName = null;
                mEventDateTime = null;
                mEventLocation = null;
                mEventDescription = null;
                mEventBannerPath = null;
                mEventKey = null;
            } else {
                mEventName = extras.getString(EXTRA_NAME);
                mEventDateTime = extras.getString(EXTRA_DATE);
                mEventLocation = extras.getString(EXTRA_LOCATION);
                mEventDescription = extras.getString(EXTRA_DESCRIPTION);
                mEventBannerPath = extras.getString(EXTRA_FILE_PATH);
                mEventKey = extras.getString(EXTRA_EVENT_KEY);
            }
        } else {
            mEventName = (String) savedInstanceState.getSerializable(EXTRA_NAME);
            mEventDateTime = (String) savedInstanceState.getSerializable(EXTRA_DATE);
            mEventLocation = (String) savedInstanceState.getSerializable(EXTRA_LOCATION);
            mEventDescription = (String) savedInstanceState.getSerializable(EXTRA_DESCRIPTION);
            mEventBannerPath = (String) savedInstanceState.getSerializable(EXTRA_FILE_PATH);
            mEventKey = (String) savedInstanceState.getSerializable(EXTRA_EVENT_KEY);
        }

        mEventNameText = (TextView) findViewById(R.id.event_details_name);
        mEventDateTimeText = (TextView) findViewById(R.id.event_details_date_time);
        mEventLocationText = (TextView) findViewById(R.id.event_details_location);
        mEventDescriptionText = (TextView) findViewById(R.id.event_details_description);
        mEventImageView = (ImageView) findViewById(R.id.event_details_banner);

        mEventProgressBar = (ProgressBar) findViewById(R.id.event_details_progress_bar);

        mRequestButton = (Button) findViewById(R.id.event_details_request_reimbursement);

        mEventNameText.setText(mEventName);
        mEventDateTimeText.setText(mEventDateTime);
        mEventLocationText.setText(mEventLocation);
        mEventDescriptionText.setText(mEventDescription);

        //checks if the event banner has been downloaded yet and sets the image view when it finishes downloading
        StorageReference thumbnailRef = mStorageReference.child(mEventBannerPath);
        if (thumbnailRef != null) {
            final long TWO_MEGABYTE = 2 * 1024 * 1024;
            thumbnailRef.getBytes(TWO_MEGABYTE).addOnSuccessListener(new OnSuccessListener<byte[]>() {
                @Override
                public void onSuccess(byte[] bytes) {
                    mEventProgressBar.setVisibility(View.GONE);
                    mEventImageView.setImageBitmap(BitmapFactory.decodeByteArray(bytes,
                            0, bytes.length));
                }
            }).addOnFailureListener(new OnFailureListener() {
                @Override
                public void onFailure(@NonNull Exception exception) {
                    mEventProgressBar.setVisibility(View.GONE);
                    // Handle any errors
                }
            });
        }

        //opens a form that requests a reimbursement for a transaction related to this event
        mRequestButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent requestReimbursementIntent = new Intent(EventDetails.this, RequestReimbursement.class);

                requestReimbursementIntent.putExtra(EXTRA_EVENT_KEY, mEventKey);
                
                startActivity(requestReimbursementIntent);
            }
        });
    }


}
