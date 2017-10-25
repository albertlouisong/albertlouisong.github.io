public class BoxCollection
{
    ArrayList<Box> boxes;
    
    
    public BoxCollection(float boxY, float boxD, float boxEdgeCurve)
    {
        boxes = new ArrayList<Box>();
        
        
        //Initalizing and setting up box Positions on screen
        float boxX, boxOffset = width/6;        
        boxX = boxOffset;
        
        // First Box from Left to right
        Treebox treeBox  = new Treebox(boxX - boxD / 2, boxY, boxD, boxEdgeCurve);
        boxes.add(treeBox);
        boxX += boxOffset;
        
        // Second Box
        Cloudbox cloudBox  = new Cloudbox(boxX - boxD / 2, boxY, boxD, boxEdgeCurve);
        boxes.add(cloudBox);
        boxX += boxOffset;
        
        // Third Box
        Rockbox rockBox  = new Rockbox(boxX - boxD / 2, boxY, boxD, boxEdgeCurve);
        boxes.add(rockBox);
        boxX += boxOffset;
        
        // Forth Box
        Sunbox sunBox  = new Sunbox(boxX - boxD / 2, boxY, boxD, boxEdgeCurve);
        boxes.add(sunBox);
        boxX += boxOffset;
        
        // Fifth Box
        Windbox windBox  = new Windbox(boxX - boxD / 2, boxY, boxD, boxEdgeCurve);
        boxes.add(windBox);
        boxX += boxOffset;
       
      
    }
    
    public void updateObjects(ArrayList<Ball> balls){
     for(Box b : boxes){
       b.updateObjects(balls);
     }
    }
    
    public void updateBoxes(ArrayList<Ball> balls)
    {
        for(Box b : boxes)
        {
            b.updateBox(); 
            b.updateAudio();
            b.ballAndBoxCollision(balls);
        }
      
      
    }
    
    public void startAudio() 
    {
      for (Box box : boxes)
      {
        box.startAudio();
      }
    }
    
    public void updateRate(float rate)
    {
      for (Box box : boxes)
      {
        box.updateRate(rate);
      }
    }
    
    public void updateFilter()
    {
      for (Box box : boxes)
      {
        if (box.getId().equals("wind"))
        {
          switch (box.getLevel())
          {
            case 1:
              for (Box b : boxes)
              {
                b.updateFilter(5000.0,0.5,BiquadFilter.NOTCH);
              }
              break;
            case 2:
              for (Box b : boxes)
              {
                b.updateFilter(10000,0.5,BiquadFilter.NOTCH);
              }
              break;
            case 3:
              for (Box b : boxes)
              {
                b.updateFilter(5000,0.5,BiquadFilter.BP_PEAK);
              }
              break;
            case 0:
              for (Box b : boxes)
              {
                b.updateFilter(20000.0,0.5,BiquadFilter.BUTTERWORTH_LP);
              }
              break;
          }
        }
      }
    }
  
  
}





















public abstract class Box
{
    float boxX, boxY, boxD, boxEdgeCurve;
    Boolean active, coolDown;
    long time, delay;
    long animTime, animDelay;
    int frame, frameMax, level, actionLevel, coolDownWait, coolDownTime;
    PImage image;
    PImage[] storedImages;
    GranularSamplePlayer lv1,lv2,lv3;
    Gain g1,g2,g3;
    Glide on,off,rate,grainSize;
    BiquadFilter f1,f2,f3;
    String lv1Sample,lv2Sample,lv3Sample,id;
    ArrayList<Object> objects;
    
   
   public Box(float boxX, float boxY, float boxD, float boxEdgeCurve)
   {
       this.boxX = boxX;
       this.boxY = boxY;
       this.boxD = boxD;
       this.boxEdgeCurve = boxEdgeCurve;
       active = false;
       level = actionLevel = 0;
       coolDownWait = 3000;// 3 Seconds
       coolDown = false;
       objects = new ArrayList<Object>();
   }
   
    public void updateObjects(ArrayList<Ball> balls){
     for(Object object : objects){
      object.updateObject();
      object.imageCollision(balls);
     }
    }
    
    
   public void clearObjects(){
     objects.clear(); 
   }
   
