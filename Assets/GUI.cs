using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {
    
    public Text txtv1;
    public Text txtv2;
    public Text txtv3;
    public Text txtv4;
    public Text txtv5;
    public Text txtv6;

    public Text txd1;
    public Text txd2;
    public Text txd3;
    public Text txd4;
    public Text txd5;
    public Text txd6;
    public InputField IFD1;
    public InputField IFD2;
    public InputField IFD3;
    public InputField IFD4;
    public InputField IFD5;
    public InputField IFD6;

    public InputField IFtv1;
    public InputField IFtv2;
    public InputField IFtv3;
    public InputField IFtv4;
    public InputField IFtv5;
    public InputField IFtv6;

    public InputField IFIP;
    public InputField IFPort;

    public InputField IFFunctionName;
    private DSbot dsbot;
    
    public M0609 m0609;
    public A0509S a0509s;
    public M1013 m1013;
    public FunctionList flist;
    private int FunctionNameNum,DataNum;
    private int RobotNum;
    private bool RunFunction;
    private int runCnt;
    List<RobotControlCommand> commandlist;
    // Use this for initialization
    void Start () {
        
        IFFunctionName.text = "......";
        FunctionNameNum = 0;
        RobotNum = 1;
        IFIP.text = "....";
        IFPort.text = "....";
        DataNum = 0;
        commandlist = new List<RobotControlCommand>();
        dsbot = new DSbot();
        RunFunction = false;
         
    }

    // Update is called once per frame
    void Update() {
        if (FunctionNameNum == 0) { IFFunctionName.text = "......"; }
        else if (FunctionNameNum == 1) { IFFunctionName.text = "MoveJ"; }
        else if (FunctionNameNum == 2) { IFFunctionName.text = "Move"; }
        else if (FunctionNameNum == 3) { IFFunctionName.text = "MoveL"; }
        else { IFFunctionName.text = "Home"; }
        switch (RobotNum) {
            case 0:
                break;
            case 1:
            if (DataNum == 0)
            {
                    txd1.text = "x :";
                    txd2.text = "y :";
                    txd3.text = "z :";
                    txd4.text = "a :";
                    txd5.text = "b :";
                    txd6.text = "r :";
                    IFD1.text = m0609.x.ToString();
                IFD2.text = m0609.y.ToString();
                IFD3.text = m0609.z.ToString();
                IFD4.text = m0609.a.ToString();
                IFD5.text = m0609.b.ToString();
                IFD6.text = m0609.r.ToString();
            }
            else
            {
                    txd1.text = "q1 :";
                    txd2.text = "q2 :";
                    txd3.text = "q3 :";
                    txd4.text = "q4 :";
                    txd5.text = "q5 :";
                    txd6.text = "q6 :";
                    IFD1.text = m0609.q1.ToString();
                IFD2.text = m0609.q2.ToString();
                IFD3.text = m0609.q3.ToString();
                IFD4.text = m0609.q4.ToString();
                IFD5.text = m0609.q5.ToString();
                IFD6.text = m0609.q6.ToString();
            }
                break;
            case 2:
                break;
            case 3:
                break;
        }
        if (RunFunction) {
            if (commandlist.Count != 0)
            {
                switch (RobotNum)
                {
                    case 0:

                        break;
                    case 1:
                        if (commandlist.Count <= runCnt) { RunFunction = false; }
                        else{
                            if (m0609.RobotMove == false)
                            {

                                RobotControlCommand rcc = commandlist[runCnt++];
                                if (rcc.Command == 1) {
                                    m0609.FMoveJ(rcc.d1, rcc.d2, rcc.d3, rcc.d4, rcc.d5, rcc.d6);
                                    dsbot.MoveJ(rcc.d1, rcc.d2, rcc.d3, rcc.d4, rcc.d5, rcc.d6);
                                }
                                else if (rcc.Command == 2) {
                                    m0609.FMove(rcc.d1, rcc.d2, rcc.d3, rcc.d4, rcc.d5, rcc.d6);
                                    dsbot.MoveJ(m0609.tq1, m0609.tq2, m0609.tq3, m0609.tq4, m0609.tq5, m0609.tq6);
                                }
                                else if (rcc.Command == 3) {
                                    m0609.FMoveL(rcc.d1, rcc.d2, rcc.d3, rcc.d4, rcc.d5, rcc.d6);
                                }
                                else {
                                    m0609.FHome();
                                    dsbot.MoveJ(m0609.hq1, m0609.hq2, m0609.hq3, m0609.hq4, m0609.hq5, m0609.hq6);
                                }
                                
                            }
                        }
                        
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
        }

    }
    public void OnApplicationQuit() {
        dsbot.Close();
    }
    public void btConnectClick()
    {
        dsbot.Init("192.168.137.191",12345);
        dsbot.MoveJ(m0609.q1, m0609.q2, m0609.q3, m0609.q4, m0609.q5, m0609.q6);
    }
    public void btJointClick()
    {
        DataNum = 1;
    }
    public void btAxisClick() {
        DataNum = 0;    
    }
    public void btHomeClick()
    {
        if (RobotNum == 0) { }
        if (RobotNum == 1) { m0609.SetHome(); }
        if (RobotNum == 2) { }
        if (RobotNum == 3) { }
    }
    public void btFunctionMoveJClick()
    {
        FunctionNameNum = 1;
        txtv1.text = "q1";
        txtv2.text = "q2";
        txtv3.text = "q3";
        txtv4.text = "q4";
        txtv5.text = "q5";
        txtv6.text = "q6";
    }
    public void btFunctionMoveClick() {
        FunctionNameNum = 2;
        txtv1.text = "x";
        txtv2.text = "y";
        txtv3.text = "z";
        txtv4.text = "a";
        txtv5.text = "b";
        txtv6.text = "r";
    }
    public void btFunctionMoveLClick() {
        FunctionNameNum = 3;
        txtv1.text = "x";
        txtv2.text = "y";
        txtv3.text = "z";
        txtv4.text = "a";
        txtv5.text = "b";
        txtv6.text = "r";
    }
    public void btFunctionHomeClick() {
        FunctionNameNum = 4;
        txtv1.text = "d1";
        txtv2.text = "d2";
        txtv3.text = "d3";
        txtv4.text = "d4";
        txtv5.text = "d5";
        txtv6.text = "d6";
    }
    public void btRobotMoveClick() {
        float d1, d2, d3, d4, d5, d6;
        if (FunctionNameNum == 4) {
            switch (RobotNum)
            {
                case 0:
                    break;
                case 1:
                    m0609.FHome();
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            return;
        }
        try
        {
            d1 = float.Parse(IFtv1.text.ToString());
            d2 = float.Parse(IFtv2.text.ToString());
            d3 = float.Parse(IFtv3.text.ToString());
            d4 = float.Parse(IFtv4.text.ToString());
            d5 = float.Parse(IFtv5.text.ToString());
            d6 = float.Parse(IFtv6.text.ToString());
        }
        catch {
            return;
        }
        switch (RobotNum) {
            case 0:
                break;
            case 1:
                if (FunctionNameNum == 1) {
                    m0609.FMoveJ(d1, d2, d3, d4, d5, d6);
                    dsbot.MoveJ(d1, d2, d3, d4, d5, d6);
                }
                else if (FunctionNameNum == 2) {
                    m0609.FMove(d1, d2, d3, d4, d5, d6);
                    dsbot.MoveJ(m0609.tq1, m0609.tq2, m0609.tq3, m0609.tq4, m0609.tq5, m0609.tq6);
                }
                else if (FunctionNameNum == 3) { m0609.FMoveL(d1, d2, d3, d4, d5, d6); }
                else { m0609.FHome(); }
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    public void btSaveClick() {
        RobotControlCommand rcc = new RobotControlCommand();
        if (FunctionNameNum == 0) return;
        try
        {
            rcc.Command = FunctionNameNum;
            if (FunctionNameNum != 4)
            {
                rcc.d1 = float.Parse(IFtv1.text.ToString());
                rcc.d2 = float.Parse(IFtv2.text.ToString());
                rcc.d3 = float.Parse(IFtv3.text.ToString());
                rcc.d4 = float.Parse(IFtv4.text.ToString());
                rcc.d5 = float.Parse(IFtv5.text.ToString());
                rcc.d6 = float.Parse(IFtv6.text.ToString());
            }
            else {
                flist.AddFunction("Home", "", "", "", "", "", "");
            }
        }
        catch {
            return;
        }
        commandlist.Add(rcc);
        flist.AddFunction(IFFunctionName.text.ToString(), IFtv1.text.ToString(), IFtv2.text.ToString(), IFtv3.text.ToString(),IFtv4.text.ToString(), IFtv5.text.ToString(), IFtv6.text.ToString());
        FunctionNameNum = 0;
        IFtv1.text = "";
        IFtv2.text = "";
        IFtv3.text = "";
        IFtv4.text = "";
        IFtv5.text = "";
        IFtv6.text = "";

        txtv1.text = "d1";
        txtv2.text = "d2";
        txtv3.text = "d3";
        txtv4.text = "d4";
        txtv5.text = "d5";
        txtv6.text = "d6";
        m0609.ListFileSave();
    }
    public void btClearClick() {
        FunctionNameNum = 0;
        IFtv1.text = "";
        IFtv2.text = "";
        IFtv3.text = "";
        IFtv4.text = "";
        IFtv5.text = "";
        IFtv6.text = "";

        txd1.text = "d1";
        txd2.text = "d2";
        txd3.text = "d3";
        txd4.text = "d4";
        txd5.text = "d5";
        txd6.text = "d6";
        m0609.ListClear();
    }
    public void btRUNClick() {
        RunFunction = true;
        runCnt = 0;
    }
    public void btA0509Click() {
        m0609.transform.position = new Vector3(0, 0, -100);
        a0509s.transform.position = new Vector3(0, 0, 0);
        m1013.transform.position = new Vector3(0, 0, -100);
        RobotNum = 0;
    }
    public void btM0609Click() {
        m0609.transform.position = new Vector3(0, 0, 0);
        a0509s.transform.position = new Vector3(0, 0, -100);
        m1013.transform.position = new Vector3(0, 0, -100);
        RobotNum = 1;
    }
    public void btM1013Click()
    {
        m0609.transform.position = new Vector3(0, 0, -100);
        a0509s.transform.position = new Vector3(0, 0, -100);
        m1013.transform.position = new Vector3(0, 0, 10);
        RobotNum = 0;
    }
    public void btH2017Click()
    {
        m0609.transform.position = new Vector3(0, 0, -100);
        a0509s.transform.position = new Vector3(0, 0, -100);
        m1013.transform.position = new Vector3(0, 0, -100);
        RobotNum = 0;
    }
    public void btClearAllClick() {
        flist.AllDelete();
        commandlist.Clear();
    }
    public void MoveJ(float q1, float q2, float q3, float q4, float q5, float q6) {
        dsbot.MoveJ(q1, q2, q3, q4, q5, q6);
    }
}
