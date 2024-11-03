using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A0509S : MonoBehaviour {
    public GameObject J1;
    public GameObject J2;
    public GameObject J3;
    public GameObject J4;
    public GameObject J5;
    public GameObject J6;
    private A0509SJacobian J;
    public float x, y, z, a, b, r;
    private float tx, ty, tz, ta, tb, tr;
    private int cnt;
    public float q1, q2, q3, q4, q5, q6;
    public float hq1, hq2, hq3, hq4, hq5, hq6;
    private float tq1, tq2, tq3, tq4, tq5, tq6;
    private bool MoveLFunction;
    private bool MoveLFunc2;
    public bool RobotMove;

    // Use this for initialization
    void Start() {
        J = new A0509SJacobian();
        q1 = 0;
        q2 = 0;
        q3 = 0;
        q4 = 0;
        q5 = 0;
        q6 = 0;
        tq1 = 0;
        tq2 = 0;
        tq3 = 0;
        tq4 = 0;
        tq5 = 0;
        tq6 = 0;
        MoveLFunction = false;
        MoveLFunc2 = true;
        RobotMove = false;
    }
    public void OnApplicationQuit()
    {
        J.Close();
    }
    // Update is called once per frame
    void Update() {
        dhMat h = new dhMat();
        dhMat h01, h12, h23, h34, h45, h56, h6e, h0e;
        float e1, e2, e3, e4, e5, e6;
        float[] q = new float[6];
        if (RobotMove == true)
        {
            e1 = tq1 - q1;
            e2 = tq2 - q2;
            e3 = tq3 - q3;
            e4 = tq4 - q4;
            e5 = tq5 - q5;
            e6 = tq6 - q6;

            if (MoveLFunction == false)
            {
                if (Mathf.Abs(e1) < 0.01 && Mathf.Abs(e2) < 0.01 && Mathf.Abs(e3) < 0.01 &&
                Mathf.Abs(e4) < 0.01 && Mathf.Abs(e5) < 0.01 && Mathf.Abs(e6) < 0.01) { RobotMove = false; }
            }
            else
            {
                if (MoveLFunc2)
                {
                    float ex, ey, ez, ea, eb, er;
                    ex = tx - x;
                    ey = ty - y;
                    ez = tz - z;
                    ea = ta - DEG(a);
                    eb = tb - DEG(b);
                    er = tr - DEG(r);

                    if (Mathf.Abs(ex) < 0.1f && Mathf.Abs(ey) < 0.1f && Mathf.Abs(ez) < 0.1f &&
                        Mathf.Abs(ea) < 0.1f && Mathf.Abs(eb) < 0.1f && Mathf.Abs(er) < 0.1f)
                    {
                        MoveLFunction = false;
                        RobotMove = false;
                        MoveLFunc2 = false;

                    }
                    J.SetQ(RAD(q1), RAD(q2), RAD(q3), RAD(q4), RAD(q5), RAD(q6));
                    if (cnt == 0) { J.SetX(this.x, this.y, this.z, DEG(this.a), DEG(this.b), DEG(this.r)); }
                    int E = J.MoveL(tx, ty, tz, ta, tb, tr, 100, cnt + 1);

                    if (E == 0)
                    {
                        cnt += 1;
                        q = J.GetQ();
                        tq1 = DEG(q[0]);
                        tq2 = DEG(q[1]);
                        tq3 = DEG(q[2]);
                        tq4 = DEG(q[3]);
                        tq5 = DEG(q[4]);
                        tq6 = DEG(q[5]);
                    }
                    else
                    {
                        q = J.GetQ();
                        tq1 = DEG(q[0]);
                        tq2 = DEG(q[1]);
                        tq3 = DEG(q[2]);
                        tq4 = DEG(q[3]);
                        tq5 = DEG(q[4]);
                        tq6 = DEG(q[5]);
                    }
                }
            }
            if (RobotMove)
            {
                q1 += e1 * 0.05f;
                q2 += e2 * 0.05f;
                q3 += e3 * 0.05f;
                q4 += e4 * 0.05f;
                q5 += e5 * 0.05f;
                q6 += e6 * 0.05f;
                J1.transform.Rotate(new Vector3(0, 0, e1 * 0.05f));
                J2.transform.Rotate(new Vector3(0, 0, e2 * 0.05f));
                J3.transform.Rotate(new Vector3(0, 0, e3 * 0.05f));
                J4.transform.Rotate(new Vector3(0, 0, e4 * 0.05f));
                J5.transform.Rotate(new Vector3(0, 0, e5 * 0.05f));
                J6.transform.Rotate(new Vector3(0, 0, e6 * 0.05f));
            }
        }
        h01 = h.Trans(0, 0, 155.5f) * h.RotZ(RAD(q1));
        h12 = h.Trans(0, 141.5f, 0) * h.RotX(RAD(-90)) * h.RotZ(RAD(q2));
        h23 = h.Trans(0, -409f, 0) * h.RotZ(RAD(q3));
        h34 = h.Trans(0, 0, -141.5f) * h.RotX(RAD(90)) * h.RotZ(RAD(q4));
        h45 = h.Trans(0, 122, 367) * h.RotX(RAD(-90)) * h.RotZ(RAD(q5));
        h56 = h.Trans(0, 0, -122) * h.RotX(RAD(90)) * h.RotZ(RAD(q6));
        h6e = h.Trans(0, 0, 127);
        h0e = h01 * h12 * h23 * h34 * h45 * h56 * h6e;
    }
    float RAD(float q) { return q / (180.0f / 3.141592f);}
    float DEG(float q){return q * (180.0f / 3.141592f);}

public bool FMove(float x, float y, float z, float a, float b, float r)
    {
        if (MoveLFunction) return false;
        float[] q;
        float Q1, Q2, Q3, Q4, Q5, Q6;
        Q1 = 10.0f;
        Q2 = 10.0f;
        Q3 = 10.0f;
        Q4 = 10.0f;
        Q5 = 10.0f;
        Q6 = 10.0f;

        J.SetQ(RAD(Q1), RAD(Q2), RAD(Q3), RAD(Q4), RAD(Q5), RAD(Q6));
        J.Move(x, y, z, a, b, r);
        q = J.GetQ();
        tq1 = DEG(q[0]);
        tq2 = DEG(q[1]);
        tq3 = DEG(q[2]);
        tq4 = DEG(q[3]);
        tq5 = DEG(q[4]);
        tq6 = DEG(q[5]);
        RobotMove = true;
        return true;
    }
    public bool FMoveL(float x, float y, float z, float a, float b, float r)
    {

        cnt = 0;
        tx = x;
        ty = y;
        tz = z;
        ta = a;
        tb = b;
        tr = r;
        MoveLFunction = true;
        MoveLFunc2 = true;
        RobotMove = true;


        return true;
    }
    public bool FMoveJ(float q1, float q2, float q3, float q4, float q5, float q6)
    {
        if (MoveLFunction) return false;
        tq1 = q1;
        tq2 = q2;
        tq3 = q3;
        tq4 = q4;
        tq5 = q5;
        tq6 = q6;
        RobotMove = true;
        return true;
    }
    public bool FHome()
    {
        if (MoveLFunction) return false;
        tq1 = hq1;
        tq2 = hq2;
        tq3 = hq3;
        tq4 = hq4;
        tq5 = hq5;
        tq6 = hq6;
        RobotMove = true;
        return true;
    }
    public bool SetHome()
    {
        hq1 = q1;
        hq2 = q2;
        hq3 = q3;
        hq4 = q4;
        hq5 = q5;
        hq6 = q6;
        return true;
    }
}