   public void updateBox()
   {
     animate();
       if(active)
//<<<<<<< HEAD
       {                 
           
//=======  
           ArrayList<Boolean> addDebris = new ArrayList<Boolean>();
           ArrayList<Float> posX = new ArrayList<Float>();
           ArrayList<Float> posY = new ArrayList<Float>();
           // remove objects marked for removal
           for(Iterator<Object> ob = objects.iterator(); ob.hasNext();){
             Object object = ob.next();
             if(object.remove){
               if(object.type.equals("Rock")){
                addDebris.add(true);
                posX.add(object.x+object.image.width/2);
                posY.add(object.y+object.image.height/2);
               }
               ob.remove();
             }
           }
           
           if(!addDebris.isEmpty()){
             for(int i = 0; i < addDebris.size(); i++){
               if(addDebris.get(i)){
                 objects.add(new Debris(posX.get(i),posY.get(i)));
                 objects.add(new Debris(posX.get(i),posY.get(i)));
                 objects.add(new Debris(posX.get(i),posY.get(i)));
                 objects.add(new Debris(posX.get(i),posY.get(i)));
                 objects.add(new Debris(posX.get(i),posY.get(i)));
               }
             }
           }
           
           // Do action only once per hit
           if(actionLevel < level)
           {
             actionLevel++;
             doAction();
           }
//>>>>>>> 94c77d492f36048607e7f592e7cf99dd8afb75a6
           // Drawing the box around the box to indicate what level it is currently at and to show active
           switch(level)
           {
               case 1:
                 // Uncomment to see what it looks like scrolling down a white box
                 //fill(255);
                 //rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                 if(coolDown)
                 {
                     fill(204, 153, 0);//Bronze
                     rect(boxX - 10, boxY - 10, boxD + 20, map(millis() - coolDownTime, 0, coolDownWait, 0, boxD + 20), boxEdgeCurve);
                     fill(255);
                     if(millis() - coolDownTime >= coolDownWait)
                        coolDown = false;
                 }
                 else
                 {
                     fill(204, 153, 0);//Bronze
                     rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                     fill(255);
                 }
                 
                 break; 
                 
               case 2:
                 //fill(255);
                 //rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                 if(coolDown)
                 {
                     fill(192, 192, 192);
                     rect(boxX - 10, boxY - 10, boxD + 20, map(millis() - coolDownTime, 0, coolDownWait, 0, boxD + 20), boxEdgeCurve);
                     fill(255);
                     if(millis() - coolDownTime >= coolDownWait)
                        coolDown = false;
                 }
                 else
                 {
                     fill(192, 192, 192);
                     rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                     fill(255);
                 }
                 
                 break;
                 
               case 3:
                 //fill(255);
                 //rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                 if(coolDown)
                 {
                     fill(255, 215, 0);
                     rect(boxX - 10, boxY - 10, boxD + 20, map(millis() - coolDownTime, 0, coolDownWait, 0, boxD + 20), boxEdgeCurve);
                     fill(255);
                     if(millis() - coolDownTime >= coolDownWait)
                        coolDown = false;
                 }
                 else
                 {
                     fill(255, 215, 0);
                     rect(boxX - 10, boxY - 10, boxD + 20, boxD + 20, boxEdgeCurve);
                     fill(255);
                 }
                 
                 break;
                 
               case 0:
                 
                 
                 break;
           }
           
           
       }
       else
       {
         if(level == 0 && actionLevel == 3)
         {
           actionLevel = 0;
           clearObjects();
         }
         if(coolDown && level == 0)
         {
             fill(255, 215, 0);
             rect(boxX - 10, boxY - 10, boxD + 20, map(millis() - coolDownTime, 0, coolDownWait, boxD + 20, 0), boxEdgeCurve);
             fill(255);
             if(millis() - coolDownTime >= coolDownWait)
                coolDown = false;
         }
         //coolDown = false;
       }
       doTheme(); //Do the blocks theme
     
     
       rect(boxX, boxY, boxD, boxD, boxEdgeCurve);
       if(image != null)
       {
           image(image,boxX-5,boxY-5);
       }
   }
   
