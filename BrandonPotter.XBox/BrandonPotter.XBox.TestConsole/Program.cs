using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandonPotter.XBox.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            XBoxControllerWatcher xbcw = new XBoxControllerWatcher();
            xbcw.ControllerConnected += OnControllerConnected;
            xbcw.ControllerDisconnected += OnControllerDisconnected;
            xbcw.ControllerStateChange += Xbcw_ControllerStateChange1;

            Console.WriteLine("Press any key to exit.");

            while (!Console.KeyAvailable)
            {
                System.Threading.Thread.Sleep(5);
            }
        }

        private static void Xbcw_ControllerStateChange1(XBoxController controller)
        {
            var c = XBoxController.GetConnectedControllers();
            if(c != null)
            {
                Console.WriteLine("({0,6:0.00}:{1,6:0.00}) ({2,6:0.00}:{3,6:0.00}) {4,1} {5,1} {6,1} {7,1}",
                                  c.ThumbLeftX, c.ThumbLeftY, c.ThumbRightX, c.ThumbRightY,
                                  c.ButtonAPressed ? 'A' : ' ',
                                  c.ButtonBPressed ? 'B' : ' ',
                                  c.ButtonXPressed ? 'X' : ' ',
                                  c.ButtonYPressed ? 'Y' : ' ');
            }
        }

        private static void OnControllerDisconnected(XBoxController controller)
        {
            Console.WriteLine("Controller Disconnected: Player " + controller.PlayerIndex.ToString());
        }

        private static void OnControllerConnected(XBoxController controller)
        {
            Console.WriteLine("Controller Connected: Player " + controller.PlayerIndex.ToString());
        }
    }
}
 