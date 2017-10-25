import beads.*;
import org.jaudiolibs.beads.*;
import java.util.Iterator;
import SimpleOpenNI.*;
SimpleOpenNI kinectContext;

// Please Note the libary's needed above also note that if you want to use the mouse x and y to control the cannon and tempo change the
// useKinect variable below to false.
// Also Note at any stage you can reset the application by pressing R.

boolean useKinect = false;
boolean hasKinectUser;

Controller controller;
AudioContext ac;
color BACKGROUND, NIGHT, SUNRISE, MORNING, DAY, TEMP, STORM, SUNCOLOR;
boolean storm;
float windSpeed;

void setup()
{  
//<<<<<<< HEAD
    noStroke();
    size(1280, 770);
    //size(1920, 1035);
    //fullScreen(); //does not work in processing 2
    controller = new Controller();
    NIGHT = color(50, 50, 50);;
    BACKGROUND = NIGHT;
    SUNRISE = color(254, 91, 53);
    MORNING = color(115, 166, 191);
    DAY = color(153, 221, 255);
    STORM = color(80, 92, 117);
    storm = false;
        
//=======
    //size(1280, 800);
    //frameRate(30);

    //controller = new Controller();
    kinectContext = new SimpleOpenNI(this);
    if(kinectContext.isInit())
    {
      useKinect = true;
      // enable depthMap generation 
      kinectContext.enableDepth();
      // enable skeleton generation for all joints
      kinectContext.enableUser(); 
    }   
//>>>>>>> c2dcdee1145ff23a4957c761cd3ce5aa478891e1
}

void draw()
{
//<<<<<<< HEAD
    //println(frameRate);
    //background(153,221,255); //sky
    background(BACKGROUND);
//<<<<<<< HEAD
//=======
    kinectContext.update();
    controller.update();
//>>>>>>> 8c8db45d7a7882feaba52f624f86b1045e060edc
    
    
    
//=======
    //clear();
    //background(50);
    
    //controller.update();
}

public PVector getJointPosition(int joint) 
  {
    PVector jointPositionRealWorld = new PVector();
    PVector jointPositionProjective = new PVector();
    kinectContext.getJointPositionSkeleton(1, joint, jointPositionRealWorld);
    kinectContext.convertRealWorldToProjective(jointPositionRealWorld, jointPositionProjective);
    return jointPositionProjective;
  }
  
void onNewUser(SimpleOpenNI curContext, int userId)
{
  curContext.startTrackingSkeleton(userId);
  hasKinectUser = true;
}

void onLostUser(SimpleOpenNI curContext, int userId)
{
  hasKinectUser = false;
//>>>>>>> c2dcdee1145ff23a4957c761cd3ce5aa478891e1
}

void keyPressed()
{
   switch(key)
   {
      case 'r':
        ac.stop();
        controller = new Controller();     
        break; 
   }
}
