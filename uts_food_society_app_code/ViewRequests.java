package com.uts.foodsoc.utsfoodsocietyapp;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;

import com.google.firebase.database.ChildEventListener;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.Query;

import java.util.ArrayList;

/**
 * Activity that displays the requests for reimbursement by committee members
 */
public class ViewRequests extends AppCompatActivity {

    private DatabaseReference mDatabaseReference;

    private ArrayList<ReimbursementRequest> mRequests = new ArrayList<ReimbursementRequest>();
    private RequestAdapter mRequestAdapter;
    private RecyclerView mRecyclerView;

    /**
     * Initialises recycler view, adapter and data to be displayed
     * @param savedInstanceState previously saved state of the app
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_requests);

        mDatabaseReference = FirebaseDatabase.getInstance().getReference();

        mRecyclerView = (RecyclerView) findViewById(R.id.view_requests_recyclerview);

        //Initialises recycler view parameters and links to adapter
        mRequestAdapter = new RequestAdapter(this, mRequests);
        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(getApplicationContext());
        mRecyclerView.setLayoutManager(mLayoutManager);
        mRecyclerView.setItemAnimator(new DefaultItemAnimator());
        mRecyclerView.setAdapter(mRequestAdapter);

        //Downloads data from the requests branch in the Firebase database and adds it to the array list
        Query requestQuery = mDatabaseReference.child("requests").orderByChild("purchaser");
        requestQuery.addChildEventListener(new ChildEventListener() {

            @Override
            public void onChildAdded(DataSnapshot dataSnapshot, String s) {
                ReimbursementRequest request = dataSnapshot.getValue(ReimbursementRequest.class);

                boolean match = false;
                for (ReimbursementRequest check : mRequests) {
                    if (check.getKey() == request.getKey()) {
                        match = true;
                    }
                }
                if (!match) {
                    request.setKey(dataSnapshot.getKey());
                    mRequests.add(request);
                    mRequestAdapter.notifyDataSetChanged();
                }

            }

            @Override
            public void onChildChanged(DataSnapshot dataSnapshot, String s) {

            }

            @Override
            public void onChildRemoved(DataSnapshot dataSnapshot) {

            }

            @Override
            public void onChildMoved(DataSnapshot dataSnapshot, String s) {

            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });

    }
}
