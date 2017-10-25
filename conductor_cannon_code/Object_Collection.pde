

public abstract class Object{
  
  long time, delay;
  float x,y;
  float dx,dy;
  PImage image;
  boolean remove;
  String type;
  
  public Object(float x, float y){
   remove = false;
   this.x = x;
   this.y = y;
   this.time = millis();
  }
  
  public void updateObject(){
    x+=dx;
    y+=dy;
    update();
  }
  
  public abstract void update();
  
  
  //Image collision stub !!!! Dave replace this with your actual collision code !!!!
  public void imageCollision(ArrayList<Ball> balls){
    for(Ball ball : balls){
      if(pp_collision(ball.getImage(),ball.getX(),ball.getY(),image,x,y) && !ball.inObject){
        ball.inObject = true;
        ball.objectIn = this;
        ball.verticalVelocity = -ball.verticalVelocity;
        ball.horizontalVelocity = random(-2, 2);
        doSomething();
      }
    }
  }
  
  public abstract void doSomething();
  
  public boolean timeUp(long time , long delay){
    return millis() - time >= delay;
  }
  
}

public class Cloud extends Object{
  
  ArrayList<Object> raindrops;
  PImage fadeImage;
  boolean raining;
  int fade;
  int variation;
  
  public Cloud(float x, float y){
   super(x,y);
   dx = 3;
   delay = 750;
   fade = 255;
   variation = (int)random(1,5);
   image = loadImage("data/sprites/CloudSpr"+ variation +".png");
   image.resize(image.width/2, image.height/2);
   raindrops = new ArrayList<Object>();
   raining = false;
   type = "Cloud";
  }
  
  @Override
  public void update(){
    if(x > width - image.width || x < 0){
      dx = -dx;
    }
    if(raining){
      if(timeUp(time, (long)random(delay - 200, delay + 200))){
        time = millis();
        raindrops.add(new Raindrop(random(x + 25, x + image.width - 25),y+image.height/2));
      }
      for(Object r : raindrops){
        r.update();
      }
      for(Iterator<Object> r = raindrops.iterator(); r.hasNext();){
        if(r.next().remove){
          r.remove();
        }
      }
    }
    image(image, x, y);
    if(raining){
      fade();
    }
  }
  
  public void startRaining(){
    raining = true;
    fadeImage = loadImage("data/sprites/CloudSpr"+ variation +".png");
    fadeImage.resize(fadeImage.width/2, fadeImage.height/2);
    setDark();
  }
  private void fade(){
   if(fade != 0){
    tint(255, fade);
    fade -= 1;
    image(fadeImage,x,y);
   }
    tint(255, 255);
  }
  public void setDark(){
   image = loadImage("data/sprites/CloudSpr"+ variation +"d.png");
   image.resize(image.width/2, image.height/2);
  }
  
  @Override
  public void imageCollision(ArrayList<Ball> balls){
    for(Ball ball : balls){
      if(pp_collision(ball.getImage(),ball.getX(),ball.getY(),image,x,y) && !ball.inObject){
        ball.verticalVelocity = -ball.verticalVelocity;
        ball.horizontalVelocity = random(-2, 2);
        ball.inObject = true;
        ball.objectIn = this;
        }
      for(Object r : raindrops){
        ((Raindrop) r).rainCollision(balls);
      }
    }
  }
  
  @Override
  public void doSomething(){
    
  }
  
}

public class Raindrop extends Object{
  
  public Raindrop(float x, float y){
    super(x,y);
    dy = 1;
    type = "Raindrop";
    image = loadImage("data/sprites/RaindropSpr.png");
  }
  
  @Override
  public void update(){
    image(image, x, y);
    dy += 0.2;
    dx += windSpeed;
    y += dy;
    x += dx;
    if(y > height){
      remove = true;
    }
  }
  
  public void rainCollision(ArrayList<Ball> balls){
    for(Ball ball : balls){
      if(pp_collision(ball.getImage(),ball.getX(),ball.getY(),image,x,y)){
        ball.wetBall();
        remove = true;
      }
    }
  }
  
  @Override
  public void doSomething(){
    
  }
  
}

public class Rock extends Object{
  double hp = 100;
  int frame;
  PImage[] storedImages;
  int variation;
  public Rock(float x, float y){
    super(x,y);
    frame = 0;
    variation = (int)random(1,6);
    image = loadImage("data/sprites/RockSpr" + variation + "0.png");
    image.resize(image.width/2, image.height/2);
    storedImages = new PImage[3];
    loadImages();
    type = "Rock";
  }
 
  private void loadImages(){
    for(int i = 0; i < storedImages.length; i++){
      storedImages[i] = loadImage("data/sprites/RockSpr" + variation + "" + (i+1) + ".png");
      storedImages[i].resize(storedImages[i].width/2, storedImages[i].height/2);
    }
  }
 
  @Override
  public void update(){
    image(image, x, y);
  }
  
  @Override
  public void doSomething(){
    hp -= 25;
    if(hp > 0 && hp % 25 == 0){
      nextImage();
    }else if(hp <= 0){
     remove = true; 
    }
  }
  
