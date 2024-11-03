using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class A0509SJacobian{
    float nx, ny, nz, na, nb, nr;
    public A0509SJacobian()
    {

        RAD = 180.0f / 3.141592f;
        J = Jacobian_Create();
        Jacobian_Initialzation(J);
        Jacobian_Init(J, 6, 6);
        q = new float[6];
        for (int i = 0; i < 6; i++) q[i] = 10.0f / (180.0f / 3.141592f);

    }

    public void SetQ(float q1, float q2, float q3, float q4, float q5, float q6)
    {
        q[0] = q1;
        q[1] = q2;
        q[2] = q3;
        q[3] = q4;
        q[4] = q5;
        q[5] = q6;
    }
    public float[] GetQ() { return q; }
    public void SetX(float x, float y, float z, float a, float b, float r)
    {
        nx = x;
        ny = y;
        nz = z;
        na = a;
        nb = b;
        nr = r;
    }
    public void Move(float x, float y, float z, float a, float b, float r)
    {
        Jacobian_IK(J, q, x, y, z, a, b, r, 10000);
    }
    public int MoveL(float x, float y, float z, float a, float b, float r, int n, int cnt)
    {
        float X, Y, Z, A, B, R;
        float e = (float)(cnt) / (float)(n);

        X = nx + (x - nx) * e;
        Y = ny + (y - ny) * e;
        Z = nz + (z - nz) * e;
        A = na + (a - na) * e;
        B = nb + (b - nb) * e;
        R = nr + (r - nr) * e;
        Jacobian_IK(J, q, X, Y, Z, A, B, R, 1000);
        if (cnt >= n) return 1;
        return 0;
    }
    public void Close()
    {
        Jacobian_Close(J);
    }
    [DllImport("A0509S")]
    private static extern int TestDLL();
    [DllImport("A0509S")]
    private static extern IntPtr Jacobian_Create();
    [DllImport("A0509S")]
    private static extern IntPtr Jacobian_Initialzation(IntPtr J);
    [DllImport("A0509S")]
    private static extern IntPtr Jacobian_Init(IntPtr J, int n, int m);
    [DllImport("A0509S")]
    private static extern IntPtr Jacobian_IK(IntPtr J, float[] q, float x, float y, float z, float a, float b, float r, int n);
    [DllImport("A0509S")]
    private static extern void Jacobian_Close(IntPtr J);
    // Use this for initialization
    IntPtr J;
    float[] q;

    float RAD;
}
