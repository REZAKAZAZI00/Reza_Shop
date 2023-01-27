

namespace Shop.Core.Generator
{
    public class NameGenerator
    {

        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0,30);
        }

        public static string GenerateNameForImage()
        {
            return Guid.NewGuid().ToString().Replace("-", "")[..20];
        }

    }
}
//string URL = "";
//List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 0 };
//List<char> characters = new List<char>()
//    {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i','l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z','D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'Y', 'Z'};

//// Create one instance of the Random  
//Random rand = new Random();
//// run the loop till I get a string of 10 characters  
//for (int i = 0; i < 11; i++)
//{
//    // Get random numbers, to get either a character or a number...  
//    int random = rand.Next(0, 3);
//    if (random == 1)
//    {
//        // use a number  
//        random = rand.Next(0, numbers.Count - 1);
//        URL += numbers[random].ToString();
//    }
//    else
//    {
//        // Use a character  
//        random = rand.Next(0, characters.Count);
//        URL += characters[random].ToString();
//    }
//}
//return URL;
//        }