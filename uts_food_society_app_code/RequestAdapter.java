package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by albiedw on 11/10/17.
 */

public class RequestAdapter extends RecyclerView.Adapter<RequestAdapter.ViewHolder> {

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

    private ArrayList<ReimbursementRequest> mRequestsList;
    private Context mContext;

    public class ViewHolder extends RecyclerView.ViewHolder{

        public TextView requestPurchaser;
        public TextView requestEvent;
        public TextView requestAmount;
        public Button requestButton;

        public ViewHolder(View itemView) {
            super(itemView);

            requestPurchaser = (TextView) itemView.findViewById(R.id.request_view_purchaser);
            requestEvent = (TextView) itemView.findViewById(R.id.request_view_event_name);
            requestAmount = (TextView) itemView.findViewById(R.id.request_view_amount);
            requestButton = (Button) itemView.findViewById(R.id.request_view_button);
        }
    }

    public RequestAdapter (Context context, ArrayList<ReimbursementRequest> requests) {
        this.mContext = context;
        this.mRequestsList = requests;
    }

    @Override
    public RequestAdapter.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.activity_request_view, parent, false);

        return new ViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(RequestAdapter.ViewHolder holder, final int position) {

        final ReimbursementRequest request = mRequestsList.get(position);

        holder.requestButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent requestViewIntent = new Intent(mContext, RequestDetails.class);

                requestViewIntent.putExtra(EXTRA_PURCHASER, request.getPurchaser());
                requestViewIntent.putExtra(EXTRA_BSB, request.getBsb());
                requestViewIntent.putExtra(EXTRA_ACCOUNT, request.getAccountNumber());
                requestViewIntent.putExtra(EXTRA_AMOUNT, request.getAmount());
                requestViewIntent.putExtra(EXTRA_GOODS, request.getGoods());
                requestViewIntent.putExtra(EXTRA_COMPANY, request.getCompany());
                requestViewIntent.putExtra(EXTRA_PAYMENT_METHOD, request.getPurchaseMethod());
                requestViewIntent.putExtra(EXTRA_FILE_PATH, request.getReceiptPicture());
                requestViewIntent.putExtra(EXTRA_EVENT, request.getEventName());
                requestViewIntent.putExtra(EXTRA_PURCHASE_DATE, request.getPurchaseDate());
                requestViewIntent.putExtra(EXTRA_RECEIPT_KEY, request.getReceiptKey());
                requestViewIntent.putExtra(EXTRA_REQUEST_KEY, request.getKey());

                mContext.startActivity(requestViewIntent);

            }
        });

        holder.requestPurchaser.setText(request.getPurchaser());
        holder.requestAmount.setText(request.getAmount());
        holder.requestEvent.setText(request.getEventName());

    }

    @Override
    public int getItemCount() {
        return mRequestsList.size();
    }
}
