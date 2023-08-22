using System.Text;
using Newtonsoft.Json;

namespace Misaka.Hitokoto.VPet.Models
{
    public class HitokotoItem
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("uuid")] public string Uuid { get; set; }

        [JsonProperty("hitokoto")] public string Hitokoto { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("from")] public string From { get; set; }

        [JsonProperty("from_who")] public string? FromWho { get; set; }

        [JsonProperty("creator")] public string Creator { get; set; }

        [JsonProperty("creator_uid")] public long CreatorUid { get; set; }

        [JsonProperty("reviewer")] public long Reviewer { get; set; }

        [JsonProperty("commit_from")] public string CommitFrom { get; set; }

        [JsonProperty("created_at")] public string CreatedAt { get; set; }

        [JsonProperty("length")] public long Length { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder($"『{Hitokoto}』");
            stringBuilder.Append("——");

            if (FromWho != null) stringBuilder.AppendFormat("{0}「{1}」", FromWho, From);
            else
                stringBuilder.AppendFormat("「{0}」", From);

            return stringBuilder.ToString();
        }
    }
}