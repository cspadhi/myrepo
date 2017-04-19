//---------------------------------------------------------------
//  <copyright file="GetProtectionGroupRecoveryPointsCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-ProtectionGroupRecoveryPoints cmdlet.
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
    /// Get protection group recovery points.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ProtectionGroupRecoveryPoints", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(IEnumerable<RecoveryPointDetailsForAzure>))]
    public class GetProtectionGroupRecoveryPointsCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public GetProtectionGroupRecoveryPointsForAzureInput Input { get; set; }

        /// <summary>
        /// Gets or sets the protection group ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionGroupId { get; set; }

        /// <summary>
        /// Gets or sets maximum number of latest recovery point return in output response.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public int MaxRecoveryPoints { get; set; }

        /// <summary>
        /// Gets or sets recovery point filter.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public long RecoveryPointFilter { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            GetProtectionGroupRecoveryPointsForAzureInput getProtectionGroupRecoveryPointsForAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    getProtectionGroupRecoveryPointsForAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    getProtectionGroupRecoveryPointsForAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (getProtectionGroupRecoveryPointsForAzureInput != null)
            {
                this.WriteObject(getProtectionGroupRecoveryPointsForAzureInput);
                List<RecoveryPointDetailsForAzure> recoveryPoints =
                    this.GetProtectionGroupRecoveryPointsForAzure(getProtectionGroupRecoveryPointsForAzureInput);
                this.WriteObject(recoveryPoints, true);
            }
        }

        /// <summary>
        /// Creates GetProtectionGroupRecoveryPointsForAzureInput object from input parameters.
        /// </summary>
        /// <returns>GetProtectionGroupRecoveryPointsForAzureInput object.</returns>
        private GetProtectionGroupRecoveryPointsForAzureInput Load()
        {
            var getProtectionGroupRecoveryPointsForAzureInput =
                new GetProtectionGroupRecoveryPointsForAzureInput()
                {
                    ProtectionGroupId = this.ProtectionGroupId,
                    MaxRecoveryPoints = this.MaxRecoveryPoints,
                    RecoveryPointFilter = this.RecoveryPointFilter
                };

            return getProtectionGroupRecoveryPointsForAzureInput;
        }

        /// <summary>
        /// Makes GetProtectionGroupRecoveryPointsForAzure call.
        /// </summary>
        /// <param name="getProtectionGroupRecoveryPointsForAzureInput">GetProtectionGroupRecoveryPointsForAzureInput object.</param>
        /// <returns>List of recovery points.</returns>
        private List<RecoveryPointDetailsForAzure> GetProtectionGroupRecoveryPointsForAzure(GetProtectionGroupRecoveryPointsForAzureInput getProtectionGroupRecoveryPointsForAzureInput)
        {
            List<RecoveryPointDetailsForAzure> recoveryPoints = RcmClientForInterService.GetProtectionGroupRecoveryPointsForAzure(
                getProtectionGroupRecoveryPointsForAzureInput,
                PSRcmClient.GetServiceOperationContext());

            return recoveryPoints;
        }
    }
}