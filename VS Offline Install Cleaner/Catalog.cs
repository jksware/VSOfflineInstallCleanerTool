using Newtonsoft.Json;
using System.Collections.Generic;

namespace VsOfflineInstallCleaner
{
    public class Catalog
    {
        [JsonProperty("packages")]
        public IList<Package> Packages { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// Microsoft.VisualStudio.Debugger.Concord.Remote.Resources
    ///     ,version=17.0.31512.284
    ///     ,chip=x64
    ///     ,language=zh-CN
    ///     ,productarch=neutral
    ///     ,machinearch=ARM64
    /// 
    /// Microsoft.VisualStudio.Branding.Enterprise
    ///     ,version=17.0.31512.422
    ///     ,language=zh-CN
    ///     ,branch=preview
    ///     ,productarch=x64
    /// </example>
    public partial class Package
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("chip")]
        public string Chip { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("productarch")]
        public string ProductArch { get; set; }

        [JsonProperty("machinearch")]
        public string MachineArch { get; set; }
    }
}
