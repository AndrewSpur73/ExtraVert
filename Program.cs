// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using System.IO.Compression;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Plant 1",
        LightNeeds = 1,
        AskingPrice = 5.00M,
        City = "Nashville",
        ZIP = 37203,
        Sold = true
    },
        new Plant()
    {
        Species = "Plant 2",
        LightNeeds = 2,
        AskingPrice = 10.00M,
        City = "Gallatin",
        ZIP = 37066,
        Sold = true
    },
        new Plant()
    {
        Species = "Plant 3",
        LightNeeds = 3,
        AskingPrice = 15.00M,
        City = "Hendersonville",
        ZIP = 37075,
        Sold = false
    },
        new Plant()
    {
        Species = "Plant 4",
        LightNeeds = 4,
        AskingPrice = 20.00M,
        City = "Knoxville",
        ZIP = 37916,
        Sold = false
    },
        new Plant()
    {
        Species = "Plant 5",
        LightNeeds = 5,
        AskingPrice = 25.00M,
        City = "Nashville",
        ZIP = 37203,
        Sold = false
    }
};

Random random = new();
Plant? POTD = null;
while (POTD == null || POTD.Sold)
{
    int randomPlant = random.Next(1, plants.Count);
    POTD = plants[randomPlant];
}

string greeting = @$"Welcome to ExtraVert
Your one-stop shop for plants
The Plant of the day is {POTD.Species}!";

Console.WriteLine(greeting);

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an Option:
0. Exit
1. View All Plants
2. Post a Plant
3. Adopt a Plant
4. Delist a Plant
5. Plant of the Day
6. Search by Light Needs
7. Plant Stats");
    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ListPlants();
    }
    else if (choice == "2") 
    {
        AddPlant();
    }
    else if (choice == "3")
    {
        AdoptPlant();
    }
    else if (choice == "4")
    {
        DelistPlant();
    }
    else if (choice == "5")
    {
        PlantOfTheDay();
    }
    else if (choice == "6")
    {
        SearchByLightNeeds();
    }
    else if (choice == "7")
    {
        PlantStats();
    }
    else
    {
        Console.WriteLine("Please select an option");
    }
}

void ListPlants()
{
    Console.WriteLine("Plants:");
    for (int i = 0; i < plants.Count; i++)
    {
        string soldStatus = plants[i].Sold ? "sold" : "available";
        Console.WriteLine($"{i + 1}.{plants[i].Species} in {plants[i].City} {soldStatus} for {plants[i].AskingPrice} dollars.");
    }
}

void AddPlant()
{
    Console.WriteLine("Please input the plant species");
    string plantSpecies = Console.ReadLine();
    Console.WriteLine("Please input the light needs of the plant on a scale from 1-5");
    int plantLightNeeds = Int32.Parse(Console.ReadLine());
    Console.WriteLine("Please input the asking price of the plant");
    decimal plantAskingPrice = Decimal.Parse(Console.ReadLine());
    Console.WriteLine("Please input the origin city of the plant");
    string plantCity = Console.ReadLine();
    Console.WriteLine("Please input a zip code");
    int plantZIP = Int32.Parse(Console.ReadLine());
    Console.WriteLine("Please supply the date this plant is available until.");
    Console.WriteLine("Year: ex. 2024");
    int plantYear = int.Parse(Console.ReadLine());
    Console.WriteLine("Month: ex. 01 - January.");
    int plantMonth = int.Parse(Console.ReadLine());
    Console.WriteLine("Day: ex. 1-31");
    int plantDay = int.Parse(Console.ReadLine());

    try
    {
        Plant PlantToAdd = new Plant();
        PlantToAdd.Species = plantSpecies;
        PlantToAdd.LightNeeds = plantLightNeeds;
        PlantToAdd.AskingPrice = plantAskingPrice;
        PlantToAdd.City = plantCity;
        PlantToAdd.ZIP = plantZIP;
        PlantToAdd.Sold = false;
        PlantToAdd.AvailableUntil = new DateTime(plantYear, plantMonth, plantDay);

        plants.Add(PlantToAdd);
    }
    catch
    {
        Console.WriteLine("Please pick a valid date!");
    }
}

