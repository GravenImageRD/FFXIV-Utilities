using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput.Native;
using EmoteList = Emotes.Emotes;
using Input = InputSimulator.InputSimulator;

namespace AutoInput
{
    internal class Program
    {
        static IntPtr FF14WindowHandle;

        static void GetFFWindowHandle()
        {
            Process p = Process.GetProcessesByName("ffxiv_dx11").FirstOrDefault();
            if (p == null)
            {
                throw new InvalidOperationException("Couldn't find a window handle for FF14. Is it running?");
            }

            FF14WindowHandle = p.MainWindowHandle;
        }

        static void Main(string[] args)
        {
            GetFFWindowHandle();
            var RNG = new Random();

            while (true)
            {
                // Might as well jump or dance or something.
                //int Thing = RNG.Next(0, 10);
                //switch (Thing)
                //{
                //    case 0:
                //        {

                //            break;
                //        }

                //    default:
                //        {
                //            Input.SendKeyPressSequence(FF14WindowHandle, EmoteList.GetEmoteSequence(Thing));
                //            break;
                //        }
                //}

                Console.WriteLine("Jump!");
                Input.SendKeyPress(FF14WindowHandle, new Input.KeyData() { Code = VirtualKeyCode.SPACE });
                var NextJump = DateTime.Now + TimeSpan.FromMinutes(7) + TimeSpan.FromSeconds(RNG.Next(0, 360));
                while (DateTime.Now < NextJump)
                {
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                    Console.WriteLine($"Next jump in {(NextJump - DateTime.Now).TotalMinutes:0} minutes.");
                }
            }
        }
    }
}
