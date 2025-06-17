using System.Text.Json;
using System.Net.WebSockets;

var tareas = await GetTareas();

foreach(var tar in tareas)
{
    if(tar.completed)
        Console.WriteLine($"Titulo: {tar.title} Estado: {tar.completed}" );
}

Console.WriteLine("------------------------------------------------------");

foreach(var tar in tareas)
{
    if(!tar.completed)
        Console.WriteLine($"Titulo: {tar.title} Estado: {tar.completed}" );
}

string textoGuardar = JsonSerializer.Serialize(tareas);
GuardarArchivoTexto("tareas.json" , textoGuardar);

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
void GuardarArchivoTexto(string nombreArchivo, string datos)
    {
        using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
        {
            using (var strWriter = new StreamWriter(archivo))
            {
                strWriter.WriteLine("{0}", datos);
                strWriter.Close();
            }
        }
    }

/* //Guardo el archivo
Console.WriteLine("--Serializando--");
string alumnosJson = JsonSerializer.Serialize(listadoAlumnos);
Console.WriteLine("Archivo Serializado : " + alumnosJson);
Console.WriteLine("--Guardando--");
miHelperdeArchivos.GuardarArchivoTexto(NombreArchivo, alumnosJson) */




