using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace MouseInverter
{
    class Inverter
    {
        private Point currentPosition;

        private bool running;

        private bool exit;

        static int Main(string[] args)
        {

            Inverter inverter = new Inverter();

            Console.CancelKeyPress += delegate
            {
                inverter.Stop();
            };

            inverter.Start();
            while (true)
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }
                
        private void MouseLoop()
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            while (!this.exit)
            {
                Point newPosition = Cursor.Position;

                int bottom = this.currentPosition.Y - (newPosition.Y - this.currentPosition.Y);
                int maxHeight = SystemInformation.VirtualScreen.Height;
                if (bottom > maxHeight)
                {
                    bottom = maxHeight - 10;
                }
                else if (bottom < 0)
                {
                    bottom = 10;
                }

                int right = this.currentPosition.X + (this.currentPosition.X - newPosition.X);
                int maxWidth = SystemInformation.VirtualScreen.Width;
                if (right > maxWidth)
                {
                    right = maxWidth - 10;
                }
                else if (right < 0)
                {
                    right = 10;
                }
                
                Cursor.Position = new Point(right, bottom);
                this.currentPosition = Cursor.Position;
                Thread.Sleep(1);
            }
            this.exit = false;
        }

        public void Start()
        {
            Console.WriteLine("给小仙女心心~");

            float y, x, z, f;
            for (y = 1.5f; y > -1.5f; y -= 0.1f)
            {
                for (x = -1.5f; x < 1.5f; x += 0.05f)
                {
                    z = x * x + y * y - 1;
                    f = z * z * z - x * x * y * y * y;
                    Console.Write(f <= 0.0f ? ".:-=+*#%@"[(int)(f * -8.0f)] : ' ');
                }
                Console.WriteLine();
            }

            this.currentPosition = Cursor.Position;
            this.running = true;
            (new Thread(new ThreadStart(this.MouseLoop))).Start();
        }

        public void Stop()
        {
            this.running = false;
            this.exit = true;
        }
    }
}
