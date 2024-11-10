using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class InterfaceHandler
    {

        public static void PrintMenu(int x, int y, ref Tuple<int, int, int>[] routes, ref RouteControl routecontrol)
        {
            int i = 0;
            foreach (Tuple<int, int, int> route in routes)
            {
                Console.SetCursorPosition(x, y + i);
                i++;
                Console.WriteLine("{0}, destination {1}, {2} min", route.Item1, routecontrol.Get_Name(route.Item3), route.Item2);
            }
        }

        public static void DrawLine(int x, int y, int count, bool vertical)
        {
            if (vertical)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.Write("║");
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Console.SetCursorPosition(x + i, y);
                    Console.Write("═");
                }
            }

        }

        private static void BusThingy()
        {
            Console.SetCursorPosition(60, 4);
            Console.WriteLine("                          __");
            Console.SetCursorPosition(60, 5);
            Console.WriteLine(" .-----------------------'  |");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine("/| _ .---. .---. .---. .---.|");
            Console.SetCursorPosition(60, 7);
            Console.WriteLine("|j||||___| |___| |___| |___||");
            Console.SetCursorPosition(60, 8);
            Console.WriteLine("|=|||=======================|");
            Console.SetCursorPosition(60, 9);
            Console.WriteLine("[_|j||(O)\\__________|(O)\\___]");
            Console.SetCursorPosition(4, 4);
        }

        public static void PrintBordersInit()
        {
            Console.SetCursorPosition(3, 2);
            Console.Write("Введите комманду");
            Console.SetCursorPosition(3, 3);
            Console.Write('╔');
            DrawLine(4, 3, 40, false);
            Console.SetCursorPosition(3, 3);
            DrawLine(3, 4, 1, true);
            Console.SetCursorPosition(3, 5);
            Console.Write('╚');
            DrawLine(4, 5, 40, false);
            Console.SetCursorPosition(43, 3);
            Console.Write('╗');
            DrawLine(43, 4, 1, true);
            Console.SetCursorPosition(43, 5);
            Console.Write('╝');
            BusThingy();
        }

        public static void PrintBorders(int len)
        {
            DrawLine(3, 8, len + 1, true);
            DrawLine(43, 8, len + 1, true);
            DrawLine(4, 8, 40, false);
            DrawLine(4, 8 + len + 1, 40, false);
            Console.SetCursorPosition(3, 8);
            Console.Write('╔');
            Console.SetCursorPosition(3, 8 + len + 1);
            Console.Write('╚');
            Console.SetCursorPosition(43, 8);
            Console.Write('╗');
            Console.SetCursorPosition(43, 8 + len + 1);
            Console.Write('╝');
        }

        public static void PrintInterface(ref Tuple<int, int, int>[] routes, ref RouteControl routecontrol, string station_name)
        {
            if ((routes == null || routes.Length == 0) && routecontrol.Get_Id(station_name) == -1000)
            {
                Console.SetCursorPosition(4, 6);
                Console.Write("No Such Station :(");
                Console.SetCursorPosition(4, 4);
                return;
            } else if (routes == null || routes.Length == 0)
            {
                Console.SetCursorPosition(4, 6);
                Console.Write("No Buses coming found :(");
                Console.SetCursorPosition(4, 4);
                return;
            }
            PrintMenu(4 , 9, ref routes, ref routecontrol);
            PrintBorders(routes.Length);
        }

        public static void PrintInterfaceInit()
        {
            PrintBordersInit();
            Console.SetCursorPosition(4, 4);
        }

    }

}
