package com.uts.foodsoc.utsfoodsocietyapp;


/**
 * Created by albiedw on 28/8/17.
 */

/**
 * Receipt object reflects a row in the treasury receipt spreadsheet on the UTS Food Appreciation
 * Society Google Drive
 */
public class Receipt {

    private String mId;
    private String mAmount;
    private String mCompany;
    private String mGoods;
    private String mPurchaseMethod;
    private String mPurchaseDate;
    private String mPurchaser;
    private String mReimbursed;
    private String mReimburseDate;
    private String mActivateReimbursed;

    public Receipt (String id, String amount, String company, String goods, String paymentmethod,
                    String datePurchased, String purchaser) {
        this.mId = id;
        this.mAmount = amount;
        this.mCompany = company;
        this.mGoods = goods;
        this.mPurchaseMethod = paymentmethod;
        this.mPurchaseDate = datePurchased;
        this.mPurchaser = purchaser;
        this.mReimbursed = "No";
        this.mActivateReimbursed = "No";
    }

    public String getId() { return mId; }
    public void setId(String mId) { this.mId = mId; }

    public String getAmount() { return mAmount; }
    public void setAmount(String mAmount) { this.mAmount = mAmount; }

    public String getCompany() { return mCompany; }
    public void setCompany(String mCompany) { this.mCompany = mCompany; }

    public String getGoods() { return mGoods; }
    public void setGoods(String mGoods) { this.mGoods = mGoods; }

    public String getPurchaseMethod() { return mPurchaseMethod; }
    public void setPurchaseMethod(String mPurchaseMethod) { this.mPurchaseMethod = mPurchaseMethod; }

    public String getPurchaseDate() { return mPurchaseDate; }
    public void setPurchaseDate(String mPurchaseDate) { this.mPurchaseDate = mPurchaseDate; }

    public String getPurchaser() { return mPurchaser; }
    public void setPurchaser(String mPurchaser) { this.mPurchaser = mPurchaser; }

    public String isReimbursed() { return mReimbursed; }
    public void setReimbursed(String mReimbursed) { this.mReimbursed = mReimbursed; }

    public String getReimburseDate() { return mReimburseDate;}
    public void setReimburseDate(String mReimburseDate) { this.mReimburseDate = mReimburseDate; }

    public String isActivateReimbursed() { return mActivateReimbursed; }
    public void setActivateReimbursed(String mActivateReimbursed) { this.mActivateReimbursed = mActivateReimbursed; }


}