using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class M0609 : MonoBehaviour {

    // Use this for initialization
    public GameObject J1;
    public GameObject J2;
    public GameObject J3;
    public GameObject J4;
    public GameObject J5;
    public GameObject J6;
    private Jacobian m0609;
    public GUI gui;
    public float x, y, z, a, b, r;
    private float tx, ty, tz, ta, tb, tr;
    private int cnt;
    public float q1, q2, q3, q4, q5, q6;
    public float hq1, hq2, hq3, hq4, hq5, hq6;
    public float tq1, tq2, tq3, tq4, tq5, tq6;
    private bool MoveLFunction;
    private bool MoveLFunc2;
    public bool RobotMove;
    List<float> listx, listy, listz;
    RobotControlCommand Control;
    
    private float RAD;
	void Start () {
        listx = new List<float>();
        listy = new List<float>();
        listz = new List<float>();

        RAD = 180.0f / 3.141592f;
        m0609 = new Jacobian();
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
        m0609.Close();
    }
        // Update is called once per frame
    void Update () {
        dhMat h = new dhMat();
        dhMat h01, h12, h23, h34, h45, h56, h6e;
        dhMat h02, h03, h04, h05, h06, h0e;
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
            else {
                if (MoveLFunc2)
                {
                    float ex, ey, ez, ea, eb, er;
                    ex = tx - x;
                    ey = ty - y;
                    ez = tz - z;
                    ea = ta - a * RAD;
                    eb = tb - b * RAD;
                    er = tr - r * RAD;

                    if (Mathf.Abs(ex) < 0.1f && Mathf.Abs(ey) < 0.1f && Mathf.Abs(ez) < 0.1f &&
                        Mathf.Abs(ea) < 0.1f && Mathf.Abs(eb) < 0.1f && Mathf.Abs(er) < 0.1f)
                    {
                        MoveLFunction = false;
                        RobotMove = false;
                        MoveLFunc2 = false;

                    } 
                    m0609.SetQ(q1/RAD, q2 / RAD, q3 / RAD, q4 / RAD, q5 / RAD, q6 / RAD);
                    if (cnt == 0) { m0609.SetX(this.x, this.y, this.z, this.a*RAD, this.b * RAD, this.r * RAD); }
                    int E = m0609.MoveL(tx, ty, tz, ta, tb, tr,300,cnt);

                    listx.Add(x);
                    listy.Add(y);
                    listz.Add(z);
                    if (E == 0)
                    {
                        cnt += 1;
                        q = m0609.GetQ();
                        tq1 = q[0] * RAD;
                        tq2 = q[1] * RAD;
                        tq3 = q[2] * RAD;
                        tq4 = q[3] * RAD;
                        tq5 = q[4] * RAD;
                        tq6 = q[5] * RAD;
                        //gui.MoveJ(tq1, tq2, tq3, tq4, tq5, tq6);

                    }
                    else
                    {
                        q = m0609.GetQ();
                        tq1 = q[0] * RAD;
                        tq2 = q[1] * RAD;
                        tq3 = q[2] * RAD;
                        tq4 = q[3] * RAD;
                        tq5 = q[4] * RAD;
                        tq6 = q[5] * RAD;
                    }
                    
                }
            }
            if (RobotMove)
            {
                q1 += e1 * 0.01f;
                q2 += e2 * 0.01f;
                q3 += e3 * 0.01f;
                q4 += e4 * 0.01f;
                q5 += e5 * 0.01f;
                q6 += e6 * 0.01f;

                //gui.MoveJ(q1, q2, q3, q4, q5, q6);
                J1.transform.Rotate(new Vector3(0, 0, e1 * 0.01f));
                J2.transform.Rotate(new Vector3(0, 0, e2 * 0.01f));
                J3.transform.Rotate(new Vector3(0, 0, e3 * 0.01f));
                J4.transform.Rotate(new Vector3(0, 0, e4 * 0.01f));
                J5.transform.Rotate(new Vector3(0, 0, e5 * 0.01f));
                J6.transform.Rotate(new Vector3(0, 0, e6 * 0.01f));

                listx.Add(x);
                listy.Add(y);
                listz.Add(z);
            }
        }
        h01 = h.Trans(0, 0, 135.0f) * h.RotZ(q1 / RAD);
        h12 = h.Trans(0, -170.2f, 0) * h.RotX(90.0f / RAD) * h.RotZ(q2 / RAD);
        h23 = h.Trans(0, 411.0f, 0) * h.RotZ(q3 / RAD);
        h34 = h.Trans(0, 0, -164.0f) * h.RotX(-90.0f / RAD) * h.RotZ(q4 / RAD);
        h45 = h.Trans(0, -146.0f, 368.0f) * h.RotX(90.0f / RAD) * h.RotZ(q5 / RAD);
        h56 = h.Trans(0, 0, -146.0f) * h.RotX(-90.0f / RAD) * h.RotZ(q6 / RAD);
        h6e = h.Trans(0, 0, 121.0f);

        h02 = h01 * h12;
        h03 = h02 * h23;
        h04 = h03 * h34;
        h05 = h04 * h45;
        h06 = h05 * h56;
        h0e = h06 * h6e;

        
        this.x = h0e.v[12];
        this.y = h0e.v[13];
        this.z = h0e.v[14];
        float[] rpy = h0e.RPY();
        this.a = rpy[0];
        this.b = rpy[1];
        this.r = rpy[2];
    }
    public bool FMove(float x,float y,float z,float a,float b,float r) {
        if (MoveLFunction) return false;
        float[] q;
        float Q1, Q2, Q3, Q4, Q5, Q6;
        Q1 = 10.0f;
        Q2 = 10.0f;
        Q3 = 10.0f;
        Q4 = 10.0f;
        Q5 = 10.0f;
        Q6 = 10.0f;

        m0609.SetQ(Q1 / RAD, Q2 / RAD, Q3 / RAD, Q4 / RAD, Q5 / RAD, Q6 / RAD);
        m0609.Move(x, y, z, a, b, r);
        q = m0609.GetQ();
        tq1 = q[0] * RAD;
        tq2 = q[1] * RAD;
        tq3 = q[2] * RAD;
        tq4 = q[3] * RAD;
        tq5 = q[4] * RAD;
        tq6 = q[5] * RAD;
        RobotMove = true;
        return true;
    }
    public void ListClear() {
        listx.Clear();
        listy.Clear();
        listz.Clear();
    }
    public void ListFileSave()
    {
        StreamWriter write = File.AppendText("test.py");
        write.Write("x = [");
        for (int i = 0; i < listx.Count; i++) {
            string str = string.Format("{0},", listx[i]);
            write.Write(str);
        }
        write.WriteLine("]");
        write.Write("y = [");
        for (int i = 0; i < listy.Count; i++)
        {
            string str = string.Format("{0},", listy[i]);
            write.Write(str);
        }
        write.WriteLine("]");

        write.Write("z = [");
        for (int i = 0; i < listz.Count; i++)
        {
            string str = string.Format("{0},", listz[i]);
            write.Write(str);
        }
        write.WriteLine("]");
        write.Close();
    }
    public bool FMoveL(float x, float y, float z, float a, float b, float r) {
        
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
    public bool FMoveJ(float q1, float q2, float q3, float q4, float q5, float q6) {
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
