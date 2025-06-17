using System.Text.Json;
using System.Net.WebSockets;

var tareas = await GetTareas();

foreach(var tar in tareas)
{
    Console.WriteLine($"Tarea id: {tar.id} Titulo: {tar.title} UserID: {tar.userId}" );
}

static async Task<List<Tarea>> GetTareas()
{
    var url = "https://jsonplaceholder.typicode.com/todos/";
    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        List<Tarea> coinDesk = JsonSerializer.Deserialize<List<Tarea>>(responseBody);
        return coinDesk;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        return null;
    }
}




