using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherForecaster.Rest
{
    namespace JsonClasses
    {
        public class Coord
        {
            [JsonProperty(PropertyName = "lat")] public double latitude;

            [JsonProperty(PropertyName = "lon")] public double longitude;
        }

        public class LocalNames
        {
            [JsonProperty(PropertyName = "en")] public string en { get; set; }
        }

        public class Location
        {
            [JsonProperty(PropertyName = "lat")] public double latitude;

            [JsonProperty(PropertyName = "lon")] public double longitude;

            [JsonProperty(PropertyName = "name")] public string Name { get; set; }

            [JsonIgnore] public LocalNames LocalNames { get; set; }

            [JsonProperty(PropertyName = "country")]
            public string Country { get; set; }

            [JsonProperty(PropertyName = "state")] public string State { get; set; }
        }


        public class InnerWeather
        {
            [JsonProperty(PropertyName = "description")]
            public string description;

            [JsonProperty(PropertyName = "icon")] public string icon;

            [JsonProperty(PropertyName = "id")] public int id;

            [JsonProperty(PropertyName = "main")] public string main;
        }

        public class Temp
        {
            [JsonProperty(PropertyName = "feels_like")]
            public double feels_like;

            [JsonProperty(PropertyName = "grnd_level")]
            public int grnd_level;

            [JsonProperty(PropertyName = "humidity")]
            public int humidity;

            [JsonProperty(PropertyName = "pressure")]
            public int pressure;

            [JsonProperty(PropertyName = "sea_level")]
            public int sea_level;

            [JsonProperty(PropertyName = "temp")] public double temp;

            [JsonProperty(PropertyName = "temp_max")]
            public double temp_max;

            [JsonProperty(PropertyName = "temp_min")]
            public double temp_min;
        }

        public class ForecastTemp
        {
            [JsonProperty(PropertyName = "temp")] public double Temp { get; set; }

            [JsonProperty(PropertyName = "feels_like")]
            public double FeelsLike { get; set; }

            [JsonProperty(PropertyName = "temp_min")]
            public double TempMin { get; set; }

            [JsonProperty(PropertyName = "temp_max")]
            public double TempMax { get; set; }

            [JsonProperty(PropertyName = "pressure")]
            public int Pressure { get; set; }

            [JsonProperty(PropertyName = "sea_level")]
            public int SeaLevel { get; set; }

            [JsonProperty(PropertyName = "grnd_level")]
            public int GroundLevel { get; set; }

            [JsonProperty(PropertyName = "humidity")]
            public int Humidity { get; set; }

            [JsonProperty(PropertyName = "temp_kf")]
            public double TempKf { get; set; }
        }


        public class Wind
        {
            [JsonProperty(PropertyName = "deg")] public int deg;

            [JsonProperty(PropertyName = "gust")] public double gust;

            [JsonProperty(PropertyName = "speed")] public double speed;
        }

        public class Clouds
        {
            [JsonProperty(PropertyName = "all")] public int all;
        }

        public class SunriseAndSunset
        {
            [JsonProperty(PropertyName = "country")]
            public string country;

            [JsonProperty(PropertyName = "sunrise")]
            public long sunrise;

            [JsonProperty(PropertyName = "sunset")]
            public long sunset;
        }

        public class CurrentWeather
        {
            [JsonProperty(PropertyName = "base")] public string BaseInfo;

            [JsonProperty(PropertyName = "clouds")]
            public Clouds clouds;

            [JsonProperty(PropertyName = "cod")] public int cod;

            [JsonProperty(PropertyName = "coord")] public Coord coord;

            [JsonProperty(PropertyName = "dt")] public long dt;

            [JsonProperty(PropertyName = "id")] public int id;

            [JsonProperty(PropertyName = "main")] public Temp main;

            [JsonProperty(PropertyName = "name")] public string name;

            [JsonProperty(PropertyName = "sys")] public SunriseAndSunset SaS;

            [JsonProperty(PropertyName = "timezone")]
            public int timezone;

            [JsonProperty(PropertyName = "visibility")]
            public int visibility;

            [JsonProperty(PropertyName = "weather")]
            public List<InnerWeather> weather;

            [JsonProperty(PropertyName = "wind")] public Wind wind;
        }

        public class Pollution
        {
            [JsonProperty(PropertyName = "aqi")] public int aqi;

            [JsonProperty(PropertyName = "components")]
            public Components components;

            [JsonProperty(PropertyName = "dt")] public long dt;
        }

        public class Components
        {
            [JsonProperty(PropertyName = "co")] public double co;

            [JsonProperty(PropertyName = "nh3")] public double nh3;

            [JsonProperty(PropertyName = "no")] public double no;

            [JsonProperty(PropertyName = "no2")] public double no2;

            [JsonProperty(PropertyName = "o3")] public double o3;

            [JsonProperty(PropertyName = "pm10")] public double pm10;

            [JsonProperty(PropertyName = "pm2_5")] public double pm2_5;

            [JsonProperty(PropertyName = "so2")] public double so2;
        }

        public class ForecastWeatherInner
        {
            [JsonProperty(PropertyName = "temp")] public double Temp { get; set; }

            [JsonProperty(PropertyName = "feels_like")]
            public double FeelsLike { get; set; }

            [JsonProperty(PropertyName = "temp_min")]
            public double TempMin { get; set; }

            [JsonProperty(PropertyName = "temp_max")]
            public double TempMax { get; set; }

            [JsonProperty(PropertyName = "pressure")]
            public int Pressure { get; set; }

            [JsonProperty(PropertyName = "sea_level")]
            public int SeaLevel { get; set; }

            [JsonProperty(PropertyName = "grnd_level")]
            public int GroundLevel { get; set; }

            [JsonProperty(PropertyName = "humidity")]
            public int Humidity { get; set; }

            [JsonProperty(PropertyName = "temp_kf")]
            public double TempKf { get; set; }
        }


        public class ForecastWeather
        {
            [JsonProperty(PropertyName = "dt")] public long Dt { get; set; }

            [JsonProperty(PropertyName = "main")] public ForecastWeatherInner Main { get; set; }

            [JsonProperty(PropertyName = "weather")]
            public List<InnerWeather> Weather { get; set; }

            [JsonProperty(PropertyName = "clouds")]
            public Clouds Clouds { get; set; }

            [JsonProperty(PropertyName = "wind")] public Wind Wind { get; set; }

            [JsonProperty(PropertyName = "visibility")]
            public int Visibility { get; set; }

            [JsonProperty(PropertyName = "pop")] public int Pop { get; set; }

            [JsonProperty(PropertyName = "sys")] public Pod pod { get; set; }

            [JsonProperty(PropertyName = "dt_txt")]
            public string DtTxt { get; set; }
        }

        public class Pod
        {
            [JsonProperty(PropertyName = "pod")] public string pod { get; set; }
        }


        public class WeatherForecast
        {
            [JsonProperty(PropertyName = "cod")] public string Cod { get; set; }

            [JsonProperty(PropertyName = "message")]
            public int Message { get; set; }

            [JsonProperty(PropertyName = "cnt")] public int Count { get; set; }

            [JsonProperty(PropertyName = "list")] public List<ForecastWeather> List { get; set; }
        }

        public class AirPollution
        {
            [JsonProperty(PropertyName = "coord")] public Coord coord;

            [JsonProperty(PropertyName = "list")] public List<Pollution> mainList;
        }
    }
}