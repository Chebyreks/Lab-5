using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Lab5.Routes;
using static Lab5.Stations;

namespace Lab5
{
    internal class RouteControl
    {
        Routes routes;
        Stations stations;

        public class MyTupleComparer : Comparer<Tuple<int, int, int>>
        {
            public override int Compare(Tuple<int, int, int> x, Tuple<int, int, int> y)
            {
                return x.Item2.CompareTo(y.Item2);
            }
        }

        public RouteControl(int len)
        {
            routes = new Routes();
            stations = new Stations(len);
        }

        public int Get_Id(string name)
        {
            return stations.Get_Id(name);
        }

        public String Get_Name(int index)
        {
            return stations.Get_Name(index);
        }

        int Get_ClosestBus_OnRoute(Route route, int dest_id, int time_now, bool reverse = false)
        {
            int[] route_arr = route.Get_Route();
            int start = route.Get_Schedule_Start();
            int interval = route.Get_Interval();
            int end = route.Get_Schedule_End();

            if (reverse)
            {
                Array.Reverse(route_arr);
            }




            if (time_now <= start || time_now <= interval + end)
            {
                int time_from_start = 0;
                int i = 1;
                while (route_arr[i - 1] != dest_id)
                {
                    if (i >= route_arr.Length)
                    {
                        return -1000;
                    }
                    time_from_start += stations.Get_Time(route_arr[i - 1], route_arr[i]);
                    i++;
                }
                return start + time_from_start - time_now;
            } 
            else
            {
                int n = ((time_now - start) % interval);
                int time_closest = time_now - n;

                int time_from_start = 0;
                int i = 1;
                while (route_arr[i - 1] != dest_id)
                {
                    if (i >= route_arr.Length)
                    {
                        return -1000;
                    }
                    time_from_start += stations.Get_Time(route_arr[i - 1], route_arr[i]);
                    i++;
                }
                int past_time = time_closest;
                int count = 0;
                bool immediate = false;
                if (time_now - time_closest == time_from_start)
                {
                    immediate = true;
                }
                // Чистое безумие, которое я уже забыл как работает
                while (time_now - time_closest < time_from_start)
                {
                    past_time = time_closest;
                    count++;
                    time_closest -= interval;
                }
                int time_left = time_from_start - (time_now - past_time);
                if (time_left < 0)
                {
                    time_left += interval;
                }
                if (immediate)
                {
                    time_left = 0;
                }
                return time_left;
            }
        }

        public Tuple<int, int, int>[] Get_Routes_For_Print(String str)
        {
            int id = stations.Get_Id(str);
            List<Tuple<int, int, int>> for_print = new();
            Tuple<int, int, int>[] for_print_arr = null;
            if (id != -1000)
            {
                int[] routes_id_arr = routes.Get_Routes_Ids();
                DateTime time_datetime = DateTime.Now;
                int time_now = time_datetime.Hour * 60 + time_datetime.Minute;
                string hours;
                string minutes;
                if (time_datetime.Hour < 10)
                {
                    hours = "0" + time_datetime.Hour.ToString();
                } else
                {
                    hours = time_datetime.Hour.ToString();
                }
                if (time_datetime.Minute < 10)
                {
                    minutes = "0" + time_datetime.Minute.ToString();
                } else
                {
                    minutes = time_datetime.Minute.ToString();
                }
                Console.SetCursorPosition(4, 7);
                Console.WriteLine($"Time now : {hours}:{minutes}");
                Console.SetCursorPosition(4, 16);
                foreach (int route_id in routes_id_arr)
                {
                    Route route_inst = routes.Get_Route(route_id);
                    int interval = route_inst.Get_Interval();
                    int start = route_inst.Get_Schedule_Start();
                    int end = route_inst.Get_Schedule_End();
                    int[] arr = route_inst.Get_Route();
                    if (start > end)
                    {
                        end += 1440;
                    }
                    int from_start = Get_ClosestBus_OnRoute(route_inst, id, time_now, false);
                    int from_end = Get_ClosestBus_OnRoute(route_inst, id, time_now, true);
                    if (from_start != -1000)
                    {
                        for_print.Add(new Tuple<int, int, int>(route_id, from_start, arr[0]));
                    }
                    if (from_end != -1000)
                    {
                        for_print.Add(new Tuple<int, int, int>(route_id, from_end, arr[arr.Length - 1]));
                    }
                }
                for_print_arr = for_print.ToArray();
                Array.Sort(for_print_arr, new MyTupleComparer());
            }
            Console.SetCursorPosition(4, 4);
            return for_print_arr;
        }


        public void Add_Route(int num, int start, int end, int interval, ref String[] stations_and_values)
        {

            List<int> path_arr = new List<int>();
            for (int i = 0; i < stations_and_values.Length; i++)
            {
                String[] station_split = stations_and_values[i].Split("->");
                String[] second_stat_and_value = station_split[1].Split(' ');
                String first = station_split[0];
                String second = second_stat_and_value[0];
                int value = int.Parse(second_stat_and_value[1]);
                stations.Add_Connection(first, second, value);
                stations.Add_Connection(second, first, value);
                path_arr.Add(stations.Get_Id(first));
            }
            String[] _station_split = stations_and_values[stations_and_values.Length - 1].Split("->");
            String[] _second_stat_and_value = _station_split[1].Split(' ');
            String _first = _station_split[0];
            String _second = _second_stat_and_value[0];
            int _value = int.Parse(_second_stat_and_value[1]);
            path_arr.Add(stations.Get_Id(_second));
            Route route = new(path_arr.ToArray(), start, end,interval);
            routes.Add_Route(num, route);
        }
    }
}
