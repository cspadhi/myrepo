//---------------------------------------------------------------
//  <copyright file="GetWorkflowCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-Workflow cmdlet.
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
    /// Get workflow details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Workflow", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class GetWorkflowCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the identity of the workflow.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WorkflowId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            RcmWorkflowDetails rcmWorkflowDetails = RcmClientForInterService.GetWorkflowDetails(
                this.WorkflowId,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject(rcmWorkflowDetails);
        }
    }
}
