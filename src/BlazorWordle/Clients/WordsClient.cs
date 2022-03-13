namespace BlazorWordle.Clients;

public sealed class WordsClient
{
    private IReadOnlyList<string>? _words;
    private readonly HttpClient _client;

    public WordsClient(HttpClient client)
    {
        _client = client;
    }

    private async Task<IReadOnlyList<string>> GetWordsAsync()
    {
        if (_words is null)
        {
            var response = await _client.GetStringAsync("assets/words.txt");
            _words = response.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        return _words;
    }

    public async Task<string> GetRandomWordAsync()
    {
        var words = await GetWordsAsync();

        var word = words[Random.Shared.Next(0, words.Count)];

        if (string.IsNullOrEmpty(word))
        {
            throw new Exception("Word not found.");
        }

        return word;
    }
}