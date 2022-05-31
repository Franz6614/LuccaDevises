
// See https://aka.ms/new-console-template for more information
if (args.Length == 0)
{
    Console.WriteLine("No file name - please enter : LuccaDevises <chemin vers le fichier>");
}
else
{
    //Convertor.Tests.LaunchTest();
    Console.WriteLine(Convertor.CurrencyConvertor.Convert.Execute(args[0]));
}
Console.WriteLine("Press a touch...");

Console.Read();