using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OODProject.Network.DTOs;
public class EnemyDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("symbol")]
    public char Symbol { get; set; }
}