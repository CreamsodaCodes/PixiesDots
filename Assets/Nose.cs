using System;
using System.Collections.Generic;

static class Nose
{
    static public float smell(int x, int y, int noseQuality,int whatTag){
        for (int i = 1; i <= noseQuality; i++)
        {
            if (field.checkField(x+i,y)==whatTag) //one is double
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x-i,y)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x+i,y)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x+i,y+i)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x+i,y-i)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x-i,y+i)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x-i,y-i)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
            if (field.checkField(x,y+i)==whatTag)
                {
                    
                   return 1f/(float) Math.Sqrt(i);
                }
                
        }
        return 0f;
    }
    
}