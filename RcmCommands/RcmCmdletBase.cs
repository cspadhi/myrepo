//---------------------------------------------------------------
//  <copyright file="RcmCmdletBase.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines base class for all RCM cmdlets.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Text;
using RcmClientLib;

namespace RcmCommands
{
    /// <summary>
    /// Represents base class for all RCM commands.
    /// </summary>
    public abstract class RcmCmdletBase : PSCmdlet
    {
        /// <summary>
        /// File suffix format.
        /// </summary>
        private const string FileTimeStampSuffixFormat = "yyyy-MM-dd-THH-mm-ss-fff";

        /// <summary>
        /// Error record folder path.
        /// </summary>
        private static string errorRecordFolderPath = null;

        /// <summary>
        /// Profile directory.
        /// </summary>
        private static string profileDirectory = string.Empty;

        /// <summary>
        /// RCM client for agent.
        /// </summary>
        private RcmCreds rcmClientForAgent;

        /// <summary>
        /// RCM client for inter service.
        /// </summary>
        private RcmCreds rcmClientForInterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RcmCmdletBase"/> class.
        /// </summary>
        public RcmCmdletBase()
        {
            this.DebugMessages = new ConcurrentQueue<string>();
        }

        /// <summary>
        /// Gets debug messages.
        /// </summary>
        public ConcurrentQueue<string> DebugMessages { get; private set; }

        /// <summary>
        /// Gets RCM client for agent.
        /// </summary>
        internal RcmCreds RcmClientForAgent
        {
            get
            {
                if (this.rcmClientForAgent == null)
                {
                    this.rcmClientForAgent = PSRcmClient.GetRcmClientForAgent();
                }

                return this.rcmClientForAgent;
            }
        }

        /// <summary>
        /// Gets RCM client for inter service.
        /// </summary>
        internal RcmCreds RcmClientForInterService
        {
            get
            {
                if (this.rcmClientForInterService == null)
                {
                    this.rcmClientForInterService = PSRcmClient.GetRcmClientForInterService();
                }

                return this.rcmClientForInterService;
            }
        }

        /// <summary>
        /// Perform execute command action.
        /// </summary>
        public virtual void ExecuteRcmCmdlet()
        {
            // Do nothing.
        }

        /// <summary>
        /// Command begin process. Write to logs, setup Http Tracing and initialize profile.
        /// </summary>
        protected override void BeginProcessing()
        {
            this.LogCmdletStartInvocationInfo();
            base.BeginProcessing();
        }

