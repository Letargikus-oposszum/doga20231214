using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _20231214
{
    public class FileService
    {

        public List<Data> Reading(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            var allProducts = new List<Data>();
            foreach (var line in lines.Skip(1))
            {
                var row = line.Replace(", ", ",");
                var data = row.Split(',');

                var newProduct = new Data();
                newProduct.Id = int.Parse(data[0]);
                newProduct.Name = data[1];
                newProduct.Quantity = int.Parse(data[2]);
                newProduct.UnitPrice = decimal.Parse(data[3].Replace(".", ","));
                allProducts.Add(newProduct);
            }
            return allProducts;
        }
        public void AddProduct()
        {
            try
            {
                while (true)
                {
                    var Givenproduct = new Data();
                    Console.WriteLine("Give the name of the product (type exit to exit): ");
                    Givenproduct.Name = Console.ReadLine();
                    if (Givenproduct.Name.Contains("exit"))
                    {
                        break;
                    }

                    Console.WriteLine("Give the quantity of the product: ");
                    Givenproduct.Quantity = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Give the unitprice of the product: ");
                    Givenproduct.UnitPrice = Convert.ToInt32(Console.ReadLine());



                    string[] lines = File.ReadAllLines("candies.csv");
                    if (lines.Length > 0)
                    {
                        string lastLine = lines[lines.Length - 1];

                        // A CSV fájlban feltételezzük, hogy az id mező a sorban az első mező
                        string[] fields = lastLine.Split(',');

                        // Ellenőrizd, hogy a sor tartalmaz-e az "id" mezőt
                        if (fields.Length > 0)
                        {
                            Givenproduct.Id = Convert.ToInt32(fields[0]) + 1;
                            Console.WriteLine($"Az utolsó sorban lévő id: {Givenproduct.Id}");
                        }
                        else
                        {
                            Console.WriteLine("Az utolsó sor nem tartalmaz id mezőt.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("A CSV fájl üres.");
                    }


                    File.AppendAllText("candies.csv", Environment.NewLine + $"{Givenproduct.Id},{Givenproduct.Name},{Givenproduct.Quantity},{Givenproduct.UnitPrice}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteProduct()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter the Id of the product to delete: ");
                    string idToDeleteStr = Console.ReadLine();
                    if (idToDeleteStr.Contains("exit"))
                    {
                        break;
                    }
                    int idToDelete = Convert.ToInt32(idToDeleteStr);
                    string[] lines = File.ReadAllLines("candies.csv");

                    // Ellenőrizzük, hogy a fájl üres-e
                    if (lines.Length == 0)
                    {
                        Console.WriteLine("The CSV file is empty.");
                        return;
                    }

                    // Ellenőrizze, hogy az Id alapján létezik-e a rekord
                    bool recordFound = false;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(',');
                        if (fields.Length > 0 && int.TryParse(fields[0], out int currentId) && currentId == idToDelete)
                        {
                            // A rekordot megtaláltuk, töröljük
                            lines[i] = null;
                            recordFound = true;
                            break;
                        }
                    }

                    if (!recordFound)
                    {
                        Console.WriteLine($"No product found with Id {idToDelete}.");
                        return;
                    }

                    // Az új tartalmat írjuk vissza a fájlba (a null elemek nélkül)
                    File.WriteAllLines("candies.csv", lines);

                    Console.WriteLine($"Product with Id {idToDelete} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ProductAvgMinMax()
        {
            {
                // CSV fájl beolvasása
                List<Data> dataList = ReadCsvFile("candies.csv");

                // Legnagyobb UnitPrice meghatározása
                decimal maxUnitPrice = GetMaxUnitPrice(dataList);
                Console.WriteLine($"The maximum UnitPrice is: {maxUnitPrice}");

                // Átlag számítása
                decimal averageUnitPrice = CalculateAverageUnitPrice(dataList);
                Console.WriteLine($"The average UnitPrice is: {averageUnitPrice}");
            }
        }

        public List<Data> ReadCsvFile(string filePath)
        {
            List<Data> dataList = new List<Data>();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');

                    if (parts.Length >= 5)
                    {
                        Data data = new Data
                        {
                            Id = Convert.ToInt32(parts[0]),
                            Name = parts[1],
                            Quantity = Convert.ToInt32(parts[2]),
                            UnitPrice = Convert.ToDecimal(parts[3])
                        };

                        dataList.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataList;
        }

        static decimal GetMaxUnitPrice(List<Data> dataList)
        {
            // Ha nincs adat, a default(decimal) értékkel tér vissza
            return dataList.Count > 0 ? dataList.Max(data => data.UnitPrice) : default(decimal);
        }

        static decimal CalculateAverageUnitPrice(List<Data> dataList)
        {
            // Ha nincs adat, a default(decimal) értékkel tér vissza
            return dataList.Count > 0 ? dataList.Average(data => data.UnitPrice) : default(decimal);
        }




        public void FileWrite(decimal Price, int Quantity, int recordId)
        {
            try
            {
                List<string> lines = File.ReadAllLines("candies.csv").ToList();
                for (int i = 1; i < lines.Count; i++)
                {
                    string[] parts = lines[i].Split(',');

                    // Az ID megtalálása a sorban
                    if (int.TryParse(parts[0], out int id) && id == recordId)
                    {
                        // A quantity és price frissítése
                        parts[3] = Quantity.ToString();
                        parts[4] = Price.ToString();

                        // Az eredmény visszaírása a listába
                        lines[i] = string.Join(",", parts);
                        File.WriteAllLines("candies.csv", lines);

                        // Az azonosító beállítása a data objektumban
                        var data = new Data
                        {
                            Id = recordId,
                            Name = parts[1],
                            Quantity = Quantity,
                            UnitPrice = Price
                        };

                        // A megfelelő rekord megtalálása után nincs szükség további iterációra
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
