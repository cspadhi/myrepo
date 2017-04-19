//---------------------------------------------------------------
//  <copyright file="UpdateProtectionGroupCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for Update-ProtectionGroup cmdlet.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Management.Automation;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Update protection group.
    /// </summary>
    [Cmdlet(VerbsData.Update, "ProtectionGroup", DefaultParameterSetName = RcmParameterSets.Default)]
    public class UpdateProtectionGroupCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public ModifyProtectionGroupInput Input { get; set; }

        /// <summary>
        /// Gets or sets the ID of the protection group.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionGroupId { get; set; }

        /// <summary>
        /// Gets or sets the RPO threshold value in minutes.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public TimeSpan RpoThreshold { get; set; }

        /// <summary>
        /// Gets or sets the application consistency interval in minutes.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public TimeSpan AppConsistencyInterval { get; set; }

        /// <summary>
        /// Gets or sets the crash consistency interval in minutes.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public TimeSpan CrashConsistencyInterval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether multi VM consistency is enabled.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public bool IsMultiVmConsistencyEnabled { get; set; }

        /// <summary>
        /// Gets or sets the retention period in days.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public TimeSpan RetentionPeriod { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            ModifyProtectionGroupInput modifyProtectionGroupInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    modifyProtectionGroupInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    modifyProtectionGroupInput = this.Load();
                    break;

                default:
                    break;
            }

            if (modifyProtectionGroupInput != null)
            {
                this.WriteObject(modifyProtectionGroupInput);
                this.ModifyProtectionGroup(modifyProtectionGroupInput);
            }
        }

        /// <summary>
        /// Creates ModifyProtectionGroupInput object from input parameters.
        /// </summary>
        /// <returns>ModifyProtectionGroupInput object.</returns>
        private ModifyProtectionGroupInput Load()
        {
            ModifyProtectionGroupInput modifyProtectionGroupInput = new ModifyProtectionGroupInput()
            {
                ProtectionGroupId = this.ProtectionGroupId,
                AppConsistencyInterval = this.AppConsistencyInterval,
                CrashConsistencyInterval = this.CrashConsistencyInterval,
                IsMultiVmConsistencyEnabled = this.IsMultiVmConsistencyEnabled,
                RetentionPeriod = this.RetentionPeriod
            };

            return modifyProtectionGroupInput;
        }

        /// <summary>
        /// Makes ModifyProtectionGroup call.
        /// </summary>
        /// <param name="modifyProtectionGroupInput">ModifyProtectionGroupInput object.</param>
        private void ModifyProtectionGroup(ModifyProtectionGroupInput modifyProtectionGroupInput)
        {
            RcmClientForInterService.ModifyProtectionGroup(
                modifyProtectionGroupInput,
                PSRcmClient.GetServiceOperationContext());
            this.WriteObject("Protection group data updated in RCM.");
        }
    }
}
