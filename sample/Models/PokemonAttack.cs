using System;
using System.Text;
using Newtonsoft.Json;

namespace Shared.Models
{
    public class PokemonAttack
    {
        [JsonProperty("fast")]
        public Attack[] Fast { get; set; }

        [JsonProperty("special")]
        public Attack[] Special { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (this.Fast != null && this.Fast.Length > 0)
            {
                sb.AppendLine("Fast attacks:");
                foreach (var attack in this.Fast)
                {
                    sb.AppendLine($"- {attack.ToString()}");
                }
            }
            if (this.Special != null && this.Special.Length > 0)
            {
                sb.AppendLine("Special attacks:");
                foreach (var attack in this.Special)
                {
                    sb.AppendLine($"- {attack.ToString()}");
                }
            }

            return sb.ToString();
        }
    }
}
