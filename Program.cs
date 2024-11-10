using static Lab5.RouteControl;
using static Lab5.InterfaceHandler;
using Lab5;
internal class Program
{
    private static void Main(string[] args)
    {
        RouteControl routecontrol = new RouteControl(1000); // Бяка
        string content = System.IO.File.ReadAllText(@"..\..\..\Routes_File.txt");
        string[] routes_parsed = content.Split('\n');
        foreach (string route_parsed in routes_parsed)
        {
            string[] strs = route_parsed.Split(new string[] { ": ", " = " }, StringSplitOptions.None);
            string route_name = strs[0];
            string[] route_params = strs[1].Split(new string[] { ", " }, StringSplitOptions.None);
            int start = int.Parse(route_params[0]);
            int end = int.Parse(route_params[1]);
            int interval = int.Parse(route_params[2]);
            string[] routes_and_values = strs[2].Split(new string[] { ", " }, StringSplitOptions.None);
            routecontrol.Add_Route(int.Parse(route_name), start, end, interval, ref routes_and_values);
        }

        bool isWorking = true;
        int state = 0;
        while (isWorking)
        {
            PrintInterfaceInit();
            string comm = Console.ReadLine();
            Console.Clear();
            PrintInterfaceInit();
            if (comm == null)
            {
                Console.SetCursorPosition(4, 6);
                Console.Write("Wrong command, Try Again");
                Console.SetCursorPosition(4, 4);
                continue;
            }
            string[] coms = comm.Split(' ');
            if (coms[0] == "break")
            {
                isWorking = false;
            } 
            else if (coms[0] == "see")
            {
                if (coms.Length != 2)
                {
                    Console.SetCursorPosition(4, 6);
                    Console.Write("Wrong command, Try Again");
                    Console.SetCursorPosition(4, 4);
                    continue;
                }
                Tuple<int, int, int>[] routes_for_print = routecontrol.Get_Routes_For_Print(coms[1]);
                PrintInterface(ref routes_for_print, ref routecontrol, coms[1]);
            }
            else
            {
                Console.SetCursorPosition(4, 6);
                Console.Write("Wrong command, Try Again");
                Console.SetCursorPosition(4, 4);
                continue;
            }
        }
        Console.SetCursorPosition(0, 20);
    }
}