   public void updateAudio() 
   {
     if (active)
     {
           switch(level)
           {
               case 1:
                 g1.setGain(on);
                 break; 
                 
               case 2:
                 g1.setGain(off);
                 g2.setGain(on);
                 break;
                 
               case 3:
                 g2.setGain(off);
                 g3.setGain(on);
                 break;
                 
               case 0:
               g3.setGain(off);
                 break;
           }
         
       }
       else
       g3.setGain(off);
   }
   
   public abstract void doTheme();
   public abstract void doAction();   
   public void animate()
    {
       if(timeUp(animTime, animDelay)){
           frame++;
           if(frame > frameMax){
               frame = 1;
           }
           image = storedImages[frame - 1];
           animTime = millis();
       }
   }
   public void loadImages(String name, int frames){
     storedImages = new PImage[frames];
     for(int i = 0; i < frames; i++){
       storedImages[i] = loadImage("data/blocks/"+ name.toLowerCase() + "/" + name +" (" + (i+1) +").png");
       storedImages[i].resize((int)boxD+10,(int)boxD+10);
     }
   }
   
   //Note collision is depended on diretional speed at the moment, will need testing if ball speed is varied
   public void ballAndBoxCollision(ArrayList<Ball> balls)
   {
       for(Ball b :balls)
       {
          float x, y;
          if(b.verticalVelocity > -15)
          {
             x = b.x + (b.horizontalVelocity * 2) + 10;
             y = b.y + (b.verticalVelocity * 2);
             //line(b.x + 10, b.y, x, y);
          }
          else
          {
             x = b.x + (b.horizontalVelocity * 0.5) + 10;
             y = b.y + (b.verticalVelocity * 0.5);          
             //line(b.x + 10, b.y, x, y);
          }  
          
          
          //line(b.x + 10, b.y, x, y);
          if(!active)
          {
              
              if(x > boxX && x < boxX + boxD && y < boxY + boxD && y > boxY)
              {
                if(!b.inBlock)
                {
                  b.inBlock = true;
                  b.blockIn = this;
                  b.verticalVelocity = -b.verticalVelocity;
                  b.horizontalVelocity = -b.horizontalVelocity + random(-2, 2);
                  activateBox();
                  if (!coolDown)
                  {
                     coolDown = true;
                     coolDownTime = millis();
                  }
                }
              }
          }
          else
          {
              if(x > boxX - 10 && x < boxX - 10 + boxD + 20 && y < boxY - 10 + boxD + 20 && y > boxY - 10)
              {
                if(!b.inBlock)
                {
                  b.inObject = true;
                  b.blockIn = this;
                  b.verticalVelocity = -b.verticalVelocity;
                  b.horizontalVelocity = -b.horizontalVelocity + random(-2, 2);
                  activateBox();
                  TEMP = BACKGROUND;
                  if (!coolDown)
                  {
                     coolDown = true;
                     coolDownTime = millis();
                  }
                }
              }
          }
       }
   }
   
   public void activateBox()
   {
       
       if(active)
       {      
           if(!coolDown)
           {
               if(level == 3)
               {
                 active = false;
                 level = 0;
               }
               else 
                 level++;
           }
       }
       else
       {
            if(!coolDown)
           {
            level++;
            active = true;
           }
       }
                       
   }
   
