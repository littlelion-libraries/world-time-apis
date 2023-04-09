using Newtonsoft.Json;

namespace WorldTimeAPIs
{
    public struct WorldTimeAPIData
    {
        [JsonProperty("unixtime")] public int UnixTime;
    }
}