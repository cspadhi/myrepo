using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processing Metrics..");
            //ProcessMetrics();
            Console.WriteLine("Processing Metrics Completed.");

            Console.WriteLine("Processing Monitors..");
            ProcessMonitors();
            Console.WriteLine("Processing Monitors Completed.");
        }

        static void ProcessMetrics()
        {
            var csv = new StringBuilder();
            csv.AppendLine("\"Metric Name\",\"Dimensions\"");

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(@"F:\MDM\Metrics", "*.json");
            foreach (string file in fileEntries)
            {
                //string json = File.ReadAllText(@"F:\MDM\Metrics\RCMMDM_RcmMetrics_ArmOperationFailed.json");
                string json = File.ReadAllText(file);
                MetricData d = RcmDataContractUtils<MetricData>.JsonDeserialize(json);

                var newLine = string.Format(
                    "\"{0}\",\"{1}\"",
                    d.id,
                    string.Join(", ", d.dimensions.Select(x => x.id)));
                csv.AppendLine(newLine);
            }

            File.WriteAllText(@"F:\MDM\Metrics\metrics_compiled.csv", csv.ToString());
        }

        static void ProcessMonitors()
        {
            var csv = new StringBuilder();
            csv.AppendLine("\"Category\",\"Monitor Name\",\"Metric Name\",\"Resource Type\",\"Description\",\"Severity\",\"Frequency\",\"Lookback Duration\",\"Bucket Size In Minutes\",\"Failing Buckets\",\"Buckets Analyzed\",\"Auto Mitigate Incident\",\"Healthy Duration To Mitigate Incident\",\"Healthy Count To Mitigate Incident\",\"Custom Title\",\"Silent\",\"Disabled\",\"Rule\"");

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(@"F:\MDM\Monitors", "*.json");
            foreach (string file in fileEntries)
            {
                //string json = File.ReadAllText(@"F:\MDM\Monitors\RCMMDM_RcmMetrics_RcmManagementPlaneApiActionFailed.json");
                string json = File.ReadAllText(file);
                MonitorData d = RcmDataContractUtils<MonitorData>.JsonDeserialize(json);

                foreach (var m in d.monitors)
                {
                    foreach (var t in m.thresholds)
                    {
                        if(string.Equals(m.shouldMitigateIncident, "False", StringComparison.OrdinalIgnoreCase))
                        {
                            m.healthyDurationToMitigateIncident = string.Empty;
                            m.healthyCountToMitigateIncident = string.Empty;
                        }
                        else
                        {
                            m.healthyDurationToMitigateIncident = TimeSpan.Parse(m.healthyDurationToMitigateIncident).TotalMinutes.ToString();
                        }

                        var correlatedFailingBucketTemplate = m.templateConfiguration?.correlatedFailingBucketTemplate;
                        var alertRules = correlatedFailingBucketTemplate?.alertRules;

                        if (alertRules != null && alertRules.Any())
                        {
                            foreach (var r in alertRules)
                            {
                                var newLine = string.Format(
                                    "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"",
                                    m.category,
                                    m.id,
                                    d.id,
                                    m.resourceType,
                                    m.description,
                                    r.severity,
                                    TimeSpan.Parse(m.frequency).TotalMinutes.ToString(),
                                    TimeSpan.Parse(m.lookbackDuration).TotalMinutes.ToString(),
                                    correlatedFailingBucketTemplate.bucketSizeInMinutes,
                                    r.failingBuckets,
                                    r.bucketsAnalyzed,
                                    m.shouldMitigateIncident,
                                    m.healthyDurationToMitigateIncident,
                                    m.healthyCountToMitigateIncident,
                                    m.customTitle,
                                    string.Equals(m.isSilent, "False", StringComparison.OrdinalIgnoreCase) ? "Active" : "Silent",
                                    string.Equals(m.isDisabled, "False", StringComparison.OrdinalIgnoreCase) ? "Active" : "Disabled",
                                    "alertRules");
                                csv.AppendLine(newLine);
                            }
                        }
                        else
                        {
                            var newLine = string.Format(
                                "\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"",
                                m.category,
                                m.id,
                                d.id,
                                m.resourceType,
                                m.description,
                                t.severity,
                                TimeSpan.Parse(m.frequency).TotalMinutes.ToString(),
                                TimeSpan.Parse(m.lookbackDuration).TotalMinutes.ToString(),
                                "",
                                "",
                                "",
                                m.shouldMitigateIncident,
                                m.healthyDurationToMitigateIncident,
                                m.healthyCountToMitigateIncident,
                                m.customTitle,
                                string.Equals(m.isSilent, "False", StringComparison.OrdinalIgnoreCase) ? "Active" : "Silent",
                                string.Equals(m.isDisabled, "False", StringComparison.OrdinalIgnoreCase) ? "Active" : "Disabled",
                                "threshold");
                            csv.AppendLine(newLine);
                        }


                    }
                }
            }

            File.WriteAllText(@"F:\MDM\Monitors\monitors_compiled.csv", csv.ToString());
        }

        static void ParseAlertsCSV()
        {
            var path = @"F:\MDM\RcmAlerts.csv"; // Habeeb, "Dubai Media City, Dubai"
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Name = fields[0];
                    string Address = fields[1];
                }
            }
        }
    }

    class MetricData
    {
        public MetricData()
        {
            this.dimensions = new List<Dimension>();
        }
        public string id { get; set; }
        public List<Dimension> dimensions { get; set; }
    }

    class Dimension
    {
        public string id { get; set; }
    }

    class MonitorData
    {
        public MonitorData()
        {
            this.monitors = new List<Monitor>();
        }
        public string tenant { get; set; }
        public string component { get; set; }
        public string id { get; set; }
        public List<Monitor> monitors { get; set; }
    }

    class Monitor
    {
        public Monitor()
        {
            this.thresholds = new List<Thresholds>();
        }

        public string id { get; set; }
        public string description { get; set; }
        public string isSilent { get; set; }
        public string isDisabled { get; set; }
        public string category { get; set; }

        public string resourceType { get; set; }
        public string frequency { get; set; }
        public string lookbackDuration { get; set; }
        public string customTitle { get; set; }

        public string shouldMitigateIncident { get; set; }
        public string healthyDurationToMitigateIncident { get; set; }
        public string healthyCountToMitigateIncident { get; set; }

        public List<Thresholds> thresholds { get; set; }
        public TemplateConfiguration templateConfiguration { get; set; }

    }

    class Thresholds
    {
        public string comparator { get; set; }
        public string value { get; set; }
        public string severity { get; set; }
    }

    class TemplateConfiguration
    {
        public CorrelatedFailingBucketTemplate correlatedFailingBucketTemplate { get; set; }
    }

    class CorrelatedFailingBucketTemplate
    {
        public CorrelatedFailingBucketTemplate()
        {
            this.alertRules = new List<AlertRules>();
        }

        public string bucketSizeInMinutes { get; set; }
        public List<AlertRules> alertRules { get; set; }
    }

    class AlertRules
    {
        public string severity { get; set; }
        public string failingBuckets { get; set; }
        public string bucketsAnalyzed { get; set; }
    }

    /*
    class BlogSites
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    */

    /// <summary>
    /// Data contract utility methods.
    /// </summary>
    /// <typeparam name="T">The type being worked with.</typeparam>
    public static class RcmDataContractUtils<T>
    {
        /// <summary>
        /// Deserialize the JSON string to the expected object type using
        /// <c>Newtonsoft JsonConvert</c>.
        /// </summary>
        /// <param name="serializedString">Serialized string.</param>
        /// <returns>Deserialized object.</returns>
        public static T JsonDeserialize(string serializedString)
        {
            return JsonConvert.DeserializeObject<T>(
                serializedString,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        /// <summary>
        /// Serializes the propertyBagContainer to the string with JSON format.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized string.</returns>
        public static string JsonSerialize(T obj)
        {
            return JsonConvert.SerializeObject(
                obj,
                Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }
    }
}
