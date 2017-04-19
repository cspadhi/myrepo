﻿//---------------------------------------------------------------
//  <copyright file="UpdateJobStatusCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Update-JobStatus cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Update job status.
    /// </summary>
    [Cmdlet(VerbsData.Update, "JobStatus", DefaultParameterSetName = RcmParameterSets.Default)]
    public class UpdateJobStatusCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public RcmJob Input { get; set; }

        /// <summary>
        /// Gets or sets job identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets identifier for consumer of the job.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ConsumerId { get; set; }

        /// <summary>
        /// Gets or sets identifier for component consuming the job.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets job type.
        /// This is a ToString() representation of the
        /// <see cref="RcmJobEnum.JobType"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets job status.
        /// This is a ToString() representation of the
        /// <see cref="RcmJobStatus"/> enumeration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string JobStatus { get; set; }

        /// <summary>
        /// Gets or sets RCM service URI.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string RcmServiceUri { get; set; }

        /// <summary>
        /// Gets or sets job input payload.
        /// The payload will be serialized string representation of job input
        /// contract depending on the job type.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string InputPayload { get; set; }

        /// <summary>
        /// Gets or sets job output payload.
        /// The payload will be serialized string representation of job output
        /// contract depending on the job type.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OutputPayload { get; set; }

        /// <summary>
        /// Gets or sets job errors.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default)]
        [ValidateNotNullOrEmpty]
        public List<RcmJobError> Errors { get; set; }

        /// <summary>
        /// Gets or sets the job context.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public RcmJob.JobContext Context { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmJob rcmJob = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    rcmJob = this.Input;
                    break;

                case RcmParameterSets.Default:
                    rcmJob = this.Load();
                    break;

                default:
                    break;
            }

            if (rcmJob != null)
            {
                this.WriteObject(rcmJob);
                this.UpdateJobStatus(rcmJob);
            }
        }

        /// <summary>
        /// Creates RCM job object from input parameters.
        /// </summary>
        /// <returns>RCM job object.</returns>
        private RcmJob Load()
        {
            RcmJob rcmJob = new RcmJob()
            {
                Id = this.Id,
                ConsumerId = this.ConsumerId,
                ComponentId = this.ComponentId,
                JobType = this.JobType,
                SessionId = this.SessionId,
                JobStatus = this.JobStatus,
                RcmServiceUri = this.RcmServiceUri,
                InputPayload = this.InputPayload,
                OutputPayload = this.OutputPayload
            };

            if (this.Errors != null)
            {
                rcmJob.Errors = this.Errors;
            }

            return rcmJob;
        }

        /// <summary>
        /// Makes UpdateJobStatus call.
        /// </summary>
        /// <param name="rcmJob">RCM job object.</param>
        private void UpdateJobStatus(RcmJob rcmJob)
        {
            RcmClientForInterService.UpdateJobStatus(
                rcmJob,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Job status updated in RCM.");
        }
    }
}
