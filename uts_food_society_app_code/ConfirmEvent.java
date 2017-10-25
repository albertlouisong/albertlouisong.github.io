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
 * Confirm Event is an activity that takes the data from the previous 'CreateEvent' activity
 * and displays it for the user to double check and confirm what they have input is correct before
 * it is uploaded to the Firebase database
 */
public class ConfirmEvent extends AppCompatActivity {

    public static final String RESULT_CONFIRM = "result";

    public static final String EXTRA_NAME = "NAME";
    public static final String EXTRA_ID = "ID";
    public static final String EXTRA_DATE = "DATE";
    public static final String EXTRA_TIME = "TIME";
    public static final String EXTRA_LOCATION = "LOCATION";
    public static final String EXTRA_DESCRIPTION = "DESCRIPTION";
    public static final String EXTRA_FILE_PATH = "IMAGEFILEPATH";

    private String mEventName;
    private String mEventId;
    private String mEventDateStr;
    private String mEventTime;
    private String mEventLocation;
    private String mEventDescription;
    private String mImageFilePath;

    private TextView mEventNameText;
    private TextView mEventDateText;
    private TextView mEventTimeText;
    private TextView mEventLocationText;
    private TextView mEventDescriptionText;
    private ImageView mEventImageView;

    private CheckBox mCheckbox;
    private Button mConfirmBtn;

    /**
     * Takes extras passed on from previous activity and initialises them into text views and
     * image view
     * @param savedInstanceState saved state from before the app was previously closed
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_confirm_event);

        if (savedInstanceState == null) {
            Bundle extras = getIntent().getExtras();
            if(extras == null) {
                mEventName = null;
                mEventId = null;
                mEventDateStr = null;
                mEventTime = null;
                mEventLocation = null;
                mEventDescription = null;
                mImageFilePath = null;
            } else {
                mEventName = extras.getString(EXTRA_NAME);
                mEventId = extras.getString(EXTRA_ID);
                mEventDateStr = extras.getString(EXTRA_DATE);
                mEventTime = extras.getString(EXTRA_TIME);
                mEventLocation = extras.getString(EXTRA_LOCATION);
                mEventDescription = extras.getString(EXTRA_DESCRIPTION);
                mImageFilePath = extras.getString(EXTRA_FILE_PATH);
            }
        } else {
            mEventName = (String) savedInstanceState.getSerializable(EXTRA_NAME);
            mEventId = (String) savedInstanceState.getSerializable(EXTRA_ID);
            mEventDateStr = (String) savedInstanceState.getSerializable(EXTRA_DATE);
            mEventTime = (String) savedInstanceState.getSerializable(EXTRA_TIME);
            mEventLocation = (String) savedInstanceState.getSerializable(EXTRA_LOCATION);
            mEventDescription = (String) savedInstanceState.getSerializable(EXTRA_DESCRIPTION);
        }

        mEventNameText = (TextView) findViewById(R.id.confirm_event_name);
        mEventDateText = (TextView) findViewById(R.id.confirm_event_date);
        mEventTimeText = (TextView) findViewById(R.id.confirm_event_time);
        mEventLocationText = (TextView) findViewById(R.id.confirm_event_location);
        mEventDescriptionText = (TextView) findViewById(R.id.confirm_event_description);
        mEventImageView = (ImageView) findViewById(R.id.confirm_event_image);
        mCheckbox = (CheckBox) findViewById(R.id.confirm_event_checkbox);
        mConfirmBtn = (Button) findViewById(R.id.confirm_event_button);

        mEventNameText.setText(mEventId + ": " + mEventName);
        mEventDateText.setText(getString(R.string.date_colon) + mEventDateStr);
        mEventTimeText.setText(getString(R.string.time_colon) + mEventTime);
        mEventLocationText.setText(getString(R.string.location_colon) + mEventLocation);
        mEventDescriptionText.setText(getString(R.string.description_colon) + mEventDescription);

        File imgFile = new File(mImageFilePath);

        if(imgFile.exists()){
            Bitmap imageBmp = BitmapFactory.decodeFile(imgFile.getAbsolutePath());
            mEventImageView.setImageBitmap(imageBmp);
        }


        mCheckbox = (CheckBox) findViewById(R.id.confirm_event_checkbox);
        mConfirmBtn = (Button) findViewById(R.id.confirm_event_button);

        //checks whether the confirm box is ticked, then confirms the validity of the data to the previous activity
        mConfirmBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                boolean correctDetails = mCheckbox.isChecked();

                if (correctDetails) {
                    Intent confirm = new Intent();

                    confirm.putExtra(RESULT_CONFIRM, true);
                    setResult(Activity.RESULT_OK, confirm);

                    finish();
                }
                else {
                    Snackbar s = Snackbar.make(v, R.string.confirm_details_prompt, Snackbar.LENGTH_LONG);
                    s.show();
                }

            }
        });
    }
}
