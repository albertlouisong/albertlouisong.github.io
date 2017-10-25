package com.uts.foodsoc.utsfoodsocietyapp;

import android.content.Context;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;

import java.util.ArrayList;

/**
 * Adapts the recyclerview to display the list of events
 */

public class EventAdapter extends RecyclerView.Adapter<EventAdapter.ViewHolder> {

    public static final String EXTRA_EVENT_KEY = "EVENTKEY";
    public static final String EXTRA_NAME = "NAME";
    public static final String EXTRA_DATE = "DATE";
    public static final String EXTRA_LOCATION = "LOCATION";
    public static final String EXTRA_DESCRIPTION = "DESCRIPTION";
    public static final String EXTRA_FILE_PATH = "IMAGEFILEPATH";

    private ArrayList<Event> mEventsList;
    private Context mContext;
    private StorageReference mStorageReference;

    /**
     * initialises the view elements in the layout
     */
    public class ViewHolder extends RecyclerView.ViewHolder{

        public TextView eventName;
        public TextView eventLocation;
        public TextView eventDate;
        public TextView eventTime;
        public ImageView eventBanner;
        public ProgressBar eventProgressBar;

        public ViewHolder(View itemView) {
            super(itemView);

            eventName = (TextView) itemView.findViewById(R.id.event_view_name);
            eventLocation = (TextView) itemView.findViewById(R.id.event_view_location);
            eventDate = (TextView) itemView.findViewById(R.id.event_view_date);
            eventTime = (TextView) itemView.findViewById(R.id.event_view_time);
            eventBanner = (ImageView) itemView.findViewById(R.id.event_view_banner);
            eventProgressBar = (ProgressBar) itemView.findViewById(R.id.event_view_progress_bar);
        }
    }

    /**
     * initialises the parameters for the recycler view to be displayed
     * @param context context of the activity holding the recycler view
     * @param events the array list of events to be displayed
     */
    public EventAdapter (Context context, ArrayList<Event> events) {
        this.mContext = context;
        this.mEventsList = events;
        mStorageReference = FirebaseStorage.getInstance().getReference();
    }

    /**
     * initialises the parameters for the recycler view to be displayed
     * @param parent the parent view
     * @param viewType the type of view
     * @return view holder to hold the recycler view layout elements
     */
    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.activity_event_view, parent, false);

        return new ViewHolder(itemView);
    }

    /**
     * Sets the values of each element in the recycler view to those of the event in the list
     * @param holder view holder that holds the recycler view layout elements
     * @param position position in the events array of the displayed event
     */
    @Override
    public void onBindViewHolder(final ViewHolder holder, final int position) {

        final Event event = mEventsList.get(position);

        //Check if an image has been downloaded yet and sets the event image once it has been downloaded
        if (event.getBannerByteArray() == null) {

            StorageReference thumbnailRef = mStorageReference.child(event.getBanner());

            if (thumbnailRef != null) {
                final long TWO_MEGABYTE = 2 * 1024 * 1024;
                thumbnailRef.getBytes(TWO_MEGABYTE).addOnSuccessListener(new OnSuccessListener<byte[]>() {
                    @Override
                    public void onSuccess(byte[] bytes) {
                        event.setBannerByteArray(bytes);
                        notifyDataSetChanged();
                    }
                }).addOnFailureListener(new OnFailureListener() {
                    @Override
                    public void onFailure(@NonNull Exception exception) {
                        // Handle any errors
                        holder.eventProgressBar.setVisibility(View.INVISIBLE);
                    }
                });
            }
        }

        //Sets the event image to the image view once it has been downloaded
        if (event.getBannerByteArray() != null) {
            holder.eventBanner.setImageBitmap(BitmapFactory.decodeByteArray(event.getBannerByteArray(),
                    0, event.getBannerByteArray().length));
            holder.eventProgressBar.setVisibility(View.GONE);
        }

        holder.eventName.setText(event.getName());
        holder.eventLocation.setText(event.getLocation());
        holder.eventDate.setText(event.getMonth().substring(0, 3) + " " + event.getDateno());
        holder.eventTime.setText(event.getTime());

        //passes the event data to the next activity which shows a more in-depth description of the event in the list
        holder.eventBanner.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent eventViewIntent = new Intent(mContext, EventDetails.class);

                eventViewIntent.putExtra(EXTRA_NAME, event.getName());
                eventViewIntent.putExtra(EXTRA_LOCATION, event.getLocation());
                eventViewIntent.putExtra(EXTRA_DATE, event.getDay() + ", " + event.getMonth() + " " +
                        event.getDateno() + " at " + event.getTime());
                eventViewIntent.putExtra(EXTRA_DESCRIPTION, event.getDescription());
                eventViewIntent.putExtra(EXTRA_FILE_PATH, event.getBanner());
                eventViewIntent.putExtra(EXTRA_EVENT_KEY, event.getKey());

                mContext.startActivity(eventViewIntent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return mEventsList.size();
    }

}
