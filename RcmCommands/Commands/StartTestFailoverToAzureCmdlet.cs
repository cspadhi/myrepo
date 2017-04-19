//---------------------------------------------------------------
//  <copyright file="StartTestFailoverToAzureCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Start-TestFailoverToAzure cmdlet.
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
    /// Start test failover.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "TestFailoverToAzure", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class StartTestFailoverToAzureCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public StartTestFailoverToAzureInput Input { get; set; }

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

            StartTestFailoverToAzureInput startTestFailoverToAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    startTestFailoverToAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    startTestFailoverToAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (startTestFailoverToAzureInput != null)
            {
                this.WriteObject(startTestFailoverToAzureInput);
                RcmWorkflowDetails rcmWorkflowDetails =
                    this.StartTestFailoverToAzure(startTestFailoverToAzureInput);
                this.WriteObject(rcmWorkflowDetails);
            }
        }

        /// <summary>
        /// Creates StartTestFailoverToAzureInput object from input parameters.
        /// </summary>
        /// <returns>StartTestFailoverToAzureInput object.</returns>
        private StartTestFailoverToAzureInput Load()
        {
            StartTestFailoverToAzureInput startTestFailoverToAzureInput =
                new StartTestFailoverToAzureInput()
                {
                    ProtectionPairId = this.ProtectionPairId,
                    RecoveryPointId = this.RecoveryPointId,
                    HydrationInfo = this.HydrationInfo,
                    WorkflowId = this.WorkflowId
                };

            return startTestFailoverToAzureInput;
        }

        /// <summary>
        /// Makes StartTestFailoverToAzure call.
        /// </summary>
        /// <param name="startTestFailoverToAzureInput">StartTestFailoverToAzureInput object.</param>
        /// <returns>RCM workflow details.</returns>
        private RcmWorkflowDetails StartTestFailoverToAzure(
            StartTestFailoverToAzureInput startTestFailoverToAzureInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails =
                RcmClientForInterService.StartTestFailoverToAzure(
                    startTestFailoverToAzureInput,
                    PSRcmClient.GetServiceOperationContext());

            return rcmWorkflowDetails;
        }
    }
}
