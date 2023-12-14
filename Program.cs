using _20231214;
using System.Linq;
using System.Xml.Serialization;

var filePath = "candies.csv";

var fileservice = new FileService();
var allCandies = fileservice.Reading(filePath);
var candie = SelectCandie();
var data = new Data();

Console.WriteLine($"{candie.Id}, {candie.Name}, {candie.Quantity}, {candie.UnitPrice}");

Data SelectCandie()
{
    do
    {
        Console.WriteLine("Melyik telefon árát szeretnénk módosítani? (Id)");
        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            var product = allCandies.FirstOrDefault(product => product.Id == productId);
            if (fileservice is not null)
                return product;

            Console.WriteLine("Bike not found!");
            continue;
        }

        Console.WriteLine("Wrong id!\r\n");
    } while (true);
}


while (true)
{
    Console.WriteLine("Choose an operation (add product or remove product or rewrite product's data): ");
    string choice = Console.ReadLine();
    if (choice.Contains("add"))
    {
        choice = "";
        fileservice.AddProduct();

    }
    if (choice.Contains("remove"))
    {
        choice = "";
        fileservice.DeleteProduct();
    }
    if (choice.Contains("rewrite"))
    {
        choice = "";
        fileservice.FileWrite(candie.UnitPrice, candie.Quantity, candie.Id);
    }
    if (choice.Contains("exit"))
    {
        break;
    }

}
fileservice.ProductAvgMinMax();

