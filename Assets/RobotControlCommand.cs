using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControlCommand {
    public RobotControlCommand() { }
    public int Command;
    public float d1;
    public float d2;
    public float d3;
    public float d4;
    public float d5;
    public float d6;
    public void SetCommand(int c, float d1, float d2, float d3, float d4, float d5, float d6) {
        this.Command = c;
        this.d1 = d1;
        this.d2 = d2;
        this.d3 = d3;
        this.d4 = d4;
        this.d5 = d5;
        this.d6 = d6;
    }
}
