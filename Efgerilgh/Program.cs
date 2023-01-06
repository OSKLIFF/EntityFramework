using Efgerilgh.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efgerilgh
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkStart:
            string good;
            Console.Write("Введите название товара: ");
            good = Console.ReadLine();
            foreach (var item in GetValuesEf())
                if (item.Name == good)
                    Console.WriteLine(string.Join(" ",item.Name,item.Nums));
            goto LinkStart;
        }

        public static List<SeverData> GetValuesEf()
        {
            using (IDbConnection connection = new SqlConnection(Settings.Default.DbConnect))
            {
                IDbCommand command = new SqlCommand("SELECT Goods.Name, Storage.Nums\r\nFROM Goods\r\nJOIN Storage ON Goods.Name = Storage.Name");
                command.Connection = connection;
                connection.Open();
                IDataReader reader = command.ExecuteReader();
                List<SeverData> data = new List<SeverData>();
                while (reader.Read())
                {
                    SeverData severdata = new SeverData();
                    severdata.Name = reader.GetString(0);
                    severdata.Nums = reader.GetInt32(1);
                    data.Add(severdata);
                }
                return data;
            }
        }
    }
}
