using System.Net.Sockets;
using System.Net;

namespace Live_ConsultingKSP
{
    public class Utils
    {

        public DateTime CurrentIndianTime()
        {
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local

            return localTime;
            //u.MsgBox("Server Time : " + serverTime.ToString() + " utcTime : " + utcTime + " tzi : " + tzi + " localTime : " + localTime);
        }
        public string GetResponse(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream receiveStream = response.GetResponseStream();
                System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, System.Text.Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                return sResponse;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string GetUUID()
        {
            string uuid = Guid.NewGuid().ToString("N");
            string numb = uuid.Substring(0, 8) + "-" + uuid.Substring(8, 4)
                + "-" + uuid.Substring(12, 4) + "-" + uuid.Substring(16, 4)
                + "-" + uuid.Substring(20, 8);
            return numb;
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var localIp = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            return localIp?.ToString() ?? "No Network Adapter Found";
        }

        public decimal GetRecordNo()
        {
            DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
            decimal Dat = Convert.ToDecimal(localTime.ToString("dd/MM/yyyy HH:mm:ss").Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", ""));
            return Dat;
        }

    }
}
