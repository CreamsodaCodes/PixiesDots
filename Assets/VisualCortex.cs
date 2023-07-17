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
    static public float GetLineFieldsANDSize(int x, int y, int visionLength,int whatTag, int direction) //1 nord 2 ost ...
    {
        if (direction == 1)
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x+i,y)==whatTag)
                {
                    
                  return (float)(field.gameboard[x+i,y].sizeFood/10f);
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
                    return (float)(field.gameboard[x-i,y].sizeFood/10f);
                  
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
                    return (float)(field.gameboard[x,y+i].sizeFood/10f);
                  
                }
            }
            return 0f;
        }
        else
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x,y-i)==whatTag&&field.gameboard[x,y-i]!=null)
                {
                    return (float)(field.gameboard[x,y-i].sizeFood/10f);
                  
                }
            }
            return 0f;
        }

    }

        static public float GetLineFieldsANDRed(int x, int y, int visionLength,int whatTag, int direction) //1 nord 2 ost ...
    {
        if (direction == 1)
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x+i,y)==whatTag)
                {
                    
                  return (float)(field.gameboard[x+i,y].redness);
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
                    return (float)(field.gameboard[x-i,y].redness);
                  
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
                    return (float)(field.gameboard[x,y+i].redness);
                  
                }
            }
            return 0f;
        }
        else
        {
            for (int i = 1; i <= visionLength; i++)
            {
                if (field.checkField(x,y-i)==whatTag&&field.gameboard[x,y-i]!=null)
                {
                    return (float)(field.gameboard[x,y-i].redness);
                  
                }
            }
            return 0f;
        }

    }
    
}