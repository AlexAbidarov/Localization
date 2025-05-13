using DevExpress.AIIntegration;
using Microsoft.Extensions.AI;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LocalizationStorage.AI {
    internal class AISettings {
        const string keyName = @"AI\azure.key";
        const string requestName = @"AI\request.txt";
        readonly string azureOpenAIEndpoint, azureOpenAIKey, request = null;
        public static readonly AISettings Default = new(Application.StartupPath);
        public AISettings(string appPath) {
            FileInfo fi = new(Path.Combine(appPath, keyName));
            if(fi.Exists) {
                using(StreamReader sr = fi.OpenText()) {
                    azureOpenAIEndpoint = sr.ReadLine();
                    azureOpenAIKey = sr.ReadLine();
                }
            }
            if(!KeysExist) {
                azureOpenAIEndpoint = AIHelper.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
                azureOpenAIKey = AIHelper.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");
            }
            fi = new FileInfo(Path.Combine(appPath, requestName));
            if(fi.Exists) {
                var sb = new StringBuilder();
                using(StreamReader sr = fi.OpenText()) {
                    string line = null;
                    while((line = sr.ReadLine()) != null)
                        sb.AppendLine(line);
                }
                request = $"{sb}";
            }
            AIContainerRegistration();
        }
        void AIContainerRegistration() {
            var defaultContainer = AIExtensionsContainerDesktop.Default;
            defaultContainer.Register<GermanTranslateExtension>(typeof(GermanTranslateRequest));

            IChatClient asChatClient = new Azure.AI.OpenAI.AzureOpenAIClient(
                new Uri(azureOpenAIEndpoint),
                new System.ClientModel.ApiKeyCredential(azureOpenAIKey)).AsChatClient("GPT4o");
            defaultContainer.RegisterChatClient(asChatClient);
        }
        readonly string keyError = $"<< {keyName} is not found >>";
        readonly string requestError = $"<< {requestName} is not found >>";
        public bool KeysExist => !(string.IsNullOrEmpty(azureOpenAIEndpoint) && string.IsNullOrEmpty(azureOpenAIKey));
        public bool RequestExists => !string.IsNullOrEmpty(request);
        public string AzureOpenAIEndpoint => string.IsNullOrEmpty(azureOpenAIEndpoint) ? keyError : azureOpenAIEndpoint;
        public string AzureOpenAIKey => string.IsNullOrEmpty(azureOpenAIKey) ? keyError : azureOpenAIKey;
        public string AIRequest => string.IsNullOrEmpty(request) ? requestError : request;
        public bool UseExistingGerman { get; set; } = true;
        public bool UseExistingRussian { get; set; } = true;
        public float Temperature { get; set; } = 0.05f;
    }
}
