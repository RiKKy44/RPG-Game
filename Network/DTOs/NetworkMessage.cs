using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OODProject.Network.DTOs;

public class NetworkMessage
{
    [JsonPropertyName("type")]
    public MessageType Type { get; set; }


    //content of the message (JSON), can be GameStateDTO etc.
    [JsonPropertyName("payload")]
    public string Payload { get; set; }
}