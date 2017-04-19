//---------------------------------------------------------------
//  <copyright file="StartFailoverToAzureCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Start-FailoverToAzure cmdlet.
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
    /// Start failover.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "FailoverToAzure", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class StartFailoverToAzureCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public StartFailoverToAzureInput Input { get; set; }

        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// Gets or sets the recovery point ID of the failover recovery point.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string RecoveryPointId { get; set; }

        /// <summary>
        /// Gets or sets the detail information required for hydration.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public HydrationToAzureInput HydrationInfo { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workflow.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WorkflowId { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            StartFailoverToAzureInput startFailoverToAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    startFailoverToAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    startFailoverToAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (startFailoverToAzureInput != null)
            {
                this.WriteObject(startFailoverToAzureInput);
                RcmWorkflowDetails rcmWorkflowDetails =
                    this.StartFailoverToAzure(startFailoverToAzureInput);
                this.WriteObject(rcmWorkflowDetails);
            }
        }

        /// <summary>
        /// Creates StartFailoverToAzureInput object from input parameters.
        /// </summary>
        /// <returns>StartFailoverToAzureInput object.</returns>
        private StartFailoverToAzureInput Load()
        {
            StartFailoverToAzureInput startFailoverToAzureInput = new StartFailoverToAzureInput()
            {
                ProtectionPairId = this.ProtectionPairId,
                RecoveryPointId = this.RecoveryPointId,
                HydrationInfo = this.HydrationInfo,
                WorkflowId = this.WorkflowId
            };

            return startFailoverToAzureInput;
        }

        /// <summary>
        /// Makes StartFailoverToAzure call.
        /// </summary>
        /// <param name="startFailoverToAzureInput">StartFailoverToAzureInput object.</param>
        /// <returns>RCM workflow details.</returns>
        private RcmWorkflowDetails StartFailoverToAzure(
            StartFailoverToAzureInput startFailoverToAzureInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails = RcmClientForInterService.StartFailoverToAzure(
                startFailoverToAzureInput,
                PSRcmClient.GetServiceOperationContext());

            return rcmWorkflowDetails;
        }
    }
}
