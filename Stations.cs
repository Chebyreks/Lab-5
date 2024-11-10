using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Stations
    {
        Dictionary<String, int> Stations_Id;
        Dictionary<int, String> Stations_Id_Reverse; // Почему? Так надо...
        int id_pointer = 0;
        int[][] AdjacencyMatrix;

        public Stations(int len)
        {
            Stations_Id = new Dictionary<String, int>();
            Stations_Id_Reverse = new Dictionary<int, string>();
            AdjacencyMatrix = new int[len][];
            for (int i = 0; i < AdjacencyMatrix.Length; i++)
            {
                AdjacencyMatrix[i] = new int[len];
            }
        }

        private void Give_Id(String station_name)
        {
            Stations_Id.Add(station_name, id_pointer);
            Stations_Id_Reverse.Add(id_pointer, station_name);
            id_pointer++;
        }

        public int Get_Time(int start, int end)
        {
            return AdjacencyMatrix[start][end];
        }

        public String Get_Name(int id)
        {
            if (Stations_Id_Reverse.ContainsKey(id))
            {
                return Stations_Id_Reverse[id];
            }
            else
            {
                return "THE WHAT????";
            }
        }

        public int Get_Id(String station_name)
        {
            if (Stations_Id.ContainsKey(station_name))
            {
                return Stations_Id[station_name];
            } else
            {
                return -1000;
            }
        }

        public void Add_Connection(String start, String end, int value)
        {
            if (!Stations_Id.ContainsKey(start))
            {
                Give_Id(start);
            }
            if (!Stations_Id.ContainsKey(end))
            {
                Give_Id(end);
            }
            AdjacencyMatrix[Stations_Id[start]][Stations_Id[end]] = value;
        }

        public Dictionary<String, int> Get_Stations()
        {
            return Stations_Id;
        }

    }
}
