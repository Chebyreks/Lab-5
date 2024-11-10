using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Route
    {
        int[] Stations_Arr;
        int Schedule_Start;
        int Schedule_End;
        int Interval;

        public Route(int[] Stations_Arr, int Schedule_Start, int Schedule_End, int Interval)
        {
            this.Stations_Arr = Stations_Arr;
            this.Schedule_Start = Schedule_Start;
            this.Schedule_End = Schedule_End;
            this.Interval = Interval;
        }

        public int Get_Interval()
        {
            return this.Interval; 
        }

        public int Get_Schedule_Start()
        {
            return this.Schedule_Start;
        }

        public int Get_Schedule_End()
        {
            return this.Schedule_End;
        }

        public int[] Get_Route()
        {
            return Stations_Arr;
        }

        bool Verify_Route_Time(int time)
        {
            return (Schedule_Start < time && time < Schedule_End);
        }
    }

    internal class Routes
    {
        Dictionary<int, Route> Routes_Dict;

        public Routes()
        {
            Routes_Dict = new Dictionary<int, Route>();
        }

        public void Add_Route(int num, Route route)
        {
            Routes_Dict.Add(num, route);
        }

        public Route Get_Route(int num)
        {
            return Routes_Dict[num];
        }

        public int[] Get_Routes_Ids()
        {
            int[] routes_arr = new int[Routes_Dict.Count];
            int i = 0;
            foreach (int route_num in Routes_Dict.Keys) {
                routes_arr[i] = route_num;
                i++;
            }
            return routes_arr;
        }
    }
}
