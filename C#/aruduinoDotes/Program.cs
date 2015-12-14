using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO.Ports;

namespace aruduinoDotes
{
    class Program
    {
        static SerialPort sp = new SerialPort();
        static void Main(string[] args)
        {
            try
            {
                sp.PortName = SerialPort.GetPortNames()[0];
                sp.DataReceived += ArduinoDataRecieved;
                sp.Open();
                sp.BaudRate = 9600;

                Console.WriteLine("Connected to Arduino at" + SerialPort.GetPortNames()[0]);

                while (true) {
                    LookForGame();

                    sp.Write("Hi Arduino");

                    System.Threading.Thread.Sleep(30000); //Time user has to accept match
                }
               
                sp.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        static void LookForGame() {
            while (!FindReadyBox(GetScreenshot()))
                System.Threading.Thread.Sleep(1000);
        }

        static void ArduinoDataRecieved(Object sender, SerialDataReceivedEventArgs e) {
            string data = sp.ReadTo("\n");
            Console.WriteLine(data);
            if (data.ToString().Trim() == "Y") {
                Console.WriteLine("User accepted Match");
                //pressEnter();
            }
            else
                Console.WriteLine("User declined Match");       
        }

        static void pressEnter() {
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
        }

        static Bitmap GetScreenshot()
        {  
            Bitmap screenShot = new Bitmap(2560, 1600, PixelFormat.Format32bppArgb);

            Graphics gfxScreenshot = Graphics.FromImage(screenShot);

            gfxScreenshot.CopyFromScreen(0, 0, 0, 0, new Size(2560, 1600), CopyPixelOperation.SourceCopy);
            
            return screenShot;
        }

        static bool TestFindReadyBox() {
            Bitmap dotes = (Bitmap)Image.FromFile("unnamed.png");

            if (FindReadyBox(dotes))
            {
                Console.WriteLine("FindReadyBox() succesful");
                return true;
            }
            else
                Console.WriteLine("FindReadyBox() test failed");
            return false;
        }

        static bool FindReadyBox(Bitmap dotes, bool debugOutput = false)
        {
            int totalPixelsInRange = 0;

            Color calibrationPixel = Color.FromArgb(255, 43, 97, 68);

            for (int x = 620; x < 680; x++)
            {
                Color currentPixel = dotes.GetPixel(x, 503);

                if (debugOutput)
                    Console.WriteLine("R: {0}\tB: {1}\tG: {2}", currentPixel.R, currentPixel.G, currentPixel.B);

                int deltaR = Math.Abs(currentPixel.R - calibrationPixel.R);
                int deltaG = Math.Abs(currentPixel.G - calibrationPixel.G);
                int deltaB = Math.Abs(currentPixel.B - calibrationPixel.B);

                if (deltaR < 5 && deltaG < 5 && deltaB < 5)
                    totalPixelsInRange++;

                if (debugOutput)
                    Console.WriteLine("Delta: R: {0}\tG: {1}\tB: {2}", deltaR, deltaG, deltaB);
            }

            if (totalPixelsInRange >= 30)
                return true;

            return false;
        }
    }
    
}


