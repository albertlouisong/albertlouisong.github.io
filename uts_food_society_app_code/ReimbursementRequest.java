package com.uts.foodsoc.utsfoodsocietyapp;

/**
 * Created by albiedw on 5/10/17.
 */

/**
 * Object representing a request from a UTS Food Society member to be reimbursed for a purchase
 * made related to an event held
 */
public class ReimbursementRequest {

    private String mPurchaser;
    private String mBsb;
    private String mAccountNumber;
    private String mAmount;
    private String mGoods;
    private String mCompany;
    private String mPurchaseMethod;
    private String mReceiptPicture;
    private String mEventKey;
    private String mEventName;
    private String mPurchaseDate;
    private String mEventId;
    private String mReceiptKey;
    private String mKey;

    public ReimbursementRequest (String purchaser, String bsb, String accountNumber, String amount, String goods,
                                 String company, String purchaseMethod, String date, String receiptPicture, String eventKey,
                                 String receiptKey, String key) {
        this.mPurchaser = purchaser;
        this.mBsb = bsb;
        this.mAccountNumber = accountNumber;
        this.mAmount = amount;
        this.mGoods = goods;
        this.mCompany = company;
        this.mPurchaseMethod = purchaseMethod;
        this.mPurchaseDate = date;
        this.mReceiptPicture = receiptPicture;
        this.mEventKey = eventKey;
        this.mReceiptKey = receiptKey;
        this.mKey = key;
    }

    public ReimbursementRequest () {

    }

    public String getPurchaser() {
        return mPurchaser;
    }
    public void setPurchaser(String mPurchaser) {
        this.mPurchaser = mPurchaser;
    }

    public String getBsb() {
        return mBsb;
    }
    public void setBsb(String mBsb) {
        this.mBsb = mBsb;
    }

    public String getAccountNumber() {
        return mAccountNumber;
    }
    public void setAccountNumber(String mAccountNumber) {
        this.mAccountNumber = mAccountNumber;
    }

    public String getAmount() {
        return mAmount;
    }
    public void setAmount(String mAmount) {
        this.mAmount = mAmount;
    }

    public String getGoods() {
        return mGoods;
    }
    public void setGoods(String mGoods) {
        this.mGoods = mGoods;
    }

    public String getCompany() {
        return mCompany;
    }
    public void setCompany(String mCompany) {
        this.mCompany = mCompany;
    }

    public String getPurchaseMethod() {
        return mPurchaseMethod;
    }
    public void setPurchaseMethod(String mPurchaseMethod) { this.mPurchaseMethod = mPurchaseMethod; }

    public String getReceiptPicture() {
        return mReceiptPicture;
    }
    public void setReceiptPicture(String mReceiptPicture) { this.mReceiptPicture = mReceiptPicture; }

    public String getEventKey() {
        return mEventKey;
    }
    public void setEventKey(String mEventKey) { this.mEventKey = mEventKey; }

    public String getEventName() {
        return mEventName;
    }

    public void setEventName(String mEventName) {
        this.mEventName = mEventName;
    }

    public String getPurchaseDate() {
        return mPurchaseDate;
    }

    public void setPurchaseDate(String mPurchaseDate) {
        this.mPurchaseDate = mPurchaseDate;
    }

    public String getEventId() {
        return mEventId;
    }

    public void setEventId(String mEventId) {
        this.mEventId = mEventId;
    }

    public String getReceiptKey() {
        return mReceiptKey;
    }

    public void setReceiptKey(String mReceiptKey) {
        this.mReceiptKey = mReceiptKey;
    }

    public String getKey() {
        return mKey;
    }

    public void setKey(String mKey) {
        this.mKey = mKey;
    }
}
