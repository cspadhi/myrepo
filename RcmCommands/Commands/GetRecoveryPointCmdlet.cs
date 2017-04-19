//---------------------------------------------------------------
//  <copyright file="GetRecoveryPointCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-RecoveryPoint cmdlet.
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
    /// Get recovery point details.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "RecoveryPoint", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(RecoveryPointDetailsForAzure))]
    public class GetRecoveryPointCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public GetRecoveryPointDetailsForAzureInput Input { get; set; }

        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

        /// <summary>
        /// Gets or sets the recovery point ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string RecoveryPointId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            GetRecoveryPointDetailsForAzureInput getRecoveryPointDetailsForAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    getRecoveryPointDetailsForAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    getRecoveryPointDetailsForAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (getRecoveryPointDetailsForAzureInput != null)
            {
                this.WriteObject(getRecoveryPointDetailsForAzureInput);
                RecoveryPointDetailsForAzure recoveryPointDetailsForAzure =
                    this.GetRecoveryPointDetailsForAzure(getRecoveryPointDetailsForAzureInput);
                this.WriteObject(recoveryPointDetailsForAzure);
            }
        }

        /// <summary>
        /// Creates GetRecoveryPointDetailsForAzureInput object from input parameters.
        /// </summary>
        /// <returns>GetRecoveryPointDetailsForAzureInput object.</returns>
        private GetRecoveryPointDetailsForAzureInput Load()
        {
            var getRecoveryPointDetailsForAzureInput = new GetRecoveryPointDetailsForAzureInput()
            {
                ProtectionPairId = this.ProtectionPairId,
                RecoveryPointId = this.RecoveryPointId
            };

            return getRecoveryPointDetailsForAzureInput;
        }

        /// <summary>
        /// Makes GetRecoveryPointDetailsForAzure call.
        /// </summary>
        /// <param name="getRecoveryPointDetailsForAzureInput">GetRecoveryPointDetailsForAzureInput object.</param>
        /// <returns>Recovery point details.</returns>
        private RecoveryPointDetailsForAzure GetRecoveryPointDetailsForAzure(
            GetRecoveryPointDetailsForAzureInput getRecoveryPointDetailsForAzureInput)
        {
            RecoveryPointDetailsForAzure recoveryPointDetailsForAzure =
                RcmClientForInterService.GetRecoveryPointDetailsForAzure(
                    getRecoveryPointDetailsForAzureInput,
                    PSRcmClient.GetServiceOperationContext());

            return recoveryPointDetailsForAzure;
        }
    }
}