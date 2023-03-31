using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

namespace godzillabanana{
public class SerialFlower : MonoBehaviour
{

    public String portName = "/dev/cu.usbmodem11101";  // 1. use the port name for your Arduino, such as /dev/tty.usbmodem1411 for Mac or COM3 for PC, make sure to update it in the inspector
    private SerialPort serialPort = null; 
    private int baudRate = 9600;  // 2. match your rate from your serial in Arduino
    private int readTimeOut = 100;
    private string serialInput;
    public int PetCount;
   private int currentlyActivePetals;
    public int AttachedPetals;
    bool programActive = true;
    Thread thread;

    bool close = false;

    public FlowerManagement flowerMango;

    void Start()
    {
        try
        {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.RtsEnable = true;
            serialPort.ReadTimeout = readTimeOut;
            serialPort.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        thread = new Thread(new ThreadStart(ProcessData));  // serial events are now handled in a separate thread
        thread.Start();
        
       PetCount = 0;
       currentlyActivePetals = AttachedPetals;
       
    }

    void ProcessData()
    {
        Debug.Log("Thread: Start");
        while (programActive)
        {
            try
            {
                serialInput = serialPort.ReadLine();
            }
            catch (TimeoutException)
            {

            }
        }
        Debug.Log("Thread: Stop");
    }

    void Update()
    {
        if (serialInput != null)
        {
            string[] strEul = serialInput.Split(';'); 
             // 3. splite the data into an array using semicolon ' ; '
             for (int i =0; i< strEul.Length; i++){
                Debug.Log("input 0: "+strEul[0]);
             }
            if (strEul.Length >= 1) // 4. only move forward if every input expected has been received. In this case, was have 2 inputs - a button (0 or 1) and an analog values between 0 and 1023
            { 
                //5. insert your game logic here
                if (int.Parse(strEul[0]) != currentlyActivePetals) // if button pressed
                {
                  Debug.Log("plucked");
                   flowerMango.Pluck(PetCount);
                   currentlyActivePetals = int.Parse(strEul[0]);
                   PetCount++;
                }

                
        } 
        else { Debug.Log("Oh no! No inputs at all! call robot police!!");}
    }

    static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    void OnDisable()  // attempts to closes serial port when the gameobject script is on goes away
    {
        programActive = false;
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
}
}