package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import com.google.firebase.database.ChildEventListener;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import java.util.ArrayList;

/**
 * Homepage activity that shows a list of events held by the UTS Food Appreciation Society
 */
public class MainActivity extends AppCompatActivity {

    private DatabaseReference mEventsDatabaseReference;

    private ArrayList<Event> mEventsList = new ArrayList<Event>();
    private EventAdapter mEventAdapter;
    private RecyclerView mRecyclerView;


    /**
     * Initialises the view, adapters and data needed to display all the event information
     * @param savedInstanceState saved state from before the app was previously closed
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        mEventsDatabaseReference = FirebaseDatabase.getInstance().getReference();

        mRecyclerView = (RecyclerView) findViewById(R.id.main_recyclerview);

        //Initialisation of the recycler view linking it to the adapter
        mEventAdapter = new EventAdapter(this, mEventsList);
        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(getApplicationContext());
        mRecyclerView.setLayoutManager(mLayoutManager);
        mRecyclerView.setItemAnimator(new DefaultItemAnimator());
        mRecyclerView.setAdapter(mEventAdapter);

        //Downloads the data from the events branch in the Firebase database and adds it to the ArrayList
        DatabaseReference eventsRef = mEventsDatabaseReference.child("events");
        eventsRef.addChildEventListener(new ChildEventListener() {

            @Override
            public void onChildAdded(DataSnapshot dataSnapshot, String s) {
                Event event = dataSnapshot.getValue(Event.class);

                boolean match = false;
                for (Event check : mEventsList) {
                    if (check.getKey() == event.getKey()) {
                        match = true;
                    }
                }

                if (!match) {
                    event.setKey(dataSnapshot.getKey());
                    mEventsList.add(event);
                    mEventAdapter.notifyDataSetChanged();
                }

            }

            @Override
            public void onChildChanged(DataSnapshot dataSnapshot, String s) {
                Event event = dataSnapshot.getValue(Event.class);


            }

            @Override
            public void onChildRemoved(DataSnapshot dataSnapshot) {
                Event event = dataSnapshot.getValue(Event.class);

            }

            @Override
            public void onChildMoved(DataSnapshot dataSnapshot, String s) {
                Event event = dataSnapshot.getValue(Event.class);

            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    /**
     * Initialises menu that leads to optional activities to add event or view reimbursement requests
     * @param item chosen option
     * @return menu item selected
     */
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        if (id == R.id.action_add_event) {
            Intent addEventIntent = new Intent(MainActivity.this, CreateEvent.class);
            startActivity(addEventIntent);
        }

        if (id == R.id.action_view_requests) {
            Intent viewRequestsIntent = new Intent(MainActivity.this, ExecutiveSignIn.class);
            startActivity(viewRequestsIntent);
        }

        return super.onOptionsItemSelected(item);
    }

}
