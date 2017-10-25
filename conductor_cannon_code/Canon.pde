
public class Canon
{
    float x, y;
    int cWidth = 75;
    int cHeight = 75;
    PImage cannon;
  
    public Canon(int x, int y)
    {
        cannon = loadImage("qubodup-Cartoon-cannon-kind-of-thing.png");
        this.x = x;
        this.y = y;
    }
  
    public void updateCanon()
    {
        //rect(x,y,width,height);
        image(cannon, x, y, cWidth, cHeight);
    }
}
