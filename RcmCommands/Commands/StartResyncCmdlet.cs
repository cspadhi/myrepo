//---------------------------------------------------------------
//  <copyright file="StartResyncCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Start-Resync cmdlet.
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
    /// Start resync.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "Resync", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class StartResyncCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public StartResyncInput Input { get; set; }

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
        /// Gets or sets the resync option.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResyncOption { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            StartResyncInput startResyncInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    startResyncInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    startResyncInput = this.Load();
                    break;

                default:
                    break;
            }

            if (startResyncInput != null)
            {
                this.WriteObject(startResyncInput);
                RcmWorkflowDetails rcmWorkflowDetails = this.StartResync(startResyncInput);
                this.WriteObject(rcmWorkflowDetails);
            }
        }

        /// <summary>
        /// Creates StartResyncInput object from input parameters.
        /// </summary>
        /// <returns>StartResyncInput object.</returns>
        private StartResyncInput Load()
        {
            StartResyncInput startResyncInput = new StartResyncInput()
            {
                ProtectionPairId = this.ProtectionPairId,
                WorkflowId = this.WorkflowId,
                ResyncOption = this.ResyncOption
            };

            return startResyncInput;
        }

        /// <summary>
        /// Makes StartResync call.
        /// </summary>
        /// <param name="startResyncInput">StartResyncInput object.</param>
        /// <returns>RCM workflow details.</returns>
        private RcmWorkflowDetails StartResync(StartResyncInput startResyncInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails = RcmClientForInterService.StartResync(
                startResyncInput,
                PSRcmClient.GetServiceOperationContext());

            return rcmWorkflowDetails;
        }
    }
}
