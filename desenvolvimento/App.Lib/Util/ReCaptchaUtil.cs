using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.Util
{

    public class ReCaptchaUtil
    {
        public static bool Validate(string PrivateKey, string ReCaptchaResponse)
        {
            var client = new System.Net.WebClient();
            string downloadedString = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, ReCaptchaResponse));
            ReCaptchaUtil captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaUtil>(downloadedString);
            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string SuccessStr
        {
            get { return m_SuccessStr; }
            set { m_SuccessStr = value; }
        }
        private string m_SuccessStr;

        public bool Success
        {
            get
            {
                return Boolean.TrueString == SuccessStr;
            }
            set
            {
                SuccessStr = (value) ? "true" : "false";
            }
        }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }
        private List<string> m_ErrorCodes;
    }
}