   public void audioSetup()
    {
      try
        {
          this.lv1 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv1Sample));
          this.lv2 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv2Sample));
          this.lv3 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv3Sample));
        }
        catch (Exception e)
        {
          println("Exception while attempting to load sample!");
          e.printStackTrace();
          exit();
        }
      this.f1 = new BiquadFilter(ac,BiquadFilter.BUTTERWORTH_LP,20000.0,0.5f);
      this.f2 = new BiquadFilter(ac,BiquadFilter.BUTTERWORTH_LP,20000.0,0.5f);
      this.f3 = new BiquadFilter(ac,BiquadFilter.BUTTERWORTH_LP,20000.0,0.5f);
      this.f1.addInput(lv1);
      this.f2.addInput(lv2);
      this.f3.addInput(lv3);
      this.lv1.setKillOnEnd(false);
      this.lv2.setKillOnEnd(false);
      this.lv3.setKillOnEnd(false);
      on = new Glide(ac,1,0);
      off = new Glide(ac,0,0);
      rate = new Glide(ac,1,0);
      grainSize = new Glide(ac,150,20);
      this.lv1.setGrainSize(grainSize);
      this.lv2.setGrainSize(grainSize);
      this.lv3.setGrainSize(grainSize);
      this.g1 = new Gain(ac,1,off);
      this.g2 = new Gain(ac,1,off);
      this.g3 = new Gain(ac,1,off);
      this.g1.addInput(f1);
      this.g2.addInput(f2);
      this.g3.addInput(f3);
      ac.out.addInput(g1);
      ac.out.addInput(g2);
      ac.out.addInput(g3);
      this.lv1.setRate(rate);
      this.lv2.setRate(rate);
      this.lv3.setRate(rate);
      this.lv1.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
      this.lv2.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
      this.lv3.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
    }
    
    public void startAudio()
    {
      lv1.start();
      lv2.start();
      lv3.start();
    }
    
    public void updateRate(float TempoRate) 
    {
//      float halfHeight = height/2;
//      if (mouseY < height*5/8)
//          rate.setValue((height-mouseY)/halfHeight);
        //println(TempoRate);
        if(TempoRate < 0.5) rate.setValue(0.5);
        if(TempoRate >= 0.5 && TempoRate < 0.6) rate.setValue(0.6);
        if(TempoRate >= 0.6 && TempoRate < 0.7) rate.setValue(0.7);
        if(TempoRate >= 0.7 && TempoRate < 0.8) rate.setValue(0.8);
        if(TempoRate >= 0.8 && TempoRate < 0.9) rate.setValue(0.9);
        if(TempoRate >= 0.9 && TempoRate < 1.0) rate.setValue(1.0);
        if(TempoRate >= 1.0 && TempoRate < 1.1) rate.setValue(1.1);
        if(TempoRate >= 1.1 && TempoRate < 1.2) rate.setValue(1.2);
        if(TempoRate >= 1.2 && TempoRate < 1.3) rate.setValue(1.3);
        if(TempoRate >= 1.3 && TempoRate < 1.4) rate.setValue(1.4);
        if(TempoRate >= 1.4 && TempoRate < 1.5) rate.setValue(1.5);
        if(TempoRate >= 1.5 && TempoRate < 1.6) rate.setValue(1.6);
        if(TempoRate >= 1.6 && TempoRate < 1.7) rate.setValue(1.7);
        if(TempoRate >= 1.7 && TempoRate < 1.8) rate.setValue(1.8);
        if(TempoRate >= 1.8 && TempoRate < 1.9) rate.setValue(1.9);
        if(TempoRate >= 1.9 && TempoRate < 2.0) rate.setValue(2.0);
        if(TempoRate >= 2.0) rate.setValue(2.0);
    }
    
    public boolean timeUp(long time , long delay){
      return millis() - time >= delay;
    }
    
    public void updateFilter(float freq, float q, BiquadFilter.Type type)
    {
      f1.setFrequency(freq);
      f1.setQ(q);
      f1.setType(type);
      f2.setFrequency(freq);
      f2.setQ(q);
      f2.setType(type);
      f3.setFrequency(freq);
      f3.setQ(q);
      f3.setType(type);
    }
    
    public boolean getActive()
    {
      return active;
    }
    
    public int getLevel()
    {
      return level;
    }
    
    public String getId()
    {
      return id;
    }
  
}
//Blocks start here ---------------------------------------------------------------
public class Treebox extends Box
{
  
