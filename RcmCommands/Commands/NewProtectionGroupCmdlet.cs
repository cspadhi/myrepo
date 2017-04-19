//---------------------------------------------------------------
//  <copyright file="NewProtectionGroupCmdlet.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for New-ProtectionGroup cmdlet.
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
    /// Create protection group.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "ProtectionGroup", DefaultParameterSetName = RcmParameterSets.Default)]
    [OutputType(typeof(ProtectionGroupDetails))]
    public class NewProtectionGroupCmdlet : RcmCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.ByObject, Mandatory = true)]
        [ValidateNotNull]
        public CreateProtectionGroupInput Input { get; set; }

        /// <summary>
        /// Gets or sets the ID of the protection group.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

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

        /// <summary>
        /// Gets or sets the protection service URI.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionServiceUri { get; set; }

        /// <summary>
        /// Gets or sets protection service location.
        /// </summary>
        [Parameter(ParameterSetName = RcmParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProtectionServiceLocation { get; set; }

        #endregion

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteRcmCmdlet()
        {
            base.ExecuteRcmCmdlet();

            CreateProtectionGroupInput createProtectionGroupInput = null;
            switch (this.ParameterSetName)
            {
                case RcmParameterSets.ByObject:
                    createProtectionGroupInput = this.Input;
                    break;

                case RcmParameterSets.Default:
                    createProtectionGroupInput = this.Load();
                    break;

                default:
                    break;
            }

            if (createProtectionGroupInput != null)
            {
                this.WriteObject(createProtectionGroupInput);
                ProtectionGroupDetails protectionGroupDetails =
                    this.CreateProtectionGroup(createProtectionGroupInput);
                this.WriteObject(protectionGroupDetails);
            }
        }

        /// <summary>
        /// Creates CreateProtectionGroupInput object from input parameters.
        /// </summary>
        /// <returns>CreateProtectionGroupInput object.</returns>
        private CreateProtectionGroupInput Load()
        {
            CreateProtectionGroupInput createProtectionGroupInput =
                new CreateProtectionGroupInput()
                {
                    Id = this.Id,
                    AppConsistencyInterval = this.AppConsistencyInterval,
                    CrashConsistencyInterval = this.CrashConsistencyInterval,
                    IsMultiVmConsistencyEnabled = this.IsMultiVmConsistencyEnabled,
                    RetentionPeriod = this.RetentionPeriod,
                    ProtectionServiceUri = this.ProtectionServiceUri,
                    ProtectionServiceLocation = this.ProtectionServiceLocation
                };

            return createProtectionGroupInput;
        }

        /// <summary>
        /// Makes CreateProtectionGroup call.
        /// </summary>
        /// <param name="createProtectionGroupInput">CreateProtectionGroupInput object.</param>
        /// <returns>Protection group details.</returns>
        private ProtectionGroupDetails CreateProtectionGroup(
            CreateProtectionGroupInput createProtectionGroupInput)
        {
            ProtectionGroupDetails protectionGroupDetails =
                RcmClientForInterService.CreateProtectionGroup(
                    createProtectionGroupInput,
                    PSRcmClient.GetServiceOperationContext());

            return protectionGroupDetails;
        }
    }
}
