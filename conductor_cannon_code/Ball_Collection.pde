/*public class BallCollection
{    
    ArrayList<Ball> balls;
   
    public BallCollection()
    {
        balls = new ArrayList<Ball>();         
    }
  
    public void addBall(float ballX, float ballY, boolean wind, float ballSpeed)
    {
        Ball ball = new Ball(ballX, ballY, wind, ballSpeed);
      
        balls.add(ball);
    }
    
    public void updateBalls()
    {
        Iterator<Ball> it = balls.iterator();
        Ball b;
    
        while (it.hasNext())
        {
            b = it.next();
            
            b.updateBall();
          
            // If the ball is outside the boundries of the window plus 50 pixels it will be removed
            // from the collection
            if (b.x < 0 - 50 || b.x > width + 50 || b.y < 0 - 50 || b.y > height + 50)
            {
               it.remove();
            }                         
        }             
    }
  
}




public class Ball
{   
    private float x, y;
    private float verticalVelocity, horizontalVelocity;
    private boolean remove;
    private boolean wind;
    private double windChange;
    private boolean wet;
    private float wetVerticalVelocity;
    private int hit;    
    private boolean inObject;
    PImage image;
    
    public Ball(float x, float y, boolean wind, float speed)
    {
        this.x = x;
        this.y = y;
        this.wind = wind;
        verticalVelocity = speed;
        windChange = random(0.5,1.5);
        image = loadImage("balls/ball.png"); 
        inObject = false;
      
    }
    
    public void updateBall()
    {
        image(image, x, y);
        
        if(wet){
         wetVerticalVelocity += 0.4; 
        }
        
        horizontalVelocity += windSpeed*windChange;
        
        y += verticalVelocity + wetVerticalVelocity;
        x += horizontalVelocity;
        
        
      
    }
    
    public void wetBall()
    {
      wet = true;
      image = loadImage("balls/ballWet.png"); 
    }
       
    public float GetVerticalVelocity()
    {
      return verticalVelocity;
    }
    
    public float GetHorizontalVelocity()
    {
      return horizontalVelocity;
    }
    
    public float getX()
    {
     return x; 
    }
    
    public float getY()
    {
     return y; 
    }  
    public int getHit()
    {
     return hit; 
    }
  
  
}*/
