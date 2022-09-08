using System.Text.Json;

public class taskResponse
{
    public string action { get; private set; }
    public string result { get; private set; }
    public string response { get; private set; }

    public taskResponse(string action, string result, string response)
    {
        this.action = action;
        this.result = result;
        this.response = response;
    }

    public string jsonResponse()
    {
        return JsonSerializer.Serialize<taskResponse>(this);
    }
}