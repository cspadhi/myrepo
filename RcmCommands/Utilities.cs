//---------------------------------------------------------------
//  <copyright file="Utilities.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for utility methods.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace RcmCommands
{
    /// <summary>
    /// Class to define Utility methods.
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Serialize the T as xml using DataContract Serializer.
        /// </summary>
        /// <typeparam name="T">The type name.</typeparam>
        /// <param name="value">The T object.</param>
        /// <returns>The serialized object.</returns>
        public static string Serialize<T>(T value)
        {
            if (value == null)
            {
                return null;
            }

            string serializedValue;

            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamReader reader = new StreamReader(memoryStream))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memoryStream, value);
                memoryStream.Position = 0;
                serializedValue = reader.ReadToEnd();
            }

            return serializedValue;
        }

        /// <summary>
        /// Deserialize the xml as T.
        /// </summary>
        /// <typeparam name="T">The type name.</typeparam>
        /// <param name="xml">The xml as string.</param>
        /// <returns>The equivalent T.</returns>
        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(typeof(T));
                return (T)deserializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Method to write content to a file.
        /// </summary>
        /// <typeparam name="T">Class to be serialized.</typeparam>
        /// <param name="fileContent">Content to be written to the file.</param>
        /// <param name="filePath">The path where the file is to be created.</param>
        /// <param name="fileName">Name of the file to be created.</param>
        /// <returns>File path with file name as string.</returns>
        public static string WriteToFile<T>(T fileContent, string filePath, string fileName)
        {
            string fullFileName = Path.Combine(filePath, fileName);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fullFileName, false))
            {
                string contentToWrite = Serialize<T>(fileContent);
                file.WriteLine(contentToWrite);
            }

            return fullFileName;
        }

        /// <summary>
        /// Method to create object from XML.
        /// </summary>
        /// <typeparam name="T">Class to be deserialized.</typeparam>
        /// <param name="path">XML file path.</param>
        /// <returns>Object from XML.</returns>
        public static T GetObjectFromXML<T>(string path)
        {
            T obj;
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(new XmlTextReader(path));
            }
            else
            {
                throw new InvalidOperationException(string.Format("File not found: {0}", path));
            }

            return obj;
        }

        /// <summary>
        /// Method to create object from JSon.
        /// </summary>
        /// <typeparam name="T">Class to be deserialized.</typeparam>
        /// <param name="path">JSon file path.</param>
        /// <returns>Object from JSON.</returns>
        public static T GetObjectFromJson<T>(string path)
        {
            string filePath = Path.GetDirectoryName(path);

            string globalInputFile = filePath + "\\GlobalConfig.json";
            string localInputFile = filePath + "\\LocalConfig.json";
            string inputFile = path;

            Dictionary<string, object> globalConfigs = GenerateConfigParamsFromFile(globalInputFile);
            Dictionary<string, object> localConfigs = GenerateConfigParamsFromFile(localInputFile);

            Dictionary<string, object> configs = globalConfigs.Union(localConfigs).ToDictionary(k => k.Key, v => v.Value);

            try
            {
                string dataContract = File.ReadAllText(inputFile);

                foreach (var param in configs)
                {
                    dataContract = dataContract.Replace(
                        param.Key,
                        param.Value.ToString());
                }

                return JsonConvert.DeserializeObject<T>(dataContract);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Method to generate configuration parameters mapping from the configuration file.
        /// </summary>
        /// <param name="configFile">Configuration file in question.</param>
        /// <returns>Returns configuration parameters name value pair.</returns>
        public static Dictionary<string, object> GenerateConfigParamsFromFile(string configFile)
        {
            Dictionary<string, object> configs = new Dictionary<string, object>();
            Dictionary<string, string> configData = new Dictionary<string, string>();

            if (!File.Exists(configFile))
            {
                return configs;
            }

            string dataFromConfigFile = File.ReadAllText(configFile);
            configData = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                dataFromConfigFile);

            foreach (var data in configData)
            {
                string[] tokens = data.Value.Split(new char[] { ',' }, 2);

                Type type = Type.GetType(tokens[0]);
                string value = tokens[1];
                if (value == "<generate>")
                {
                    value = GenerateType(type);
                }

                object valueobj = TypeDescriptor.GetConverter(type).ConvertFrom(value);
                configs.Add(data.Key, valueobj);
            }

            return configs;
        }

        /// <summary>
        /// Method to generate new strings.
        /// </summary>
        /// <param name="typeObj">The type of object.</param>
        /// <returns>Returns the generated string.</returns>
        public static string GenerateType(Type typeObj)
        {
            if (typeObj == typeof(Guid))
            {
                return Guid.NewGuid().ToString();
            }

            // TODO : Currently Guid format only added. Other tyoes need to add.
            return string.Empty;
        }
    }
}
