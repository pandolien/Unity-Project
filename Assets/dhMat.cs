using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dhMat
{
    public float[] v;
    public dhMat(){
        Initialzation();
    }
    public void Initialzation(){
        int i,j;
        v = new float[16];
        for(i = 0; i< 4;i++){
            for(j = 0; j<4;j++){
                if(i == j)v[i*4+j] = 1.0f;
                else v[i*4+j] = 0.0f;
            }
        }
    }
    public dhMat Trans(float x,float y,float z){
        dhMat h = new dhMat();
        h.v[12] = x;
        h.v[13] = y;
        h.v[14] = z;
        return h;
    }
    public dhMat RotX(float q){
        dhMat h = new dhMat();
        float c,s;
        c = (float)Math.Cos(q);
        s = (float)Math.Sin(q);
        h.v[5] = c;
        h.v[9] = -s;
        h.v[6] = s;
        h.v[10] = c;
        return h;
    }
    public dhMat RotY(float q){
        dhMat h = new dhMat();
        float c,s;
        c = (float)Math.Cos(q);
        s = (float)Math.Sin(q);
        h.v[0] = c;
        h.v[8] = s;
        h.v[2] = -s;
        h.v[10] = c;
        return h;
    }
    public dhMat RotZ(float q){
        dhMat h = new dhMat();
        float c,s;
        c = (float)Math.Cos(q);
        s = (float)Math.Sin(q);
        h.v[0] = c;
        h.v[4] = -s;
        h.v[1] = s;
        h.v[5] = c;
        return h;
    }
    public static dhMat operator *(dhMat h1,dhMat h2){
        dhMat h = new dhMat();
        int i,j,k,cnt = 0;
        for(i = 0; i< 4;i++){
            for(j = 0;j< 4;j++){
                float sum = 0.0f;
                for(k = 0;k <4;k++){sum +=(h1.v[k*4+j]*h2.v[i*4+k]);}
                h.v[cnt] = sum;
                cnt+=1;
            }
        }
        return h;
    }
    public float[] RPY(){
        float[] rpy = new float[3];
        float cr31;
        rpy[1] = (float)Math.Atan2(-v[2], Math.Sqrt(v[0] * v[0] + v[1] * v[1]));
        cr31 = (float)Math.Cos(rpy[1]);
        rpy[0] = (float)Math.Atan2(v[1]/cr31,v[0]/cr31);
        rpy[2] = (float)Math.Atan2(v[6]/cr31,v[10]/cr31);
        return rpy;
    }
}
