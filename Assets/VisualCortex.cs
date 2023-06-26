using System;
using System.Collections.Generic;
static class VisualCortex
{
    static public float GetLineFields(int x, int y, int visionLength,int whatTag, int direction) //1 nord 2 ost ...
    {
        if (direction == 1)
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x+i,y)==whatTag)
                {
                   return 1f/(float) Math.Sqrt(i);
                }
            }
            return 0f;
        }
        else if (direction == 3)
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x-i,y)==whatTag)
                {
                   return 1f/(float) Math.Sqrt(i);
                }
            }
            return 0f;
        }
        else if (direction == 3)
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x,y+i)==whatTag)
                {
                   return 1f/(float) Math.Sqrt(i);
                }
            }
            return 0f;
        }
        else
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x,y-i)==whatTag)
                {
                   return 1f/(float) Math.Sqrt(i);
                }
            }
            return 0f;
        }

    }
    
}