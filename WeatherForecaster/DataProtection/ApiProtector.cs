using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace WeatherForecaster.DataProtection
{
    

    class ApiProtector
    {
        class DataContainer
        {
            public string ApiKey = string.Empty;
        }

        private const string _filename = "data.bin"; 
        private const IsolatedStorageScope _scope = IsolatedStorageScope.Application | IsolatedStorageScope.User;

        private DataContainer _data;

        public string GetApi()
        {
            return _data.ApiKey;
        }

        public void SetApi(string api)
        {
            _data.ApiKey = api;
        }

        public ApiProtector()
        {
            _data = new DataContainer();

            RetrieveData();
        }

        public void RetrieveData()
        {
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(_scope, null))
                {
                    using (var stream = isoStore.OpenFile(_filename, System.IO.FileMode.Open))
                    {
                        byte[] content = new byte[stream.Length];
                        stream.Read(content);
                        var unserialised = DataEncryptor.Decrypt(content);
                        _data = JsonConvert.DeserializeObject<DataContainer>(unserialised);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Debug.WriteLine($"Error DataProtection.Unprotect: Data was not decrypted. An error occurred. {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        public void SaveData()
        {
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(_scope, null))
                {
                    using (var stream = isoStore.OpenFile(_filename, System.IO.FileMode.Create))
                    {
                        var serialised = JsonConvert.SerializeObject(_data);
                        var content = DataEncryptor.Encrypt(serialised);
                        stream.Write(content);
                    }
                }
            }
            catch (CryptographicException ex)
            {
                Debug.WriteLine($"Error DataProtection.Protect: Data was not decrypted. An error occurred. {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
