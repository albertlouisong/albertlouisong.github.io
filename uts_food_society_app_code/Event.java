package com.uts.foodsoc.utsfoodsocietyapp;

import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by albiedw on 28/8/17.
 */


/**
 * Event class is an object that represents an event held by UTS Food Appreciation society that is
 * displayed in the home page recycler view and event details pages
 */
public class Event {

    private String mName;
    private String mMonth;
    private String mDay;
    private String mDateno;
    private String mTime;
    private String mDescription;
    private String mLocation;
    private String mBanner;
    private String mKey;
    private String mId;
    private int mReceiptCount;
    private byte[] mBannerByteArray;

    public Event () {

    }

    public Event (String eventName, Date date, String time,
                  String description, String location, String banner, String id) {
        this.mName = eventName;
        this.mMonth = new SimpleDateFormat("MMMM").format(date);
        this.mDay = new SimpleDateFormat("EEEE").format(date);
        this.mDateno = new SimpleDateFormat("d").format(date);
        this.mTime = time;
        this.mDescription = description;
        this.mLocation = location;
        this.mBanner = banner;
        this.mId = id;
        this.mReceiptCount = 0;
    }

    public String getName() { return mName; }
    public void setName(String mName) { this.mName = mName; }

    public String getMonth() { return mMonth; }
    public void setMonth(String mMonth) { this.mMonth = mMonth; }

    public String getDay() { return mDay; }
    public void setDay(String mDay) { this.mDay = mDay; }

    public String getDateno() { return mDateno; }
    public void setDateno(String mDateno) { this.mDateno = mDateno; }

    public String getTime() { return mTime; }
    public void setTime(String time) { this.mTime = time; }

    public String getDescription() { return mDescription; }
    public void setDescription(String mDescription) { this.mDescription = mDescription; }

    public String getLocation() { return mLocation; }
    public void setLocation(String location) { this.mLocation = location; }

    public String getBanner() { return mBanner; }
    public void setBanner(String mBanner) { this.mBanner = mBanner; }

    public byte[] getBannerByteArray() { return mBannerByteArray; }
    public void setBannerByteArray(byte[] mBannerByteArray) { this.mBannerByteArray = mBannerByteArray; }

    public String getKey() { return mKey; }
    public void setKey(String mKey) { this.mKey = mKey; }

    public String getId() { return mId; }
    public void setId(String mId) { this.mId = mId; }

    public int getReceiptCount() {
        return mReceiptCount;
    }

    public void setReceiptCount(int mReceiptCount) {
        this.mReceiptCount = mReceiptCount;
    }
}