  private void nextImage(){
    image = storedImages[frame++];
  }
  
}

public class Debris extends Object{
  
  public Debris(float x, float y){
    super(x,y);
    dy = random(-8,-6);
    dx = random(-1.5,1.5);
    image = loadImage("data/sprites/DebrisSpr.png");
    image.resize(image.width/2, image.height/2);
    type = "Debris";
  }
  
  @Override
  public void update(){
    image(image,x,y);
    dy += 0.3;
    if(y > height){
     remove = true;
    } 
  }
  
  @Override
  public void doSomething(){
    
  }
  
}

public class Tree extends Object{
  int frame;
  PImage[] storedImages;
  public Tree(float x, float y){
    super(x,y);
    image = loadImage("data/sprites/TreeSpr1.png");
    this.y = height - 240;
    this.x = random(-image.width/2,width - image.width/2);
    storedImages = new PImage[2];
    storedImages[0] = loadImage("data/sprites/TreeSpr2.png");
    storedImages[1] = loadImage("data/sprites/TreeSpr3.png");
    storedImages[1].resize((int)(storedImages[1].width/1.5), (int)(storedImages[1].height/1.5));
    frame = 0;
  }
  
  @Override
  public void update(){
    image(image,x,y);
    
  }
  
  public PImage getStoredImages(int i){
    frame++;
    if(i == 0){
      y -= storedImages[i].height/1.75;
      x -= storedImages[i].width/3;
    }else if(i == 1){
      y -= storedImages[i].height/2.5;
      x -= storedImages[i].width/4.75;
    }

    return storedImages[i];
  }
  
  @Override
  public void doSomething(){
    
  }
  
  @Override
  public void imageCollision(ArrayList<Ball> balls){
    if(frame > 0){
      for(Ball ball : balls){
      if(pp_collision(ball.getImage(),ball.getX(),ball.getY(),image,x,y) && !ball.inObject){
        ball.verticalVelocity = -7.5;
        ball.inObject = true;
        ball.objectIn = this;
      }
      else if(!pp_collision(ball.getImage(),ball.getX(),ball.getY(),image,x,y))
      {
        if(ball.verticalVelocity == 7.5){
          ball.verticalVelocity = -15;
        }
      }
     }
    }

  }
  
}




final int ALPHALEVEL = 20;
// This method is from http://www.openprocessing.org/sketch/149174 (See Sources.txt)
public boolean pp_collision(PImage imgA, float aix, float aiy, PImage imgB, float bix, float biy) {
  int topA, botA, leftA, rightA;
  int topB, botB, leftB, rightB;
  int topO, botO, leftO, rightO;
  int ax, ay;
  int bx, by;
  int APx, APy, ASx, ASy;
  int BPx, BPy; //, BSx, BSy;

  topA   = (int) aiy;
  botA   = (int) aiy + imgA.height;
  leftA  = (int) aix;
  rightA = (int) aix + imgA.width;
  topB   = (int) biy;
  botB   = (int) biy + imgB.height;
  leftB  = (int) bix;
  rightB = (int) bix + imgB.width;

  if (botA <= topB  || botB <= topA || rightA <= leftB || rightB <= leftA)
    return false;

  // If we get here, we know that there is an overlap
  // So we work out where the sides of the overlap are
  leftO = (leftA < leftB) ? leftB : leftA;
  rightO = (rightA > rightB) ? rightB : rightA;
  botO = (botA > botB) ? botB : botA;
  topO = (topA < topB) ? topB : topA;


  // P is the top-left, S is the bottom-right of the overlap
  APx = leftO-leftA;   
  APy = topO-topA;
  ASx = rightO-leftA;  
  ASy = botO-topA-1;
  BPx = leftO-leftB;   
  BPy = topO-topB;

  int widthO = rightO - leftO;
  boolean foundCollision = false;

  // Images to test
  imgA.loadPixels();
  imgB.loadPixels();

  // These are widths in BYTES. They are used inside the loop
  //  to avoid the need to do the slow multiplications
  int surfaceWidthA = imgA.width;
  int surfaceWidthB = imgB.width;

  boolean pixelAtransparent = true;
  boolean pixelBtransparent = true;

  // Get start pixel positions
  int pA = (APy * surfaceWidthA) + APx;
  int pB = (BPy * surfaceWidthB) + BPx;

  ax = APx; 
  ay = APy;
  bx = BPx; 
  by = BPy;
  for (ay = APy; ay < ASy; ay++) {
    bx = BPx;
    for (ax = APx; ax < ASx; ax++) {
      pixelAtransparent = alpha(imgA.pixels[pA]) < ALPHALEVEL;
      pixelBtransparent = alpha(imgB.pixels[pB]) < ALPHALEVEL;

      if (!pixelAtransparent && !pixelBtransparent) {
        foundCollision = true;
        break;
      }
      pA ++;
      pB ++;
      bx++;
    }
    if (foundCollision) break;
    pA = pA + surfaceWidthA - widthO;
    pB = pB + surfaceWidthB - widthO;
    by++;
  }
  return foundCollision;
}
