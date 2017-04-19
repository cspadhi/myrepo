//---------------------------------------------------------------
//  <copyright file="NewProtectionCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-Protection cmdlet.
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
    /// Enable protection.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Protection", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RcmWorkflowDetails))]
    public class NewProtectionCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public EnableProtectionToAzureInput Input { get; set; }

        /// <summary>
        /// Gets or sets the ID of the workflow.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string WorkflowId { get; set; }

        /// <summary>
        /// Gets or sets the protection pair identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// Gets or sets the protection group identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionGroupId { get; set; }

        /// <summary>
        /// Gets or sets the source server machine ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceMachineId { get; set; }

        /// <summary>
        /// Gets or sets the gateway properties.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public EnableProtectionToAzureInput.GatewayInput GatewayProperties { get; set; }

        /// <summary>
        /// Gets or sets the protection service URI.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionServiceUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether compression is enabled.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public bool IsCompressionEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether encryption is enabled.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public bool IsEncryptionOverWireEnabled { get; set; }

        /// <summary>
        /// Gets or sets the list of replication pairs.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<EnableProtectionToAzureInput.ReplicationPairInput> ReplicationPairs { get; set; }

        /// <summary>
        /// Gets or sets the details required to access the critical queue
        /// for posting the messages in the critical service bus queue.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public EnableProtectionToAzureInput.ServiceBusConfigurationInput CriticalQueueConfig { get; set; }

        /// <summary>
        /// Gets or sets the details required to access the informational queue
        /// for posting the messages in the informational service bus queue.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public EnableProtectionToAzureInput.ServiceBusConfigurationInput InformationalQueueConfig { get; set; }

        /// <summary>
        /// Gets or sets the context that is to be supplied back with each message
        /// associated with the machine being protected.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string MessageContext { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            EnableProtectionToAzureInput enableProtectionToAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    enableProtectionToAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    enableProtectionToAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (enableProtectionToAzureInput != null)
            {
                this.WriteObject(enableProtectionToAzureInput);
                RcmWorkflowDetails rcmWorkflowDetails =
                    this.EnableProtectionToAzure(enableProtectionToAzureInput);
                this.WriteObject(rcmWorkflowDetails);
            }
        }

        /// <summary>
        /// Creates EnableProtectionToAzureInput object from input parameters.
        /// </summary>
        /// <returns>EnableProtectionToAzureInput object.</returns>
        private EnableProtectionToAzureInput Load()
        {
            EnableProtectionToAzureInput enableProtectionToAzureInput =
                new EnableProtectionToAzureInput()
                {
                    WorkflowId = this.WorkflowId,
                    ProtectionPairId = this.ProtectionPairId,
                    ProtectionGroupId = this.ProtectionGroupId,
                    SourceMachineId = this.SourceMachineId,
                    GatewayProperties = this.GatewayProperties,
                    ProtectionServiceUri = this.ProtectionServiceUri,
                    IsCompressionEnabled = this.IsCompressionEnabled,
                    IsEncryptionOverWireEnabled = this.IsEncryptionOverWireEnabled,
                    ReplicationPairs = this.ReplicationPairs,
                    CriticalQueueConfig = this.CriticalQueueConfig,
                    InformationalQueueConfig = this.InformationalQueueConfig,
                    MessageContext = this.MessageContext
                };

            return enableProtectionToAzureInput;
        }

        /// <summary>
        /// Makes EnableProtectionToAzure call.
        /// </summary>
        /// <param name="enableProtectionToAzureInput">EnableProtectionToAzureInput object.</param>
        /// <returns>RCM workflow details.</returns>
        private RcmWorkflowDetails EnableProtectionToAzure(
            EnableProtectionToAzureInput enableProtectionToAzureInput)
        {
            RcmWorkflowDetails rcmWorkflowDetails =
                RcmClientForInterService.EnableProtectionToAzure(
                    enableProtectionToAzureInput,
                    PSRcmClient.GetServiceOperationContext());

            return rcmWorkflowDetails;
        }
    }
}
