//---------------------------------------------------------------
//  <copyright file="GetProtectionPairRecoveryPointsCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Get-ProtectionPairRecoveryPoints cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Get protection pair recovery points.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "ProtectionPairRecoveryPoints", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(IEnumerable<RecoveryPointDetailsForAzure>))]
    public class GetProtectionPairRecoveryPointsCmdlet : RcmCmdletBase
    {
        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public GetProtectionPairRecoveryPointsForAzureInput Input { get; set; }

        /// <summary>
        /// Gets or sets the protection pair ID.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionPairId { get; set; }

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

            GetProtectionPairRecoveryPointsForAzureInput getProtectionPairRecoveryPointsForAzureInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    getProtectionPairRecoveryPointsForAzureInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    getProtectionPairRecoveryPointsForAzureInput = this.Load();
                    break;

                default:
                    break;
            }

            if (getProtectionPairRecoveryPointsForAzureInput != null)
            {
                this.WriteObject(getProtectionPairRecoveryPointsForAzureInput);
                List<RecoveryPointDetailsForAzure> recoveryPoints =
                    this.GetProtectionPairRecoveryPointsForAzure(getProtectionPairRecoveryPointsForAzureInput);
                this.WriteObject(recoveryPoints.Select(x => x), true);
            }
        }

        /// <summary>
        /// Creates GetProtectionPairRecoveryPointsForAzureInput object from input parameters.
        /// </summary>
        /// <returns>GetProtectionPairRecoveryPointsForAzureInput object.</returns>
        private GetProtectionPairRecoveryPointsForAzureInput Load()
        {
            var getProtectionPairRecoveryPointsForAzureInput =
                new GetProtectionPairRecoveryPointsForAzureInput()
                {
                    ProtectionPairId = this.ProtectionPairId,
                    MaxRecoveryPoints = this.MaxRecoveryPoints,
                    RecoveryPointFilter = this.RecoveryPointFilter
                };

            return getProtectionPairRecoveryPointsForAzureInput;
        }

        /// <summary>
        /// Makes GetProtectionPairRecoveryPointsForAzure call.
        /// </summary>
        /// <param name="getProtectionPairRecoveryPointsForAzureInput">GetProtectionPairRecoveryPointsForAzureInput object.</param>
        /// <returns>List of recovery points.</returns>
        private List<RecoveryPointDetailsForAzure> GetProtectionPairRecoveryPointsForAzure(GetProtectionPairRecoveryPointsForAzureInput getProtectionPairRecoveryPointsForAzureInput)
        {
            List<RecoveryPointDetailsForAzure> recoveryPoints = RcmClientForInterService.GetProtectionPairRecoveryPointsForAzure(
                getProtectionPairRecoveryPointsForAzureInput,
                PSRcmClient.GetServiceOperationContext());

            return recoveryPoints;
        }
    }
}