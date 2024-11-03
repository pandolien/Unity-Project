using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DSbot{
    public DSbot() {
        DLL = DSbot_Initialzation();
    }
    public void Close() {
        DSbot_Close(DLL);
        DSbot_Del(DLL);
    }
    public void Init(string ip, int port = 12345) {
        DSbot_Init(DLL,ip, port);
    }
    public void MoveJ(float q1, float q2, float q3, float q4, float q5, float q6) {
        DSbot_AMoveJ(DLL, q1, q2, q3, q4, q5, q6);
    }
    public IntPtr DLL;
    [DllImport("DSbot")]
    private static extern IntPtr DSbot_Initialzation();
    [DllImport("DSbot")]
    private static extern void DSbot_Init(IntPtr D,string ip,int port);
    [DllImport("DSbot")]
    private static extern void DSbot_Close(IntPtr D);
    [DllImport("DSbot")]
    private static extern void DSbot_Del(IntPtr D);
    [DllImport("DSbot")]
    private static extern void DSbot_AMoveJ(IntPtr D, float q1, float q2, float q3, float q4, float q5, float q6);
    

}
