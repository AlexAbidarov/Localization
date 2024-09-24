using DevExpress.AIIntegration;
using DevExpress.AIIntegration.Extensions;
using DevExpress.AIIntegration.Services.Chat;
using DevExpress.XtraEditors;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalizationStorage.AI {
    internal class AIHelper {
        internal static string GetEnvironmentVariable(string name) {
            string value = Environment.GetEnvironmentVariable(name) ?? string.Empty;
            if(string.IsNullOrEmpty(value)) {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(@$"Please specify {name} KEY.");
                string readLine = Console.ReadLine();
                if(readLine != null && !string.IsNullOrEmpty(readLine)) {
                    value = readLine;
                    Environment.SetEnvironmentVariable(name, value);
                }
            }
            return value;
        }
        public static string GetTranslation(string suggest, string translation) { 
            if(string.IsNullOrEmpty(suggest)) return translation;
            if(LocalizationHelper.ValueExist(translation) && suggest != translation) {
                if(XtraMessageBox.Show($"AI suggests replacing\r\n{translation}\r\nwith\r\n{suggest}\r\nDo you agree?",
                    "AI translation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    return suggest;
            }
            return translation;
        }
    }
    public static class ExtensionsClass {
        public static Task<TextResponse> GermanTranslateAsync(this IAIExtensionsContainer container, GermanTranslateRequest request, CancellationToken cancellationToken = default(CancellationToken)) {
            IAIExtension<GermanTranslateRequest, TextResponse> iAIExtension = (container.GetExtension(typeof(GermanTranslateRequest)) as IAIExtension<GermanTranslateRequest, TextResponse>);
            return iAIExtension.ExecuteAsync(request, cancellationToken);
        }
    }
    public class GermanTranslateRequest : TextRequest {
        public const string FormatString = "English - {0}, Russian - {1}, German - {2}";
        public GermanTranslateRequest(string English, string Russian, string German) :
            base(GetRequestCondition(English, Russian, German)) {
        }
        public GermanTranslateRequest(string condition) :
            base(condition) {
        }
        public static string GetRequestCondition(string English, string Russian, string German) {
            return string.Format(FormatString, English, Russian, German);
        }
    }
    public class GermanTranslateExtension : ChangeTextExtension<GermanTranslateRequest> {
        public GermanTranslateExtension(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        protected override void ConfigureDefaultRequestParameters(ChatMessageRequest chatRequest, GermanTranslateRequest request) {
            base.ConfigureDefaultRequestParameters(chatRequest, request);
            chatRequest.Temperature = 0.1f;
        }
        protected override string GetSystemPrompt(GermanTranslateRequest request) {
            return AISettings.Default.AIRequest;
        }
    }
}
