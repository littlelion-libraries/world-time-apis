using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorldTimeAPIs
{
    public class DateTimeFactory : IDateTimeFactory
    {
        private const string Uri = "https://worldtimeapi.org/api/timezone/";
        private Func<string, IEnumerator<(int, string)>> _getRequestAsync;
        private Action<string> _log;

        public Func<string, IEnumerator<(int, string)>> GetRequestAsync
        {
            set => _getRequestAsync = value;
        }
        
        public Action<string> Log
        {
            set => _log = value;
        }

        public static DateTime CreateDateTime(string value)
        {
            var unixTime = JsonConvert.DeserializeObject<WorldTimeAPIData>(value).UnixTime;
            return DateTime.UnixEpoch + TimeSpan.FromSeconds(unixTime);
        }

        public IEnumerator<DateTime?> CreateDateTimeAsync()
        {
            var routine = _getRequestAsync(CreateUri(TimeZones.Utc));
            while (routine.MoveNext())
            {
                yield return null;
            }

            var (code, message) = routine.Current;
            switch (code)
            {
                case 200:
                    yield return CreateDateTime(message);
                    break;
                default:
                    _log($"code: {code}, message: {message}");
                    yield return null;
                    break;
            }
        }

        public static string CreateUri(string timeZones)
        {
            return Uri + timeZones;
        }
    }
}