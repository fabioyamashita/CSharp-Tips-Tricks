using System.Text.Json.Nodes;

namespace Fetching_Data_with_JsonNode
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var uri = "https://pokeapi.co/api/v2/pokemon/";
            var jsonNodeTotalPokemons = await Fetch(uri);
            var totalPokemons = jsonNodeTotalPokemons!["count"];
            Console.WriteLine($"Total Pokémons: {totalPokemons}");

            Console.Write("\nEnter a pokémon id: ");
            var pokemonId = Console.ReadLine();

            try
            {
                // Property
                var jsonNodePokemon = await Fetch($"{uri}{pokemonId}");
                var pokemonName = jsonNodePokemon!["name"];
                Console.WriteLine($"\nPokémon #{pokemonId}: {pokemonName?.ToString().ToUpper()}");

                // Object
                var specieURL = jsonNodePokemon!["species"]!.AsObject()!["url"];
                Console.WriteLine($"Specie URL: {specieURL}");

                // Array
                Console.WriteLine("Types:");
                var types = jsonNodePokemon!["types"]!.AsArray();
                foreach (var type in types)
                {
                    Console.WriteLine($"- {type!["type"]!["name"]}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task<JsonNode> Fetch(string url)
        {
            var http = new HttpClient();
            var response = await http.GetAsync(url);
            var message = await response.Content.ReadAsStringAsync();
            var jsonNode = JsonNode.Parse(message);

            return jsonNode;
        }
    }
}