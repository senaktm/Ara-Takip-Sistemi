using MongoDB.Bson.Serialization.Attributes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace yazlab2_1.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        public DateTime date { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public int carID { get; set; }

      
        

        /*
        public List<Car> ReadCSVFile()
        {  
            var counter = 0;
            string filePath = @"C:/Users/FIRAT/Desktop/allCars4.csv";
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = null;

            reader = new StreamReader(stream);
            List<Car> csvFile = new List<Car>();


            while (!reader.EndOfStream)
            {
               

                var line = reader.ReadLine();
                var values = line.Split(',');
               

                var data = new Car()
                {
                    date = Convert.ToDateTime(values[0]),
                    lat = values[1],
                    lon = values[2],
                    carID = Convert.ToInt32(values[3])

                };


                csvFile.Add(data);
                counter++;

                foreach (var coloumn1 in csvFile)
                {
                    Console.WriteLine(coloumn1);
                }

            }
            return csvFile;
        }



        */

            
        }

    }



