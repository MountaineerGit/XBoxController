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
                System.Threading.Thread.Sleep(100);
            }
        }

        private static void Xbcw_ControllerStateChange1(XBoxController controller)
        {
            foreach (var c in XBoxController.GetConnectedControllers())
            {
                Console.WriteLine("Controller " + c.PlayerIndex.ToString() + 
                    " L(" + c.ThumbLeftX.ToString("0.0") + "," +
                    c.ThumbLeftY.ToString("0.0") + ")" +
                    " R(" + c.ThumbRightX.ToString("0.0") + "," +
                    c.ThumbRightY.ToString("0.0") + ")");
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
 