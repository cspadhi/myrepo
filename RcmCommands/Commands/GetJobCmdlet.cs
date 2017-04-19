//---------------------------------------------------------------
//  <copyright file="GetJobCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-Job cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Get job details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Job", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmJob))]
    public class GetJobCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the identity of the job.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the identity of the consumer.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ConsumerId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmJob rcmJob = RcmClientForInterService.GetJobDetails(
                this.JobId,
                this.ConsumerId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject(rcmJob);
        }
    }
}
