// See https://aka.ms/new-console-template for more information
using congestion.calculator;
using Presentation.Models;

Console.WriteLine("Hello, World!");
Console.WriteLine("I am tax calculator:D");
var tempDates = new DateTime[16];
tempDates[0] = DateTime.Parse("2013-01-14 21:00:00");
tempDates[1] = DateTime.Parse("2013-01-15 21:00:00");
tempDates[2] = DateTime.Parse("2013-02-07 06:23:27");
tempDates[3] = DateTime.Parse("2013-02-07 15:27:00");
tempDates[4] = DateTime.Parse("2013-02-08 06:27:00");
tempDates[5] = DateTime.Parse("2013-02-08 06:20:27");
tempDates[6] = DateTime.Parse("2013-02-08 14:35:00");
tempDates[7] = DateTime.Parse("2013-02-08 15:29:00");
tempDates[8] = DateTime.Parse("2013-02-08 15:47:00");
tempDates[9] = DateTime.Parse("2013-02-08 16:01:00");
tempDates[10] = DateTime.Parse("2013-02-08 16:48:00");
tempDates[11] = DateTime.Parse("2013-02-08 17:49:00");
tempDates[12] = DateTime.Parse("2013-02-08 18:29:00");
tempDates[13] = DateTime.Parse("2013-02-08 18:35:00");
tempDates[14] = DateTime.Parse("2013-03-26 14:25:00");
tempDates[15] = DateTime.Parse("2013-03-28 14:07:27");
var myCar = new Car(congestion.calculator.enums.TollFreeVehicles.NotTollFree,congestion.calculator.enums.VehicleTypes.Car);
var _taxTestDbContext = new TaxTestDbContext();
var _helper = new congestion.calculator.Helpers.CongestionTaxFromDBHelper(_taxTestDbContext);
var result = _helper.GetTax(tempDates, myCar);
Console.WriteLine("your total is {0}",result.Values.Sum());
foreach(var item in result)
{
    Console.WriteLine("day : "+ item.Key + " , amount :"+item.Value.ToString());
}
Console.ReadLine();