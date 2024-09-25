using Azure;
using Azure.AI.OpenAI;
using DevExpress.AIIntegration;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LocalizationStorage.AI {
    internal class AISettings {
        const string keyName = @"AI\azure.key";
        const string requestName = @"AI\request.txt";
        string azureOpenAIEndpoint, azureOpenAIKey, request = null;
        public static readonly AISettings Default = new AISettings(Application.StartupPath);
        public AISettings(string appPath) {
            FileInfo fi = new FileInfo(Path.Combine(appPath, keyName));
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
                StringBuilder sb = new StringBuilder();
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
            defaultContainer.RegisterChatClientOpenAIService(
                new AzureOpenAIClient(new Uri(azureOpenAIEndpoint),
                new AzureKeyCredential(azureOpenAIKey)), "GPT4o");
        }
        string keyError = $"<< {keyName} is not found >>";
        string requestError = $"<< {requestName} is not found >>";
        public bool KeysExist => !(string.IsNullOrEmpty(azureOpenAIEndpoint) && string.IsNullOrEmpty(azureOpenAIKey));
        public bool RequestExists => !string.IsNullOrEmpty(request);
        public string AzureOpenAIEndpoint => string.IsNullOrEmpty(azureOpenAIEndpoint) ? keyError : azureOpenAIEndpoint;
        public string AzureOpenAIKey => string.IsNullOrEmpty(azureOpenAIKey) ? keyError : azureOpenAIKey;
        public string AIRequest => string.IsNullOrEmpty(request) ? requestError : request;
        public bool UseExistingGerman { get; set; } = true;
        public bool UseExistingRussian { get; set; } = true;
        public float Temperature { get; set; } = 0.45f;
    }
}
