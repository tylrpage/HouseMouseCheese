using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.IO.Ports;

namespace HouseMouseCheese.Helpers
{
    public class ArduinoSerialWriter
    {
        private string _comName;
        private int _chunkSize; // how many bytes are written to the serial port at a time
        private SerialPort _sp;

        public ArduinoSerialWriter(string comName, int chunkSize)
        {
            _comName = comName;
            _chunkSize = chunkSize;
            _sp = new SerialPort()
            {
                PortName = _comName,
                DtrEnable = true,
                RtsEnable = true,
                ReceivedBytesThreshold = 1
            };
            _sp.DataReceived += _sp_DataReceived;
        }

        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Console.WriteLine(sp.ReadLine());
        }

        ~ArduinoSerialWriter()
        {
            if (_sp.IsOpen)
            {
                _sp.Close();
            }
        }

        public void WriteFrame(Frame frame)
        {
            if (!_sp.IsOpen)
                throw new Exception("Serial port must be open first");

            byte[] data = GetBytesFromPixels(frame.Pixels);
            int offset = 0;
            while (offset < data.Length)
            {
                _sp.Write(data, offset, _chunkSize);
                offset += _chunkSize;
            }           
        }

        public void WritePattern(Pattern pattern)
        {
            _sp.Open();
            foreach (Frame frame in pattern.Frames)
            {
                WriteFrame(frame);
                Thread.Sleep(50);
            }
            _sp.Close();
        }

        private byte ApplyTransparency(byte color, byte transparency)
        {
            float level = (int)transparency / 255f;
            int newColor = (int)((int)color * level);
            return Convert.ToByte(newColor);
        }

        private byte[] GetBytesFromPixels(Pixel[] pixels)
        {
            byte[] ret = new byte[pixels.Length * 3];
            for (int i = 0; i < pixels.Length; i++)
            {
                Pixel pixel = pixels[i];
                ret[i*3 + 0] = ApplyTransparency(pixel.Color.R, pixel.Color.A);
                ret[i * 3 + 1] = ApplyTransparency(pixel.Color.G, pixel.Color.A);
                ret[i*3 + 2] = ApplyTransparency(pixel.Color.B, pixel.Color.A);
            }
            return ret;
        }
    }
}
