using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OODProject.Network.DTOs;


public class GameStateDTO
{
    [JsonPropertyName("map")]
    public string[]? MapGrid { get; set; }

    [JsonPropertyName("players")]
    public List<PlayerDTO> Players { get; set; } = new();

    [JsonPropertyName("enemies")]
    public List<EnemyDTO> Enemies { get; set; } = new();

    [JsonPropertyName("messages")]
    public List<string> RecentMessages { get; set; } = new();

    [JsonPropertyName("logs")] 
    public List<string> Logs { get; set; } = new List<string>();

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("actions")]
    public List<string> ActionDescriptions { get; set; } = new List<string>();
    [JsonPropertyName("mapItems")]
    public List<string> MapItems { get; set; } = new List<string>();

    [JsonPropertyName("currentEnemy")]
    public EnemyDTO? CurrentEnemy { get; set; }

    [JsonPropertyName("currentView")]
    public int CurrentView { get; set; }
}
