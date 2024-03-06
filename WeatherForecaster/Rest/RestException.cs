using System;
using System.Windows;
using Newtonsoft.Json;
using RestSharp;

namespace WeatherForecaster.Rest;

public class RawErrorResponse
{
    public string code;
    public string error;
}

public class RestClientErrorException : Exception
{
    private readonly RawErrorResponse _response;

    public RestClientErrorException(RestResponse rawResponse)
    {
        HttpCode = (int)rawResponse.StatusCode;

        try
        {
            if (string.IsNullOrEmpty(rawResponse.Content)) Console.WriteLine("Content for parsing is empty");

            if (!string.IsNullOrEmpty(rawResponse.Content) && rawResponse.Content.Contains("403 Forbidden"))
                _response = new RawErrorResponse { code = "forbidden", error = "VPN error" };

            if (_response == null) _response = JsonConvert.DeserializeObject<RawErrorResponse>(rawResponse.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Content for parsing: {rawResponse.Content} \nException: {ex.Message}");
        }

        if (_response == null) _response = new RawErrorResponse { code = HttpCode.ToString(), error = "null" };

        Error = _response.error;
    }

    public override string Message => HttpCode + " " + Description;

    public int HttpCode { get; }

    public string Error { get; private set; }

    public string Description
    {
        get
        {
            try
            {
                var description = Application.Current.Resources[_response.code].ToString();
                if (description != "-") return description;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ErrorDecoder.GetErrorDescription {ex.Message}");
            }

            return $"Error {HttpCode} - {_response.error}";
        }
    }
}