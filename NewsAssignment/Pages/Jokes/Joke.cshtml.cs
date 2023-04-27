using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NewsAssignment.Pages.Jokes
{
    public class JokeModel : PageModel
    {
        public int NumOfJokes { get; set; } = 10; //limit
        public class Joke
        {
            public string joke { get; set; }
        }
        public class jokeList
        {
            public string count { get; set; }
            public List<Joke> jokes { get; set; }
        }

        private readonly ILogger<JokeModel> _logger;

        public JokeModel(ILogger<JokeModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Jokejoke { get; set; }
        public Joke[] jokes;

        public async Task<IActionResult> OnGet()
        {
            Uri mb = new Uri("https://api.api-ninjas.com/v1/dadjokes?limit=" + NumOfJokes);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", "+pstQyak7kkQIm1yJCuIlA==uGsyupWKQbsUGkUP");
            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                jokes = JsonConvert.DeserializeObject<Joke[]>(data);
                //jokes = JokeResults.jokes;
            }
            return Page();
        }
    }
}
