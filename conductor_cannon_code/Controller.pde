public class Controller
{
    BallCollection balls;   
    BoxCollection boxes;
    Canon canon;
    int time;
    float fireRate = 1500; //Canon Fire rate in millseconds
    float ballSpeed = -15; //initail ball speed 
    boolean wind; //Is wind active?
    float boxD = 75, boxY = 25, boxEdgeCurve = 15; // Variables for box locations, size, etc;
    float lastKnownX = 600;
    float lastKnownY = 1500;
    
    //Min-Max values for cannon x
    float minCannonX = 0;
    float maxCannonX = 1280;
    
    //Max and min firerate in miliseconds.
    int maxFireRate = 500;
    int minFireRate = 1500;
    
    //Min-Max values for kinect input
    float minX = 50;
    float maxX = 550;
    float minY = 0;
    float maxY = 450;
    
     
    public Controller()
    {
        ac = new AudioContext();
        boxes = new BoxCollection(boxY, boxD, boxEdgeCurve);
        balls = new BallCollection();
        canon = new Canon(width / 2, height - 85); // Location of canon at start
        
        ac.start();
        boxes.startAudio();
        wind = false;
        time = millis();
        canon.x = width/2;
    }
    
    
    
    public void update()
    {
        //fireRate = (int)map(mouseY, 0, height, 500, 2000);
        //FIRE!!!
        textSize(60);
        fireRate = getY();
        if(millis() - time >= fireRate)
        {
            balls.addBall(canon.x + canon.cWidth / 2 - 10 , canon.y, false, ballSpeed); 
            time = millis();
        }
//<<<<<<< HEAD
      
        //canon.x = mouseX - canon.cWidth / 2;
        canon.x = (int)getX() - canon.cWidth / 2;
        fill(172,115,57); //dirt colour
        rect(0,height*4/5,width,height*1/5); //dirt
        boxes.updateBoxes(balls.balls);
        boxes.updateRate(map(fireRate, 1500, 500, 0.5, 2));
        boxes.updateFilter();
        boxes.updateObjects(balls.balls);
        balls.updateBalls();
        canon.updateCanon();
       
         
      
//=======
        
        
//>>>>>>> c2dcdee1145ff23a4957c761cd3ce5aa478891e1
        
        //balls.updateBalls();
        //canon.updateCanon();
        //println(fireRate);
    }
    
    public float getX()
    {
      if(hasKinectUser)
      {
        lastKnownX = map(getJointPosition(SimpleOpenNI.SKEL_LEFT_HAND).x,minX,maxX,maxCannonX,minCannonX) ;
      }
      if(lastKnownX < minCannonX) lastKnownX = minCannonX;
      if(lastKnownX > maxCannonX) lastKnownX = maxCannonX;
      return useKinect ? lastKnownX : mouseX - canon.cWidth / 2;
    }
    
    public float getY()
    {
      if(hasKinectUser)
      {
        lastKnownY = map(getJointPosition(SimpleOpenNI.SKEL_RIGHT_HAND).y,minY,maxY,maxFireRate,minFireRate);
      }
      if(useKinect)
      {
        if(lastKnownY > minFireRate) lastKnownY = minFireRate;
        if(lastKnownY < maxFireRate) lastKnownY = maxFireRate;
        return lastKnownY;
      }
       else
       {
         return (int)map(mouseY, 0, height, 500, 1500);
       }
    }
    
  
}