    public Treebox(float boxX, float boxY, float boxD, float boxEdgeCurve)
    {
        super(boxX, boxY, boxD, boxEdgeCurve);
        this.id = "tree";
        this.lv1Sample = "/data/samples/tree_lv1.wav";
        this.lv2Sample = "/data/samples/tree_lv2.wav";
        this.lv3Sample = "/data/samples/tree_lv3.wav";
        image = loadImage("data/blocks/tree/Tree (1).png");
        image.resize((int)boxD+10,(int)boxD+10);
        animDelay = 25;
        frameMax = 101;
        loadImages("Tree", frameMax);
        audioSetup();
    }
    
    public void doTheme()
    {
      
    }
      
    public void doAction()
    {
      if(level == 1){
        objects.add(new Tree(0,0));
      }else if(level == 2){
        objects.get(0).image = ((Tree) objects.get(0)).getStoredImages(0);
      }else if(level == 3){
        objects.get(0).image = ((Tree) objects.get(0)).getStoredImages(1);
      }

    }
    public void collision()
    {
      
    }
    
    
}


public class Cloudbox extends Box
{
    int lightningTime, lightningWait, lightningTimeOff, lightningWaitOff = 250, colorCount = 101;
    boolean lightning;
    public Cloudbox(float boxX, float boxY, float boxD, float boxEdgeCurve)
    {
        super(boxX, boxY, boxD, boxEdgeCurve);   
        this.id = "cloud";
        this.lv1Sample = "/data/samples/cloud_lv1.wav";
        this.lv2Sample = "/data/samples/cloud_lv2.wav";
        this.lv3Sample = "/data/samples/cloud_lv3.wav";
        image = loadImage("data/blocks/cloud/Cloud (1).png");
        image.resize((int)boxD+10,(int)boxD+10);
        animDelay = 30;
        frameMax = 86;
        loadImages("Cloud",frameMax);
        audioSetup();
        lightningWait = 3500;
    }
    
    public void doTheme()
    {
        switch(level)
        {
           case 0: 
             
             if(lightning)
             {
               BACKGROUND = TEMP;
               lightning = false;
             }
             if(colorCount != 101)
             {
             BACKGROUND = color(map(colorCount, 0, 100, red(TEMP), red(SUNCOLOR)), 
                map(colorCount, 0, 100, green(TEMP), green(SUNCOLOR)), 
                map(colorCount, 0, 100, blue(TEMP), blue(SUNCOLOR)));
                          
                colorCount++;
             }
             if(colorCount >= 101)
             {
               storm = false;
             }
             break;
           case 1:
             
             storm = false;
             break;
           case 2:
             storm = true;
             if(BACKGROUND > NIGHT && coolDown)
             {
                BACKGROUND = color(map(millis() - coolDownTime, 0, coolDownWait, red(TEMP), red(STORM)), 
                      map(millis() - coolDownTime, 0, coolDownWait, green(TEMP), green(STORM)), 
                      map(millis() - coolDownTime, 0, coolDownWait, blue(TEMP), blue(STORM)));                 
             }
             break;
           case 3:
             colorCount = 0;
             storm = true;
             if(millis() - lightningTime >= lightningWait)
             {
                TEMP = BACKGROUND;
                
                BACKGROUND = color(255);              
                
                lightningTime = millis();
                lightningTimeOff = millis();
                lightning = true;
             }
             if(millis() - lightningTimeOff >= lightningWaitOff && lightning)
             {
                 BACKGROUND = TEMP;    
                 lightning = false;
             }
                          
             break;
          
          
        }
    }
  
    public void doAction()
    { 
      lightningTime = millis();
      objects.add(new Cloud(0,125));
      if(level >= 2){
        for(Object object : objects){
          ((Cloud) object).startRaining();
        }
      }
    }
    public void collision()
    {
      
    }
    