void AdoptPlant()
{
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();

    Console.WriteLine("Select a plant using the corresponding number: \n");
    for (int i = 0; i < availablePlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}.{availablePlants[i].Species}");
    }
    Console.WriteLine();
    int choice = Convert.ToInt32(Console.ReadLine());

    if (choice > 0 && choice <= availablePlants.Count) 
    { 
        Plant selectedPlant = availablePlants[choice - 1];
        selectedPlant.Sold = true;
        Console.WriteLine($"\n{selectedPlant.Species} has been adopted!");
    }
}

void DelistPlant()
{
    string choice = null;

    while (choice != "0") 
    { 
        try 
        {
            Console.WriteLine("0. Goodbye");
            for (int i = 0; i < plants.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plants[i].Species}");
            }
            choice = Console.ReadLine();
            plants.RemoveAt(Int32.Parse(choice) - 1);
        }
        catch 
        { 
            break;
        }
    }
}

void PlantOfTheDay()
{
    Random random = new();
    Plant? POTD = null;
    while (POTD ==  null || POTD.Sold) 
    {
    int randomPlant = random.Next(1, plants.Count);
    POTD = plants[randomPlant];
    }
    Console.WriteLine($@"The Plant of the day is {POTD.Species}. It is from {POTD.City} {POTD.ZIP}. It needs a light level (1-5) of: {POTD.LightNeeds} and costs: {POTD.AskingPrice}.");

}

void SearchByLightNeeds()
{
    List<Plant> availablePlants = plants.Where(p => !p.Sold).ToList();

    int choice;
    Console.WriteLine("Enter the maximum Light Needs Rating (1-5): ");
    choice = Int16.Parse(Console.ReadLine());
    List<Plant> searchedPlant = availablePlants.Where(plant => plant.LightNeeds == choice).ToList();
    foreach (Plant plant in searchedPlant)
    {
        Console.WriteLine(plant.Species);
    }
}

void PlantStats()
{
    LowestPrice();
    NumberOfAvailablePlants();
    HighestLightNeeds();
    AverageLightNeeds();
    PercentageOfPlantsAdopted();
}

void LowestPrice()
{
    var availablePlants = plants.Where(p => !p.Sold);
    var plantLowestPrice = availablePlants.OrderBy(p => p.AskingPrice).First().Species;
    Console.WriteLine($"The least expensive plant is {plantLowestPrice}");
}

void NumberOfAvailablePlants()
{
    var availablePlantsCount = plants.Count(p => !p.Sold);
    Console.WriteLine($"There are {availablePlantsCount} plants available.");
}

void HighestLightNeeds()
{
    var availablePlants = plants.Where(p => !p.Sold);
    var plantHighestNeeds = availablePlants.OrderByDescending(p => p.LightNeeds).First().Species;
    Console.WriteLine($"The plant with the highest light needs is {plantHighestNeeds}");
}

void AverageLightNeeds()
{
    var availablePlants = plants.Where(p => !p.Sold);
    var averageLightNeeds = availablePlants.Average(p => p.LightNeeds);
    Console.WriteLine($"The average light needs is {averageLightNeeds}");
}

void PercentageOfPlantsAdopted()
{
    var adoptedPlantsCount = plants.Count(p => p.Sold);
    var totalPlantsCount = plants.Count();
    double percentPlantsAdopted = Math.Round((double)adoptedPlantsCount / totalPlantsCount * 100);
    Console.WriteLine($"Percentage of Plants Adopted: {percentPlantsAdopted}%");
}


string PlantDetails(Plant plant)
{
    string plantString = $"Species: {plant.Species}\n" +
                         $"Light Needs: {plant.LightNeeds}/10\n" +
                         $"Asking Price: {plant.AskingPrice:C}\n" +
                         $"City: {plant.City}\n" +
                         $"ZIP Code: {plant.ZIP}\n" +
                         $"Sold: {(plant.Sold ? "Yes" : "No")}\n" +
                         $"Available Until: {plant.AvailableUntil}\n";

    return plantString;
}