        /// <summary>
        /// Process record.
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();
                this.ExecuteRcmCmdlet();
            }
            catch (Exception ex)
            {
                this.WriteExceptionError(ex);
            }
        }

        /// <summary>
        /// Perform end of pipeline processing.
        /// </summary>
        protected override void EndProcessing()
        {
            this.LogCmdletEndInvocationInfo();
            base.EndProcessing();
        }

        /// <summary>
        /// Log command invocation start.
        /// </summary>
        protected virtual void LogCmdletStartInvocationInfo()
        {
            if (string.IsNullOrEmpty(this.ParameterSetName))
            {
                this.WriteDebugWithTimestamp(string.Format(
                    "{0} begin processing without ParameterSet.",
                    this.GetType().Name));
            }
            else
            {
                this.WriteDebugWithTimestamp(string.Format(
                    "{0} begin processing with ParameterSet '{1}'.",
                    this.GetType().Name,
                    this.ParameterSetName));
            }
        }

        /// <summary>
        /// Log command invocation end.
        /// </summary>
        protected virtual void LogCmdletEndInvocationInfo()
        {
            string message = string.Format("{0} end processing.", this.GetType().Name);
            this.WriteDebugWithTimestamp(message);
        }

        /// <summary>
        /// Write error message.
        /// </summary>
        /// <param name="errorRecord">Error record object.</param>
        protected new void WriteError(ErrorRecord errorRecord)
        {
            this.RecordDebugMessages();
            base.WriteError(errorRecord);
        }

        /// <summary>
        /// Throw terminating error.
        /// </summary>
        /// <param name="errorRecord">Error record object.</param>
        protected new void ThrowTerminatingError(ErrorRecord errorRecord)
        {
            this.RecordDebugMessages();
            base.ThrowTerminatingError(errorRecord);
        }

        /// <summary>
        /// Method for write object.
        /// </summary>
        /// <param name="sendToPipeline">sendToPipeline object.</param>
        protected new void WriteObject(object sendToPipeline)
        {
            this.RecordDebugMessages();
            base.WriteObject(sendToPipeline);
        }

        /// <summary>
        /// Method for write object.
        /// </summary>
        /// <param name="sendToPipeline">sendToPipeline object.</param>
        /// <param name="enumerateCollection">enumerateCollection flag.</param>
        protected new void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            this.RecordDebugMessages();
            base.WriteObject(sendToPipeline, enumerateCollection);
        }

        /// <summary>
        /// Method for write verbose message.
        /// </summary>
        /// <param name="text">Text message.</param>
        protected new void WriteVerbose(string text)
        {
            this.RecordDebugMessages();
            base.WriteVerbose(text);
        }

        /// <summary>
        /// Method for write warning message.
        /// </summary>
        /// <param name="text">Text message.</param>
        protected new void WriteWarning(string text)
        {
            this.RecordDebugMessages();
            base.WriteWarning(text);
        }

        /// <summary>
        /// Method for write command details.
        /// </summary>
        /// <param name="text">Text message.</param>
        protected new void WriteCommandDetail(string text)
        {
            this.RecordDebugMessages();
            base.WriteCommandDetail(text);
        }

        /// <summary>
        /// Method for write progress.
        /// </summary>
        /// <param name="progressRecord">progressRecord object.</param>
        protected new void WriteProgress(ProgressRecord progressRecord)
        {
            this.RecordDebugMessages();
            base.WriteProgress(progressRecord);
        }

        /// <summary>
        /// Method for write debug message.
        /// </summary>
        /// <param name="text">Text message.</param>
        protected new void WriteDebug(string text)
        {
            this.RecordDebugMessages();
            base.WriteDebug(text);
        }

        /// <summary>
        /// Method for write verbose message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        /// <param name="args">Additional arguments.</param>
        protected void WriteVerboseWithTimestamp(string message, params object[] args)
        {
            this.WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        /// <summary>
        /// Method for write verbose message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        protected void WriteVerboseWithTimestamp(string message)
        {
            this.WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        /// <summary>
        /// Method for write warning message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        protected void WriteWarningWithTimestamp(string message)
        {
            this.WriteWarning(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        /// <summary>
        /// Method for write debug message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        /// <param name="args">Additional arguments.</param>
        protected void WriteDebugWithTimestamp(string message, params object[] args)
        {
            this.WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        /// <summary>
        /// Method to write debug message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        protected void WriteDebugWithTimestamp(string message)
        {
            this.WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        /// <summary>
        /// Method to write error message with timestamp.
        /// </summary>
        /// <param name="message">Text message.</param>
        protected void WriteErrorWithTimestamp(string message)
        {
            this.WriteError(
                new ErrorRecord(
                    new Exception(string.Format("{0:T} - {1}", DateTime.Now, message)),
                    string.Empty,
                    ErrorCategory.NotSpecified,
                    null));
        }

        /// <summary>
        /// Write an error message for a given exception.
        /// </summary>
        /// <param name="ex">The exception resulting from the error.</param>
        protected virtual void WriteExceptionError(Exception ex)
        {
            Debug.Assert(ex != null, "ex cannot be null or empty.");
            this.WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
        }

        /// <summary>
        /// Method to record debug messages.
        /// </summary>
        private void RecordDebugMessages()
        {
            // Create 'ErrorRecords' folder under profile directory, if not exists
            if (string.IsNullOrEmpty(errorRecordFolderPath)
                || !Directory.Exists(errorRecordFolderPath))
            {
                errorRecordFolderPath = Path.Combine(profileDirectory, "ErrorRecords");
                Directory.CreateDirectory(errorRecordFolderPath);
            }

            CommandInfo cmd = this.MyInvocation.MyCommand;

            string filePrefix = cmd.Name;
            string timeSampSuffix = DateTime.Now.ToString(FileTimeStampSuffixFormat);
            string fileName = filePrefix + "_" + timeSampSuffix + ".log";
            string filePath = Path.Combine(errorRecordFolderPath, fileName);

            StringBuilder sb = new StringBuilder();
            sb.Append("Module : ").AppendLine(cmd.ModuleName);
            sb.Append("Cmdlet : ").AppendLine(cmd.Name);

            sb.AppendLine("Parameters");
            foreach (var item in this.MyInvocation.BoundParameters)
            {
                sb.Append(" -").Append(item.Key).Append(" : ");
                sb.AppendLine(item.Value == null ? "null" : item.Value.ToString());
            }

            sb.AppendLine();

            foreach (var content in this.DebugMessages)
            {
                sb.AppendLine(content);
            }

            this.WriteFile(filePath, sb.ToString());
        }

        /// <summary>
        /// Method to log message to file.
        /// </summary>
        /// <param name="filePath">Log file path.</param>
        /// <param name="content">Log content.</param>
        private void WriteFile(string filePath, string content)
        {
        }
    }
}
