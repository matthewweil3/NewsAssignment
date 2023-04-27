using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace NewsAssignment.Pages.Jokes
{
    public class JokeModel : PageModel
    {
        public int NumOfJokes { get; set; } //limit
        public class joke
        {
            public string id { get; set; }
            public string j { get; set; }
        }
        public class jokeList
        {
            public string count { get; set; }
            public List<joke> jokes { get; set; }
        }

        private readonly ILogger<JokeModel> _logger;

        public JokeModel(ILogger<JokeModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Jokejoke { get; set; }

        public async Task<IActionResult> OnPost()
        {
            Uri mb = new Uri("https://api.api-ninjas.com/v1/dadjokes?limit=" + NumOfJokes);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("+pstQyak7kkQIm1yJCuIlA==uGsyupWKQbsUGkUP", "");
            HttpResponseMessage response = await client.GetAsync(mb.ToString());

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var JokelResults = JsonConvert.DeserializeObject<jokeList>(data);
                js = labelResults.js;
            }
            return Page();
        }
    }
}