    @Override public void audioSetup()
    {
      try
        {
          this.lv1 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv1Sample));
          this.lv2 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv2Sample));
          this.lv3 = new GranularSamplePlayer(ac, new Sample(sketchPath("") + lv3Sample));
        }
        catch (Exception e)
        {
          println("Exception while attempting to load sample!");
          e.printStackTrace();
          exit();
        }
      this.lv1.setKillOnEnd(false);
      this.lv2.setKillOnEnd(false);
      this.lv3.setKillOnEnd(false);
      on = new Glide(ac,1,0);
      off = new Glide(ac,0,0);
      rate = new Glide(ac,1,0);
      grainSize = new Glide(ac,150,20);
      this.lv1.setGrainSize(grainSize);
      this.lv2.setGrainSize(grainSize);
      this.lv3.setGrainSize(grainSize);
      this.g1 = new Gain(ac,1,off);
      this.g2 = new Gain(ac,1,off);
      this.g3 = new Gain(ac,1,off);
      this.g1.addInput(lv1);
      this.g2.addInput(lv2);
      this.g3.addInput(lv3);
      ac.out.addInput(g1);
      ac.out.addInput(g2);
      ac.out.addInput(g3);
      this.lv1.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
      this.lv2.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
      this.lv3.setLoopType(SamplePlayer.LoopType.LOOP_FORWARDS);
    }
    
    @Override public void updateRate(float tempoRate)
    {
    }
    
    @Override public void updateFilter(float freq, float q, BiquadFilter.Type type)
    {
    }
}


public class Rockbox extends Box
{
    public Rockbox(float boxX, float boxY, float boxD, float boxEdgeCurve)
    {
        super(boxX, boxY, boxD, boxEdgeCurve);   
        this.id = "rock";
        this.lv1Sample = "/data/samples/rock_lv1.wav";
        this.lv2Sample = "/data/samples/rock_lv2.wav";
        this.lv3Sample = "/data/samples/rock_lv3.wav";
        image = loadImage("data/blocks/rock/Rock (1).png");
        image.resize((int)boxD+10,(int)boxD+10);
        animDelay = 25;
        frameMax = 150;
        loadImages("Rock",frameMax);
        audioSetup();
    }
    
    public void doTheme()
    {
      
    }
  
    public void doAction()
    {
      float randX = random(-image.width/2,width - image.width/2);
      boolean flag = true;
      Object rock = new Rock(randX, height - 250);
      if(objects.size() >= 1){
        for(int i = 0 ; i < objects.size(); i++){
         if(pp_collision(rock.image, rock.x, rock.y, objects.get(i).image, objects.get(i).x, objects.get(i).y)){
          rock.x = random(-image.width/2,width - image.width/2);
          i = 0;
         } 
        }
      }
      objects.add(rock);
    }
    public void collision()
    {
      
    }

}

public class Sunbox extends Box
{
    int colorCount = 100, sunX, sunY;
    PImage sun;
      
    public Sunbox(float boxX, float boxY, float boxD, float boxEdgeCurve)
    {
        super(boxX, boxY, boxD, boxEdgeCurve);  
        this.id = "sun";
        this.lv1Sample = "/data/samples/sun_lv1.wav";
        this.lv2Sample = "/data/samples/sun_lv2.wav";
        this.lv3Sample = "/data/samples/sun_lv3.wav";
        image = loadImage("data/blocks/sun/Sun (1).png");
        image.resize((int)boxD+10,(int)boxD+10);
        animDelay = 50;
        frameMax = 80;
        loadImages("Sun",frameMax);
        sun = loadImage("data/sprites/SunSpr.png");
        sunX = width / 2 - (183 / 2);
        sunY = height;
        audioSetup();
    }
  
