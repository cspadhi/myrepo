//---------------------------------------------------------------
//  <copyright file="SetResyncRequiredCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Set-ResyncRequired cmdlet.
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
    /// Set resync required.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "ResyncRequired", DefaultParameterSetName = RcmParameterSets.Default)]
    public class SetResyncRequiredCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public GatewaySetResyncRequiredInput Input { get; set; }

        /// <summary>
        /// Gets or sets the source machine identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceMachineId { get; set; }

        /// <summary>
        /// Gets or sets the source disk identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceDiskId { get; set; }

        /// <summary>
        /// Gets or sets the target server.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string TargetServer { get; set; }

        /// <summary>
        /// Gets or sets the target disk identifier.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string TargetDiskId { get; set; }

        /// <summary>
        /// Gets or sets replication pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ReplicationPairId { get; set; }

        /// <summary>
        /// Gets or sets the drain session id.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DrainSessionId { get; set; }

        /// <summary>
        /// Gets or sets error code describing the resync required reason.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResyncRequiredErrorCode { get; set; }

        /// <summary>
        /// Gets or sets message describing the resync required reason and recommended action.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ResyncRequiredMessage { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            GatewaySetResyncRequiredInput gatewaySetResyncRequiredInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    gatewaySetResyncRequiredInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    gatewaySetResyncRequiredInput = this.Load();
                    break;

                default:
                    break;
            }

            if (gatewaySetResyncRequiredInput != null)
            {
                this.WriteObject(gatewaySetResyncRequiredInput);
                this.SetResyncRequired(gatewaySetResyncRequiredInput);
            }
        }

        /// <summary>
        /// Creates GatewaySetResyncRequiredInput object from input parameters.
        /// </summary>
        /// <returns>GatewaySetResyncRequiredInput object.</returns>
        private GatewaySetResyncRequiredInput Load()
        {
            GatewaySetResyncRequiredInput gatewaySetResyncRequiredInput = new GatewaySetResyncRequiredInput()
            {
                SourceMachineId = this.SourceMachineId,
                SourceDiskId = this.SourceDiskId,
                TargetServer = this.TargetServer,
                TargetDiskId = this.TargetDiskId,
                ReplicationPairId = this.ReplicationPairId,
                DrainSessionId = this.DrainSessionId,
                ResyncRequiredErrorCode = this.ResyncRequiredErrorCode,
                ResyncRequiredMessage = this.ResyncRequiredMessage
            };

            return gatewaySetResyncRequiredInput;
        }

        /// <summary>
        /// Makes SetResyncRequired call.
        /// </summary>
        /// <param name="gatewaySetResyncRequiredInput">GatewaySetResyncRequiredInput object.</param>
        private void SetResyncRequired(GatewaySetResyncRequiredInput gatewaySetResyncRequiredInput)
        {
            RcmClientForInterService.SetResyncRequired(
                gatewaySetResyncRequiredInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Resync required set in RCM.");
        }
    }
}
