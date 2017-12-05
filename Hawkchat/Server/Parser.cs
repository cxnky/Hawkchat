using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;

namespace Hawkchat.Server
{
    public class Parser
    {

        public static void ParseCommand(string command, Message message)
        {

            dynamic jsonResponse = new JObject();

            switch (command)
            {

                case "AUTH":
#if DEBUG
                    jsonResponse.authenticated = true;
#else
                    jsonResponse.authenticated = false;
#endif
                    message.Reply(jsonResponse.ToString(Formatting.None));
                    break;

            }

        }

    }
}
