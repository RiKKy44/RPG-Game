using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OODProject.Network.DTOs;

public class ClientCommandDTO
{
    [JsonPropertyName("playerId")]
    public int PlayerId { get; set; }

    //console key
    [JsonPropertyName("key")]
    public int KeyCode { get; set; }

    [JsonPropertyName("viewMode")]
    public int ViewMode { get; set; }

    [JsonPropertyName("inventoryPointer")]
    public int InventoryPointer { get; set; }
}