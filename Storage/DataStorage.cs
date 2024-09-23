using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvyGarage.DataStorage
{
    public class DataStorage<T> where T : class
    {
        public string DataPath { get; private set; }

        public DataStorage(string dir, string fileName)
        {
            this.DataPath = Path.Combine(dir, fileName);
        }

		public void Save(T obj)
		{
			string value = JsonConvert.SerializeObject(obj, Formatting.Indented);
			using (StreamWriter streamWriter = new StreamWriter(this.DataPath, false))
			{
				streamWriter.Write(value);
			}
		}
		public T Read()
		{
			bool flag = File.Exists(this.DataPath);
			T result;
			if (flag)
			{
				string text;
				using (StreamReader streamReader = File.OpenText(this.DataPath))
				{
					text = streamReader.ReadToEnd();
				}
				result = JsonConvert.DeserializeObject<T>(text);
			}
			else
			{
				result = default(T);
			}
			return result;
		}
	}
}