    public void doTheme()
    {  
        
        if (coolDown)
        {
            switch(level)
            {         
                
                case 1://Sunrise  
                  //image(sun, width / 2 - (183 / 2), height / 2);                  
                  sunY = (int) map(millis() - coolDownTime, 0, coolDownWait, height, height - height * 1/5 - 183/2);
                  
                  SUNCOLOR = color(map(millis() - coolDownTime, 0, coolDownWait, 50, red(SUNRISE)), 
                    map(millis() - coolDownTime, 0, coolDownWait, 50, green(SUNRISE)), 
                    map(millis() - coolDownTime, 0, coolDownWait, 50, blue(SUNRISE)));  
                  if(!storm)
                  {
                      BACKGROUND = SUNCOLOR;
                  }
                  else
                  {
                     BACKGROUND = color(map(millis() - coolDownTime, 0, coolDownWait, 50, red(STORM)), 
                      map(millis() - coolDownTime, 0, coolDownWait, 50, green(STORM)), 
                      map(millis() - coolDownTime, 0, coolDownWait, 50, blue(STORM)));
                  }
                                    
                  break;
                  
                case 2://Morning
                  sunY = (int) map(millis() - coolDownTime, 0, coolDownWait, height - height * 1/5 - 183/2, height / 2 - 100);
                  
                    SUNCOLOR = color(map(millis() - coolDownTime, 0, coolDownWait, red(SUNRISE), red(MORNING)), 
                      map(millis() - coolDownTime, 0, coolDownWait, green(SUNRISE), green(MORNING)), 
                      map(millis() - coolDownTime, 0, coolDownWait, blue(SUNRISE), blue(MORNING)));
                  if(!storm)
                  {
                      BACKGROUND = SUNCOLOR;
                  }
                  break;
                  
                case 3://Day
                  colorCount = 0;
                  sunY = (int) map(millis() - coolDownTime, 0, coolDownWait, height / 2 - 100, boxY + boxD + 20);
                  
                    SUNCOLOR = color(map(millis() - coolDownTime, 0, coolDownWait, red(MORNING), red(DAY)), 
                      map(millis() - coolDownTime, 0, coolDownWait, green(MORNING), green(DAY)), 
                      map(millis() - coolDownTime, 0, coolDownWait, blue(MORNING), blue(DAY))); 
                  if(!storm)
                  {
                      BACKGROUND = SUNCOLOR;
                  }
                  break;                                        
            }
        }
         //Night
         if(level == 0 && colorCount != 101)
         {               
            sunY = (int) map(colorCount, 0, 100, boxY + boxD + 20, -183);
            if(!storm)
            {
                SUNCOLOR = color(map(colorCount, 0, 100, red(DAY), red(NIGHT)), 
                  map(colorCount, 0, 100, green(DAY), green(NIGHT)), 
                  map(colorCount, 0, 100, blue(DAY), blue(NIGHT)));
                  BACKGROUND = SUNCOLOR;
            }
             else
             {
                SUNCOLOR = color(map(colorCount, 0, 100, red(STORM), red(NIGHT)), 
                  map(colorCount, 0, 100, green(STORM), green(NIGHT)), 
                  map(colorCount, 0, 100, blue(STORM), blue(NIGHT)));
                  BACKGROUND = SUNCOLOR;
             }
              
            if (colorCount != 101)
              colorCount++;
         }  
         
           
         image(sun, sunX, sunY);          
         fill(172,115,57); //dirt colour
         rect(0,height*4/5,width,height*1/5); //dirt
    }
    
    @Override
    public void updateObjects(ArrayList<Ball> balls){
     for(Object object : objects){
      object.updateObject();
      object.imageCollision(balls);
      
     }
    }
    
    public void doAction()
    {
      
    }
    
    public void collision()
    {
      
    }
    
    
}

public class Windbox extends Box
{
  
  
    public Windbox(float boxX, float boxY, float boxD, float boxEdgeCurve)
    {
        super(boxX, boxY, boxD, boxEdgeCurve);  
        this.id = "wind";
        image = loadImage("data/blocks/wind/Wind (1).png");
        image.resize((int)boxD+10,(int)boxD+10);
        animDelay = 50;
        frameMax = 92;
        loadImages("Wind",frameMax);
    }
    
    public void doTheme()
    {
        switch(level)
        {
            case 0:
              windSpeed = 0f;
              break;
              
            case 1:
              windSpeed = 0.1f;
              break;
              
            case 2:
              windSpeed = 0.2f;
              break;
              
            case 3:
              windSpeed = 0.3f;
              break;                    
        }
    }
    public void doAction()
    {
        
    }
    public void collision()
    {
      
    }
    
    @Override public void startAudio()
    {
    }
    
    @Override public void updateAudio()
    {

    }
    
    @Override public void updateRate(float tempoRate)
    {
    }
    
    @Override public void updateFilter(float freq, float q, BiquadFilter.Type type)
    {
    }
}