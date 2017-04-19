//---------------------------------------------------------------
//  <copyright file="StartReplicationCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Start-Replication cmdlet.
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
    /// Start replication.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "Replication", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class StartReplicationCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public StartReplicationInput Input { get; set; }

        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workflow.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the ID used to link a start IR request with
        /// a complete IR message that comes later.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string CorrelationId { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            StartReplicationInput startReplicationInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    startReplicationInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    startReplicationInput = this.Load();
                    break;

                default:
                    break;
            }

            if (startReplicationInput != null)
            {
                this.WriteObject(startReplicationInput);
                RcmWorkflowDetails rcmWorkflowDetails =
                    this.StartReplication(startReplicationInput);
                this.WriteObject(rcmWorkflowDetails);
            }
        }

        /// <summary>
        /// Creates StartReplicationInput object from input parameters.
        /// </summary>
        /// <returns>StartReplicationInput object.</returns>
        private StartReplicationInput Load()
        {
            StartReplicationInput startReplicationInput = new StartReplicationInput()
            {
                ProtectionPairId = this.ProtectionPairId,
                WorkflowId = this.WorkflowId
            };

            return startReplicationInput;
        }

        /// <summary>
        /// Makes StartReplication call.
        /// </summary>
        /// <param name="startReplicationInput">StartReplicationInput object.</param>
        /// <returns>RCM workflow details.</returns>
        private RcmWorkflowDetails StartReplication(StartReplicationInput startReplicationInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails = RcmClientForInterService.StartReplication(
                startReplicationInput,
                PSRcmClient.GetServiceOperationContext());

            return rcmWorkflowDetails;
        }
    }
}
